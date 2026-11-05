---
title: R4 · Renderer
status: draft
sources: [docs/DESIGN.md (round 1, round 4), docs/ROADMAP.md#R4]
last-verified: none (plan only)
---

# 🎨 R4 · Renderer

> "ut will be a AAA game graphics that can do all the microbes and viruses an d gene mapings and peices for every traight and skill and evolutionary thing like breathing air to walking upright to photosynethesiis" · "3d top down world with god mode space mechanics"

<!-- IMG: docs/img/renderer-zoom-ladder.png | One shot per zoom level: virus, cell, creature, landscape, planet, solar system, galaxy -->
> 🖼️ *One shot per zoom level: virus, cell, creature, landscape, planet, solar system, galaxy* — rendered by the game's own 3D generator

## 🧩 Parts
| Part | Draws |
|---|---|
| 🏔️ Terrain | Ground, rock, ice, made from planet data |
| 🌊 Water | Oceans, rivers, waves, tides |
| ☁️ Sky + weather | Air, clouds, storms, auroras |
| 💡 Light + shadow | Sun, stars, day and night |
| 🦠 Life | Bodies **built from their genes**, never hand-modelled |
| 🧬 Gene map | Which gene pieces make which trait |
| 🌌 Space | Stars, planets, nebulas, Dyson spheres |

## 🔎 One smooth zoom
Scroll from a single microbe up to the whole galaxy with no loading screens. Each zoom band draws the right level of detail.

## 🖥️ Talking to the graphics card
✅ Direct3D 12, which is part of Windows itself, so it stays inside rule 1.

## 🖼️ The 3D image generator
> "we will need to design the #D genrator and botting of them"

All icons and art come from the game's own generator: creatures from genomes, planets from seeds, icons from the same renderer. See [R17](r17-docs-and-capture.md).

## 🎨 Style samples come first

No final art style is locked in until the creator compares samples (`docs/DESIGN.md`, round 6).

| | |
|---|---|
| 🖼️ **What** | 3 examples of each subject, each drawn in 3 styles: 9 pictures per subject |
| 🎨 **Styles** | RimWorld-like (flat, top-down), Populous-like (low-poly 3D god view), Stardew-like (pixel art), all original |
| 🪶 **Rule** | Light on the graphics card. Thousands of living things move and spread at once |
| ⏱️ **When** | Right after the basic renderer works |

Subjects: microbes, viruses, intelligent creatures, a handful of other living things, galaxy and solar system layouts, planets, stars, gas giants, Dyson spheres, spaceships, space colonies, space stations, resource fields, and all 118 elements.
