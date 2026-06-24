# Codex Implementation Continuation Prompt

Use this prompt to start a Codex session focused on continuing implementation work.

```text
You are working in /home/gernsback/source/cyber-arpg, a Godot 4 .NET/C# cyberpunk ARPG combat sandbox.

This is an implementation session, not a tutoring session. The learning/review flow is handled separately in docs/CODEX_TUTOR_PROMPT.md.

Start by orienting yourself:
1. Run `git status --short --branch`.
2. Read README.md, docs/ROADMAP.md, and docs/ARCHITECTURE.md.
3. Read the current combat path:
   - scripts/gameplay/entities/PlayerController.cs
   - scripts/gameplay/abilities/KineticBurstProjectile.cs
   - scripts/gameplay/damage/DamageRequest.cs
   - scripts/gameplay/damage/DamageResolver.cs
   - scripts/gameplay/damage/DamageResult.cs
   - scripts/gameplay/damage/IDamageable.cs
   - scripts/gameplay/entities/EnemyDummy.cs
   - scripts/gameplay/combat/CombatDebugLog.cs
   - scripts/ui/hud/PlayerHud.cs
4. Verify the baseline before editing:
   - `dotnet build`
   - `godot --headless --path . --scene res://scenes/arena/Arena.tscn --quit-after 2`

Current project state:
- Main playable scene: scenes/arena/Arena.tscn.
- Current milestone: Milestone 2, First Combat Event Pipeline.
- Implemented loop:
  - WASD movement
  - mouse aiming
  - camera follow
  - Health and Energy HUD
  - left-click Kinetic Burst
  - projectile collision with enemy dummy
  - damage resolved through DamageRequest/DamageResolver
  - EnemyDummy health loss
  - HUD combat log
  - floating damage numbers
  - enemy dummy hit flash

Important boundaries:
- Keep changes small, playable, and easy to inspect.
- Do not introduce broad architecture unless the current task genuinely needs it.
- Do not add a generalized VFX framework, data-driven ability framework, loot, progression, status effects, or modifier system yet.
- Do not start wave spawning yet.
- Do not rewrite the current combat pipeline just to make it more abstract.
- Preserve user changes. Do not revert unrelated work.

Recommended next implementation goal:
Close out the static dummy combat loop with a tiny defeated-state presentation.

Implement the smallest useful version:
- When EnemyDummy reaches 0 health, make the defeated state visible in-game.
- Keep the behavior local to EnemyDummy for now.
- Consider disabling its collision or otherwise preventing repeated projectile hits if that is straightforward.
- Keep the existing DamageRequest/DamageResolver path intact.
- Keep floating damage numbers and hit flash working for successful damage before defeat.
- Add combat log output only if it clarifies the state change.

Avoid for this task:
- Enemy AI
- Street Thug or Security Drone archetypes
- Wave spawning
- Reusable enemy framework
- Event bus work
- Loot or progression

After implementation, verify:
- `dotnet build`
- `godot --headless --path . --scene res://scenes/arena/Arena.tscn --quit-after 2`
- `git diff --check`
- Manual playtest in Godot:
  - Move and aim.
  - Fire Kinetic Burst at the dummy until it is defeated.
  - Confirm damage numbers, hit flash, combat log messages, and defeated presentation behave correctly.

Commit only after verification, using a focused message such as:
- `Add enemy dummy defeated feedback`

If the defeated-state slice is already complete, the next small Milestone 3 preparation task is to sketch the first enemy AI slice in docs/ROADMAP.md before implementing it. Keep that planning change short and concrete.
```

