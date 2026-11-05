---
title: R14 · Multiplayer
status: draft
sources: [docs/DESIGN.md (rounds 1, 2), docs/ROADMAP.md#R14]
last-verified: none (plan only)
---

# 👥 R14 · Multiplayer

> "single/coop/multi player see each others cursors all working to one goal." · Host on one player's computer: "yes"

## 🧭 Modes
| Mode | Players |
|---|---|
| 🧍 Single | One god |
| 🤝 Co-op / multi | Several gods in one shared universe, working toward one goal |

## 🖥️ How it connects
- **One player's computer hosts** and runs the simulation.
- The others send their actions (powers, camera) and receive what changed.
- Everyone sees everyone else's **cursor**.

✅ Our own reliable messaging over UDP, built on Windows sockets. The host shares a join code; up to 8 players.
