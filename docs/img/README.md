# 🖼️ Doc Images: Slots and the Capture Tool

> "we will need to design the #D genrator and botting of them"

DNA-AND uses **no outside images**. Every picture in the docs is rendered by the game's own 3D generator. Until the game exists, each picture is a **slot**.

## ✍️ Writing a slot

Put two lines where the image goes:

```
<!-- IMG: docs/img/<slug>.png | <what the image shows> -->
> 🖼️ *<what the image shows>* — rendered by the game's own 3D generator
```

| Part | Rule |
|---|---|
| `docs/img/<slug>.png` | Where the image will be saved. Always under `docs/img/` |
| `<what the image shows>` | A plain description of the scene. The capture tool builds its scene from it |
| Second line | What readers see until the real image arrives |

## 🏷️ Naming rules

| Rule | Example |
|---|---|
| Lowercase, words joined by `-` | `first-cells-shallow-sea.png` |
| Start with the area of the game | `power-`, `stage-`, `age-`, `planet-`, `space-`, `ui-`, `icon-` |
| Icons go in `docs/img/icons/` | `docs/img/icons/power-fire.png` |
| One slug = one image | Reuse the slug to reuse the image |
| PNG only | `.png` |

## 🤖 How the capture tool fills slots

1. 🔍 **Scan:** finds every `<!-- IMG: ... -->` marker in every `.md` file.
2. 🎬 **Stage:** builds the scene with the game's own engine (seed, camera, zoom, time).
3. 📸 **Render:** saves the PNG to the slug path.
4. ✏️ **Swap:** replaces the `> 🖼️` placeholder line with the real image. The `<!-- IMG -->` marker stays, so the tool can re-render it later.

After the swap, a slot looks like this:

```
<!-- IMG: docs/img/<slug>.png | <what the image shows> -->
![<what the image shows>](<relative path to the png> "<what the image shows>")
```

✅ The capture tool is a mode of the game exe itself.

## 📋 Slots so far

Run the capture tool's scan to list every slot. Until then, search the repo for `IMG:`.

## 🎨 Style comes first

Doc images can only be rendered after the creator picks one of the 3 sample styles (RimWorld-like, Populous-like or Stardew-like). See `docs/DESIGN.md`, round 6.
