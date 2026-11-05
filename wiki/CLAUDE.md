# 🧭 Builder Wiki — Schema & Rules

This is the **builder wiki** for DNA-AND: the map of how the game is built. The player wiki lives in `docs/wiki/`. This one is for us.

> The wiki is the map, the code is the territory. When they disagree, trust the code and fix the page.

## 📄 Page frontmatter (required on every page)

```yaml
---
title: <page title>
status: draft | verified | stale
sources: [<paths or docs this page is based on>]
last-verified: <git commit hash, or "none (plan only)">
---
```

| Status | Meaning |
|---|---|
| 🟡 `draft` | Describes the **plan**. No code exists yet, or the code isn't checked against this page |
| 🟢 `verified` | Checked line by line against the code at `last-verified` |
| 🔴 `stale` | Code changed since `last-verified`. Don't trust the page until it's re-checked |

Right now **every page is `draft`** and `last-verified: none (plan only)`. No game code exists yet.

## ✍️ Writing rules

- **Rule 1, no dependencies.** Pages never describe using outside code, libraries, tools, compilers or data. The only outside things are Windows itself, the GPU driver, and jsonbin (behind the side car).
- **Rule 3, no making things up.** Anything not decided in `docs/DESIGN.md` is marked `💬 *Suggestion*` and logged as a multiple-choice question in `docs/suggestions/`.
- Short sections, tables, emoji icons. No text walls.
- Quote the creator's words verbatim when a page rests on them.

## 🔁 Upkeep

After any significant change:
1. Update every page the change touches (content + `last-verified`).
2. Keep `wiki/index.md` in sync.
3. Append an entry to `wiki/log.md`.
4. If a page can't be updated properly, set `status: stale`. Never leave it silently wrong.
