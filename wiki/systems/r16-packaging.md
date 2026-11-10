---
title: R16 · Packaging
status: partial
sources: [docs/DESIGN.md (rounds 1, 2, 4), docs/ROADMAP.md#R16, installer/Setup.cs, release.cmd, universe-sync.cmd, linux/run.sh]
last-verified: 700c705
---

# 📦 R16 · Packaging

> "once we build the exe like the past project like gladiator" · "should be a release page on both with link to it in the readme first thing"

## 🎁 What ships
| File | What it is |
|---|---|
| `DNA-AND-Setup.exe` | The installer, built with our own toolchain |
| `DNA-AND.exe` | The game itself |

✅ Our own setup exe, like Gladiators' `Setup.exe` but with no outside installer tools.

## 🚀 Release pages (decided)
Every release goes up on **both**:
- 🏠 <https://git.unityailab.com/GFourteen/DNA-AND/releases>
- 🐙 <https://github.com/Unity-Lab-AI/DNA-AND/releases>

## 🔨 Built so far

- `installer/Setup.cs`: our own installer wizard, the payload inside itself (Programs folder, Start menu and desktop shortcuts, the Apps list entry, an uninstaller; over an earlier copy it offers to delete its clips, screenshots and settings, each on its own). `installer/MakeIcons.cs` draws the icons.
- `release.cmd`: the preview build, `release/DNA-AND-Setup-0.1.4.exe` and `release/DNA-AND-0.1.4-windows.zip`; `universe-sync.cmd` and `Universe/release.cmd` make the Universe program's own installer and zip the same way; the sync also carries `reference/` (the Uni Vs Matics equations and builders the `.c97` clip is ported from, DESIGN 672).
- Released: 0.1.0, then 0.1.1 (the super-review's sets, 2026-11-10), then 0.1.2 (render by scale, 2026-11-10), then 0.1.3 (the ground held still while time runs fast, 2026-11-10), then 0.1.4 (natural ice edges, 2026-11-10), of both on both release pages (git.unityailab.com and GitHub).
- Linux today: through Wine, with `linux/run.sh` in the zip (`docs/release/UNIVERSE-LINUX.md`).

## 🔮 Later
Native Mac and Linux builds (Vulkan, their own windowing, a C# runtime there), once [R2](r02-windows-layer.md) has their layers.
