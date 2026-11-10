# 🧬 DNA-AND — Setup Guide

> 🔞 **DNA-AND is an 18+ game.** It includes sex, killing and murder.

Get from download to your first living cell in a few minutes.

<!-- IMG: docs/img/setup-banner.png | A young blue planet with the first glowing cells in a shallow sea -->
> 🖼️ *A young blue planet with the first glowing cells in a shallow sea* — rendered by the game's own 3D generator

---

## ⬇️ 1. Download

Get the latest `DNA-AND-Setup.exe` from either release page. They're the same file.

| | Release page |
|---|---|
| 🏠 | **[git.unityailab.com/GFourteen/DNA-AND/releases](https://git.unityailab.com/GFourteen/DNA-AND/releases "Unity AI Lab's own git server")** |
| 🐙 | **[github.com/Unity-Lab-AI/DNA-AND/releases](https://github.com/Unity-Lab-AI/DNA-AND/releases "GitHub mirror")** |

## 🖥️ 2. Check your PC

| | Minimum | Recommended |
|---|---|---|
| 🪟 System | Windows 10 / 11, 64-bit | Windows 11, 64-bit |
| 🧠 CPU | 4 cores | 8+ cores |
| 💾 RAM | 8 GB | 16 GB |
| 🎨 Graphics | Direct3D 12 card, 4 GB | Direct3D 12 card, 8 GB |
| 📀 Disk | Under 2 MB (a 0.7 MB installer) | Under 2 MB |
| 🌐 Internet | Only for the leaderboard, Genebank and online multiplayer | — |

## 📦 3. Install

1. Double-click **`DNA-AND-Setup.exe`**.
2. Windows may ask *"Do you want to allow this app to make changes?"*. Click **Yes**.
3. Pick a folder, or keep the default.
4. Click **Install**, then **Launch**.

## 🚀 4. First launch

> 🔭 **In the 0.1.5 universe preview** the program opens on the universe's landing page (type a seed or leave it blank, Begin or Take the tour), not the main menu below: the menu, the powers and the saves are the full game's, still to come. The preview's keys are in the [README](../README.md#-keys).

| Step | What you'll see |
|---|---|
| 🏠 | The main menu: New World, Continue / Load, Multiplayer, Genebank + Leaderboard, Wiki, Settings, Credits, Quit |
| ⚙️ | Open **Settings** first to set graphics, sound and controls |
| 🌍 | Click **New World**, pick a seed or go random, and you're on a young, lifeless planet |
| ⚡ | Use your first powers to shape the planet until life can start |

🎮 **Controls:** WASD or screen edges to pan · scroll wheel to zoom · right-drag to rotate · left-click to use a power

## 🔄 Updating

Download the newer `DNA-AND-Setup.exe` and run it. It installs over the old version, and **your saves are kept**.

## 🗑️ Uninstalling

**Windows Settings → Apps → Installed apps → DNA-AND → Uninstall.**

💾 Your saved worlds are **kept**. The uninstaller asks whether you want them deleted too.

## 🆘 Trouble?

| Problem | Try |
|---|---|
| Won't start | Update your graphics driver (DNA-AND needs Direct3D 12) |
| Leaderboard won't load | Check your internet connection. The game still plays offline |
| Running slow | Settings → Graphics → lower the quality (in the universe preview: View → Render scale and View → GPU use) |

---

📖 Next: the [Player Wiki](wiki/README.md "Every system, explained") · 🛠️ Building from source: [How to Build](HOW-TO-BUILD.md "Build DNA-AND from source")

## 🐧 On Linux

An `.exe` does not run on Linux by itself. Take the **zip** from the releases page (not the installer), unpack it, and run `run-on-linux.sh` inside it: it runs the game through **Wine**, which carries Direct3D 12 on Vulkan, in a private Wine folder of its own. It needs Wine 8 or newer and your card's Vulkan driver, and it tells you what to install if either is missing. The whole way, the Steam way, and what is proven so far: [On Linux](release/UNIVERSE-LINUX.md).

