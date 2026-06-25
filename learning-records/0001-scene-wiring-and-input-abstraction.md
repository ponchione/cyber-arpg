# Scene Wiring And Input Abstraction

The user correctly identified that `project.godot` maps `primary_fire` to left mouse input and understood that this input-action layer lets gameplay code read intent instead of physical controls. A misconception was corrected: `FireKineticBurst()` uses the assigned projectile scene, but the actual `KineticBurstScene` assignment lives in `Player.tscn` as a serialized value for an exported C# property.

**Evidence**

The user explained that changing right-click support should start in `project.godot` so implementation code does not need to care which physical input maps to primary fire.

**Implications**

Future sessions can build on input-action abstraction, but should continue reinforcing the distinction between exported C# slots, `.tscn` scene data, and runtime instantiation.
