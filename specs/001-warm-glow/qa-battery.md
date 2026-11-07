# QA battery: Warm Glow (CharmUraniumMug)

Deploy: Pockets, CharmCore and CharmUraniumMug are in the r2modman `Debug` profile (compare hashes with the builds). Restart the game first.

## Quick start with debug mode
Set `DebugMode = true` in the `Debug` section of `BepInEx/config/com.osmar.CharmUraniumMug.cfg` (off by default). When a level starts, a Uranium Mug is placed on the floor about 2.5 m in front of you; the log then also shows the detailed lines. Any level works. To get another one at any moment, set `SpawnNow = true` in the same section while a level is running: the valuable is placed near you and the switch turns itself back off (the file is re-read about once a second).

## Single player
- [ ] Log shows `Charm registered: Uranium Mug - Warm Glow` and `CharmUraniumMug 1.0.0 loaded!`.
- [ ] Store the valuable in a slot: the slot shows its picture and value.
- [ ] Take some damage so health is below max, store the mug, then crouch and stay crouched: health goes up by 1 about every 3 seconds, with no screen glitch.
- [ ] Stand up: healing stops; crouch again: it restarts (the timer starts over).
- [ ] Hold crouch at full health for 30 s: no healing and the slot value does not drop.
- [ ] While healing, the slot value drops about 1% of the original value per second (in whole-dollar steps).
- [ ] Tumble (jump/tumble key) while hurt: no healing while tumbling.
- [ ] Crawl under a low obstacle while hurt: healing continues (counts as crouching).
- [ ] Die or get downed: no healing, no errors in the log.
- [ ] With `DebugMode` on, the log shows `Warm Glow: health X -> Y` lines.
- [ ] Take the valuable out: the power stops.
- [ ] Use it until the valuable breaks: the slot empties, the power stops, no errors in the log.

## Settings
- [ ] `HealIntervalSeconds = 0.5` and `HealAmount = 20`: fast, big heals; `HealIntervalSeconds = 30`: one heal per 30 s. `WearPercent = 0`: no wear.
- [ ] Charm `Enabled = false`: no effect and no wear. CharmCore `Enabled = false`: nothing works.

## Multiplayer (host and client)
- [ ] The effect works on the client, other players see the client's health bar rise, the value drops on both machines.
- [ ] Host settings override the client's.
- [ ] Client leaves and rejoins: no stuck veil.

Log patterns: `Charm registered`, `threw`, `Exception`.
