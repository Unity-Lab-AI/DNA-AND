---
title: R4 · Renderer
status: verified
sources: [docs/DESIGN.md (round 1, round 4, items 62-76), docs/ROADMAP.md#R4, src/render/, src/gen/PlanetTerrain.cs, src/shell/PlanetView.cs]
last-verified: 034010e
---

# 🎨 R4 · Renderer

> "ut will be a AAA game graphics that can do all the microbes and viruses an d gene mapings and peices for every traight and skill and evolutionary thing like breathing air to walking upright to photosynethesiis" · "3d top down world with god mode space mechanics"

<!-- IMG: docs/img/renderer-zoom-ladder.png | One shot per zoom level: virus, cell, creature, landscape, planet, solar system, galaxy -->
> 🖼️ *One shot per zoom level: virus, cell, creature, landscape, planet, solar system, galaxy* — rendered by the game's own 3D generator

## 🧩 Parts
| Part | Draws |
|---|---|
| 🏔️ Terrain | Ground, rock, ice, made from planet data |
| 🌊 Water | Oceans, rivers, waves, tides |
| ☁️ Sky + weather | Air, clouds, storms, auroras |
| 💡 Light + shadow | Stars, starlight, day and night |
| 🦠 Life | Bodies **built from their genes**, never hand-modelled |
| 🧬 Gene map | Which gene pieces make which trait |
| 🌌 Space | Stars, planets, nebulas, Dyson spheres |

## 🔎 One smooth zoom
Scroll from a single microbe up to the whole galaxy with no loading screens. Each zoom band draws the right level of detail.

## 🖥️ Talking to the graphics card
✅ Direct3D 12, which is part of Windows itself, so it stays inside rule 1.

## 🖼️ The 3D image generator
> "we will need to design the #D genrator and botting of them"

All icons and art come from the game's own generator: creatures from genomes, planets from seeds, icons from the same renderer. See [R17](r17-docs-and-capture.md).

## 🎨 Style samples come first

The art style is locked: **Populous-like (faceted low-poly, tilted 3D god view), for everything** (`docs/DESIGN.md`, items 61, 65, 67). The other two styles were compared and dropped, and their code is gone.

| | |
|---|---|
| 🖼️ **What** | Each sample set opens on its own (`--set2`, `--set3`, `--set4`); Keepers are ticked from the menu and saved in `picks.txt` |
| ✅ **Approved** | Sets 1–6: galaxies, solar systems, stars, planets and gas giants, resource fields, the Dyson sphere (pictures in `docs/samples/`) |
| 🪶 **Rule** | Light on the graphics card. Thousands of living things move and spread at once |

Subjects: microbes, viruses, intelligent creatures, a handful of other living things, galaxy and solar system layouts, planets, stars, gas giants, Dyson spheres, spaceships, space colonies, space stations, resource fields, and all 118 elements.

## 🧬 Assets come from the species

Every asset is generated procedurally from a seed. Built things (cities, spaceships, stations, colonies) are generated from the genes and body plan of the species that builds them. An insect species gets insect cities and insect ships (`docs/DESIGN.md`, round 12).

## 🏭 Live generators

The renderer draws what the live generators build. Every asset is generated on the fly and nothing is pre-built. See [R4.4 · Live generators](r04b-generators.md).

## 🔨 Built so far

| Part | How it works | Where |
|---|---|---|
| 💡 Billboards | Glowing or faceted points for stars, gas, rings, weather; they test depth but never write it | `src/render/Pipeline.cs`, `Draw.cs` |
| 🧱 Solid ground | Flat-shaded triangles lit by one star; three modes: solid, glowing air (adds light), see-through (clouds, water) | `src/render/MeshPipeline.cs` |
| 🕳️ Depth | A depth buffer, so the ground hides what is behind it | `src/render/Gpu.cs` |
| 🌍 Planets | A cube blown into a ball; each face splits into smaller pieces near the camera (up to 40 levels, about a micrometre). Pieces are made nearest first, a few milliseconds per frame. Layers: ground and sea floor, see-through sea, turning clouds, a glowing air shell | `src/gen/PlanetTerrain.cs` |
| 🪂 Fly-down | Orbit to the surface, then on down to micrometres; the camera always lands on finished ground; rain, snow, ash or dust under clouds; under water it turns dark and murky | `src/shell/PlanetView.cs` |
| 🪐 Resource fields | Volcanoes, rivers and 16 kinds of field placed by real geology, seen on the ground; R lights every field in its own colour | `src/gen/PlanetTerrain.cs` |
| 🏭 Dyson sphere | A shell around a star: industrial machinery outside (sectors, trenches, blocks, towers, pipes), a planet landscape turned inside out on the inside, held on by spin; free flight inside | `src/gen/PlanetTerrain.cs`, `src/gen/DysonGen.cs`, `src/shell/PlanetView.cs` |
| 🌌 The journey | One zoom from a universe of galaxies to a galaxy (zoom where the mouse points, click any star), a solar system (click a planet), a planet, its surface, then sea life at true scale or the soil fading to black, then microbes. Zooming out climbs back up | `src/shell/UniverseView.cs`, `DeepView.cs`, `PlanetView.cs`, `src/sim/SeaLife.cs`, `src/sim/MicrobeWorld.cs` |
| 🧪 Elements | All 118 as icons: bars, crystals, powders, drops and clouds by state at any temperature and pressure | `src/shell/ElementView.cs`, `src/gen/ElementGen.cs`, `IconFont.cs` |
| 🦠 Microbes | A live ecosystem: genes, mutation, selection, viruses by surface match | `src/sim/MicrobeWorld.cs`, `src/shell/MicrobeView.cs` |
| 🔍 Render scale | The game draws at 50, 75, 100, 150 or 200% of the window's pixels and the picture is stretched to fit; the mouse is scaled to match. Picked from the Render scale menu, or `--scale <percent>` on test runs | `src/platform/Window.cs`, `Input.cs`, `src/Program.cs` |
| 📷 Own pictures | `--shot <s> <file.bmp>` copies the finished frame off the graphics card; `--tour <name> <km,...>` takes one per height | `src/render/Gpu.cs`, `src/Program.cs` |

Everything is drawn relative to the camera in double precision, so nothing shakes from galaxy size down to micrometres.

Display safety: windowed only, vsync, Alt+Enter blocked, an in-game watchdog, a keep-or-revert pop-up on test runs, and timed test runs that are force-closed if they overrun.

## 🎨 Locked styles

| Subject | Style | Decided |
|---|---|---|
| Galaxies (all types) | Populous-like (tilted 3D, faceted) | Round 23 |
