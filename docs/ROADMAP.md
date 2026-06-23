# Cyberpunk ARPG Combat Sandbox Roadmap

## Roadmap Philosophy

This roadmap is milestone-based rather than time-based.

The project is a side project, so progress should be measured by working systems, not calendar deadlines. Each milestone should produce something playable, testable, or demonstrable.

The goal is to avoid building a pile of disconnected experiments. Every milestone should move the project closer to a small but impressive ARPG combat sandbox.

## Guiding Principles

- Build the smallest version that proves the system.

- Prefer working gameplay over theoretical architecture.

- Keep visuals simple until the combat systems are interesting.

- Make debugging tools part of the product.

- Keep systems data-driven where practical.

- Avoid multiplayer, procedural campaign generation, and large UI systems during the MVP.

- Do not build a full ARPG before the combat sandbox is fun and understandable.


## Milestone 0: Project Setup and Technical Baseline

### Goal

Create a clean Godot 4 + C# project foundation that can grow without becoming messy immediately.

### Build Tasks

- Create the Godot 4 .NET project.

- Set up the Git repository.

- Add initial documentation.

    - `README.md`

    - `PROJECT_BRIEF.md`

    - `ARCHITECTURE.md`

    - `ROADMAP.md`

- Create initial folder structure.

- Create a placeholder arena scene.

- Add placeholder player object.

- Configure input mappings for WASD movement and mouse aiming.

- Add a basic debug overlay or placeholder debug panel.


### Systems Demonstrated

- Project organization

- Basic Godot/C# setup

- Input configuration

- Scene structure

- Documentation-first planning


### Definition of Done

- The project launches.

- The player placeholder appears in a simple arena.

- WASD input is recognized.

- Mouse position or aim direction can be read.

- The repository has useful starter docs.

- The README clearly describes the project goal.


## Milestone 1: Player Movement and Arena Feel

### Goal

Make the player movement and aiming feel good enough to support combat testing.

### Build Tasks

- Implement WASD movement.

- Implement mouse aiming.

- Add camera follow.

- Add simple collision with arena bounds.

- Add placeholder player sprite or shape.

- Add movement tuning values.

    - Move speed

    - Acceleration

    - Deceleration

    - Rotation or aim responsiveness

- Add simple UI for health and Energy.


### Systems Demonstrated

- Input handling

- Player controller

- Movement tuning

- Basic UI

- Arena scene management


### Definition of Done

- The player can move smoothly around the arena.

- The player can aim with the mouse.

- The camera follows the player.

- The player cannot leave the arena bounds.

- Health and Energy are visible.


## Milestone 2: First Combat Event Pipeline

### Goal

Build the first version of the event-driven combat pipeline.

### Build Tasks

- Create a simple local Event Bus.

- Add a basic enemy dummy.

- Add the first active ability: Kinetic Burst.

- Spawn a projectile from the player.

- Detect projectile collision with an enemy.

- Emit a hit event.

- Resolve damage through a basic damage system.

- Display damage numbers or debug damage text.

- Add a combat event log.


### Systems Demonstrated

- Event-driven combat

- Projectile spawning

- Collision detection

- Damage requests

- Damage resolution

- Debug observability


### Definition of Done

- The player can fire Kinetic Burst.

- Kinetic Burst creates a projectile.

- The projectile can hit an enemy.

- The enemy loses health.

- The combat log records the major events.

- Damage is resolved through a damage system, not directly inside the projectile script.


## Milestone 3: Enemy AI and Wave Spawning

### Goal

Turn the arena into a basic combat scenario instead of a static test room.

### Build Tasks

- Add basic enemy health and death behavior.

- Add Street Thug enemy.

    - Chases the player

    - Attacks at close range

- Add Security Drone enemy.

    - Moves toward range

    - Fires a simple projectile

- Add Riot Bot enemy.

    - Slow movement

    - Higher health

    - Basic armor or damage reduction

- Add wave spawning.

- Emit enemy death events.

- Display simple wave information in UI.


### Systems Demonstrated

- Enemy AI

- Wave spawning

- Health/death lifecycle

- Enemy attack behavior

- Event-driven death handling


### Definition of Done

- Enemies spawn in waves.

- Enemies move and attack.

- The player can kill enemies.

- Enemy deaths emit events.

- The arena has a simple repeatable combat loop.


## Milestone 4: Energy, Cooldowns, and Ability Rules

### Goal

Add enough ability rules to make skills feel like ARPG abilities rather than raw inputs.

### Build Tasks

- Add Energy cost to abilities.

- Add Energy regeneration.

- Add cooldown tracking.

- Prevent ability use when Energy is too low.

- Prevent ability use while on cooldown.

- Add cooldown UI or debug display.

- Add structured ability definitions.

- Move ability tuning values into data where practical.


### Systems Demonstrated

- Resource management

- Ability validation

- Cooldown handling

- Data-driven ability configuration

- UI/debug feedback


### Definition of Done

- Abilities require Energy.

- Abilities respect cooldowns.

- The player can see or inspect Energy and cooldown state.

- Ability use flows through a validation step.

- Kinetic Burst is defined cleanly enough to support more abilities.


## Milestone 5: Status Effects

### Goal

Add reusable status effects and prove that abilities can apply secondary effects.

### Build Tasks

- Create Status Effect System.

- Add status effect definitions.

- Add duration tracking.

- Add stacking or refresh rules.

- Add EMP Pulse ability.

    - AoE effect

    - Shock/Signal tags

    - Applies Jammed

- Add Jammed status.

    - Reduces enemy attack speed or disables enemy attacks temporarily

- Show active statuses in debug output.


### Systems Demonstrated

- Status effect modeling

- AoE queries

- Duration management

- Modifier interaction

- Combat debugging


### Definition of Done

- EMP Pulse can hit multiple enemies.

- EMP Pulse applies Jammed.

- Jammed has a visible gameplay effect.

- Status effects expire correctly.

- The combat log records status application and expiration.


## Milestone 6: Modifier System

### Goal

Create the foundation for ARPG-style buildcraft.

### Build Tasks

- Create a Modifier model.

- Support additive percentage modifiers.

- Support flat stat modifiers.

- Support tag-based modifiers.

- Apply modifiers from player build state.

- Apply modifiers to damage.

- Apply modifiers to Energy cost or cooldown.

- Add debug inspection for current modifiers.


### Systems Demonstrated

- Data-driven modifiers

- Tag-based rules

- Damage scaling

- Resource scaling

- Build extensibility


### Definition of Done

- A modifier can increase Kinetic damage.

- A modifier can increase Shock damage.

- A modifier can reduce Energy cost for Hack abilities.

- A modifier can affect only abilities with matching tags.

- Modifier calculations can be inspected in debug output.


## Milestone 7: Loot and Items

### Goal

Add basic loot drops that can affect the player build.

### Build Tasks

- Add Item model.

- Add item rarity.

    - Common

    - Enhanced

    - Rare

- Add basic item types.

    - Weapon

    - Cyberware

    - Armor

    - Modifier Chip

- Add loot tables.

- Roll loot from enemy deaths.

- Spawn loot objects in the arena.

- Allow player pickup.

- Apply item modifiers to the player.

- Show picked-up item details in debug output or basic UI.


### Systems Demonstrated

- Loot generation

- Item modeling

- Modifier integration

- Death event subscribers

- Basic build progression


### Definition of Done

- Enemies can drop items.

- The player can pick up items.

- Items can grant modifiers.

- Item modifiers affect combat.

- Loot behavior is driven by death events.


## Milestone 8: Augmentation Grid Prototype

### Goal

Implement the first version of the shared passive tree system.

### Build Tasks

- Create Augmentation Grid node model.

- Define nodes in data.

- Add node neighbor relationships.

- Add Origin starting nodes.

- Allow node unlocks through debug UI.

- Apply node modifiers to the player.

- Validate that nodes must connect to already-unlocked nodes.

- Add several nodes for each Origin region.


### Systems Demonstrated

- Graph-based progression

- Passive modifiers

- Build state

- Unlock validation

- Data-driven progression


### Definition of Done

- The player has an Origin starting node.

- The player can unlock connected nodes.

- Nodes grant modifiers.

- Passive modifiers affect combat.

- The system does not require a polished visual tree yet.


## Milestone 9: Origins

### Goal

Add the four starting Origins and connect them to the Augmentation Grid.

### Build Tasks

- Define Street Ronin.

- Define Netrunner.

- Define Chrome Vanguard.

- Define Dronewright.

- Give each Origin starting stats.

- Give each Origin starting abilities.

- Give each Origin starting gear.

- Assign each Origin a starting Augmentation Grid node.

- Add a simple Origin selection screen or debug selector.


### Systems Demonstrated

- Class/origin modeling

- Starting build state

- Data-driven character setup

- Progression integration


### Definition of Done

- The player can start as one of four Origins.

- Each Origin starts in a different Augmentation Grid region.

- Each Origin has distinct starting stats or abilities.

- No Origin is hard-locked out of any later build path.


## Milestone 10: Ability Variety

### Goal

Add enough ability variety to prove that the combat system supports different mechanical styles.

### Build Tasks

- Add Mono-Blade Dash.

    - Movement

    - Melee hit detection

    - Short-range burst

- Add Deploy Sentry.

    - Spawned entity

    - Ownership

    - Simple targeting

- Add Neural Burn.

    - Damage over time

    - Signal or Bio damage

- Add ability tags and scaling rules.

- Add basic ability selection or loadout configuration.


### Systems Demonstrated

- Movement skills

- Deployables

- Damage over time

- Ability tagging

- Loadout modeling

- Reusable ability architecture


### Definition of Done

- The game has at least five mechanically distinct abilities.

- Abilities use shared systems instead of one-off scripts.

- Each ability emits useful combat events.

- Modifiers can affect abilities by tag.


## Milestone 11: Debug and Portfolio Tooling

### Goal

Make the project easy to understand, inspect, and explain.

### Build Tasks

- Improve combat event log.

- Add entity inspector.

- Add active status effect inspector.

- Add modifier inspector.

- Add ability cooldown inspector.

- Add damage breakdown output.

- Add loot roll output.

- Add README diagrams or flow descriptions.


### Systems Demonstrated

- Observability

- Debug-first architecture

- Explainable combat systems

- Portfolio polish


### Definition of Done

- A viewer can understand what happened in combat by reading the debug panel.

- Damage calculations can be inspected.

- Active modifiers can be inspected.

- Status effects can be inspected.

- The README explains the architecture clearly.


## Milestone 12: Vertical Slice Polish

### Goal

Turn the systems sandbox into a small, cohesive portfolio demo.

### Build Tasks

- Improve arena visuals.

- Add basic sound effects.

- Add basic ability visual effects.

- Add enemy spawn indicators.

- Add pause/restart flow.

- Add start screen.

- Add short project description in-game.

- Add final README polish.

- Add screenshots or GIFs.

- Add build instructions.


### Systems Demonstrated

- Productization

- UX polish

- Demo readiness

- Portfolio presentation


### Definition of Done

- The project can be shown to another person without explanation.

- The README has screenshots or GIFs.

- The game has a clean start-to-combat flow.

- The combat sandbox feels like a coherent demo.

- The core architecture is visible and documented.


## Later Expansion Backlog

These ideas should be considered only after the main combat sandbox is stable.

### Gameplay Expansion

- Boss encounter

- Additional arenas

- Elite enemy modifiers

- More enemy factions

- Environmental hazards

- More cyberware abilities

- More weapon bases

- More status effects

- More passive nodes

- Keystone passives


### Systems Expansion

- Save/load

- Build planner

- Combat replay system

- Procedural item affixes

- Ability modifier chips

- Local simulation testing tool

- Performance profiling mode

- Enemy behavior trees

- More advanced targeting systems


### Portfolio Expansion

- Architecture diagrams

- Automated test suite

- Video demo

- Technical blog post

- Public project board

- Devlog notes

- Comparison of early and final architecture decisions


## Recommended Build Order Summary

The recommended build order is:

1. Project setup

2. Movement and arena

3. First combat event pipeline

4. Enemy AI and waves

5. Energy and cooldowns

6. Status effects

7. Modifier system

8. Loot and items

9. Augmentation Grid

10. Origins

11. Ability variety

12. Debug and portfolio polish

13. Vertical slice polish


This order prioritizes the playable combat loop first, then adds buildcraft systems once the core combat loop exists.

## What Not To Do Early

Avoid spending early effort on:

- Complex art

- Large passive tree UI

- Dozens of abilities

- Dozens of item affixes

- Multiplayer

- Procedural generation

- Character customization

- Long story content

- Full inventory management

- Perfect animation systems


Those features can be added later, but they should not delay the first working combat loop.

## Project Health Check

The project is on track if:

- It remains playable after each milestone.

- The architecture is getting clearer, not more tangled.

- New abilities are becoming easier to add.

- New modifiers are becoming easier to add.

- Debugging combat interactions is becoming easier.

- The README is improving alongside the code.

- The project can be explained as a systems-design portfolio piece, not just a game prototype.