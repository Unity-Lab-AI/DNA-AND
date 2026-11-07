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

✅ **Passed:** both the `/super-review` and the `/unity` check. Then round 15 changed the order: **content first** (R0), then code ("Content first, then code (Suggested)"). All code follows `docs/WORK-LAWS.md`: **the docs are the spec**.

### [x] R0 — Game content (before code): sample subjects done

Produced (researched, 1,000+ entries):
  - `content/black-holes.dnat`
  - `content/cities.dnat`
  - `content/elements.dnat`
  - `content/galaxies.dnat`
  - `content/gas-giants.dnat`
  - `content/life/animals.dnat`
  - `content/life/archaea.dnat`
  - `content/life/bacteria.dnat`
  - `content/life/extinct.dnat`
  - `content/life/fungi.dnat`
  - `content/life/intelligent.dnat`
  - `content/life/plants.dnat`
  - `content/life/protists.dnat`
  - `content/life/viruses.dnat`
  - `content/moons.dnat`
  - `content/nebulas.dnat`
  - `content/physics.dnat`
  - `content/planets.dnat`
  - `content/resource-fields.dnat`
  - `content/small-bodies.dnat`
  - `content/solar-systems.dnat`
  - `content/space-tech.dnat`
  - `content/stars.dnat`

> "well i thought we needed to write the games content before we could have something to generate in the game engine"

The content the live generators work from: seeds, ranges, parts and rules, written now in our own text format (`docs/CONTENT-FORMAT.md`) and compiled to our own binary by our toolchain later. Sample subjects first, then the rest.

### [x] R1 — Build setup: C# with the compiler built into Windows
> "Use C# built into Windows"

The game is written in C# and compiled by `csc.exe`, the C# compiler that ships with Windows (.NET Framework 4.x, C# 5). Only the core runtime and Windows' own system files are used, called directly from our code. A single `build.cmd` makes `DNA-AND.exe`. The earlier self-built Helix toolchain is retired (rounds 19–20).

### [x] R2 — Talking to Windows directly (Mac and Linux later)
> "all three" / "nothing of this game runs in a html its its own system and program"

Opening a window, keyboard and mouse, sound, files, the clock, threads, network. Windows first, with no outside libraries. The only outside software allowed is Windows itself and the graphics card driver ("sure").

### [~] R3 — Engine core
The base every system sits on: math (vectors, matrices, noise, random numbers that repeat from a seed), memory handling, the "arrays" that hold billions of things, multithreading, saving data to disk.

- Real physics underneath everything: gravity, electromagnetism, and the strong and weak nuclear forces ("dont forget the forces of nature weak and nuclear forces of atoms pysics electormagnatisim magma in plantes that arte tectonic with magnic fields and shit come one wwhat about gravity and atmosphers").
- Big memory blocks reserved up front per system.
- One worker per CPU core, pulling jobs from a shared queue.
- 64-bit positions with a moving origin.
- Our own seeded random generator.
- Save files in our own versioned binary chunks.
> "the arrays"

### [~] R4 — AAA 3D graphics, built from scratch
> "ut will be a AAA game graphics that can do all the microbes and viruses an d gene mapings and peices for every traight and skill and evolutionary thing like breathing air to walking upright to photosynethesiis" / "3d top down world with god mode space mechanics"

Talking to the graphics card per system, lighting, shadows, water, sky, terrain, creatures, zooming from a quark up to the cosmic web of galaxies.

- Uses **Direct3D 12**, which is part of Windows.
- Also covers our own **3D image generator** and the **auto-capture tool** (a mode of the game exe) that renders every image the docs need: "we will need to design the #D genrator and botting of them".

### [x] R4.4 — Live generators (no pre-built assets)
> "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living"

Every asset is built live, on the fly, from data arrays, while life evolves: creatures, plants, microbes, cities, tech, space tech, planets, stars, galaxies, resources, elements, weather and sound. Nothing ships pre-made. Spec: `wiki/systems/r04b-generators.md`.

### [x] R4.5 — Art style samples (before any final art)
> "things like ingame microbes viruses and intelligernt creature should all have samples 1,2,3 made in 3 diffeernt styles 1,2,3 for a hand ful of things in the game before we go with any one style" / "try a rimworld like and a, populous like, and a stardew like"

The live generators (R4.4) make samples of each subject, opened one set at a time. The first sets compared 3 styles (RimWorld-like, Populous-like, Stardew-like). **Populous-like won and is locked for everything** (`docs/DESIGN.md`, item 67), so the sets that remain show that style only. Sets 1–8 are approved: galaxies, solar systems, stars, planets and gas giants, resource fields, the Dyson sphere, the elements and the microbes, and they join into one zoom from galaxies to microbes. The rest follow in the order each one depends on the one before: the kingdoms of life, intelligent creatures, cities, then stations, colonies and ships.

- **Live viewer:** our own program in its own window. Pick a subject and see its 9 samples moving and rotatable ("Live viewer, moving (Suggested)").
- **Direct3D 12** from day one ("Straight to Direct3D 12"), with our own shader-code generator.
- **Procedural:** every sample is generated from a seed ("and remember thses things are get dynamic procedural generations"). Built things follow the species that builds them: "ie insect species has insect spaceships ansd insect cities".

### [x] R5 — The universe: stars, planets, solar systems, galaxies
> "stars+planets>solar sytems> galaxys" / "obviously galaxy size to buidl dyson spheres and ingalactic war"

**Content:** `content/` galaxies, solar-systems, stars, black-holes, nebulas, small-bodies.

Random generation from a seed, at full galaxy scale, with more than one galaxy.

- 100 kinds of space objects: Oort clouds, the Kuiper belt, comets, asteroid belts, nebulas, moons, black holes and the rest. Comets and meteorites really hit planets (`docs/wiki/space-and-dyson-spheres.md`, round 16).

- Galaxy sizes: Small 100, Medium 500, Large 2,000, Massive 10,000 star systems, every one fully simulated ("Fewer, every one detailed").

### [ ] R6 — The planet: Earth and unlockable templates
> "EARTH(but other templets too you can unlock after reaching certain points on Earth." / "full massive wolrd with everything(when i say everything i fucking mean everything) that a wrold has on it like earth"

**Content:** `content/` planets, gas-giants, moons, physics, resource-fields, elements.

Ground, rock, plates, mountains, oceans, rivers, ice, air, weather, seasons, day and night, chemistry, minerals. "Everything" is the **top 100 of every category**, from `docs/LISTS.md`: real seeds each, with the rest invented live (round 14) ("do top 100 for everything..."). Entries are "real and dynamic invented combo procedurally".

- All 118 elements. Lab-made ones (93+) unlock once a civilization creates them.
- The surface is split into equal-size cells on a sphere.
- Planet interiors (core, mantle, magma), plate tectonics or a stagnant lid, a geodynamo magnetic field, and layered atmospheres that can be kept or lost.
- 100 planet templates. The first to unlock are Ocean, Desert, Ice, Jungle, Volcanic, Tidally locked and Gas-giant moon.
- On other templates, stages and ages can come in their own order.

### [ ] R7 — Real-style genomes
> "Real-style" / "genome of all creatures liveing and even viruses"

Real-style DNA that builds each body and behavior, with mutation, breeding (with or without sex), and viruses that change genes. A "gene map" showing which pieces give which trait or skill.

- DNA is stored at 2 bits per letter.

### [ ] R8 — Life: every kingdom, phylum to species
> "fully dynamic ggenology with microbes and viruses and plants and animals and insects" / "they will be like ants ais with genes that use thatev eveold or been infected with u know EVOLUTION"

**Content:** `content/` life/viruses, bacteria, archaea, protists, fungi, plants, animals, extinct, intelligent.

The full tree of life: viruses, bacteria, archaea, protists, fungi, plants and animals, each down to species (`docs/wiki/tree-of-life.md`). Each creature acts on its genes, like an ant. Crowds of the same kind are grouped by where they live ("1 worm can be 4396 worms"). Food chains, disease, extinction, new species.

- 100 stages of life (`docs/wiki/stages-of-life.md`), each with its own milestone.
- One group per cell and species. A group splits into a new species when its genes drift, and individuals appear when you zoom in.
- **Layered mini AIs:** individual → group → hive → species → galactic mind. They're selfish-gene driven, death always comes, and bigger steers smaller. A layer only appears when evolution builds it, and brainless life acts through chemistry ("and we will have to make the mini ai's that are individual, species, hive, glactic mind like typ dynamic control ai of what they are suppose to so selfish gene like all life tries to get more and do more before death but life is life death comes").
- **All things flock together:** every kind climbs the 100-step grouping ladder, and any line can reach intelligence ("and all things flock togther meaning intigenct>tirbes>villages>towns>cities wolf>pack>horde>intelligence>humanoioidwolf but more steps with all things do you get what i mean by all things").
- Mixed species can team up, tame, farm, rule or merge ("1 yes just like real life, one species can subjegate another in ever way possible this is a 18 plus game because it has all type of things form sex to killing and murder").
- The tree of life: circular family and gene trees, sortable by planet, system or galaxy, with every rank tracked to 100 ("option 1, every thing blows up to 100 ,, nutiorents, elements, species, the whoel kingdom philum class gensis species thing is all 100 tracking with the whole lengogy tree of life the circular type ones for the heretitary and the genr tree of lifee palnet or solor sytem or galaxy sortable").

### [ ] R9 — Intelligent life and civilization
> "intelligent life has technology and culture and war and tribalisze and pollitics and stuff like real life does" / "intelligent life has amazing ways of killing its self"

**Content:** `content/` cities, life/intelligent, resource-fields.

Tribes, culture, politics, wars, technology trees, and the ways civilizations can wipe themselves out.

- 100 ages in real-history order (`docs/wiki/ages-of-civilization.md`). A collapse can send a civilization back to earlier ages.
- The tech tree has 100 branches in 10 families. Each discovery needs the one before it in its family, plus links across families.
- 100 kinds of society (`docs/wiki/society.md`). 100 ways civilizations end.
- Intelligent life gradually takes over harvesting for the player, in 6 steps (`docs/wiki/tech-tree.md`).

### [ ] R10 — Space expansion up to Dyson spheres
> "space expansion to dysonsphere" / "to get to other planbets intelligent life has to get there first" / "ingalactic war"

**Content:** `content/` space-tech, small-bodies, resource-fields, black-holes.

Space travel, spreading life to other planets, colonies, Dyson spheres, war across galaxies.

- Genebank genomes arrive by meteorite, from the first-life stage on.
- Worlds of every makeup: a 10 × 10 matrix of conditions, each level needing its own tech. Travel climbs a 100-step ladder from rockets to hyperdrives to intergalactic travel. Plus 100 space resources and 100 kinds of colony (mining outposts, prison colonies and more), round 16: "remebr space has all kind sof things for ort clouds to comets and merots hitting planets and astorid belts and nebulas andplanets of all makes ups with temp ranges that need different techs and inter solarystem flt hyperdrives for glaaxy to galaxty ect ect all planed out in all docs" / "resources and colinatzitions of other planets mining outsposert prison coloniers all of it".

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
- **A non-intrusive HUD:** a slim header (settings, wiki, souls, counters, time), a build-your-own WoW-style action bar, click-to-open and drag-to-move panels, an element tree, and painter brushes whose sizes unlock as life spreads (round 18, `docs/wiki/hud.md`).
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

### [x] R16 — Shipping the Windows exe (Mac and Linux later; on Linux now through Wine, with `linux/run.sh` in the zip and `docs/release/UNIVERSE-LINUX.md`)
> "once we build the exe like the past project like gladiator"

A finished Windows program, built entirely with our own tools.

- Our own `DNA-AND-Setup.exe` installer, posted on release pages on both git.unityailab.com and GitHub ("should be a release page on both with link to it in the readme first thing").
- Uninstall keeps saves and asks whether to delete them.

### [~] R17 — Docs, wiki, how-to, setup, readmes
> "the todo needs made and all the docs and wikis and how to and setup and readmes"

Kept up to date alongside every stage above.

> "okay we need to write all the wikis and docs and readmes for the entire project in full to complete final endgame and play through completely in full no text wealls easy and fun to read beautifully written and well designed and formated with tool tips and everything like a AAA game using all the icons and images of the game in the docs with links and relseas link at top of readme and everything as if the game was full and complete wee will make everything but the geam itself yet"

The full set is written: the player wiki, the builder wiki, setup, the build guide and the README. Public docs carry no work notes (WORK-LAWS LAW 15). Still open: filling the 55 image slots (needs R4), and keeping every page true as real code lands.

---

### [~] R18 — Every body landed on and zoomed into, with its real make-up
> "ie i should be able to find a gold astroid or nickle being most common ect ect and zoom infinated on any body or abject" / "stars shoiuld be mostly hydrogen till getting to it heavey m,etal cores ect ect"

Every body a place to land and zoom into without end, as worlds already are, with its real make-up from a template: asteroids by class (stony, carbonaceous, metal of iron and nickel with gold and platinum in their real traces), comets' ice and dust, moons' regolith, stars mostly hydrogen and helium down through their layers to the heavier elements at the core, neutron stars, the gas of nebulas. DESIGN 486. Status 2026-11-07: the small bodies (asteroids by class with their real traces, comets, Kuiper bodies, rogue planets), the stars' surface make-up and plasma, and the neutron star's crust are built (DESIGN 546); left: a star's layers down through to the heavier elements at its core.

### [x] R19 — Black holes: the shadow by the law, the disc bent by the hole, the fall to the singularity down the gravity well
> "black holes tho will be a special case to fall into event horzon in pitch black till u get to the singularity of near infinate quarks in one smallest space singularity" / "FUCK LOOK UP A BALCK HOLE GRAVITY WELL AND WHAT IT LOOKS LIKE"

Every hole's shadow black at the size the law gives from where you stand; the disc a thin sheet, its far side arched over the shadow, its captured light bent round the shadow's edge, lopsided by its beaming; a supermassive feeder tearing several stars at once; the approach down to the horizon with the disc all round; the fall through it, black, the gravity well round you as a grid from the metric (Flamm's surface outside the throat, its inner surface to the point), in looking back at the horizon's ring, the view turning over on the way down; the escape speed, your own time left and the tide on the card; the singularity at the quarks' size. Built 2026-11-07 (DESIGN 547, 548, 553, 554, 555, 557, 558, 559, 560; the fall as travel on the law's own clock, the funnel to the point and the cards in the dive, 562 to 568). Status note of R18 below: the make-up of the small bodies, the stars' surfaces and the neutron star's crust came with it (DESIGN 546); what is left of R18 is a star's layers down to its core.

### [x] R20 — Supernovae: every galaxy's own, of every real kind, with what they leave
> "and what about super novas and nurton star creation and black hole creations and shit should super novas be very bright seen far away like 100s of time brighter than stars, check the math do the research , make it happen" / "like solar systems should be wiped out if a super nova"

Every galaxy's supernovae from its seed and the clock (content/physics.dnat, researched rates and light curves): six kinds in every stage, each as bright as physics says from where you stand, the fireball at its real size in the colour of its heat, the shells seen only from near in the real colours of their gas, the neutron star, black hole or magnetar born at the middle, the star that died gone with its worlds, its neighbours scorched; the tour's flyby. Built 2026-11-07 (DESIGN 550, 553, 557).

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
