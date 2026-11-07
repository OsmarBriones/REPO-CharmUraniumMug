## User Scenarios

### User Story 1 - Warm Glow (Priority: P1)
A player stores the Uranium Mug in a pocket and gets this power while it stays there: you slowly regenerate health while crouching.
**Acceptance**: with the valuable pocketed the power works; with it out of the pocket it does not; each use wears the valuable and shows the cooldown on the slot.

## Requirements
- FR-001: Registers a charm with CharmCore for the prefab Valuable Manor Uranium Mug.
- FR-002: Settings through CharmCore: Enabled, WearPercent, CooldownSeconds, DebugMode; host-authoritative except DebugMode.
- FR-003: All players install it (Category 2); depends on Pockets and CharmCore.