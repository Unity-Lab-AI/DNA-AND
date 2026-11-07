---
title: R3 · Engine core
status: partial
sources: [docs/ROADMAP.md#R3, docs/TODO.md#R3]
last-verified: fdaea69
---

# ⚙️ R3 · Engine Core

> "the arrays"

The base every system sits on.

| Module | Job |
|---|---|
| ➗ Math | Vectors, matrices, noise, seeded random numbers |
| 🧠 Memory | Holding billions of things without slowdown |
| 🗃️ Arrays | Tightly packed tables of creatures, cells, stars |
| 🧵 Jobs | Spreading work across every CPU core |
| 💾 Saves | Writing and reading whole universes |

## 🎲 Same seed = same universe
Every random choice comes from the world seed, so one seed always rebuilds the same galaxy.
✅ Our own small seeded random generator, written from scratch.

## 🔭 Galaxy-size numbers
Distances run from a virus to across galaxies.
✅ 64-bit numbers with a "moving origin" that re-centres on the camera.

## 🧠 Memory and 🧵 threads
✅ Large pre-reserved memory pools per system.
✅ One worker per CPU core, pulling small jobs from a shared queue.
- Built: `src/core/Jobs.cs`. `Jobs.Run` queues one job; `Jobs.For` spreads a loop over every core and waits. Used by: a planet measuring its 100 regions, and the land building every newly seen species' body at once.

## 💾 Save files
✅ Our own binary format made of versioned chunks (universe, planets, life, civs, player).
- Built: `src/core/SaveFile.cs`. A file is `DNAS`, the file version, then chunks (four-letter name, chunk version, length, bytes); unknown chunks are skipped, and a save is written aside first so a crash never leaves half a file. Genomes are saved field by field by name, so new genes can be added without breaking old saves.
- First chunk: `LIFE`, a planet's whole life (clock, souls, every species and where it lives, the random generator's state). Test switches: `--savelife <file>` after `--evolve`, `--loadlife <file>`.

## 📌 Decided specs (build from these)

- Physics core from `content/physics.dnat`: four forces, particles and atoms, isotopes and decay, fusion and fission.

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/physics.dnat`: 49 entries (The four forces of nature, Particles and atoms, Inside a planet, What moves inside a planet, Atmospheres) and 8 rules
- `content/elements.dnat`: 118 entries (Main) and 5 rules

## 🔨 Built so far

- Maths: `src/core/MathCore.cs` has vectors, matrices, our own random numbers (SplitMix64 and xoshiro256**) and value noise.
- Content: `Content.cs` loads the `.dnat` files.
- Precision: positions are kept relative to whatever the camera is centred on, in double precision. The camera is built from its direction and then moved out, so it stays exact from galaxy size down to a micrometre.
- Decay before your eyes (DESIGN 583): `content/elements.dnat` half-life and decays; MatterView.SetClock (the universe's clock each frame), Daughter/DaughterOf (each atom's own moment by its hash and half-life), DecayFlash (the particle flying out), the cards' half-life lines.
