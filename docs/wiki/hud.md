# 🖥️ The HUD

[🏠 Wiki home](README.md) · [📖 Glossary](glossary.md) · [❓ FAQ](faq.md)

> *"it should be intrusive so the game window is as big as possible and everthing is click to open and click hold drag to move"*
>
> (Read as **non-intrusive**, since the goal is the biggest possible game window.)

<!-- IMG: docs/img/hud-overview.png | The full game screen: a living planet filling the window, a slim header bar on top, an action bar at the bottom, one panel open and being dragged -->
> 🖼️ *The full game screen: the world fills the window, with a slim header on top and the action bar below* — rendered by the game's own 3D generator

**The world is the screen.** The HUD stays thin and out of the way. Everything else is a panel you open with a click and move wherever you like.

---

## 📐 The layout

```
┌──────────────────────────────────────────────────────────────────┐
│ ⚙️ 📖 │ 👻 Souls │ 🌱 Life │ 🩸 Harvested │ ⏸ ▶ ▶▶ … │ 📅 Date │  ← header
│                                                                  │
│                                                                  │
│                    🌍  THE WORLD (as big as possible)            │
│                                                                  │
│          ┌───────────────┐                                       │
│          │ ⚗️ Element tree │  ← panels: click to open,           │
│          └───────────────┘     click-hold-drag to move           │
│                                                                  │
│       [1][2][3][4][5][6][7][8][9][0][-][=]   🖌️ brush size       │  ← action bar
└──────────────────────────────────────────────────────────────────┘
```

---

## 🔝 The header bar

One slim strip along the top.

| | Item | What it shows / does |
|:-:|---|---|
| ⚙️ | **Settings** | Click to open Settings |
| 📖 | **Wiki** | Click to open the in-game wiki (or press F1) |
| 👻 | **Souls** | Your currency, live |
| 🌱 | **Life** | The amount of life in your galaxy |
| 🩸 | **Harvested** | The amount of life harvested |
| 🏆 | **Score** | Life + harvested, your leaderboard score |
| ⏯️ | **Time** | Pause and the 5 speeds (1×, 10×, 100×, 1,000×, max) |
| 📅 | **Date** | How far your world has come, in years |

> 🔔 Little [milestone](milestones.md "100 per stage") notices slide in from the header and fade away without stopping time.

---

## 🎛️ The action bar: build your own

A WoW-style action bar along the bottom.

| | |
|--|--|
| 🧩 **Build it yourself** | Drag any unlocked [power](god-powers.md "All 100 powers"), tool or brush onto a slot |
| ⌨️ **Hotkeys** | Each slot has a key, and every key can be rebound |
| ➕ **More bars** | Add extra bars, and place them anywhere |
| ⏳ **Cooldowns** | Slots show a sweep while a T81–T100 power recharges |
| 💸 **Cost** | Hover a slot to see its soul cost at your current brush size |

---

## 🪟 Panels: click to open, click-hold-drag to move

Every panel works the same way:

| Do this | It does |
|---|---|
| 🖱️ **Click** its button | Opens the panel |
| ✋ **Click, hold, drag** its title | Moves it anywhere |
| ❌ **Click** the close mark | Closes it |
| 📌 | The game remembers where you left each panel |

**Panels:** element tree, powers, nudges, tree of life, species, souls and harvest, civilizations, milestones, Genebank and leaderboard, multiplayer, wiki, settings.

---

## ⚗️ The element tree

The full periodic table as a panel. Pick any of the [118 elements](elements.md "The full periodic table") and paint it into the world. Lab-made elements (93+) appear once a civilization first creates them.

---

## 🖌️ Painter tools

Shape the world like a painter. Pick a tool, pick a brush, and paint.

| Tool | Paints |
|---|---|
| ⚗️ **Element brush** | Any element from the element tree |
| 💧 **Water brush** | Water, from a puddle to a sea |
| 🔥 **Heat brush** | Warmth (Fire) |
| ❄️ **Cold brush** | Cold |
| 🧬 **Gene brush** | Add genes to the life you paint over |
| ⚡ **Power brush** | Any power on your action bar, over an area |
| 🩸 **Harvest brush** | Reap souls from what you paint over |

### 📏 Brush sizes grow as life spreads

| | |
|--|--|
| 🔓 **Bigger brushes unlock** | As time goes on and life spreads through your galaxy |
| 🌍 **Bigger reach** | Harvesting and powers cover bigger areas with bigger brushes, from one pond to a continent to a whole planet |
| 💸 **Bigger cost** | Cost = tier × area × strength, so a bigger brush costs more souls |
| 🎚️ **Strength** | A second slider sets how strong each stroke is |

---

## 🔗 See also

[Controls](controls.md "Mouse, keys, and zooming from microbe to galaxy") · [Menus & Settings](menus-and-settings.md "Main menu, saving and settings") · [God Powers](god-powers.md "All 100 powers, pluses and minuses") · [Elements](elements.md "All 118 elements and what they do") · [Souls & Harvesting](souls-and-harvesting.md "The currency of a god") · [🏠 Wiki home](README.md)
