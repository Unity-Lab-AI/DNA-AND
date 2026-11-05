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

_(Round 1 and 2 answered — see `docs/DESIGN.md` and `docs/FINALIZED.md`.)_

### Lists and powers (from round 2 answers)
- [ ] Top 100 categories list, for approval: "do top 100 for everything from viruse types, aminals, insects, fish, birds, environmets, senses, abilities, and not limited to thed bit to incluse the top one hundered top onehundreds" (`docs/LISTS.md`)
- [ ] Fill each approved category with its top 100 entries — "real and dynamic invented combo procedurally"
- [ ] Procedural generator that invents new entries on top of the real ones
- [ ] 100 god powers, each with uses, pluses and minuses: "they all need their usesas and pluses and minuses in some cases raise temp with fire lower with cold add water to flood sun to dry it up solar flare to fuck with tech, ,ect ect ect ect ect ect ectr x100" (`docs/POWERS.md`)
- [ ] Builder wiki for us, plus the in-game wiki ("both")
- [ ] Unity AI Lab team credits, website and git/GitHub links: "this deserves the unity ai lab team credits and website .com with the git and github links"

### R1 — Our own build tools
- [ ] Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code
- [ ] Use it to build a fuller machine-code writer (an "assembler")
- [ ] Design our own programming language for the game
- [ ] Build the compiler for our language, written in our own assembler
- [ ] Rebuild the compiler in our own language, so it builds itself
- [ ] Teach it to make Windows exes
- [ ] (Later) Teach it to make Mac programs — "windows only for now"
- [ ] (Later) Teach it to make Linux programs — "windows only for now"
- [ ] Write the how-to for building the game with our tools

### R2 — Talking to each system
- [ ] Windows: window, keyboard, mouse, sound, files, clock, threads, network
- [ ] (Later) Mac: same list
- [ ] (Later) Linux: same list

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
- [ ] Earth template: the full "everything" list (top 100 lists)
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
- [ ] Power list (`docs/POWERS.md`)
- [ ] Unlocking powers with points from the amount of life in your galaxies
- [ ] Paying for powers by harvesting life: "maybe u have to harvet life and later on u get the intelligent sure intelligences using their powers to harvest life for you"
- [ ] Endgame: intelligent life harvests life for its own needs, spreads and uses up nutrients and resources: "since all life is like a virus advanced forms seprad and use up avasil nutrientrs and resources and have to spread. jsut liek real life"
- [ ] Gene powers with good and bad outcomes
- [ ] Life starting from the player's first action

### R12 — Time
- [ ] Pause and speed controls
- [ ] Automatic speed jumps at milestones
- [ ] Balancing so a game reaches space in about 4 to 6 hours

### R13 — Game shell
- [ ] Open-ended play: "open ended till u resarts to get a new world and galaxy"
- [ ] Main screen
- [ ] In-game wiki of everything
- [ ] Settings
- [ ] Save, load and quit

### R14 — Multiplayer
- [ ] Single player
- [ ] Co-op and multiplayer in one shared world (one player's computer hosts)
- [ ] Seeing each other's cursors

### R15 — Leaderboard and Genebank
- [ ] Our own secure web connection code (approved)
- [ ] Leaderboard service as a swappable side car: "lets keep it as a side car so if needed we can pop something else in there."
- [ ] Sending scores to jsonbin
- [ ] Combined score from all the categories
- [ ] Top players uploading a genome to the Genebank
- [ ] Bringing a Genebank genome into a world by meteorite

### R16 — Shipping
- [ ] Windows exe
- [ ] (Later) Mac program
- [ ] (Later) Linux program

### R17 — Docs
- [ ] Setup guide
- [ ] How-to guides
- [ ] Builder wiki and in-game wiki
- [ ] Keep README and DESIGN up to date

---

## TOMBSTONES

_(none)_
