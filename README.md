# Charm Uranium Mug

Warm Glow: you slowly regenerate health while crouching.

> **Everyone in the lobby must install this mod, together with Pockets and CharmCore.** The power changes what your own character can do and its state shows on your inventory slot, so friends without the mods would see an empty slot and be unable to use the pocket.

## How to Use

1. Find the **Uranium Mug** valuable and store it in a free inventory slot with [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/) (grab it, press the slot key).
2. Crouch while you are hurt: a little health comes back every few seconds for as long as you stay crouched. Standing up, tumbling or being at full health stops it.
3. Take the valuable out of the pocket and the power stops.

## Features

- **Warm Glow:** the mug warms you from the pocket: crouch and you slowly regenerate health.
- **It costs the valuable:** the mug loses a little value for every second it is really healing you (never while your health is full), and a worn-out valuable breaks.
- **Everything is configurable:** wear, how often you heal and how much. The host's choices apply to everyone.

## Requirements
- [BepInEx Pack for R.E.P.O.](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
- [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/)
- [CharmCore](https://thunderstore.io/c/repo/p/OsmarBriones/CharmCore/)

## Installation
1. Install through r2modman / Thunderstore Mod Manager (Pockets and CharmCore come with it), or place `CharmUraniumMug.dll` next to `Pockets.dll` and `CharmCore.dll` in `BepInEx/plugins`. **All players need it.**
2. Launch the game once; the configuration file is created inside `BepInEx/config`.

## Configuration
Settings are in `com.osmar.CharmUraniumMug.cfg` inside `BepInEx/config`, section `Charm.UraniumMug`. The host's choices apply to everyone.

- `Enabled` (default `true`): turn this charm on or off.
- `WearPercent` (default `1`): share of the valuable's original value lost for each second the mug is healing you.
- `HealIntervalSeconds` (default `3`, 0.5 to 30): seconds of crouching between two heals.
- `HealAmount` (default `1`, 1 to 20): health points restored on each heal.

Section `Debug`: `DebugMode` (default `false`) places a Uranium Mug on the floor in front of you when a level starts (host or singleplayer) and writes extra log lines. For testing.

## Issues & Bug Reports
The official way to report issues, suggest improvements, or submit feedback is by opening an issue on the official GitHub repository:
👉 [GitHub Issues](https://github.com/OsmarBriones/REPO-CharmUraniumMug/issues)

## Credits
Developed by **Osmar Briones**
