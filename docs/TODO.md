# TODO — Minor Task List (Active Tasks)

**Tier 2 of 3** — the MINOR task list. Holds active tasks (pending + in_progress) at the day-to-day work grain. Each minor task lives under a major milestone in `docs/ROADMAP.md` and decomposes further into entries in `docs/DECOMPOSED.md` when YOLO mode picks it up.

Completed tasks move to `docs/FINALIZED.md` per `.claude/CONSTRAINTS.md §FINALIZED BEFORE DELETE`. Never delete a task description; only flip status (LAW: NEVER DELETE TODO INFO).

Status markers:
- `[ ]` pending
- `[~]` in_progress
- `[x]` complete (move to FINALIZED.md immediately, never leave here)

LAW #0 reminder: every task description preserves the user's verbatim words.

**Three-tier cascade:** ROADMAP.md (major) → TODO.md (minor, this file) → DECOMPOSED.md (decomposed). YOLO mode reads all three and works the cascade — see `.claude/commands/yolo.md` and `.claude/WORKFLOW.md §YOLO MODE`.


---

## In progress

### R17 — Docs
- [~] Write all wikis, docs and readmes as if the game were complete: "okay we need to write all the wikis and docs and readmes for the entire project in full to complete final endgame and play through completely in full no text wealls easy and fun to read beautifully written and well designed and formated with tool tips and everything like a AAA game using all the icons and images of the game in the docs with links and relseas link at top of readme and everything as if the game was full and complete wee will make everything but the geam itself yet"
  - ✅ Written: README, player wiki (`docs/wiki/`, 20 pages), builder wiki (`wiki/`), `docs/SETUP.md`, `docs/HOW-TO-BUILD.md`, `docs/img/README.md`.
  - ✅ All 47 open questions decided and applied (`docs/DESIGN.md` round 5).
  - ⏳ Left: fill the 55 image slots once R4's 3D generator and capture tool exist.

---

## Pending

### Lists
- [ ] Fill each approved category with its top 100 entries — "real and dynamic invented combo procedurally"
  - 100 categories × 100 entries = 10,000 entries (`docs/LISTS.md`, approved: "Use them as is").
  - Real-world entries only. The invented ones come from the generator below.
- [ ] Procedural generator that invents new entries on top of the real ones

### R1 — Our own build tools (Helix)
- [ ] Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code
  - Decided: a **hex loader** typed in as raw bytes. It turns a text file of hex digits into a Windows exe. Broken down in `docs/DECOMPOSED.md`.
- [ ] Use it to build a fuller machine-code writer (an "assembler")
- [ ] Design our own programming language for the game
  - Decided: **Helix**. Low-level and C-like, with manual memory and no garbage collector.
- [ ] Build the compiler for our language, written in our own assembler
  - The first Helix compiler (`dnac0.exe`).
- [ ] Rebuild the compiler in our own language, so it builds itself
  - `dnac.exe`, written in Helix. It has to build itself twice and come out byte-identical.
- [ ] Teach it to make Windows exes
- [ ] (Later) Teach it to make Mac programs — "windows only for now"
- [ ] (Later) Teach it to make Linux programs — "windows only for now"
- [ ] Write the how-to for building the game with our tools
  - `docs/HOW-TO-BUILD.md` has the plan. Exact commands get added as each step exists.

### R2 — Talking to Windows
- [ ] Windows: window, keyboard, mouse, sound, files, clock, threads, network
  - Only Windows itself and the graphics card driver may be used ("sure").
- [ ] (Later) Mac: same list
- [ ] (Later) Linux: same list

### R3 — Engine core
- [ ] Math: vectors, matrices, noise, seeded random numbers
  - Our own seeded random generator. 64-bit positions with a moving origin.
- [ ] Memory and the giant data arrays for billions of things
  - Big memory blocks reserved up front per system.
- [ ] Multithreading so the simulation uses every CPU core
  - One worker per core, pulling jobs from a shared queue.
- [ ] Save and load file format
  - Our own versioned binary chunks: universe, planets, life, civilizations, player.

### R4 — Graphics
- [ ] Our own 3D image generator for all game icons and art: "we will need to design the #D genrator and botting of them"
- [ ] Auto-capture tool that renders the doc images and fills every image slot in the docs
  - A mode of the game exe. It finds every `<!-- IMG: ... -->` marker and renders that image (`docs/img/README.md`).
- [ ] Talk to the graphics card on each system
  - Direct3D 12 (part of Windows).
- [ ] Terrain, water, sky, weather, lighting, shadows
- [ ] Drawing creatures built from their genes
- [ ] Smooth zoom from microbe to planet to solar system to galaxy
- [ ] Top-down god camera
- [ ] Visual gene map for every trait and skill

### R4.5 — Art style samples
- [ ] Style samples, 3 samples × 3 styles: "remembr we arent using other peoples shit unless we absoulutley have to so plan on buiolding it all and things like ingame microbes viruses and intelligernt creature should all have samples 1,2,3 made in 3 diffeernt styles 1,2,3 for a hand ful of things in the game before we go with any one style and same for things like the galaxy layouts solarsystem layouts and planets and stars and dyson spheres and space ships and space colonires ands space stations and rouseource firleds and gas giants and athe whole periodic table of elements"
  - Styles: RimWorld-like, Populous-like, Stardew-like, all drawn originally by us: "try a rimworld like and a, populous like, and a stardew like".
  - Light on the GPU, built for living things that move and spread: "not one thats too gpu intense and these will be living things in the environment moving and spreading as told before".
  - Waits on R4's basic renderer: "we cant do em till you build tyhier egine and shit".
- [ ] Samples: Microbes (3 examples × 3 styles)
- [ ] Samples: Viruses (3 examples × 3 styles)
- [ ] Samples: Intelligent creatures (3 examples × 3 styles)
- [ ] Samples: A handful of other living things (plants, insects, animals, fish, birds) (3 examples × 3 styles)
- [ ] Samples: Galaxy layouts (3 examples × 3 styles)
- [ ] Samples: Solar system layouts (3 examples × 3 styles)
- [ ] Samples: Planets (3 examples × 3 styles)
- [ ] Samples: Stars (3 examples × 3 styles)
- [ ] Samples: Gas giants (3 examples × 3 styles)
- [ ] Samples: Dyson spheres (3 examples × 3 styles)
- [ ] Samples: Space ships (3 examples × 3 styles)
- [ ] Samples: Space colonies (3 examples × 3 styles)
- [ ] Samples: Space stations (3 examples × 3 styles)
- [ ] Samples: Resource fields (3 examples × 3 styles)
- [ ] Samples: The whole periodic table of elements (all 118) (3 examples × 3 styles)
- [ ] Side-by-side comparison sheet of every subject, for the creator to pick one style
- [ ] Lock in the chosen style, recorded verbatim in `docs/DESIGN.md`

### R5 — Universe
- [ ] Galaxy generation from a seed
  - Sizes: Small, Medium, Large, Massive.
- [ ] Solar systems: stars and planets
- [ ] Multiple galaxies

### R6 — Planet
- [ ] Earth template: the full "everything" list (top 100 lists)
  - Surface split into equal-size cells on a sphere.
- [ ] Unlockable other planet templates and what unlocks them
  - 100 templates. The first to unlock are Ocean, Desert, Ice, Jungle, Volcanic, Tidally locked and Gas-giant moon. Stages and ages can come in their own order on these.
- [ ] Ground, plates, mountains, oceans, rivers, ice
- [ ] Air, weather, climate, seasons, day and night
- [ ] Chemistry and minerals that life can use
  - All 118 elements. Lab-made ones (93+) unlock once a civilization creates them.

### R7 — Genomes
- [ ] Real-style DNA format
  - 2 bits per letter (A, C, G, T).
- [ ] How DNA builds a body and its behavior
- [ ] Mutation and breeding, with and without sex
- [ ] Viruses changing genes
- [ ] Map of gene pieces to traits ("breathing air to walking upright to photosynethesiis")

### R8 — Life
- [ ] Viruses
- [ ] Microbes
- [ ] Plants
- [ ] Insects
- [ ] Animals
- [ ] Gene-driven ant-like behavior for each creature
- [ ] Grouping crowds by area ("1 worm can be 4396 worms")
  - One group per cell and species. A group splits when its genes drift, and individuals appear when you zoom in.
- [ ] Food chains, disease, extinction, new species
- [ ] All things flock together, a 100-step grouping ladder for every kind of life: "and all things flock togther meaning intigenct>tirbes>villages>towns>cities wolf>pack>horde>intelligence>humanoioidwolf but more steps with all things do you get what i mean by all things"
  - Steps listed in `docs/wiki/groups-and-flocking.md`. Each kind takes its own path, and groups merge and split.
  - Any line of life can climb to intelligence, not just apes.
- [ ] Mixed-species groups and subjugation: "1 yes just like real life, one species can subjegate another in ever way possible this is a 18 plus game because it has all type of things form sex to killing and murder"
- [ ] Tree of life: "option 1, every thing blows up to 100 ,, nutiorents, elements, species, the whoel kingdom philum class gensis species thing is all 100 tracking with the whole lengogy tree of life the circular type ones for the heretitary and the genr tree of lifee palnet or solor sytem or galaxy sortable"
  - A circular family tree (heredity) and a gene tree. Sortable by planet, solar system or galaxy. Extinct branches are kept.
  - Every rank, from domain to species, is tracked to 100: real plus invented.
- [ ] Top 100 nutrients list
- [ ] Milestones for every stage of life
  - All 100 stages (`docs/wiki/stages-of-life.md`). Each stage unlocks the powers that fit it, with stronger "+" versions later.

### R9 — Intelligent life
- [ ] Tribes and culture
- [ ] Politics
- [ ] War
- [ ] Technology tree
  - 100 ages in real-history order, 8 branches. Each discovery needs the one before it on its branch, plus links across branches (`docs/wiki/tech-tree.md`).
- [ ] Ways civilizations destroy themselves
  - A collapse can send a civilization back to earlier ages.

### R10 — Space
- [ ] Space travel and reaching other planets
- [ ] Spreading life and colonies
- [ ] Dyson spheres
- [ ] War across galaxies

### R11 — God mode
- [ ] Build all 100 powers from the power list (`docs/POWERS.md`, `docs/wiki/god-powers.md`)
- [ ] Unlocking powers with points from the amount of life in your galaxies
  - Six unlock points: Start, Life, Land, Mind, Civ, Space.
- [ ] Paying for powers by harvesting life: "maybe u have to harvet life and later on u get the intelligent sure intelligences using their powers to harvest life for you"
  - The currency is **Souls**. Cost = tier × area × strength. Tier 4–5 powers have cooldowns.
  - Harvesting is slow and shows as a glow that scares intelligent life. Mass deaths give lots of souls.
- [ ] Endgame: intelligent life harvests life for its own needs, spreads and uses up nutrients and resources: "since all life is like a virus advanced forms seprad and use up avasil nutrientrs and resources and have to spread. jsut liek real life"
  - Six harvest steps, from farming to intergalactic (`docs/wiki/tech-tree.md`).
- [ ] Gene powers with good and bad outcomes
- [ ] Life starting from the player's first action
  - Starting toolkit: lightning, heat (fire), cold, water, spark of life, add genes, and add elements: "option one but things like add genes add elements ect ect real life so full perioditic table of elements".

### R12 — Time
- [ ] Pause and speed controls
  - Pause plus 1×, 10×, 100×, 1,000× and max.
- [ ] Automatic speed jumps at milestones
  - Drops to 1× with a short cinematic. Can be turned off.
- [ ] Balancing so a game reaches space in about 4 to 6 hours
  - Each stage has its own years-per-second.

### R13 — Game shell
- [ ] 18+ rating shown on the store/release page, installer and main menu: "this is a 18 plus game"
- [ ] Open-ended play: "open ended till u resarts to get a new world and galaxy"
- [ ] Main screen
  - New World, Continue/Load, Multiplayer, Genebank + Leaderboard, Wiki, Settings, Credits, Quit.
- [ ] In-game wiki of everything
  - Reads the same pages as `docs/wiki/`.
- [ ] Settings
  - Graphics, Sound, Controls (every key can be rebound), Gameplay, Online, Accessibility.
- [ ] Save, load and quit
  - Autosave every 10 minutes and at every milestone. Unlimited slots.
- [ ] Controls: mouse + keys
  - WASD/edge pan, scroll-wheel zoom, right-drag rotate, left-click power, Space pause, 1–5 speeds, Esc menu, F1 wiki.

### R14 — Multiplayer
- [ ] Single player
- [ ] Co-op and multiplayer in one shared world (one player's computer hosts)
  - Join code, up to 8 players, our own reliable messaging over UDP.
- [ ] Seeing each other's cursors

### R15 — Leaderboard and Genebank
- [ ] Our own secure web connection code (approved)
  - TLS 1.3, written from scratch.
- [ ] Leaderboard service as a swappable side car: "lets keep it as a side car so if needed we can pop something else in there."
  - Its own small program on the same computer.
- [ ] Sending scores to jsonbin
- [ ] Score = life in your galaxy + life harvested: "life in your galaxy and life \"havested\""
- [ ] Top players uploading a genome to the Genebank
  - The weekly top three ("Genome of the Week").
- [ ] Bringing a Genebank genome into a world by meteorite
  - Available from the first-life stage on.

### R16 — Shipping
- [ ] Windows exe
- [ ] Our own `DNA-AND-Setup.exe` installer
  - Uninstall keeps saves and asks whether to delete them.
- [ ] Post each release on both release pages (git.unityailab.com and GitHub)
- [ ] (Later) Mac program
- [ ] (Later) Linux program

### R17 — Docs (upkeep)
- [ ] Fill all 55 image slots with the R4 capture tool
- [ ] Keep README and DESIGN up to date
- [ ] Keep every wiki page true as code lands: update `last-verified` and flip `status` from draft to verified

---

## TOMBSTONES

- **"Combined score from all the categories"** (R15). Replaced by the creator's round 4 answer: "life in your galaxy and life \"havested\"". The new task is under R15.
- **"Power list (`docs/POWERS.md`)"** (R11). The list itself is done (see FINALIZED). Replaced by "Build all 100 powers from the power list".
