# Changelog

## [Unreleased]

### Added
- Warm Glow: you slowly regenerate health while crouching (`HealIntervalSeconds` 3 s, `HealAmount` 1 by default). Wear is 1% per second of actual healing.
- `Debug.DebugMode` (off by default): puts the Uranium Mug on the floor at the start of each level and writes detailed logs.

### Changed
- Runtime hygiene pass: heal timer reset at level end, debug log in the per-frame effect guarded.
