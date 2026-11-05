# 🛠️ How to Build DNA-AND from Source

> 🟢 Building works today: `build.cmd` makes `build/DNA-AND.exe`, which opens the game window. This page grows as each part of the game is built.

## 🚫 The one rule

No outside libraries, packages, engines or build tools. DNA-AND is **written in C#** and built with the **C# compiler that comes with Windows**. Besides our own code, it uses only Windows itself (its compiler, core runtime and system files) and your **graphics card driver**.

---

## 🧰 What you need

| Need | Why |
|---|---|
| 🪟 Windows 10 / 11, 64-bit | It already includes the C# compiler and runtime the build uses |
| 📁 This repo | Everything else is in here |

That's it. Nothing to download or install.

---

## ▶️ Building

1. Open the repo folder.
2. Run **`build.cmd`**.
3. The game appears at **`build/DNA-AND.exe`**.

🔭 **Samples:** run `build\DNA-AND.exe`, then click a galaxy to zoom deep into it. Scroll to zoom, drag to rotate, **Enter** visits the star in the middle, **H** goes to the core black hole, **1–5** set time speed, **Space** pauses, and **Esc** or **right-click** goes back.

🧪 **Look-around test:** `test.cmd` opens the game and leaves it running until you close it. **Keep going or revert** in the menu bar asks whether to keep it running: **Yes** keeps going, **No** closes it safely, and no answer within 15 seconds closes it safely too. The menu bar has **Exit**, **Keep going or revert**, **Keepers** (tick the samples you like; they're saved in `picks.txt`) and **Legend** (what each sample is, and the controls).

🌌 **The journey:** run the exe with `--journey` for one continuous zoom. It starts in a field of galaxies, then goes into any galaxy, to any star you point at or click, its solar system, a planet, its surface, the sea or the soil, and the living microbes. The mouse wheel zooms where the mouse points, holding Ctrl makes it 8 times faster, and zooming out climbs back up through every level.

🧰 **Separate copies:** `build.cmd NAME` builds `build\NAME\DNA-AND.exe`, so a game that is already open is never closed by a rebuild.

🔭 **Sample sets:** run the exe with `--set2` to `--set6` to open one sample set on its own (`--set5` resource fields, `--set6` the Dyson sphere: I or C goes in and out, M is the view from the middle, inside W A S D flies and dragging looks around).

⏱️ **Quick check:** `test.cmd 3000` runs for 3 seconds and closes. Quick checks open small in the bottom-right corner and don't take the keyboard.

▶️ **Normal play:** run `play.cmd`, or double-click `build\DNA-AND.exe`. There's no timer at all.

The game always runs in a window and never changes your display settings, so closing it is always a full revert.

`build.cmd` runs the C# compiler from Windows' own .NET Framework folder over everything in `src/`. Nothing outside the repo and Windows is used.

---

## 🗂️ Source layout

One folder per part of the game:

| Folder | Part |
|---|---|
| `content/` | Game content, read by the generators |
| `src/platform/` | Talking to Windows: window, input, files, sound, timing |
| `src/core/` | Math, memory, big arrays, jobs, saves |
| `src/render/` | Direct3D 12 graphics |
| `src/gen/` | The live generators: creature, city, space tech and the rest |
| `src/universe/` · `src/planet/` | Galaxies → planets |
| `src/genome/` · `src/life/` | DNA → living things |
| `src/civ/` · `src/space/` | Civilization → space |
| `src/god/` · `src/time/` · `src/shell/` | Powers, clock, menus and HUD |
| `src/net/` · `sidecar/` | Multiplayer, leaderboard side car |

---

## 📸 Filling the doc images

After the game builds, run its capture mode to render every image slot in the docs.

---

🗺️ Back to the [README](../README.md)
