# Teaching Notes

- Current focus: trace one Kinetic Burst shot from left click to enemy hit flash in `scenes/arena/Arena.tscn`.
- Teaching style requested: reviewer/tutor first, small chunks, concrete file/method references, ask the user to explain each idea back, then give 2-4 short knowledge-check questions.
- Session stopped after Chunk 2 was introduced: aim direction and projectile spawn in `PlayerController._Ready`, `_PhysicsProcess`, `UpdateAimDirection`, and `FireKineticBurst`.
- Next session should resume by asking the user to answer the Chunk 2 explain-back and knowledge checks before moving to projectile `Area2D.BodyEntered`.
- Reinforce: exported C# property equals a configurable slot exposed to Godot; `.tscn` assigned value equals the serialized value in that slot.
- Reinforce: `project.godot` input actions map physical inputs to gameplay intent so gameplay code can read action names.
- Reinforce: `Arena.tscn` composes high-level scene instances; `Player.tscn` owns the player's internal node structure, script attachment, camera, aim pivot, and projectile scene reference.
- Avoid implementing or expanding systems during tutoring unless the user explicitly asks for a scoped exercise or code change.
