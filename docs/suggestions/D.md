# 💬 Builder Suggestions: Waiting on the Creator

Every choice below hasn't been decided yet. The docs currently use the option marked **(used in docs)**. Pick one, or write your own, and the docs get updated to match.

---

### S1 — Bootstrap seed
How does the very first hand-typed program work?
- (A) A tiny hex loader typed in as raw bytes. It turns a text file of hex digits into a program, and everything after it is written as hex text. **(used in docs)**
- (B) Type the whole first assembler in directly as raw bytes.

Where: `wiki/systems/r01-toolchain.md`, `docs/HOW-TO-BUILD.md`

### S2 — Language style
What kind of programming language do we build?
- (A) Low-level and C-like: you manage memory yourself, with no garbage collector. Fast enough for billions of things. **(used in docs)**
- (B) Higher-level, with automatic memory cleanup. Easier to write, but slower and pauses at random.

Where: `wiki/systems/r01-toolchain.md`

### S3 — Language name
What's our language called?
- (A) No name yet. The docs call it "our language". **(used in docs)**
- (B) Give it a name now.

Where: `wiki/systems/r01-toolchain.md`, `docs/HOW-TO-BUILD.md`

### S4 — Memory
How does the game hold billions of things in memory?
- (A) Each system reserves big blocks of memory up front and fills them. **(used in docs)**
- (B) Ask Windows for memory piece by piece as needed.

Where: `wiki/systems/r03-engine-core.md`

### S5 — Threading
How does the work get spread across CPU cores?
- (A) One worker per core, all pulling small jobs from a shared queue. **(used in docs)**
- (B) A fixed thread per system (one for life, one for weather, and so on).

Where: `wiki/systems/r03-engine-core.md`

### S6 — Number precision
How do we handle distances from a virus to across galaxies?
- (A) 64-bit numbers, plus a "moving origin" that re-centres on the camera. **(used in docs)**
- (B) Whole-number (fixed-point) positions per zoom level.

Where: `wiki/systems/r03-engine-core.md`

### S7 — Random numbers
Where do seeded random numbers come from?
- (A) Our own small seeded generator, written from scratch. **(used in docs)**
- (B) Several generators, each tuned to one job (terrain, life, weather).

Where: `wiki/systems/r03-engine-core.md`

### S8 — Graphics API
How does the game talk to the graphics card?
- (A) Vulkan, which comes with the graphics card driver.
- (B) Direct3D 12, which is part of Windows itself. **(used in docs)**

Where: `wiki/systems/r04-renderer.md`

### S9 — Planet grid
How is the planet's surface divided up?
- (A) Equal-size cells covering the sphere. Each cell stores ground, water, air, weather and chemistry. **(used in docs)**
- (B) A cube wrapped onto the sphere, split into square tiles.

Where: `wiki/systems/r06-planet.md`

### S10 — Genome storage
How is DNA stored?
- (A) 2 bits per letter (A, C, G, T), so 4 letters fit in a byte. **(used in docs)**
- (B) 1 byte per letter. Simpler, but uses 4 times the memory.

Where: `wiki/systems/r07-genome.md`

### S11 — Save format
What do save files look like?
- (A) Our own binary format made of versioned chunks (universe, planets, life, civilizations, player). **(used in docs)**
- (B) Readable text files. Bigger and slower.

Where: `wiki/systems/r03-engine-core.md`

### S12 — Population grouping
How do groups ("1 worm can be 4396 worms") split and merge?
- (A) One group per area cell and species. A group splits when its genes drift past a set limit, and individuals are only generated when you zoom in. **(used in docs)**
- (B) Always simulate every individual creature.
- (C) Groups only. Never show individuals.

Where: `wiki/systems/r08-life-sim.md`

### S13 — Power costs
How is the Soul cost of each power set?
- (A) Scales with the power's area and strength. **(used in docs)**
- (B) A flat price per power.
- (C) A flat price that rises each time you use it.

Where: `wiki/systems/r11-god-mode.md`

### S14 — Time scale
What does "1x" mean?
- (A) Each stage of life has its own base "years per second", tuned so a game reaches space in 4 to 6 hours. **(used in docs)**
- (B) 1x always means real time (1 second = 1 second).

Where: `wiki/systems/r12-time.md`

### S15 — In-game wiki source
Where does the in-game wiki get its pages?
- (A) The game reads the same pages as `docs/wiki/`, so there's one source for both. **(used in docs)**
- (B) A separate copy built for the game.

Where: `wiki/systems/r13-game-shell.md`

### S16 — Multiplayer transport
How do players' computers talk to the host?
- (A) Our own reliable messaging over UDP. **(used in docs)**
- (B) TCP: simpler, but laggier.

Where: `wiki/systems/r14-multiplayer.md`

### S17 — Side car shape
What form does the swappable leaderboard "side car" take?
- (A) Its own small program we write, which the game talks to on the same computer. **(used in docs)**
- (B) A module inside the game, behind one fixed set of functions.

Where: `wiki/systems/r15-leaderboard-genebank.md`

### S18 — Secure connection
Which version of secure web traffic do we write?
- (A) TLS 1.3: newest, simplest, and the one most servers expect. **(used in docs)**
- (B) TLS 1.2: older and more complicated.

Where: `wiki/systems/r15-leaderboard-genebank.md`

### S19 — Installer
How does the game get installed?
- (A) Our own `DNA-AND-Setup.exe`. **(used in docs)**
- (B) A zip file. Unzip and run, with no installer.

Where: `wiki/systems/r16-packaging.md`, `docs/SETUP.md`

### S20 — Capture tool
Where does the image "botting" tool live?
- (A) A mode of the game exe itself. **(used in docs)**
- (B) A separate program.

Where: `wiki/systems/r17-docs-and-capture.md`, `docs/img/README.md`

### S21 — System requirements
What are the minimum and recommended specs?
- (A) Minimum: Windows 10/11 64-bit, 4-core CPU, 8 GB RAM, a Direct3D 12 graphics card with 4 GB. Recommended: 8+ cores, 16 GB RAM, a graphics card with 8 GB. **(used in docs)**
- (B) Leave blank until the game can be measured.

Where: `docs/SETUP.md`

### S22 — Source layout
How is the source code arranged?
- (A) One folder per roadmap system (`src/toolchain`, `src/platform`, `src/core`, `src/render`, …). **(used in docs)**
- (B) Grouped by layer (`engine/`, `sim/`, `game/`).

Where: `docs/HOW-TO-BUILD.md`

### S23 — Disk space needed
How much disk space should SETUP.md list?
- (A) Leave "to be measured" until the first build exists (used in docs)
- (B) Set a target size now
Where: `docs/SETUP.md` → System requirements

### S24 — Uninstall and saves
Should uninstalling delete your saved worlds?
- (A) Keep saves, and ask whether to delete them
- (B) Always delete everything
- (C) Always keep saves
- Docs currently say: not decided yet
Where: `docs/SETUP.md` → Uninstalling
