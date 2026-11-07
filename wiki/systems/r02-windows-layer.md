---
title: R2 · Windows layer
status: partial
sources: [docs/DESIGN.md (round 2), docs/ROADMAP.md#R2]
last-verified: fdaea69
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

## 🔨 Built so far

- The window and its message pump: `src/platform/Window.cs`.
- The keyboard and mouse: `Input.cs`. Ctrl + wheel counts each notch eight times.
- The menu bar of the sample viewer: Exit, Keep going or revert, Keepers, Legend. The universe's own: Exit, Start page, View (GPU use, Time, FPS and stats, the tour, the periodic table, screenshot, record a clip, clip size, drag speed, invert drags, render scale) and Help (Documents, Readme, How it works, Against the field, Credits, About) (`Window.SetWorldMenu`).
- The landing page (`Launcher.cs`), the settings file (`Settings.cs`), the document viewer (`DocViewer.cs`), the stats (`Stats.cs`).
- Windows pop-up boxes (`MessageBoxTimeoutW`).
- The safety watchdog: `Watchdog.cs`.
- Per-monitor DPI awareness, windowed only.
