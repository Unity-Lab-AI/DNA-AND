---
title: R5 · Universe
status: partial
sources: [docs/DESIGN.md (rounds 1-2, items 423-431), docs/ROADMAP.md#R5, src/gen/GalaxyGen.cs, src/gen/StarGen.cs, src/gen/DysonGen.cs, src/gen/SpaceGen.cs, content/terrain.dnat]
last-verified: 95a7103
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
- `content/small-bodies.dnat`: 40 entries (Asteroids by makeup, each with its mix, makeup, density, albedo and belt-share; Asteroids by orbit; Comets, with their mix and makeup; Dwarf planets and far bodies, the far ones with theirs; Meteors and meteorites) and 8 rules
- `content/moons.dnat`: 18 entries (By how they formed, By surface and inside, By orbit) and 6 rules

## 🔨 Built so far

- **A universe of 36 galaxies** (`src/shell/GalaxiesView.cs`): the sample viewer's, in the plain DNA-AND build. The universe view (`src/shell/UniverseView.cs` and its parts `src/shell/UniverseView.*.cs`, one class in twelve files) holds about 100 billion galaxies on the cosmic web in a visible universe 13.4 billion light-years across, with no edge, only a horizon that moves with you.
- **The galaxy** (`DeepView.cs`):
  - 150,000 stars, clusters and nebulas, and the core black hole at the real centre.
  - The wheel zooms where the mouse points. Clicking any star marks it with a ring and flies to it.
- **Solar systems** (`src/gen/SpaceGen.cs`):
  - Each star's own system opens whole. Click a planet or moon, or Tab to one.
  - Star sizes are real, from `radius-sol` in content.
  - The star is drawn as a coloured shine from far away and as its plasma and corona up close.
  - Every star kind with a surface temperature and a share of stars can appear in a galaxy: 26 kinds, from red dwarfs to Wolf-Rayet stars, white dwarfs and pulsars (stellar black holes give no light and are not picked as a system's star). Its colour comes from its surface temperature, the way a glowing hot body shines (Planck's law): red when cool, white near our own star's temperature, blue when hot. How bright it shines comes from its size and temperature (one solar radius at 5772 K: one solar luminosity), which sets where its frost line and habitable zone lie.
  - The planet being approached is drawn as the real 3D planet.
  - **Which kind of world forms where is a template** (`content/terrain.dnat`, read by `SpaceGen.WorldKind`): each kind says `forms` (rocky, giant, hot-jupiter), where its orbit lies against the liquid-water zone (`orbit-hz`) and the frost line (`orbit-frost`), how bright its star is (`star-brightness`), and its `weight`, size, moons, spin (`day-hours`), colour, card label and make-up. Every value may be a range and each world rolls its own from its seed (`PlanetTerrain.Instance`). Ocean worlds and tidally locked worlds round dim red stars now form too; a new kind is one new entry.
  - **Every generator reads its template, never a name** (DESIGN 431): galaxies' arms, star forming, clusters and how their bodies lie (`galaxies.dnat`); rings on any world by its kind's `rings-chance`, drawn in the universe; volcanoes and rivers; a giant's layers (`gas-giants.dnat` `in:`); a system's number of worlds, first orbit and spacing, and how often each kind of system appears (`solar-systems.dnat`).
  - Black holes are feeding or quiet, by their kind's shares (content/bodies.dnat): a feeding one shows its accretion disc, hot and bright at its inner edge, some firing jets from their poles; a quiet one is its shadow and the light it bends.
  - Every hole's shadow is drawn black at the size the law gives from the camera's place (UniverseView.DrawShadows, ScaleHud.DrawCone); the disc's own light is bent on the card (Pipeline.cs, the hole-frame block: the far side arched over the shadow, captured light bent round its edge); the fall inside (MatterView kind 12) is travel down the axis of entry on the law's own clock (r(tau) = rs (1 - tau/tauEnd)^(2/3), shown over about thirty seconds), the gravity well round you as a grid from the metric, a long funnel to the point (UniverseView.DrawFallGrid), the card carrying the escape speed, the time since the horizon and the time left (the on-screen graph was dropped; ScaleHud.DrawWell is unused).
  - Every world's mass from its size and its kind's density (DESIGN 579: `content/terrain.dnat` density-g-cc, SpaceGen.MassOf, Body.MassKg; its card's mass, ground pull and escape speed). The gravity switch (DESIGN 580: Window.GravityOn, SpaceGen.GravityOffYear, UniverseView.ToggleGravity/StarSwell/GalaxyDrift, the point shader's extra.z). Every hole's spin and the disc's ISCO by it, the fall's twisted funnel (DESIGN 581: @law frame-dragging, Extra.Spin, IscoRs, PartsFor by spin). A star's layers (DESIGN 582: stars.dnat @layer, StarLayers/StarLayerHere/StarLayerAt, the fill inside, the layer's plasma). The tour's end figure (DESIGN 584: SeenWords, {seen}).
  - Gravitational time dilation (DESIGN 577): UniverseView.ClockFactorHere sums 2GM/(r c^2) over the system's star, the world you are at (WorldGm from its size and kind) and the holes among the extras; ClockWords puts your own clock against far clocks in the title (slower in a well, stopped at a horizon, past it where no far clock sees it); a world's card gives the microseconds a day its own pull costs clocks on its ground and a satellite's gain four radii up; the fall's card the far watcher who never sees you cross.
  - Supernovae: UniverseView.Supernovae (per galaxy, from its seed and _simYears, content/physics.dnat @law supernova and @supernova kinds), DrawSupernovae (points by physics, the fireball, the shells near only), the remnants among the bodies round you, the dead stars (_localDead), ScorchWords.
  - Orbits are not all in one plane: each planet's tilts a few degrees from the system's plane (a few far out of line), close moons lie in their planet's plane, far captured moons at any tilt, some going round backwards.
- **The Dyson sphere** (`src/gen/DysonGen.cs`, `PlanetTerrain.cs` in shell mode): an industrial hull outside and a terraformed inside-out world, with free flight.

## Inside every body, and the one tree

- **Layers for every body** (`src/gen/BodyLayers.cs`): one reader for every `@layer` in the content. Rocky worlds have crust, upper mantle, lower mantle, outer core and inner core (the Preliminary Reference Earth Model's depths, temperatures, densities and pressures); the Moon crust, mantle and core; icy moons an ice shell, a hidden ocean, a rock mantle and a core; asteroids regolith over a rubble body; comets a dust crust over an icy body; neutron stars an outer crust, an inner crust, an outer core and an unknown inner core; white dwarfs a hydrogen skin, a helium layer and a carbon-oxygen body. A solid body's card lists its layers and its whole make-up by element (an Earth-type world comes out with a core of about a third of its mass, as Earth's is).
- **The one tree** (`src/core/Thing.cs`): wherever you are, the chain from the universe down (galaxy, star, world or hole or nebula, the layer you are in, the gas or the grain, its crystal, the atom, and inside it the electron, nucleus, nucleon or quark) is one list, written on every card as "you are in:". Each level made from its parent's own make-up, and the matter view reading the layer you are in, come next.
- **A black hole's swarm, clickable** (`src/shell/UniverseView.Swarm.cs`): each star round a feeding hole is worked out on the processor exactly as the card draws it, so the one under the mouse is picked and followed round its orbit; its card gives its orbit, its year and its speed at its closest.
- **What there is to pick, at every size** (`src/shell/MatterView.Census.cs`): the matter's card says what is laid out to pick at this size, or that there is nothing real there; the zoom log writes it at every tenfold.

## Set 2: matter on the tree

- **Down through a world** (`src/shell/UniverseView.Depth.cs`): Page Down and Page Up in a solid world's matter dig through its layers to its middle; the matter there its layer's own (from the world's own make-up), the crystal round you every way, the card the layer, depth, heat, pressure and density.
- **One atom model at every size** (DESIGN 740): every atom its electron density, one record grown on the card, neighbours' clouds overlapping, its light kept by its size on screen; ions by their ionic character (Shannon radii, Pauling); the matter's own light, photons drawn from Planck's spectrum at the place's temperature, infrared to gamma, ionising where they carry more than the atom's first ionisation energy.
- **No fades**: the planets and the stars round you as bright as their apparent magnitude; nebulae grown on the card from their seeds at every distance; the sky behind the matter at every size; a world's glow gone once its ground is ready; the galaxies and the web's light handed over at one distance with their light matched (WEBMATCH in the zoom log).
- **Clickable**: a feeding hole's swarm stars, disc, jets and torn star; each matter size's census on its card.
