# cyber-arpg

A cyberpunk ARPG combat sandbox built in Godot 4 .NET and C#.

The project is a focused portfolio and learning piece for systems design. It is intentionally scoped as a one-room combat sandbox rather than a full ARPG, with combat systems built incrementally as playable slices.

## Current Milestone

Milestone 2: First Combat Event Pipeline

Current playable scene:

- `scenes/arena/Arena.tscn`

Implemented gameplay:

- WASD movement
- Mouse aiming
- Camera follow
- Health and Energy HUD
- Left-click Kinetic Burst
- Projectile collision with enemy dummy
- Damage resolved through `DamageResolver`
- HUD combat log
- Floating damage numbers
- Enemy dummy hit flash

## Current Limits

- No enemy AI
- No wave spawning
- No enemy archetypes
- No loot or progression
- No generalized VFX, ability, or event-bus architecture yet

## Run and Verify

Open `project.godot` in Godot 4 .NET, build the C# project if prompted, then run the main scene.

Command-line checks:

```sh
dotnet build
godot --headless --path . --scene res://scenes/arena/Arena.tscn --quit-after 2
```

Manual playtest:

- Open `scenes/arena/Arena.tscn`.
- Verify WASD movement, mouse aiming, and camera follow.
- Left-click to fire Kinetic Burst at the enemy dummy.
- Verify projectile collision, HUD combat log output, floating damage numbers, and the enemy dummy hit flash.

## Docs

- `docs/PROJECT_BRIEF.md`
- `docs/ARCHITECTURE.md`
- `docs/ROADMAP.md`
