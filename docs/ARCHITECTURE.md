# Cyberpunk ARPG Combat Sandbox Architecture

## Architectural Goal

The architecture should support a small but extensible ARPG combat sandbox.

The primary goal is to model complex combat interactions in a clean, testable, and data-driven way. The project should avoid tightly coupling abilities, enemies, damage logic, loot logic, and visual effects.

The architecture should make it easy to add new abilities, enemies, status effects, passive nodes, and item modifiers without rewriting core systems.

## Core Design Principles

1. Gameplay logic should be separated from presentation.

2. Abilities should emit events rather than directly controlling every outcome.

3. Damage calculation should flow through a clear pipeline.

4. Modifiers should be composable and data-driven.

5. Status effects should be reusable and inspectable.

6. Debugging tools should be treated as first-class systems.

7. The first version should be simple but not throwaway.


## High-Level Runtime Flow

Each frame, the game performs a loop similar to:

```text
Read player input
Update movement
Update active abilities
Update projectiles and area effects
Update enemy AI
Detect collisions and hits
Emit combat events
Process damage and status effects
Resolve deaths and loot drops
Update UI and debug logs
Render visuals
```

Not all systems need to run at the same frequency. Visual updates can happen every frame, while certain simulation systems can run on fixed ticks.

## Main Systems

### Entity System

Entities represent things that exist in the game world.

Examples:

- Player

- Enemy

- Projectile

- Area effect

- Drone

- Turret

- Loot drop


Entities should be composed from reusable data and behavior where possible.

Common entity data:

- Entity ID

- Position

- Velocity

- Health

- Team or faction

- Tags

- Active status effects

- Modifiers

- Collision shape


### Ability System

The Ability System is responsible for activating cyberware abilities, combat programs, weapons, drones, and movement skills.

Responsibilities:

- Validate whether an ability can be used

- Check Energy cost

- Check cooldown

- Spawn projectiles or area effects

- Emit ability events

- Apply ability tags

- Pass ability data into the combat pipeline


The Ability System should not directly handle every result of an ability. Instead, it should create events that other systems process.

Example ability lifecycle:

```text
AbilityInputReceived
AbilityActivationRequested
AbilityActivated
EnergyConsumed
ProjectileSpawned
HitDetected
DamageRequested
DamageResolved
StatusApplied
EnemyKilled
LootRolled
```

### Event Bus

The Event Bus is the communication backbone of the combat system.

Systems can publish events, and other systems can react to those events.

Example events:

- AbilityActivated

- ProjectileSpawned

- HitDetected

- DamageRequested

- DamageResolved

- StatusApplied

- EntityKilled

- LootRolled

- ItemDropped

- PassiveNodeUnlocked


The Event Bus should support debug logging so combat behavior can be inspected.

For MVP, the Event Bus can be simple and local. It does not need distributed messaging or networking.

### Damage System

The Damage System resolves damage through a consistent pipeline.

Example damage flow:

```text
Receive DamageRequested event
Identify attacker
Identify defender
Read base damage
Apply attacker modifiers
Apply ability modifiers
Apply defender mitigation
Apply resistances
Apply critical hit logic
Apply status effect logic
Subtract final health
Emit DamageResolved event
Emit EntityKilled event if health reaches zero
```

Damage should be represented by a structured object rather than raw numbers.

Example damage data:

```json
{
  "sourceEntityId": "player",
  "targetEntityId": "enemy_004",
  "abilityId": "emp_pulse",
  "baseAmount": 24,
  "damageTypes": ["Shock", "Signal"],
  "tags": ["AoE", "Hack"],
  "canCrit": true
}
```

### Modifier System

The Modifier System is one of the most important systems in the project.

Modifiers can come from:

- Origin starting stats

- Equipped weapons

- Cyberware

- Passive nodes

- Status effects

- Temporary buffs

- Enemy traits


Modifiers should be composable.

Example modifiers:

- +10% Kinetic damage

- +15 maximum Energy

- +20% AoE radius

- -10% Energy cost for Hack abilities

- +1 projectile for Projectile abilities

- +25% Drone duration

- +5% critical chance with Melee abilities


A modifier should usually include:

- Target stat

- Operation

- Value

- Required tags

- Optional conditions


Example:

```json
{
  "stat": "DamagePercent",
  "operation": "Add",
  "value": 15,
  "requiredTags": ["Shock"]
}
```

The Modifier System should support querying final calculated values.

Example queries:

```text
GetFinalDamageModifier(tags)
GetFinalEnergyCost(ability)
GetFinalCooldown(ability)
GetFinalAreaRadius(ability)
```

### Status Effect System

The Status Effect System handles temporary effects applied to entities.

Examples:

- Overheat

- Jammed

- Corrupted

- Bleeding

- Nanite Infection

- Staggered


Each status effect should define:

- Duration

- Stack behavior

- Tick behavior

- Events triggered on apply

- Events triggered on expire

- Modifiers granted while active


Example status effect:

```json
{
  "id": "jammed",
  "name": "Jammed",
  "durationSeconds": 3,
  "stackingMode": "RefreshDuration",
  "modifiers": [
    {
      "stat": "AttackSpeedPercent",
      "operation": "Add",
      "value": -30
    }
  ]
}
```

### Enemy AI System

The Enemy AI System should start simple.

Initial enemy behaviors:

- Chase player

- Stop at attack range

- Attack on cooldown

- Move away or strafe for ranged units

- Die when health reaches zero


Enemy behavior should be simple enough to support the combat sandbox without becoming the main project.

Initial enemy types:

- Street Thug

- Security Drone

- Riot Bot


### Loot System

The Loot System responds to enemy death events.

Basic flow:

```text
EntityKilled event emitted
Loot System checks killed entity
Loot table is selected
Drop chance is rolled
Item is generated
ItemDropped event is emitted
Loot object appears in arena
```

MVP loot can be simple.

Initial item types:

- Weapon

- Cyberware

- Armor

- Modifier Chip


Initial rarity tiers:

- Common

- Enhanced

- Rare


Loot should eventually support affixes, but the first version can use predefined items.

### Augmentation Grid System

The Augmentation Grid is a graph of passive nodes.

Each Origin starts at a different node. The player can unlock connected nodes as they progress.

MVP implementation can be simple:

- Nodes are defined in data.

- Each node has an ID.

- Each node lists neighboring node IDs.

- Each node grants modifiers.

- The player has a list of unlocked node IDs.

- Unlocking a node applies its modifiers.


Example node:

```json
{
  "id": "shock_damage_01",
  "name": "Capacitor Tuning",
  "description": "+10% Shock damage",
  "neighbors": ["energy_01", "shock_damage_02"],
  "modifiers": [
    {
      "stat": "DamagePercent",
      "operation": "Add",
      "value": 10,
      "requiredTags": ["Shock"]
    }
  ]
}
```

The first version does not need a polished visual grid. A simple debug UI is enough.

### Debug and Observability

Debug tooling is a major portfolio feature.

The project should include a combat event log showing what happened during combat.

Example log:

```text
Player activated EMP Pulse
Energy consumed: 35
EMP Pulse hit Security Drone
Security Drone took 24 Shock damage
Jammed applied to Security Drone for 3 seconds
Security Drone killed
Loot rolled
Item dropped: Smart Rifle Mk1
```

Debug tools should help inspect:

- Active events

- Entity health

- Active status effects

- Current modifiers

- Ability cooldowns

- Damage calculation details

- Loot rolls


This makes the architecture easier to explain to employers.

## Data-Driven Content

The project should move toward data-driven definitions for:

- Origins

- Abilities

- Enemies

- Status effects

- Passive nodes

- Items

- Modifiers

- Loot tables


Early prototypes can hardcode some data, but the architecture should be designed with data-driven content in mind.

Suggested data format:

- JSON for early prototypes

- Godot resources if they become more ergonomic

- C# classes as runtime models


## Suggested Folder Structure

```text
src/
  Core/
    Events/
    Time/
    Math/
    Tags/

  Gameplay/
    Entities/
    Abilities/
    Combat/
    Damage/
    Modifiers/
    StatusEffects/
    AI/
    Loot/
    Progression/

  Data/
    Origins/
    Abilities/
    Enemies/
    Items/
    StatusEffects/
    AugmentationGrid/
    LootTables/

  UI/
    Hud/
    Debug/
    Menus/

  Scenes/
    Arena/
    Player/
    Enemies/
    Projectiles/
    Effects/

  Tests/
    Combat/
    Modifiers/
    Damage/
    StatusEffects/
```

This structure can be adapted to Godot conventions as the project starts.

## MVP Milestones

### Milestone 1: Movement and Arena

Goal:

- Create top-down arena

- Add player movement

- Add mouse aiming

- Add camera follow

- Add placeholder art


Systems involved:

- Player controller

- Input handling

- Arena scene

- Camera


### Milestone 2: Basic Combat Events

Goal:

- Add one ability

- Spawn a projectile

- Hit enemies

- Emit damage events

- Show combat log


Systems involved:

- Ability System

- Projectile System

- Event Bus

- Damage System

- Debug Log


### Milestone 3: Enemy Waves

Goal:

- Spawn enemies in waves

- Add simple AI

- Allow enemies to attack player

- Allow enemies to die


Systems involved:

- Enemy AI

- Wave Spawner

- Health System

- Death events


### Milestone 4: Status Effects

Goal:

- Add EMP Pulse

- Apply Jammed status

- Add status duration

- Show active statuses in debug UI


Systems involved:

- Status Effect System

- Modifier System

- Event Bus


### Milestone 5: Loot Drops

Goal:

- Drop basic items on enemy death

- Let player pick up items

- Apply item modifiers


Systems involved:

- Loot System

- Item System

- Modifier System

- Inventory placeholder


### Milestone 6: Augmentation Grid Prototype

Goal:

- Define passive nodes in data

- Unlock nodes through debug UI

- Apply passive modifiers to player

- Verify modifiers affect combat


Systems involved:

- Augmentation Grid System

- Modifier System

- Player build state


## Testing Strategy

The project should include simple automated tests for core logic.

Good candidates for tests:

- Damage calculation

- Modifier stacking

- Status effect duration

- Status effect stacking rules

- Energy cost calculation

- Cooldown calculation

- Passive node unlock validation

- Loot table rolls


Testing the core systems outside the visual game loop will make the project more credible as a software engineering portfolio piece.

## Portfolio README Focus

The final README should emphasize:

- Why the project exists

- What systems it demonstrates

- How the combat architecture works

- How events flow through the game

- How data-driven abilities work

- How modifiers are composed

- How the project could scale


Suggested README tagline:

```text
A cyberpunk ARPG combat sandbox built in Godot and C#, focused on event-driven combat, data-driven abilities, modifier systems, loot generation, and extensible progression architecture.
```

## Architecture Non-Goals

The first version should avoid:

- Multiplayer netcode

- Large-scale procedural generation

- Full campaign structure

- Complex animation graphs

- Polished art pipeline

- Large inventory UI

- Online services

- Real-time economy systems


The architecture should be extensible, but the MVP should remain focused.