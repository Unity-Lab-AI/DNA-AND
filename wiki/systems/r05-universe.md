---
title: R5 · Universe
status: draft
sources: [docs/DESIGN.md (rounds 1-2), docs/ROADMAP.md#R5]
last-verified: none (plan only)
---

# 🌌 R5 · Universe

> "stars+planets>solar sytems> galaxys" · "obviously galaxy size to buidl dyson spheres and ingalactic war"

## 🪆 The nesting
| Level | Holds | Built from |
|---|---|---|
| 🌌 Universe | Many galaxies | World seed |
| 🌀 Galaxy | Solar systems | Galaxy seed (from the world seed) |
| ☀️ Solar system | Stars, planets, moons, belts | System seed |
| 🪐 Planet | A full world | Planet seed + template ([R6](r06-planet.md)) |

## ⚡ Only build what's being looked at
A full galaxy is far too big to hold in memory at once. Each level is **generated from its seed when it's needed**, and saved only once something there has changed (life, colonies, god powers).

## 🎯 Where you start
You start on an Earth-template planet in a random solar system. Life reaches other planets only after intelligent life gets there first ([R10](r10-space.md)).

## 🔁 Restart means a new universe
> "open ended till u resarts to get a new world and galaxy"
