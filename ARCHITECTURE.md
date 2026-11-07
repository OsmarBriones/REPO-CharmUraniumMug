# CharmUraniumMug Architecture

A Charm mod (layer 3 of the PocketCharms family): one valuable, one power. It depends on **Pockets** (storage) and **CharmCore** (registry, wear, cooldown display, host settings, debug mode).

## The power: Warm Glow

you slowly regenerate health while crouching. A local effect: no network message of its own.

- `UraniumMugCharm : Charm` registers the prefab name `Valuable Manor Uranium Mug`, the defaults (wear 1% per second of actual healing, no cooldown) and the host-synced settings `HealIntervalSeconds` and `HealAmount`. It sets `WearsPerSecond = true`.
- `WarmGlow` (a `MonoBehaviour` on the plugin object) runs every frame on the owner's machine. While `Charm.IsActive`, the player crouches, is not tumbling, is alive/enabled and `health < maxHealth`, it feeds `Time.deltaTime` to `Domain/HealTimer` and calls `Charm.WearForSeconds(deltaTime)`. Each time the timer reports a due heal it calls `PlayerHealth.Heal(HealAmount, false)` (effect false avoids screen glitch spam). Otherwise the timer resets.
- `Domain/HealTimer.cs` is the game-free accumulator (`Advance(delta, interval)` returns how many heals are due, capped at 5 per call so a long frame cannot burst), unit tested in `HealTimerTests`.
- `PlayerHealth.Heal` is owner-only, returns early at full HP and syncs through `UpdateHealthRPC`, so no extra networking is needed.

### Settings
| Key | Type | Range | Default | Meaning |
|---|---|---|---|---|
| `HealIntervalSeconds` | float (seconds) | 0.5 to 30 | 3 | Crouching time between heals. 0 is excluded (it would heal every frame). |
| `HealAmount` | float setting, used as whole points (rounded, at least 1) | 1 to 20 | 1 | Health points per heal. Never negative, so the charm cannot hurt. |
| `WearPercent` (CharmCore) | float | 0 to 100 | 1 | Percent of the original value lost per SECOND of actual healing. |

## Game members used

Checked by `CharmUraniumMug.Tests/GameContractTests.cs`.

- `PlayerController.instance`, `PlayerController.Crouching` (read)
- `PlayerAvatar.instance`, `playerHealth`, `isTumbling`, `deadSet`, `isDisabled` (read)
- `PlayerHealth.health`, `maxHealth` (read), `PlayerHealth.Heal(int, bool)` (called)
- `ValuableObject.dollarValueCurrent` (worn by CharmCore)

The mod needs no Harmony patch of its own.

## Debug mode

Provided by CharmCore: the `Debug.DebugMode` setting (off by default) places the valuable on the floor at the start of each level and enables `Charm.LogDebug` output.

## Known limits

- Looked up by exact prefab name; a game update that renames the prefab needs a mod update.
- Not verified in game: the build and unit tests prove the logic and that the game members exist, not the behaviour in a running game.
- `Crouching` only becomes true while grounded (or when forced by crawling/tumbling), so crouching in mid-air does not heal.

## Decisions

- **Tumbling excluded:** `PlayerController.Crouching` is also set while `PlayerAvatar.isTumbling` (PlayerController.cs ~:780) and while crawling in a tight space. Tumbling is excluded, because a tumbling player is not choosing to crouch. Crawling under low ceilings counts as crouching (the player is crouched there).
- **Wear per real healing second:** wear is charged only on frames where the player is crouching, alive and below max health, i.e. while the timer runs towards a heal. Between a heal and the next one the wear keeps ticking, since the power is working. At full health or standing there is no wear.
- **Timer resets** when crouching stops, health is full or the charm becomes inactive, so toggling crouch quickly cannot bank progress.
- **Heal without effect:** `Heal(amount, false)` avoids the camera glitch each few seconds.
- **No cooldown:** the interval is the pacing.
- **Frame-burst cap:** at most 5 heals are due in one frame.

## Runtime hygiene

- **References and level state:** fixed. `HealTimer` (inside `WarmGlow`) accumulates seconds towards the next heal; `UraniumMugCharm` now overrides `OnLevelEnded` and resets it through `WarmGlow.ResetState()`. It was already reset on every inactive or non-crouching frame. The charm holds the component only through an instance property; the component lives on the plugin's own persistent object, and no static field holds a game object.
- **Per-frame work:** `WarmGlow.Update` returns at once when the charm is inactive and allocates nothing in either state (the game's singletons are read directly, no Find calls, no collections).
- **Debug logs:** fixed. The `LogDebug` after each heal is now guarded with `if (Charm.DebugMode)`.
- **Changes to the game:** nothing needed. The mod only calls `PlayerHealth.Heal`; it changes no setting that would need restoring.
- **Static game objects and Find calls:** none.
- Known limit outside this mod: while the charm is active, CharmCore's `IsActive` and `CharmSetting.Value` build their host-setting key strings on each call; that is CharmCore's code and was left alone.
