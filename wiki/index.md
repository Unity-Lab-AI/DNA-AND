# 🗺️ DNA-AND Builder Wiki

How the game gets built, system by system, all from scratch. 🟡 = draft (plan only).

## 🧱 Foundations
| | Page | What it covers |
|---|---|---|
| 🟡 | [R1 · Toolchain](systems/r01-toolchain.md "Our own assembler + compiler, bootstrapped from raw bytes") | Building our own compiler from nothing |
| 🟡 | [R1 · Seed spec](systems/r01a-seed-spec.md "Exact spec for the hand-typed hex loader") | Step 0: what the seed does, the hex format, its exe layout, how it's proven |
| 🟡 | [R2 · Windows layer](systems/r02-windows-layer.md "Talking to Windows directly") | Window, input, sound, files, clock, threads, network |
| 🟡 | [R3 · Engine core](systems/r03-engine-core.md "Math, memory, giant arrays, threads, saves") | The base every system sits on |
| 🟡 | [R4 · Renderer](systems/r04-renderer.md "AAA 3D graphics from scratch") | Graphics; the one-world camera (galaxies to atoms, no handoffs), the CDF 9/7 coefficient waveform and mode-E, reversed float depth, background ground building, the preview menu |
| 🟡 | [R4.4 · Live generators](systems/r04b-generators.md "Every asset built on the fly, nothing pre-built") | Creature, city, space tech and 15 more generators (18 in all) |
| 🟡 | [R4.5 · Style samples](systems/r04-renderer.md#-style-samples-come-first "The style is locked; sets 1-8 approved") | Locked: Populous-like for everything. Sets 1–8 approved (galaxies to microbes, elements, Dyson sphere), the journey built |

## 🌌 The world
| | Page | What it covers |
|---|---|---|
| 🟡 | [R5 · Universe](systems/r05-universe.md "Stars, planets, solar systems, galaxies") | Built in part: universe of galaxies, galaxy, any star, its solar system, the Dyson sphere |
| 🟡 | [R6 · Planet](systems/r06-planet.md "Earth + unlockable templates") | Built in part: ground to micrometres, sea, air, weather, resource fields, sea life at true scale; the living planet on its own clock (inside, plates, quakes, eruptions, air and climate, water and weather, erosion, impacts) and its ground changing in whole-planet swaps |

## 🧬 Life
| | Page | What it covers |
|---|---|---|
| 🟡 | [R7 · Genome](systems/r07-genome.md "Real-style DNA") | DNA, mutation, breeding, viruses |
| 🟡 | [R8 · Life sim](systems/r08-life-sim.md "Viruses to animals, grouped populations") | Layered selfish-gene mini AIs, 100-step grouping ladder, tree of life, populations, evolution; food web per region; the real-life checklist of what is still missing |
| 🟡 | [R9 · Civilization](systems/r09-civilization.md "Tribes, culture, politics, war, tech") | Intelligent life of any species, 100 ages, tech tree |
| 🟡 | [R10 · Space](systems/r10-space.md "Travel, colonies, Dyson spheres") | Spreading across galaxies |

## 🎮 The game
| | Page | What it covers |
|---|---|---|
| 🟡 | [R11 · God mode + Souls](systems/r11-god-mode.md "100 powers paid for in Souls") | Powers and the Souls economy |
| 🟡 | [R12 · Time](systems/r12-time.md "Pause + 5 speeds, milestone slow-downs") | Clock and pacing |
| 🟡 | [R13 · Game shell](systems/r13-game-shell.md "Menus, wiki, settings, saves") | Everything around the world |
| 🟡 | [R14 · Multiplayer](systems/r14-multiplayer.md "Host-based, shared cursors") | Single, co-op, multi |
| 🟡 | [R15 · Leaderboard + Genebank](systems/r15-leaderboard-genebank.md "jsonbin behind a swappable side car") | Scores and shared genomes |
| 🟡 | [R16 · Packaging](systems/r16-packaging.md "The Windows exe and installer") | Shipping |
| 🟡 | [R17 · Docs + image capture](systems/r17-docs-and-capture.md "Docs, wikis, auto-filled image slots") | Docs and the image "botting" tool |

## 📦 Content
The researched content every system is built from, in `content/` (`docs/CONTENT-FORMAT.md`). Each system page lists its own content files under "Content (build from these)".

## 📚 Related
- ⚖️ [Work Laws](../docs/WORK-LAWS.md "The docs are the spec for all code"): read before writing any code
- [Design record](../docs/DESIGN.md "The creator's decisions, verbatim") · [Roadmap](../docs/ROADMAP.md) · [To-do](../docs/TODO.md)
- Player wiki: [home](../docs/wiki/README.md) · [Groups & Flocking](../docs/wiki/groups-and-flocking.md) · [Tree of Life](../docs/wiki/tree-of-life.md)
- [How to build](../docs/HOW-TO-BUILD.md) · [Setup](../docs/SETUP.md) · [Image slots](../docs/img/README.md)
- [Open suggestions (builder)](../docs/suggestions/D.md "Multiple-choice questions waiting on the creator")
- [Schema](CLAUDE.md) · [Log](log.md)
