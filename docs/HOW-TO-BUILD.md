# 🛠️ How to Build DNA-AND from Source

> ⚖️ **Read [the Work Laws](WORK-LAWS.md "The docs are the spec") first.** Every line of code is written from the docs.

> 🟡 **This is the plan.** No game code or tools exist yet. This page describes how the build **will** work, and it gets updated as each step is built.

## 🚫 The one rule

> "No dependancies!!!! WE DO NOT USE ANOYONE ELSE CODE< SCRIPTS< PROGERMAES< BINARIES <EXECULATEBLES< TANBLES< DATA< FILES< FOLDERS< DIRECTORES< OR COMPOLIERS"

No outside compiler, no libraries, no build tools. The only outside things are **Windows itself** and the **graphics card driver**.

---

## 🧰 What you need

| Need | Why |
|---|---|
| 🪟 Windows 10 / 11, 64-bit | The only system supported for now |
| 📁 This repo | Everything else is in here |

That's it. Every tool gets built from the repo itself.

## 🪜 The bootstrap chain

Each step builds the next. The first step is the only one that isn't built by a tool.

```
 0  seed.exe     ← typed in as raw machine bytes
 1  asm0.exe     ← built by seed.exe from hex text
 2  asm.exe      ← built by asm0.exe: full assembler, writes Windows exes
 3  dnac0.exe    ← built by asm.exe: first compiler for Helix
 4  dnac.exe     ← built by dnac0.exe: the compiler, now written in Helix
 5  DNA-AND.exe  ← built by dnac.exe: the game
```

| Step | Input | Output | Proof it works |
|---|---|---|---|
| 0️⃣ Seed | Hand-typed raw bytes | `seed.exe` | Turns a hex text file into the exact bytes expected |
| 1️⃣ Mini-assembler | Hex text | `asm0.exe` | Builds a small test program that runs |
| 2️⃣ Assembler | `asm0` source | `asm.exe` | Rebuilds itself identically |
| 3️⃣ Compiler v1 | Assembly source | `dnac0.exe` | Compiles the compiler's own source |
| 4️⃣ Compiler v2 | Source in Helix | `dnac.exe` | Built twice in a row, byte-identical |
| 5️⃣ Game | Game source | `DNA-AND.exe` | Launches to the main menu |

✅ The seed is a tiny hex loader. The language is **Helix**: low-level and C-like, with manual memory.

## ▶️ Running a build (once the tools exist)

1. Open the repo folder.
2. Run the bootstrap. It builds steps 0 to 4 once and then reuses them.
3. Build the game with `dnac.exe`.
4. Build the installer, `DNA-AND-Setup.exe` ([R16](../wiki/systems/r16-packaging.md)).

📝 The exact commands get written here once step 0 exists.

## 🗂️ Source layout

One folder per roadmap system:

| Folder | System |
|---|---|
| `bootstrap/` | Steps 0–2: seed, hex, assembler |
| `src/toolchain/` | Our compiler |
| `src/platform/` | Talking to Windows |
| `src/core/` | Math, memory, arrays, jobs, saves |
| `src/render/` | Graphics + the 3D image generator |
| `src/universe/` · `src/planet/` | Galaxies → planets |
| `src/genome/` · `src/life/` | DNA → living things |
| `src/civ/` · `src/space/` | Civilization → space |
| `src/god/` · `src/time/` · `src/shell/` | Powers, clock, menus |
| `src/net/` · `sidecar/` | Multiplayer, leaderboard side car |

## 📸 Filling the doc images

After the game builds, run the capture tool to render every image slot in the docs. See [docs/img/README.md](img/README.md).

---

🗺️ The full system-by-system plan is in the builder wiki: [wiki/index.md](../wiki/index.md)
