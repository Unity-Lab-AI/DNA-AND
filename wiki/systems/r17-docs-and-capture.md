---
title: R17 · Docs + image capture
status: draft
sources: [docs/DESIGN.md (round 4), docs/ROADMAP.md#R17, docs/img/README.md]
last-verified: none (plan only)
---

# 📚 R17 · Docs + Image Capture

> "the todo needs made and all the docs and wikis and how to and setup and readmes" · "we will need to design the #D genrator and botting of them"

## 📖 Two wikis (decided: "both")
| Wiki | Folder | For |
|---|---|---|
| 🎮 Player wiki | `docs/wiki/` | Players, and the in-game wiki |
| 🛠️ Builder wiki | `wiki/` | Us |

## 🖼️ Image slots
No outside images are allowed, so every picture in the docs starts as a **slot**:

```
<!-- IMG: docs/img/<slug>.png | <what the image shows> -->
> 🖼️ *<what the image shows>* — rendered by the game's own 3D generator
```

## 🤖 The capture tool ("botting")
1. 🔍 Scans every `.md` file for `<!-- IMG: ... -->` markers.
2. 🎬 For each one, builds the scene described, using the game's own generator.
3. 📸 Renders it and saves the PNG at the given path.
4. ✏️ Swaps the placeholder line for the real image.

✅ The capture tool is a mode of the game exe itself, not a separate program.

Full rules: [docs/img/README.md](../../docs/img/README.md).
