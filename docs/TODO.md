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

_(none)_

---

## Pending

### Questions for the creator (nothing below gets guessed)

- [ ] **Q1 — Build tools.** "we make it" means the very first tool gets written as raw machine bytes, then each tool builds the next one. OK to do it that way? Start on Windows (your machine) first, then Mac and Linux? Do you have a Mac and a Linux machine to test on?
- [ ] **Q2 — The one thing we can't avoid.** Windows, Mac, Linux and the graphics card's driver are other people's software, and no program can run without them. Is it OK to use those, and only those?
- [ ] **Q3 — jsonbin.** jsonbin is someone else's online service. Is it the only outside thing allowed? Connecting to it safely means writing our own encryption from scratch, which is a big job. Is that OK?
- [ ] **Q4 — Gladiator.** Where is the Gladiator project, so I can see how its exe was made?
- [ ] **Q5 — "Everything."** Should we build the full list of what an Earth needs (rock, water, air, weather, chemistry, and so on) together, item by item?
- [ ] **Q6 — Multiplayer.** Does one player's computer host the shared world, or something else?
- [ ] **Q7 — Losing.** Leave it open-ended for now ("i like it open ended")?
- [ ] **Q8 — God powers.** You said "im sure youll figure it out", but rule 3 says I don't make things up. Should I draft a power list for you to approve or change?
- [ ] **Q9 — Wikis.** Do you mean the in-game wiki only, or also a written wiki for us builders?

### R1 — Our own build tools
- [ ] Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code
- [ ] Use it to build a fuller machine-code writer (an "assembler")
- [ ] Design our own programming language for the game
- [ ] Build the compiler for our language, written in our own assembler
- [ ] Rebuild the compiler in our own language, so it builds itself
- [ ] Teach it to make Windows exes
- [ ] Teach it to make Mac programs
- [ ] Teach it to make Linux programs
- [ ] Write the how-to for building the game with our tools

### R2 — Talking to each system
- [ ] Windows: window, keyboard, mouse, sound, files, clock, threads, network
- [ ] Mac: same list
- [ ] Linux: same list

### R3 — Engine core
- [ ] Math: vectors, matrices, noise, seeded random numbers
- [ ] Memory and the giant data arrays for billions of things
- [ ] Multithreading so the simulation uses every CPU core
- [ ] Save and load file format

### R4 — Graphics
- [ ] Talk to the graphics card on each system
- [ ] Terrain, water, sky, weather, lighting, shadows
- [ ] Drawing creatures built from their genes
- [ ] Smooth zoom from microbe to planet to solar system to galaxy
- [ ] Top-down god camera
- [ ] Visual gene map for every trait and skill

### R5 — Universe
- [ ] Galaxy generation from a seed
- [ ] Solar systems: stars and planets
- [ ] Multiple galaxies

### R6 — Planet
- [ ] Earth template: the full "everything" list (waits on Q5)
- [ ] Unlockable other planet templates and what unlocks them
- [ ] Ground, plates, mountains, oceans, rivers, ice
- [ ] Air, weather, climate, seasons, day and night
- [ ] Chemistry and minerals that life can use

### R7 — Genomes
- [ ] Real-style DNA format
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
- [ ] Food chains, disease, extinction, new species
- [ ] Milestones for every stage of life

### R9 — Intelligent life
- [ ] Tribes and culture
- [ ] Politics
- [ ] War
- [ ] Technology tree
- [ ] Ways civilizations destroy themselves

### R10 — Space
- [ ] Space travel and reaching other planets
- [ ] Spreading life and colonies
- [ ] Dyson spheres
- [ ] War across galaxies

### R11 — God mode
- [ ] Power list (waits on Q8)
- [ ] Unlocking powers with points from the amount of life in your galaxies
- [ ] Gene powers with good and bad outcomes
- [ ] Life starting from the player's first action

### R12 — Time
- [ ] Pause and speed controls
- [ ] Automatic speed jumps at milestones
- [ ] Balancing so a game reaches space in about 4 to 6 hours

### R13 — Game shell
- [ ] Main screen
- [ ] In-game wiki of everything
- [ ] Settings
- [ ] Save, load and quit

### R14 — Multiplayer
- [ ] Single player
- [ ] Co-op and multiplayer in one shared world (waits on Q6)
- [ ] Seeing each other's cursors

### R15 — Leaderboard and Genebank
- [ ] Our own secure web connection code (waits on Q3)
- [ ] Sending scores to jsonbin
- [ ] Combined score from all the categories
- [ ] Top players uploading a genome to the Genebank
- [ ] Bringing a Genebank genome into a world by meteorite

### R16 — Shipping
- [ ] Windows exe
- [ ] Mac program
- [ ] Linux program

### R17 — Docs
- [ ] Setup guide
- [ ] How-to guides
- [ ] Wiki (waits on Q9)
- [ ] Keep README and DESIGN up to date

---

## TOMBSTONES

_(none)_
