---
title: R3 · Engine core
status: draft
sources: [docs/ROADMAP.md#R3, docs/TODO.md#R3]
last-verified: none (plan only)
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

## 💾 Save files
✅ Our own binary format made of versioned chunks (universe, planets, life, civs, player).

## 📌 Decided specs (build from these)

- Physics core from `content/physics.dnat`: four forces, particles and atoms, isotopes and decay, fusion and fission.
