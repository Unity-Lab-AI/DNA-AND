---
title: R11 · God mode + Souls
status: partial
sources: [docs/DESIGN.md (rounds 1-4), docs/POWERS.md, docs/ROADMAP.md#R11]
last-verified: fdaea69
---

# ⚡ R11 · God Mode + Souls

> "think populous form back in the day but gneological twists" · "only every nudge evrything but u get the power of god to basicly do everything peopel attribute god doing."

## 👻 Souls, the currency
| Source | How it works |
|---|---|
| 🌱 Living things | Slowly give off Souls. More life means more Souls |
| 🫳 Harvesting by hand | Fast, but it kills the life there. "its slow and freaks out intelligent life if seen" |
| 🌋 Disasters | Mass death gives lots of Souls: "a volcano kicking off a city will make lots of souls" |
| 🏛️ Intelligent life | Later on, it harvests for you. In the endgame, it harvests for its own needs |

## ⚡ The 100 powers
All 100 powers are listed in [docs/POWERS.md](../../docs/POWERS.md "Each with uses, pluses and minuses"), in 8 groups: weather, land and sea, air and chemistry, life, genes, civilization, space, and management.

### 🎬 Day-one powers
> "option one but things like add genes add elements ect ect real life so full perioditic table of elements"

Lightning, heat, cold, water and spark of life, plus **add genes** and **add any of the 118 elements**.

## 💸 Cost and unlocks
- Powers cost **Souls**: tier (T1–T100, every power ranked weakest to strongest) × area × strength.
- T81–T100 have cooldowns. Lower tiers don't.
- 100 unlock points = the 100 stages of life. Every power unlocks at exactly one stage, and the 5 starters at stage 1.

## 🚫 No direct editing
> "no direct edit besids using powers to influence things to happen"

Gene powers have "differnt outcomes good and bad depending."

## 🩸 Dark Harvest

100 cruel harvest methods in 10 groups (madness and beasts, disease, war, politics and law, belief, labor and economy, environment, genes and bodies, machines and control, space). Each appears at a set age and is pushed by named powers, and each pays souls per death. The group × era matrix is in `docs/wiki/dark-harvest.md`. Methods can only appear in civilizations of that age or later.

## 📌 Decided specs (build from these)

- 100 powers. Each has one unlock stage (1–100) and one tier (T1–T100). The table is in `docs/wiki/god-powers.md`. T81–T100 have cooldowns. Cost = tier × area × strength. #4 is Starshine and #81 is Stellar Flare.
- 100 nudges, each mapped to the powers that do it (`docs/wiki/nudges.md`).
- 100 soul sources (`docs/wiki/souls-and-harvesting.md`). Milestones pay no souls.

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/physics.dnat`: 49 entries (The four forces of nature, Particles and atoms, Inside a planet, What moves inside a planet, Atmospheres) and 8 rules
- `content/elements.dnat`: 118 entries (Main) and 5 rules
- `content/life/extinct.dnat`: 47 entries (Mass extinctions (the Big Five, and after), The first strange animals, Early fish and the walk onto land, Dinosaurs, Other Mesozoic giants, After the dinosaurs: the age of mammals, Extinct plants) and 5 rules

## 🔨 Decided so far

- Lightning, and Spark of Life, can start life again in more places on one planet. Each use costs Souls, like every power. Each start is its own family tree.
- Resources are for the species. The god has no use for them.
