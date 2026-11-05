---
title: R2 · Windows layer
status: draft
sources: [docs/DESIGN.md (round 2), docs/ROADMAP.md#R2]
last-verified: none (plan only)
---

# 🪟 R2 · Windows Layer

> "windows only for now" · Using Windows itself and the GPU driver: "sure"

The only code we don't write is **Windows itself** and the **graphics card driver**. Everything else talks to them directly, with no outside libraries in between.

## 📦 What this layer provides

| Piece | Talks to | Used by |
|---|---|---|
| 🪟 Window | Windows windowing | Everything you see |
| ⌨️ 🖱️ Keyboard + mouse | Windows input | Controls (mouse + keys) |
| 🔊 Sound | Windows audio | Nature sounds, UI, music |
| 💾 Files | Windows file system | Saves, settings, wiki |
| ⏱️ Clock | Windows high-precision timer | [R12 Time](r12-time.md) |
| 🧵 Threads | Windows threads | [R3 job system](r03-engine-core.md) |
| 🌐 Network | Windows sockets | [R14](r14-multiplayer.md), [R15](r15-leaderboard-genebank.md) |

## 🔮 Later
Mac and Linux each get their own version of this layer. Everything above it stays the same.
