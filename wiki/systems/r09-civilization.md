---
title: R9 · Civilization
status: draft
sources: [docs/DESIGN.md (rounds 1, 3, 4), docs/ROADMAP.md#R9]
last-verified: none (plan only)
---

# 🏛️ R9 · Civilization

> "intelligent life has technology and culture and war and tribalisze and pollitics and stuff like real life does" · "intelligent life has amazing ways of killing its self"

## 🧠 From animals to people
Intelligence is a gene-driven trait like any other. Once a species crosses into it, its groups start forming **tribes**.

## 🗺️ What a civilization runs
| System | Covers |
|---|---|
| 👥 Society | Tribes, then kingdoms, then nations |
| 🎭 Culture | Beliefs, customs, art, language |
| 🏛️ Politics | Governments, leaders, rebellions |
| ⚔️ War | Conflicts, weapons, conquest |
| 🔬 Technology | **100 real-history ages**, fine-grained |
| 💰 Trade | Economies and trade routes |
| ☠️ Self-destruction | The ways civilizations wipe themselves out |

## 👻 Souls and harvesting
> "the intelligences goal is to harvest life for its needs and since all life is like a virus advanced forms seprad and use up avasil nutrientrs and resources and have to spread"

Later in the game, intelligent life harvests life for you. In the endgame, it harvests life for its own needs. See [R11](r11-god-mode.md).

## 🙏 You only nudge
> "only every nudge evrything but u get the power of god to basicly do everything peopel attribute god doing."

## 🐺 Any species

Civilization is not only for apes. Any line that evolves intelligence climbs the 100 ages, steered by its species AI and in the end a galactic mind (`docs/DESIGN.md`, rounds 7–8). Species can team up with, tame, farm, rule or enslave each other. See `docs/wiki/groups-and-flocking.md`.

## 🩸 Dark Harvest

100 cruel harvest methods in 10 groups (madness and beasts, disease, war, politics and law, belief, labor and economy, environment, genes and bodies, machines and control, space). Each appears at a set age and is pushed by named powers, and each pays souls per death. The group × era matrix is in `docs/wiki/dark-harvest.md`. Methods can only appear in civilizations of that age or later.

## 📌 Decided specs (build from these)

- 100 ages (`docs/wiki/ages-of-civilization.md`), 100 ways civilizations end (same page).
- 100 tech branches in 10 families, and 100 harvest steps, one per age (`docs/wiki/tech-tree.md`).
- 100 kinds of society in 5 groups: rule, trade, family, belief, law (`docs/wiki/society.md`).
- 100 Dark Harvest methods, each gated by age (`docs/wiki/dark-harvest.md`).

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/cities.dnat`: 50 entries (Settlement sizes, Layouts, Districts, Infrastructure, in rough order of invention, Building materials by age) and 8 rules
- `content/life/intelligent.dnat`: 29 entries (Real thinking animals (seeds), Signs of a mind, What pushes a line toward intelligence) and 5 rules
- `content/resource-fields.dnat`: 26 entries (Made by magma, Made by hot fluids, Made by water and time, Made by weather, Fields in space) and 6 rules
