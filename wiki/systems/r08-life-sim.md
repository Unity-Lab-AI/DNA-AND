---
title: R8 · Life simulation
status: draft
sources: [docs/DESIGN.md (rounds 1, 3, 4), docs/ROADMAP.md#R8]
last-verified: none (plan only)
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
