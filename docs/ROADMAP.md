# ROADMAP — Major Task List (High-Level Phases / Milestones)

**Tier 1 of 3 — the MAJOR task list.** Holds the high-level phases / milestones that span multiple sessions or multiple PRs. Each major item breaks down into MINOR tasks in `docs/TODO.md`, which break down further into DECOMPOSED tasks in `docs/DECOMPOSED.md`.

YOLO mode reads all three tiers on activation. The cascade rule: complete the current decomposed task → next decomposed task in the same minor → if minor is done, next minor in the same major → if major is done, surface a milestone-boundary check-in to the user → start next major unless user stops.

Status markers (same scheme as TODO.md):
- `[ ]` pending
- `[~]` in_progress
- `[x]` complete (move to FINALIZED.md immediately, never leave here)

LAW #0 reminder: every milestone description preserves the user's verbatim words.

**Three-tier cascade:** ROADMAP.md (major, this file) → TODO.md (minor) → DECOMPOSED.md (decomposed). YOLO mode reads all three and works the cascade — see `.claude/commands/yolo.md` and `.claude/WORKFLOW.md §YOLO MODE`.


---

## In progress

_(none)_

---

## Pending

Every stage quotes the creator's words (see `docs/DESIGN.md` for the full record). Order goes top to bottom: each stage needs the ones above it. Stages marked **BLOCKED** wait on answers in `docs/TODO.md` → "Questions for the creator".

### [ ] R1 — Our own build tools (language + compiler), from nothing
> "we make it" / "No dependancies!!!! ... OR COMPOLIERS"

Nothing else can be built until this exists. The first piece has to be written directly as raw machine bytes, because there is no outside tool to translate it. Each later tool gets built with the one before it, until we have a full language that makes exes for all three systems. **BLOCKED** on Q1.

### [ ] R2 — Talking to Windows, Mac and Linux directly
> "all three" / "nothing of this game runs in a html its its own system and program"

Opening a window, keyboard and mouse, sound, files, the clock, threads, network. Done three times, once per system, with no outside libraries. **BLOCKED** on Q2.

### [ ] R3 — Engine core
The base every system sits on: math (vectors, matrices, noise, random numbers that repeat from a seed), memory handling, the "arrays" that hold billions of things, multithreading, saving data to disk.
> "the arrays"

### [ ] R4 — AAA 3D graphics, built from scratch
> "ut will be a AAA game graphics that can do all the microbes and viruses an d gene mapings and peices for every traight and skill and evolutionary thing like breathing air to walking upright to photosynethesiis" / "3d top down world with god mode space mechanics"

Talking to the graphics card per system, lighting, shadows, water, sky, terrain, creatures, zooming from a single microbe up to a whole galaxy. **BLOCKED** on Q2.

### [ ] R5 — The universe: stars, planets, solar systems, galaxies
> "stars+planets>solar sytems> galaxys" / "obviously galaxy size to buidl dyson spheres and ingalactic war"

Random generation from a seed, at full galaxy scale, with more than one galaxy.

### [ ] R6 — The planet: Earth and unlockable templates
> "EARTH(but other templets too you can unlock after reaching certain points on Earth." / "full massive wolrd with everything(when i say everything i fucking mean everything) that a wrold has on it like earth"

Ground, rock, plates, mountains, oceans, rivers, ice, air, weather, seasons, day and night, chemistry, minerals. The full list of "everything" needs to be worked out with the creator (Q5).

### [ ] R7 — Real-style genomes
> "Real-style" / "genome of all creatures liveing and even viruses"

Real-style DNA that builds each body and behavior, with mutation, breeding (with or without sex), and viruses that change genes. A "gene map" showing which pieces give which trait or skill.

### [ ] R8 — Life: viruses, microbes, plants, insects, animals
> "fully dynamic ggenology with microbes and viruses and plants and animals and insects" / "they will be like ants ais with genes that use thatev eveold or been infected with u know EVOLUTION"

Each creature acts on its genes, like an ant. Crowds of the same kind are grouped by where they live ("1 worm can be 4396 worms"). Food chains, disease, extinction, new species.

### [ ] R9 — Intelligent life and civilization
> "intelligent life has technology and culture and war and tribalisze and pollitics and stuff like real life does" / "intelligent life has amazing ways of killing its self"

Tribes, culture, politics, wars, technology trees, and the ways civilizations can wipe themselves out.

### [ ] R10 — Space expansion up to Dyson spheres
> "space expansion to dysonsphere" / "to get to other planbets intelligent life has to get there first" / "ingalactic war"

Space travel, spreading life to other planets, colonies, Dyson spheres, war across galaxies.

### [ ] R11 — God mode
> "think populous form back in the day but gneological twists" / "only every nudge evrything but u get the power of god to basicly do everything peopel attribute god doing." / "no direct edit besids using powers to influence things to happen" / "points are amount of life in your galaxie(S!)"

Powers that help or hurt life, unlocked over time with points. Gene powers unlocked later have good or bad outcomes. Life starts from your first action. "THIS IS NOT A MOBILE CLICKER". The full power list needs the creator (Q8).

### [ ] R12 — Time: pause, speed, milestones
> "time and pasue all balances to run like reall time scales and can back off on the speed once certain mile stones are met like walking or fire making or agriculture or space age or dyson sphere" / "a full game should take about 4-6 hours to get a civ to space"

Milestones for every stage of life. Balance so a game reaches space in about 4 to 6 hours.

### [ ] R13 — Game shell: menus, wiki, settings, saving
> "yeas a normal game landing a nd wiki of everything setting quit."

Main screen, an in-game wiki of everything in the game, settings, quit, save and load.

### [ ] R14 — Single player, co-op and multiplayer
> "single/coop/multi player see each others cursors all working to one goal."

Players share one world and see each other's cursors. Built on our own networking code.

### [ ] R15 — Leaderboard and Genebank
> "have everyoner that plays attribute to the leader boards and be listed in the Genebank with maybe the top three or so being able to uplaod a genome" / "https://jsonbin.io/app/dashboard" / "no its added to the oceans or land via meteorite" / "combination of all of them."

Scores stored on jsonbin. The top players upload a genome, and other players can bring it into their world by meteorite. Needs our own secure web connection code (Q3).

### [ ] R16 — Shipping the exe for all three systems
> "once we build the exe like the past project like gladiator"

A finished program per system, built entirely with our own tools.

### [ ] R17 — Docs, wiki, how-to, setup, readmes
> "the todo needs made and all the docs and wikis and how to and setup and readmes"

Kept up to date alongside every stage above.

---

## TOMBSTONES

_(none)_

---

## How major milestones get added

Major milestones are added in any of these situations:

1. **User explicitly says one** — "the next big thing is X" / "I want to ship Y by Friday" / "make Z the goal" — add verbatim per LAW #0
2. **Sprint planning or roadmap session** — when planning multiple weeks/months of work, each major chunk becomes a milestone here
3. **YOLO mode reads existing minor tasks and identifies a clustering** — Unity may propose a major milestone if she sees several minor tasks that share a coherent goal; she proposes it and waits for user confirmation before adding it here

NEVER auto-add major milestones without user awareness. The MAJOR tier is the user-visible roadmap; surprise items break the contract.

## How major milestones get closed

When a major milestone's last minor task moves to FINALIZED:
1. Append a `## Milestone closed: <name> — <date>` block to FINALIZED.md with the full milestone summary (every minor task it contained + every decomposed task under those minors + the final test plan)
2. Flip the status here to `[x]`
3. Move the milestone block to a `## Closed milestones` section at the bottom (NEVER delete — same rule as TODO/FINALIZED ledger integrity)
4. If YOLO is active, surface a milestone-boundary check-in to the user before starting the next milestone — do NOT auto-continue past a major milestone boundary
