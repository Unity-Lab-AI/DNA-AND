---
title: R8 · Life simulation
status: partial
sources: [docs/DESIGN.md (rounds 1, 3, 4), docs/ROADMAP.md#R8]
last-verified: 2cf573f
---

# 🦠 R8 · Life Simulation

> "they will be like ants ais with genes that use thatev eveold or been infected with u know EVOLUTION"

## 🐜 Ant-like creatures
Each creature follows simple rules set by its genes: eat, move, flee, breed, fight, rest. Complex behavior grows out of many simple creatures, the way it does in an ant colony.

## 🪱 "1 worm can be 4396 worms"
> "wher they are on the planet can be animate by what they are living in that area liek 1 worm can be 4396 worms and i suppose its possible to cover the planet win worms but unlickely with mutation and viruses and breeding if not asexual and such but that all needs worked out."

| Zoomed out | Zoomed in |
|---|---|
| 🪱 One **group** per area: a count (4,396), a shared genome, and how much the group varies | Individual worms, generated from the group's genome and variation |

When a group's genes drift far enough apart, it **splits into two groups**. That's how new species are born.
✅ One group per area cell and species. Individuals are only generated when you zoom in.

## 🔗 What runs every tick
| System | Effect |
|---|---|
| 🍽️ Food chains | Hunters, grazers, scavengers, decomposers |
| 🤒 Disease | Viruses and bacteria spread and mutate |
| 💀 Extinction | A group hits zero |
| 🌱 New species | A group splits |
| 🏁 Milestones | **100 stages of life**, each with its own milestones |

## 🗂️ What's simulated
Everything in the life lists: viruses, bacteria, archaea, fungi, single cells, plants, insects, fish, birds, mammals and more ([docs/LISTS.md](../../docs/LISTS.md)).

## 🧠 Layered mini AIs

From `docs/DESIGN.md`, round 8. Every living thing runs a mini AI driven by the selfish gene, and death always comes.

| Layer | Appears when | Notes |
|---|---|---|
| 🧪 Chemical | Always (brainless life) | Sense, then respond: move toward food (chemotaxis), quorum sensing, grow toward light, slime-mold pathfinding |
| 🐾 Individual | A nervous system evolves | Its own goals: eat, survive, mate |
| 🐺 Group | Social bonding genes evolve | Steers its members |
| 🐝 Hive | Caste and colony genes evolve | Steers its members |
| 🧬 Species | Intelligence evolves | Steers its groups |
| 🌌 Galactic mind | A galaxy-spanning civilization | Steers species and colonies |

Bigger layers steer smaller ones, and members can defect when that helps their own genes. Layers come only from evolution, with no fixed ladder steps. This also has to run fast enough for a huge population: grouped crowds share one group AI until you zoom in.

## 📌 Decided specs (build from these)

- 100 stages of life (`docs/wiki/stages-of-life.md`). Each stage unlocks exactly one power (or the 5 starters at stage 1).
- 100 milestones per stage, 10,000 in all, built from the top-100 lists, 10 per family × 10 families. Milestone 100 is the headline. Only headline milestones slow time and trigger autosave (`docs/wiki/milestones.md`).
- 100-step grouping ladder and 100 ways species mix (`docs/wiki/groups-and-flocking.md`). Tree of life with every rank tracked 100 deep (`docs/wiki/tree-of-life.md`).
- The full tree: 3 domains + 6 kingdoms + viruses, each kingdom down to species (`docs/wiki/tree-of-life.md`). Life content lives in `content/life/<kingdom>/`.

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/life/viruses.dnat`: 22 entries (Baltimore groups (how a virus's genes get read), Shapes and parts, Who they infect, Life cycles) and 6 rules
- `content/life/bacteria.dnat`: 31 entries (Shapes, Cell parts, How they get energy and food, Main groups (phyla), Group life, Swapping genes) and 5 rules
- `content/life/archaea.dnat`: 12 entries (What makes them different, Extremophiles, Main groups (phyla)) and 3 rules
- `content/life/protists.dnat`: 25 entries (Supergroups, Protist groups (seeds), How they move) and 4 rules
- `content/life/fungi.dnat`: 16 entries (Phyla, Body parts, Ways of living) and 5 rules
- `content/life/plants.dnat`: 36 entries (Groups (phyla and lines), Big splits, Organs and tissues, How they make food, How they breed) and 6 rules
- `content/life/animals.dnat`: 84 entries (Body-plan traits, Phyla, Arthropod classes, Insect orders (seeds), Fish classes, Land vertebrate classes, Bird orders (seeds), Mammal orders (seeds)) and 5 rules
- `content/life/extinct.dnat`: 47 entries (Mass extinctions (the Big Five, and after), The first strange animals, Early fish and the walk onto land, Dinosaurs, Other Mesozoic giants, After the dinosaurs: the age of mammals, Extinct plants) and 5 rules
- `content/life/intelligent.dnat`: 29 entries (Real thinking animals (seeds), Signs of a mind, What pushes a line toward intelligence) and 5 rules

## 🔨 Built so far

- **A live microbe ecosystem** (`src/sim/MicrobeWorld.cs`). Genomes set size, speed, feeding, poison, defence, coat, the temperature liked, shape and surface.
- **Breeding:** with mutation and rare big jumps, and families keep their colour.
- **Selection:** by light, food, temperature, predators and toxins.
- **Viruses:** they infect by surface match, burst the cell, and mutate.
- **Scale:** about 600 microbes and 400 viruses, each standing for a crowd of its kind.
- **One shared life simulation per planet** (`src/sim/PlanetLife.cs`):
  - 100 regions, each with up to 100 species groups. Lightning starts it (the first spark is free, then 4 Souls each).
  - Life climbs replicators > single cells > many-celled > nerves > brains > big-brained > communication. Each stage runs on its own clock, so that is about 3 hours of play.
  - Each stage competes for room only with its own kind.
- **What you see follows it:** microbes and sea animals follow the stage reached at each spot. On land, the region's plants, fungi and animals are drawn at true size from their own genes (`src/shell/LandLife.cs`), with an intelligent species' village where one lives.
