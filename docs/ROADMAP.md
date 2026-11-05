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

- **R17 — Docs**: the full docs set is written. What's left is keeping it current as the game gets built (see `docs/TODO.md`).

---

## Pending

Every stage quotes the creator's words (see `docs/DESIGN.md` for the full record). Order goes top to bottom: each stage needs the ones above it. All of the creator's questions are answered (rounds 1–5 in `docs/DESIGN.md`). Nothing is blocked. **R17 (docs) is in progress**, and **R1 is next to build.**

### 🚦 Gate before code
> "topp -100 lists go only after a /superreview and/unity check that evrything ive said is proeprly done in all todo and docs everywhere as we havent done any code yet and are still do ing docs and making the work laws to use the fucking docs to write code when we do write code so todo needs work"

✅ **Passed:** both the `/super-review` and the `/unity` check. Code starts now on the road to the samples, and the top-100 lists come after ("Samples path now, lists later (Suggested)"). All code follows `docs/WORK-LAWS.md`: **the docs are the spec**.

### [ ] R1 — Our own build tools (language + compiler), from nothing
> "we make it" / "No dependancies!!!! ... OR COMPOLIERS"

Nothing else can be built until this exists. The first piece has to be written directly as raw machine bytes, because there is no outside tool to translate it. Each later tool gets built with the one before it, until we have our own language, **Helix**, and a compiler that makes Windows exes.

- **Windows only for now** ("windows only for now"). Mac and Linux come later.
- The chain: a hex loader typed as raw bytes → assembler → first Helix compiler → Helix compiler written in Helix, which builds itself.
- Helix is low-level and C-like, with manual memory and no garbage collector.

### [ ] R2 — Talking to Windows directly (Mac and Linux later)
> "all three" / "nothing of this game runs in a html its its own system and program"

Opening a window, keyboard and mouse, sound, files, the clock, threads, network. Windows first, with no outside libraries. The only outside software allowed is Windows itself and the graphics card driver ("sure").

### [ ] R3 — Engine core
The base every system sits on: math (vectors, matrices, noise, random numbers that repeat from a seed), memory handling, the "arrays" that hold billions of things, multithreading, saving data to disk.

- Big memory blocks reserved up front per system.
- One worker per CPU core, pulling jobs from a shared queue.
- 64-bit positions with a moving origin.
- Our own seeded random generator.
- Save files in our own versioned binary chunks.
> "the arrays"

### [ ] R4 — AAA 3D graphics, built from scratch
> "ut will be a AAA game graphics that can do all the microbes and viruses an d gene mapings and peices for every traight and skill and evolutionary thing like breathing air to walking upright to photosynethesiis" / "3d top down world with god mode space mechanics"

Talking to the graphics card per system, lighting, shadows, water, sky, terrain, creatures, zooming from a single microbe up to a whole galaxy.

- Uses **Direct3D 12**, which is part of Windows.
- Also covers our own **3D image generator** and the **auto-capture tool** (a mode of the game exe) that renders every image the docs need: "we will need to design the #D genrator and botting of them".

### [ ] R4.4 — Live generators (no pre-built assets)
> "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living"

Every asset is built live, on the fly, from data arrays, while life evolves: creatures, plants, microbes, cities, tech, space tech, planets, stars, galaxies, resources, elements, weather and sound. Nothing ships pre-made. Spec: `wiki/systems/r04b-generators.md`.

### [ ] R4.5 — Art style samples (before any final art)
> "things like ingame microbes viruses and intelligernt creature should all have samples 1,2,3 made in 3 diffeernt styles 1,2,3 for a hand ful of things in the game before we go with any one style" / "try a rimworld like and a, populous like, and a stardew like"

The live generators (R4.4) make 3 examples of each subject, each drawn in all 3 styles (RimWorld-like, Populous-like, Stardew-like). That's 9 pictures per subject. The creator picks one style before any final art is made. The style has to stay light on the graphics card, because thousands of living things move and spread at once. This starts right after R4's basic renderer works.

- **Live viewer:** our own program in its own window. Pick a subject and see its 9 samples moving and rotatable ("Live viewer, moving (Suggested)").
- **Direct3D 12** from day one ("Straight to Direct3D 12"), with our own shader-code generator.
- **Procedural:** every sample is generated from a seed ("and remember thses things are get dynamic procedural generations"). Built things follow the species that builds them: "ie insect species has insect spaceships ansd insect cities".

### [ ] R5 — The universe: stars, planets, solar systems, galaxies
> "stars+planets>solar sytems> galaxys" / "obviously galaxy size to buidl dyson spheres and ingalactic war"

Random generation from a seed, at full galaxy scale, with more than one galaxy.

- Galaxy sizes: Small, Medium, Large or Massive.

### [ ] R6 — The planet: Earth and unlockable templates
> "EARTH(but other templets too you can unlock after reaching certain points on Earth." / "full massive wolrd with everything(when i say everything i fucking mean everything) that a wrold has on it like earth"

Ground, rock, plates, mountains, oceans, rivers, ice, air, weather, seasons, day and night, chemistry, minerals. "Everything" is the **top 100 of every category**, from `docs/LISTS.md`: 10–20 real seeds each, with the rest invented live (round 14) ("do top 100 for everything..."). Entries are "real and dynamic invented combo procedurally".

- All 118 elements. Lab-made ones (93+) unlock once a civilization creates them.
- The surface is split into equal-size cells on a sphere.
- 100 planet templates. The first to unlock are Ocean, Desert, Ice, Jungle, Volcanic, Tidally locked and Gas-giant moon.
- On other templates, stages and ages can come in their own order.

### [ ] R7 — Real-style genomes
> "Real-style" / "genome of all creatures liveing and even viruses"

Real-style DNA that builds each body and behavior, with mutation, breeding (with or without sex), and viruses that change genes. A "gene map" showing which pieces give which trait or skill.

- DNA is stored at 2 bits per letter.

### [ ] R8 — Life: viruses, microbes, plants, insects, animals
> "fully dynamic ggenology with microbes and viruses and plants and animals and insects" / "they will be like ants ais with genes that use thatev eveold or been infected with u know EVOLUTION"

Each creature acts on its genes, like an ant. Crowds of the same kind are grouped by where they live ("1 worm can be 4396 worms"). Food chains, disease, extinction, new species.

- 100 stages of life (`docs/wiki/stages-of-life.md`), each with its own milestone.
- One group per cell and species. A group splits into a new species when its genes drift, and individuals appear when you zoom in.
- **Layered mini AIs:** individual → group → hive → species → galactic mind. They're selfish-gene driven, death always comes, and bigger steers smaller. A layer only appears when evolution builds it, and brainless life acts through chemistry ("and we will have to make the mini ai's that are individual, species, hive, glactic mind like typ dynamic control ai of what they are suppose to so selfish gene like all life tries to get more and do more before death but life is life death comes").
- **All things flock together:** every kind climbs the 100-step grouping ladder, and any line can reach intelligence ("and all things flock togther meaning intigenct>tirbes>villages>towns>cities wolf>pack>horde>intelligence>humanoioidwolf but more steps with all things do you get what i mean by all things").
- Mixed species can team up, tame, farm, rule or merge ("1 yes just like real life, one species can subjegate another in ever way possible this is a 18 plus game because it has all type of things form sex to killing and murder").
- The tree of life: circular family and gene trees, sortable by planet, system or galaxy, with every rank tracked to 100 ("option 1, every thing blows up to 100 ,, nutiorents, elements, species, the whoel kingdom philum class gensis species thing is all 100 tracking with the whole lengogy tree of life the circular type ones for the heretitary and the genr tree of lifee palnet or solor sytem or galaxy sortable").

### [ ] R9 — Intelligent life and civilization
> "intelligent life has technology and culture and war and tribalisze and pollitics and stuff like real life does" / "intelligent life has amazing ways of killing its self"

Tribes, culture, politics, wars, technology trees, and the ways civilizations can wipe themselves out.

- 100 ages in real-history order (`docs/wiki/ages-of-civilization.md`). A collapse can send a civilization back to earlier ages.
- The tech tree has 100 branches in 10 families. Each discovery needs the one before it in its family, plus links across families.
- 100 kinds of society (`docs/wiki/society.md`). 100 ways civilizations end.
- Intelligent life gradually takes over harvesting for the player, in 6 steps (`docs/wiki/tech-tree.md`).

### [ ] R10 — Space expansion up to Dyson spheres
> "space expansion to dysonsphere" / "to get to other planbets intelligent life has to get there first" / "ingalactic war"

Space travel, spreading life to other planets, colonies, Dyson spheres, war across galaxies.

- Genebank genomes arrive by meteorite, from the first-life stage on.

### [ ] R11 — God mode
> "think populous form back in the day but gneological twists" / "only every nudge evrything but u get the power of god to basicly do everything peopel attribute god doing." / "no direct edit besids using powers to influence things to happen" / "points are amount of life in your galaxie(S!)"

Powers that help or hurt life, unlocked over time with points. Gene powers unlocked later have good or bad outcomes. Life starts from your first action. "THIS IS NOT A MOBILE CLICKER".

- 100 powers (`docs/POWERS.md`, approved: "Use them as is").
- Paid in **Souls** ("Souls"). Cost = tier × area × strength, with 100 tiers (T1–T100, ranked weakest to strongest). T81–T100 have cooldowns.
- 100 unlock points: the 100 stages of life. Every power unlocks at exactly one stage, and the 5 starters at stage 1.
- Harvesting is slow and scares intelligent life if they see it. Disasters that kill many give lots of souls ("a volcano kicking off a city will make lots of souls").
- **Dark Harvest:** 100 cruel methods (madness, plague, war, cruel laws, sacrifice, slavery, environment, gene weapons, machines, world grinders), each gated by age and matrixed by era ("i dont think you properly build the systems in for having the intelligencences harvets life for you in the most exteme 18 plus ways from driving animals mad, or probing viruses to kill better, or influsing politice to out law abortion and decrimalize rape...ect ect etc these are just a small amoutn of examples and needes the proper 100 treatment matrixed throught the full game and docs").
- Starting toolkit: lightning, heat (fire), cold, water, spark of life, add genes, and add elements from the full periodic table.

### [ ] R12 — Time: pause, speed, milestones
> "time and pasue all balances to run like reall time scales and can back off on the speed once certain mile stones are met like walking or fire making or agriculture or space age or dyson sphere" / "a full game should take about 4-6 hours to get a civ to space"

Milestones for every stage of life. Balance so a game reaches space in about 4 to 6 hours.

- Pause plus 5 speeds: 1×, 10×, 100×, 1,000× and max.
- 100 milestones per stage (10,000 in all), built from the top-100 lists ("Built from the 100 lists"). Only each stage's headline milestone drops time to 1× and plays a cinematic ("Only each stage's big one (Suggested)"). The cinematic can be turned off.
- Each stage has its own years-per-second.

### [ ] R13 — Game shell: menus, wiki, settings, saving
> "yeas a normal game landing a nd wiki of everything setting quit."

Main screen, an in-game wiki of everything in the game, settings, quit, save and load.

- Main menu: New World, Continue/Load, Multiplayer, Genebank + Leaderboard, Wiki, Settings, Credits, Quit ("all of the above and wiki and settings, i already told you this and credits").
- Autosave every 10 minutes and at every headline milestone. Unlimited slots.
- Settings groups: Graphics, Sound, Controls, Gameplay, Online, Accessibility.
- The in-game wiki reads the same pages as `docs/wiki/`.
- Open-ended: "open ended till u resarts to get a new world and galaxy".

### [ ] R14 — Single player, co-op and multiplayer
> "single/coop/multi player see each others cursors all working to one goal."

Players share one world and see each other's cursors. Built on our own networking code.

- One player's computer hosts ("yes").
- The host shares a join code. Up to 8 players.
- Our own reliable messaging over UDP.

### [ ] R15 — Leaderboard and Genebank
> "have everyoner that plays attribute to the leader boards and be listed in the Genebank with maybe the top three or so being able to uplaod a genome" / "https://jsonbin.io/app/dashboard" / "no its added to the oceans or land via meteorite" / "combination of all of them."

Scores stored on jsonbin. The top players upload a genome, and other players can bring it into their world by meteorite.

- Score = life in your galaxy + life harvested ("life in your galaxy and life \"havested\"").
- jsonbin is the only outside service, behind a swappable **side car**: its own small program on the same computer ("lets keep it as a side car so if needed we can pop something else in there.").
- Our own TLS 1.3 secure connection, written from scratch.
- The Genebank top three is locked in weekly ("Genome of the Week").

### [ ] R16 — Shipping the Windows exe (Mac and Linux later)
> "once we build the exe like the past project like gladiator"

A finished Windows program, built entirely with our own tools.

- Our own `DNA-AND-Setup.exe` installer, posted on release pages on both git.unityailab.com and GitHub ("should be a release page on both with link to it in the readme first thing").
- Uninstall keeps saves and asks whether to delete them.

### [~] R17 — Docs, wiki, how-to, setup, readmes
> "the todo needs made and all the docs and wikis and how to and setup and readmes"

Kept up to date alongside every stage above.

> "okay we need to write all the wikis and docs and readmes for the entire project in full to complete final endgame and play through completely in full no text wealls easy and fun to read beautifully written and well designed and formated with tool tips and everything like a AAA game using all the icons and images of the game in the docs with links and relseas link at top of readme and everything as if the game was full and complete wee will make everything but the geam itself yet"

The full set is written: the player wiki, the builder wiki, setup, the build guide and the README. Still open: filling the 55 image slots (needs R4), and keeping every page true as real code lands.

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
