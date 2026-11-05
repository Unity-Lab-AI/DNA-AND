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
💬 *Suggestion:* our own small seeded random generator, written from scratch. → [S7](../../docs/suggestions/D.md#s7--random-numbers)

## 🔭 Galaxy-size numbers
Distances run from a virus to across galaxies.
💬 *Suggestion:* 64-bit numbers with a "moving origin" that re-centres on the camera. → [S6](../../docs/suggestions/D.md#s6--number-precision)

## 🧠 Memory and 🧵 threads
💬 *Suggestion:* large pre-reserved memory pools per system. → [S4](../../docs/suggestions/D.md#s4--memory)
💬 *Suggestion:* one worker per CPU core, pulling small jobs from a shared queue. → [S5](../../docs/suggestions/D.md#s5--threading)

## 💾 Save files
💬 *Suggestion:* our own binary format made of versioned chunks (universe, planets, life, civs, player). → [S11](../../docs/suggestions/D.md#s11--save-format)
