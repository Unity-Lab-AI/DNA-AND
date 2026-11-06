---
title: R5 · Universe
status: partial
sources: [docs/DESIGN.md (rounds 1-2), docs/ROADMAP.md#R5, src/gen/GalaxyGen.cs, src/gen/StarGen.cs, src/gen/DysonGen.cs]
last-verified: 5dd380b
---

# 🌌 R5 · Universe

> "stars+planets>solar sytems> galaxys" · "obviously galaxy size to buidl dyson spheres and ingalactic war"

## 🪆 The nesting
| Level | Holds | Built from |
|---|---|---|
| 🌌 Universe | Many galaxies | World seed |
| 🌀 Galaxy | Solar systems | Galaxy seed (from the world seed) |
| ☀️ Solar system | Stars, planets, moons, belts | System seed |
| 🪐 Planet | A full world | Planet seed + template ([R6](r06-planet.md)) |

## ⚡ Only build what's being looked at
A full galaxy is far too big to hold in memory at once. Each level is **generated from its seed when it's needed**, and saved only once something there has changed (life, colonies, god powers).

## 🎯 Where you start
You start on an Earth-template planet in a random solar system. Life reaches other planets only after intelligent life gets there first ([R10](r10-space.md)).

## 🔁 Restart means a new universe
> "open ended till u resarts to get a new world and galaxy"

## 📌 Decided specs (build from these)
- Galaxy sizes: Small 100, Medium 500, Large 2,000, Massive 10,000 star systems, all fully simulated all the time (round 17).

- 100 space objects in 10 groups, including Oort clouds, the Kuiper belt, comets, asteroid belts and nebulas (`docs/wiki/space-and-dyson-spheres.md`).
- Impacts in 10 sizes, from comets and meteorites out of Oort clouds, the Kuiper belt and asteroid belts.

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/galaxies.dnat`: 47 entries (Galaxy types (real), Extra galaxy types (from research), Real examples, Parts a galaxy is built from, Structures between galaxies) and 11 rules
- `content/solar-systems.dnat`: 31 entries (Kinds of systems (real), Real examples, Zones a system is built from) and 12 rules
- Each galaxy kind gives its `layout:` (spiral, barred spiral, elliptical, lenticular, irregular, ring); each star its `surface-kind:`, `spots:` and `remnant:`; the Dyson builds are `@build` entries in `content/space-tech.dnat`, made of parts (mirrors, swarm, bubble, shell, jet).
- `content/stars.dnat`: 34 entries (Main-sequence stars (the seven classic types), Giants and supergiants, Young and failed stars, Dead and strange stars, Luminosity classes (how bright for its type), Extra star types) and 5 rules
- `content/black-holes.dnat`: 35 entries (Kinds of black hole, Parts, Active galaxy cores (a feeding supermassive black hole), Events, Real examples) and 7 rules
- `content/nebulas.dnat`: 12 entries (The five main kinds, Star-forming structures) and 6 rules
- `content/small-bodies.dnat`: 39 entries (Asteroids by makeup, Asteroids by orbit, Comets, Dwarf planets and far bodies, Meteors and meteorites) and 7 rules
- `content/moons.dnat`: 18 entries (By how they formed, By surface and inside, By orbit) and 6 rules

## 🔨 Built so far

- **A universe of 36 galaxies** (`src/shell/UniverseView.cs`). The wheel zooms into the galaxy under the mouse.
- **The galaxy** (`DeepView.cs`):
  - 150,000 stars, clusters and nebulas, and the core black hole at the real centre.
  - The wheel zooms where the mouse points. Clicking any star marks it with a ring and flies to it.
- **Solar systems** (`src/gen/SpaceGen.cs`):
  - Each star's own system opens whole. Click a planet or moon, or Tab to one.
  - Star sizes are real, from `radius-sol` in content.
  - The star is drawn as a coloured shine from far away and as its plasma and corona up close.
  - Every star kind with a surface temperature and a share of stars can appear in a galaxy: 26 kinds, from red dwarfs to Wolf-Rayet stars, white dwarfs and pulsars (stellar black holes give no light and are not picked as a system's star). Its colour comes from its surface temperature, the way a glowing hot body shines (Planck's law): red when cool, white near the Sun's temperature, blue when hot. How bright it shines comes from its size and temperature (the Sun: 1 radius, 5772 K), which sets where its frost line and habitable zone lie.
  - The planet being approached is drawn as the real 3D planet.
- **The Dyson sphere** (`src/gen/DysonGen.cs`, `PlanetTerrain.cs` in shell mode): an industrial hull outside and a terraformed inside-out world, with free flight.
