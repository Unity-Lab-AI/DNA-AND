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

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/physics.dnat`: 49 entries (The four forces of nature, Particles and atoms, Inside a planet, What moves inside a planet, Atmospheres) and 8 rules
- `content/elements.dnat`: 118 entries (Main) and 5 rules

## 🔨 Built so far

See `src/` and the TODO for what exists. Display safety: windowed only, vsync, Alt+Enter blocked, an in-game watchdog, and timed test runs that are force-closed if they overrun.
