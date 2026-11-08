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

🌌 **The universe preview:** `build.cmd NAME preview` builds a copy that always opens straight into the universe, from the cosmic web of galaxies and the supermassive black holes at their hearts down to the quarks.

🔭 **Sample sets:** run the exe with `--set2` to `--set6` to open one sample set on its own (`--set5` resource fields, `--set6` the Dyson sphere: I or C goes in and out, M is the view from the middle, inside W A S D flies and dragging looks around).

🎞️ **The encoders' sample:** run the exe with `--encoders` to open the universe with the encoders side by side over it: the frame as drawn, and the same frame through our own `.cdf97w`, H.264, VP9 and AV1, each with its size for the frame and its quality. Right-click a tile to keep it. It is a sample only, never part of the game.

⏱️ **Quick check:** `test.cmd 3000` runs for 3 seconds and closes. Quick checks open small in the bottom-right corner and don't take the keyboard.

▶️ **Normal play:** run `play.cmd`, or double-click `build\DNA-AND.exe`. There's no timer at all.

The game always runs in a window and never changes your display settings, so closing it is always a full revert. It opens maximized, so it draws at your screen's full resolution.

`build.cmd` runs the C# compiler from Windows' own .NET Framework folder over everything in `src/`. Nothing outside the repo and Windows is used.

### The Universe release

Universe, the standalone universe program, is this same source shipped on its own, under its own name and in its own repository (`Universe/`, a separate git repository inside this one). `universe-sync.cmd` refreshes it: it copies `src/`, `content/` (every `.dnat` renamed to `.unt`, with the headers reworded), `installer/`, `linux/run.sh` and the release docs from `docs/release/` (the readme, the guide, the build guide, the comparison, the Linux page, the credits and the in-game pages). `Universe/release.cmd` then builds `Universe.exe`, its installer and its zip, exactly as `release.cmd` does here. Both releases are built and shipped together, so the two programs never drift apart. The Universe project carries its own `.claude/` workflow, its own `docs/` workflow files and its own `wiki/`; the sync never touches them, nor its `.gitignore`.

---

## 🗂️ Source layout

One folder per part of the game. The folders that exist today:

| Folder | Part |
|---|---|
| `content/` | Game content, read by the generators (`.dnat`) |
| `src/platform/` | Talking to Windows: the window, input, menus, settings, the document viewer, the landing page |
| `src/core/` | Math, the CDF 9/7 wavelet and mode-E, physics, jobs, saves, the content reader |
| `src/render/` | Direct3D 12 graphics, the shaders, the PNG, GIF and MP4 writers |
| `src/gen/` | The live generators: galaxies, stars, star systems, worlds and their ground, bodies in space, grains, elements, creature bodies, cities |
| `src/sim/` | The living planet (core, plates, surface, air, weather, impacts), planet life, the microbe world, sea life |
| `src/shell/` | The views: the universe (one camera from the cosmic web to the quarks), the matter, the planet, the elements, the samples; the scale and cards on screen |

Planned, as the game grows (not yet in the tree): `src/genome/` and `src/life/` (DNA to living things), `src/civ/` and `src/space/` (civilization to space), `src/god/` and `src/time/` (powers and the clock), `src/net/` and `sidecar/` (multiplayer, the leaderboard side car).

---

## 📸 Filling the doc images

After the game builds, run its capture mode to render every image slot in the docs.

---

🗺️ Back to the [README](../README.md)
