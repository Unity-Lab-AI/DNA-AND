# 🗺️ DNA-AND Builder Wiki

How the game gets built, system by system, all from scratch. 🟡 = draft (plan only).

## 🧱 Foundations
| | Page | What it covers |
|---|---|---|
| 🟡 | [R1 · Toolchain](systems/r01-toolchain.md "Our own assembler + compiler, bootstrapped from raw bytes") | Building our own compiler from nothing |
| 🟡 | [R2 · Windows layer](systems/r02-windows-layer.md "Talking to Windows directly") | Window, input, sound, files, clock, threads, network |
| 🟡 | [R3 · Engine core](systems/r03-engine-core.md "Math, memory, giant arrays, threads, saves") | The base every system sits on |
| 🟡 | [R4 · Renderer](systems/r04-renderer.md "AAA 3D graphics from scratch") | Graphics, zoom from microbe to galaxy, the 3D image generator |

## 🌌 The world
| | Page | What it covers |
|---|---|---|
| 🟡 | [R5 · Universe](systems/r05-universe.md "Stars, planets, solar systems, galaxies") | Seeded galaxy generation |
| 🟡 | [R6 · Planet](systems/r06-planet.md "Earth + unlockable templates") | Ground, sea, air, weather, chemistry |

## 🧬 Life
| | Page | What it covers |
|---|---|---|
| 🟡 | [R7 · Genome](systems/r07-genome.md "Real-style DNA") | DNA, mutation, breeding, viruses |
| 🟡 | [R8 · Life sim](systems/r08-life-sim.md "Viruses to animals, grouped populations") | Ant-like creatures, populations, evolution |
| 🟡 | [R9 · Civilization](systems/r09-civilization.md "Tribes, culture, politics, war, tech") | Intelligent life |
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

## 📚 Related
- [Design record](../docs/DESIGN.md "The creator's decisions, verbatim") · [Roadmap](../docs/ROADMAP.md) · [To-do](../docs/TODO.md)
- [How to build](../docs/HOW-TO-BUILD.md) · [Setup](../docs/SETUP.md) · [Image slots](../docs/img/README.md)
- [Open suggestions (builder)](../docs/suggestions/D.md "Multiple-choice questions waiting on the creator")
- [Schema](CLAUDE.md) · [Log](log.md)
