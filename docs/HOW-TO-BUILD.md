# 🛠️ How to Build DNA-AND from Source

> 🟡 **This is the plan.** No game code or tools exist yet. This page describes how the build **will** work, and it gets updated as each step is built.

## 🚫 The one rule

No outside compiler, no libraries, no build tools. The only outside things are **Windows itself** (including the PowerShell it ships with, used only for the very first step) and the **graphics card driver**.

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
 0  asm0.ps1     ← a small assembler in PowerShell, which comes with Windows
 1  asm1.exe     ← the assembler rebuilt in our own assembly, built by asm0.ps1
 2  asm.exe      ← built by asm0.exe: full assembler, writes Windows exes
 3  dnac0.exe    ← built by asm.exe: first compiler for Helix
 4  dnac.exe     ← built by dnac0.exe: the compiler, now written in Helix
 5  DNA-AND.exe  ← built by dnac.exe: the game
```

| Step | Input | Output | Proof it works |
|---|---|---|---|
| 0️⃣ Mini-assembler | PowerShell script | `asm0.ps1` | Builds a small test exe that runs |
| 1️⃣ Assembler v1 | Our assembly | `asm1.exe` | Rebuilds itself identically |
| 2️⃣ Assembler | `asm0` source | `asm.exe` | Rebuilds itself identically |
| 3️⃣ Compiler v1 | Assembly source | `dnac0.exe` | Compiles the compiler's own source |
| 4️⃣ Compiler v2 | Source in Helix | `dnac.exe` | Built twice in a row, byte-identical |
| 5️⃣ Game | Game source | `DNA-AND.exe` | Launches to the main menu |

✅ Step 0 is a small assembler script that runs on the PowerShell built into Windows. The language is **Helix**: low-level and C-like, with manual memory.

## ▶️ Running a build (once the tools exist)

1. Open the repo folder.
2. Run the bootstrap. It builds steps 0 to 4 once and then reuses them.
3. Build the game with `dnac.exe`.
4. Build the installer, `DNA-AND-Setup.exe`.

📝 The exact commands get written here once step 0 exists.

## 🗂️ Source layout

One folder per roadmap system:

| Folder | System |
|---|---|
| `content/` | Game content in our own text format (`.dnat`), compiled to binary by our toolchain |
| `bootstrap/` | Steps 0–2: seed, hex, assembler |
| `src/toolchain/` | Our compiler |
| `src/platform/` | Talking to Windows |
| `src/core/` | Math, memory, arrays, jobs, saves |
| `src/render/` | Graphics + the 3D image generator |
| `src/gen/` | The live generators: creature, city, space tech and the rest |
| `src/universe/` · `src/planet/` | Galaxies → planets |
| `src/genome/` · `src/life/` | DNA → living things |
| `src/civ/` · `src/space/` | Civilization → space |
| `src/god/` · `src/time/` · `src/shell/` | Powers, clock, menus |
| `src/net/` · `sidecar/` | Multiplayer, leaderboard side car |

## 📸 Filling the doc images

After the game builds, run the capture tool to render every image slot in the docs. See [docs/img/README.md](img/README.md).

---

🗺️ The full system-by-system plan is in the builder wiki: [wiki/index.md](../wiki/index.md)
