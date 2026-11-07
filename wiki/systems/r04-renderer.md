---
title: R4 · Renderer
status: verified
sources: [docs/DESIGN.md (round 1, round 4, items 62-76), docs/ROADMAP.md#R4, src/render/, src/gen/PlanetTerrain.cs, src/shell/PlanetView.cs, src/shell/MatterView.cs, content/minerals.dnat, content/terrain.dnat]
last-verified: 78eaf06
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
| 💡 Billboards | Glowing or faceted points for stars, gas, rings, weather; they test depth but never write it; each draw can set a smallest and a largest size on screen (galaxy points never cover more than about 0.7 degrees) | `src/render/Pipeline.cs`, `Draw.cs` |
| 🧱 Solid ground | Triangles lit by one star, flat-shaded or smooth (a vertex can carry its own packed surface direction, unpacked per corner); three modes: solid, glowing air (adds light), see-through (clouds, water) | `src/render/MeshPipeline.cs` |
| 🕳️ Depth | A float depth buffer used reversed (near at 1, far at 0, nearer passes as greater), so precision holds at every distance, from micrometres in front of you to across a galaxy | `src/render/Gpu.cs` |
| 🌍 Planets | A cube blown into a ball; each face splits into smaller pieces near the camera (up to 40 levels, about a micrometre). Pieces are made nearest first, a few milliseconds per frame. Layers: ground and sea floor, see-through sea, turning clouds, a glowing air shell | `src/gen/PlanetTerrain.cs` |
| 🪂 Fly-down | Orbit to the surface, then on down to micrometres; the camera always lands on finished ground; rain, snow, ash or dust under clouds; under water it turns dark and murky | `src/shell/PlanetView.cs` |
| 🪐 Resource fields | Volcanoes, rivers and 16 kinds of field placed by real geology, seen on the ground; R lights every field in its own colour | `src/gen/PlanetTerrain.cs` |
| 🏭 Dyson sphere | A shell around a star: industrial machinery outside (sectors, trenches, blocks, towers, pipes), a planet landscape turned inside out on the inside, held on by spin; free flight inside | `src/gen/PlanetTerrain.cs`, `src/gen/DysonGen.cs`, `src/shell/PlanetView.cs` |
| ⚛️ Into matter | Below the finest ground, one zoom with no switches: the planet's own grains, one field for the whole world (`src/gen/GrainField.cs`: each grain fixed on the ground, its size, depth, mineral from the ground's mix and shape from the seed); the ground's finest detail is the top of these grains, and the matter view goes into the very same ones, on the land's slope; the grain you are on keeps its whole 3D body, its fine face lying over it round you in round rings (smooth shaded, its atomic terraces fading in below about 600 nm), the things on it at real sizes (silt specks, six-sided clay plates, iron-oxide skins, nanoparticles, the water film, all `@surface` entries), then from about 10 nm the mineral's real crystal filling the view: every mineral's published unit cell and sites (Crystallography Open Database), tiled by one engine, shared sites filled at their real shares, bonds as sticks by each element's covalent size; then each atom's electrons, nucleus, nucleons and quarks. An atom is never a ball: at every size it is its nucleus and its electrons' orbitals, each electron on its own path tilted its own way in 3D; inside it, down to the nucleus, the innermost electrons' cloud shimmers by its real density. Close to the ground the card works out the sand grain by grain per pixel, by the same 32-bit hash as the grain field. Every grain, thing and atom is clickable with its card. Lustre and clarity per mineral drive the shading (glassy glint, metal glint, pearly sheen, light through clear grains) | `src/shell/MatterView.cs`, `src/gen/GrainField.cs`, `src/render/MeshPipeline.cs`, `content/minerals.dnat`, `content/elements.dnat` |
| 🎬 Tours and the whole universe | About 100 billion galaxies in a visible universe 13.4 billion light-years across (`content/universe.dnat`), made by regions round you along a cosmic web of walls, filaments, clusters and voids, the rest a glow (each of its points a galaxy you can click and fly to); an edge you cannot pass; zooming out grows without end to the whole universe. Detail at every size from `Waveform` (stored CDF 9/7 octaves, mode-E past them). Electrons drawn as orbital clouds in their real shapes; gluon flux tubes and virtual quark pairs in nucleons; stars dive into plasma; the air over every ground; microbes on living worlds. Tours: `--tour name` flies `content/tours/name.dnat` with the real controls | `src/shell/WorldView.cs`, `src/core/Waveform.cs`, `src/shell/MatterView.cs`, `content/universe.dnat`, `content/tours/` |
| ❓ Preview menu | The one world's menu bar: Exit; View (scale on screen, render scale); Help (Readme, Credits, About), its words in one place | `src/platform/Window.cs`, `src/shell/PreviewText.cs` |
| 🌐 One world | `--world`: one camera through every scale, no views to switch. Universe, galaxies and star systems so far (the camera keeps its place relative to an anchor, a galaxy's centre or the star of the system it is in, so it stays exact; a galaxy is one continuous field of its own stars (its far glow's stars come first, the rest are added in the background, more are drawn as it grows on screen, each smaller), and fills in with its real stars at true density around you; the wheel homes on the star or planet you point at; the system's star shows its real body): galaxies placed by size, tilt and turn; the nearest one fades from its far glow into its own stars; the wheel flies along the mouse's line by a share of the distance to the nearest thing (a grid finds the nearest star). Star systems, planets and the ground join next, then it replaces the journey's separate views | `src/shell/WorldView.cs` |
| 🌌 The journey | One zoom from a universe of galaxies to a galaxy (zoom where the mouse points, click any star), a solar system (click a planet), a planet, its surface, then sea life at true scale or the soil fading to black, then microbes. Zooming out climbs back up | `src/shell/UniverseView.cs`, `DeepView.cs`, `PlanetView.cs`, `src/sim/SeaLife.cs`, `src/sim/MicrobeWorld.cs` |
| 🧪 Elements | All 118 as icons: bars, crystals, powders, drops and clouds by state at any temperature and pressure | `src/shell/ElementView.cs`, `src/gen/ElementGen.cs`, `IconFont.cs` |
| 🦠 Microbes | A live ecosystem: genes, mutation, selection, viruses by surface match | `src/sim/MicrobeWorld.cs`, `src/shell/MicrobeView.cs` |
| 🎞️ Clips | `--clip <start s> <seconds> <file.gif>` saves every fourth frame into a looping GIF, written by our own encoder (a fixed 252-colour palette, LZW packing); `--wheel` with `--wheelevery <n>` zooms in gently while it records, `--aimcentre` aims it at the middle, `--journeyat <s>` flies to a star's temperate world; the journey clip is `docs/img/journey.gif` | `src/render/GifWriter.cs`, `Gpu.cs`, `src/Program.cs` |
| 🎮 Any computer's card | At start the game goes through every graphics card and takes the one with the most video memory of its own (software ones skipped), so two-GPU laptops use the real one; the run log names it | `src/render/Gpu.cs` |
| 🧵 Background ground | Missing ground pieces are asked for while the planet is walked, nearest first, and built by background workers (one per core); each frame takes in what is finished (about 4 ms of uploads) and never waits | `src/gen/PlanetTerrain.cs` |
| 📐 Resolution-true ground | Ground pieces split by how big they look in pixels (tuned at 600 pixels tall), so a facet stays the same size on screen at any resolution | `src/shell/PlanetView.cs` |
| 🖥️ Native checks | `--native` opens a normal window the size of the screen (no display change, never takes the keyboard) so pictures are checked at the player's real resolution | `src/platform/Window.cs`, `src/Program.cs` |
| 🌊 Coefficient waveforms | The lab's CDF 9/7 wavelet engine (from Uni Vs Matics): a grid becomes sparse wavelet terms kept to an energy target, packed (dv1) and rebuilt exactly or at any level of detail; matches the reference engine to 3e-17. To be used for every field from galaxies to microbes (TODO R4.6) | `src/core/Wavelet97.cs` |
| ♾️ Mode-E | Endless self-similar detail past a field's finest stored level: Newton on z³ − K, a 3D cubic continuation, √10-step cellular octaves, basin colours; sampled per point at any zoom; identical to the lab's fractalize.ts | `src/core/ModeE.cs` |
| 🔍 Render scale | The game draws at 50, 75, 100, 150 or 200% of the window's pixels and the picture is stretched to fit; the mouse is scaled to match. Picked from the Render scale menu, or `--scale <percent>` on test runs | `src/platform/Window.cs`, `Input.cs`, `src/Program.cs` |
| 📷 Own pictures | `--shot <s> <file.bmp>` copies the finished frame off the graphics card; `--tour <name> <km,...>` takes one per height | `src/render/Gpu.cs`, `src/Program.cs` |

Everything is drawn relative to the camera in double precision, so nothing shakes from galaxy size down to micrometres.

Display safety: windowed only, vsync, Alt+Enter blocked, an in-game watchdog, a keep-or-revert pop-up on test runs, and timed test runs that are force-closed if they overrun.

## 🎨 Locked styles

| Subject | Style | Decided |
|---|---|---|
| Galaxies (all types) | Populous-like (tilted 3D, faceted) | Round 23 |
