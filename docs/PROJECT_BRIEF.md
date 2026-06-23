# Cyberpunk ARPG Combat Sandbox

## Working Description

This project is a top-down 2D cyberpunk ARPG combat sandbox built for portfolio and learning purposes. The goal is not to create a full commercial-scale ARPG, but to build a focused systems-driven prototype that demonstrates clean architecture, event-driven combat, data-driven abilities, modifier stacking, enemy behavior, loot generation, and progression systems.

The gameplay is inspired by ARPGs like Path of Exile, but the theme is cyberpunk rather than fantasy. Classes define where the player starts on a shared progression grid, but they do not hard-lock the player into specific mechanics. Builds are primarily defined by cyberware, abilities, gear modifiers, and progression choices.

## Core Fantasy

The player is an augmented operator fighting through hostile cyberpunk combat scenarios. Instead of spells, the player uses cyberware, combat programs, drones, implants, and modified weapons.

Examples of abilities include:

- EMP Pulse

- Mono-Blade Dash

- Signal Spike

- Neural Burn

- Deploy Sentry

- Hunter Drone

- Hydraulic Slam

- Reflex Overclock


The game should feel like a cyberpunk interpretation of ARPG buildcraft.

## Primary Portfolio Goal

The main portfolio goal is to show strong systems design.

This project should demonstrate:

- Event-driven combat architecture

- Data-driven skills and modifiers

- Clean separation between gameplay systems

- Extensible entity and ability modeling

- Status effect handling

- Damage calculation pipelines

- Loot generation and item modifiers

- Debugging and observability tools

- Practical C# game architecture


The README and project structure should make it clear that the purpose is not just to make a game, but to model a complex interactive system cleanly.

## Recommended Tech Stack

Initial recommendation:

- Engine: Godot 4

- Language: C#

- Game type: 2D top-down arena

- Controls: WASD movement with mouse aiming

- Scope: Single-room wave survival arena for MVP


This stack gives the project a strong connection to C#/.NET while keeping the game engine lightweight and approachable.

## Game Format

The first playable version should be a single-room arena.

The arena should support:

- Player movement

- Mouse aiming

- Enemy spawning

- Basic enemy AI

- Multiple active abilities

- Projectile and area attacks

- Damage numbers

- Enemy deaths

- Loot drops

- Combat event logging


The arena approach keeps the project focused on combat systems rather than level design.

## Core Gameplay Loop

1. Player enters the arena.

2. Enemies spawn in waves.

3. Player moves, aims, and uses cyberware abilities.

4. Abilities emit combat events.

5. Events resolve damage, status effects, deaths, and loot.

6. Player collects loot and improves their build.

7. Player unlocks passive nodes on the Augmentation Grid.

8. Later waves test the build.


## Core Systems

The game should be designed around the following core systems:

- Entity system

- Ability system

- Event bus

- Damage system

- Modifier system

- Status effect system

- Enemy AI system

- Loot system

- Augmentation Grid system

- Debug/combat log system


## Player Origins

The game uses four starting Origins. Origins are similar to classes, but they only determine starting stats, starting abilities, starting gear, and starting position on the Augmentation Grid.

Any Origin should eventually be able to build into any major mechanic.

### Street Ronin

A fast close-to-mid range combatant focused on mobility, critical hits, melee cyberware, sidearms, and evasive movement.

Starting themes:

- Mobility

- Melee

- Sidearms

- Crit

- Bleed-style effects

- Dash attacks


Example abilities:

- Mono-Blade Dash

- Kinetic Pistol Burst

- Reflex Overclock


### Netrunner

A hack-focused combatant built around signal damage, debuffs, energy efficiency, status effects, and control.

Starting themes:

- Signal damage

- Corruption

- Energy efficiency

- Cooldown recovery

- Crowd control

- Damage over time


Example abilities:

- Signal Spike

- Neural Burn

- EMP Pulse


### Chrome Vanguard

A heavily augmented bruiser focused on armor, health, kinetic damage, stagger, retaliation, and explosive force.

Starting themes:

- Armor

- Health

- Kinetic damage

- Stagger

- Heavy weapons

- Retaliation effects


Example abilities:

- Hydraulic Slam

- Ballistic Barrage

- Reactive Plating


### Dronewright

An engineer-style Origin focused on drones, turrets, mines, deployables, repair effects, and automated combat.

Starting themes:

- Drones

- Turrets

- Mines

- Area control

- Repair effects

- Targeting systems


Example abilities:

- Deploy Sentry

- Hunter Drone

- Arc Mine


## Progression System

The main progression system is the Augmentation Grid.

The Augmentation Grid is a shared passive tree. Each Origin begins in a different region, but all Origins can path toward any part of the grid.

The grid should include:

- Generic central nodes

- Origin-flavored starting zones

- Specialized outer clusters

- Notable nodes

- Keystone nodes


MVP target:

- 4 starting zones

- 1 shared center

- 6 specialization clusters

- 40 to 60 passive nodes

- 8 to 12 notable nodes

- 4 keystone nodes


For the first version, the grid does not need a polished visual UI. A debug menu or simple list-based unlock screen is acceptable while the system matures.

## Resource System

The main player resource is Energy.

Energy functions similarly to mana in an ARPG.

Energy is used by:

- Cyberware abilities

- Combat programs

- Drones

- Mobility skills

- Defensive abilities


Possible Energy modifiers:

- Increased maximum Energy

- Faster Energy recovery

- Reduced Energy cost for Hack abilities

- Reduced Energy cost for Drone abilities

- Energy gained on kill

- Energy gained when applying a status effect


## Ability System

Abilities are the real build-defining system.

Abilities should be treated as installed cyberware, combat programs, implants, or deployable modules.

Abilities should be data-driven where possible.

Example ability categories:

- Projectile

- Melee

- Hack

- Drone

- AoE

- Movement

- Defensive

- Utility


Example damage tags:

- Kinetic

- Thermal

- Shock

- Bio

- Signal


Example ability tags:

- Projectile

- Melee

- Hack

- Drone

- AoE

- Movement

- Cooldown

- Duration

- Energy


## Weapons and Gear

Weapons are supporting gear, not the primary identity of the character.

Weapons should provide:

- Base damage

- Tags

- Implicit modifiers

- Affixes

- Scaling hooks for some abilities


Weapon examples:

- Sidearm

- Smart Rifle

- Shotgun

- Heavy Cannon

- Mono-Blade

- Shock Baton


Cyberware and abilities define the build. Weapons support and modify the build.

## Damage Types

Initial damage types:

- Kinetic

- Thermal

- Shock

- Bio

- Signal


Each damage type can eventually support its own status effects and resistance interactions.

## Status Effects

Initial status effects:

- Overheat

- Jammed

- Corrupted

- Bleeding

- Nanite Infection

- Staggered


These should be implemented as reusable status effect definitions rather than hardcoded one-off behavior.

## MVP Scope

The first milestone should include:

- One playable arena

- WASD movement

- Mouse aiming

- One player Origin

- Three active abilities

- Three enemy types

- Basic wave spawning

- Basic damage calculation

- Basic status effects

- Enemy health and death

- Basic loot drops

- Combat event log


The MVP should prove the core combat loop and architecture.

## Initial MVP Abilities

Suggested first abilities:

1. Kinetic Burst

    - Simple projectile attack

    - Tests aiming, projectile spawning, collision, and damage

2. EMP Pulse

    - Area ability

    - Tests radius queries, status application, cooldowns, and Energy cost

3. Mono-Blade Dash

    - Movement attack

    - Tests movement, collision, melee hit detection, and short-duration effects


These three abilities cover different technical needs without overcomplicating the first build.

## Initial MVP Enemies

Suggested first enemies:

1. Street Thug

    - Basic melee chaser

2. Security Drone

    - Ranged enemy with simple movement

3. Riot Bot

    - Slower durable enemy with armor or stagger resistance


These enemies create enough variety to test combat, AI, damage types, and status effects.

## Non-Goals for MVP

The MVP should not include:

- Multiplayer

- Procedural campaign generation

- Full inventory UI

- Fully polished art

- Large passive tree UI

- Dozens of classes

- Dozens of abilities

- Complex questing

- Online economy

- Full controller support

- Advanced animation systems


These can be considered later, but they should not block the first playable version.

## Success Criteria

The MVP is successful when:

- The player can move and aim smoothly.

- The player can use multiple abilities.

- Abilities emit events into the combat system.

- Enemies can take damage, receive statuses, and die.

- Loot can drop from enemies.

- Passive modifiers can affect combat.

- The combat log clearly shows what happened.

- New abilities and modifiers can be added without rewriting core systems.

- The README explains the architecture clearly enough for an employer to understand the design.


## Future Expansion Ideas

Possible future features:

- Visual Augmentation Grid UI

- Multiple Origins selectable at character creation

- Item rarity tiers

- Procedural affixes

- Ability modifier chips

- Boss encounters

- Build planner

- Combat replay system

- Local simulation tests

- Performance profiling tools

- Optional backend leaderboard

- Optional save/load system