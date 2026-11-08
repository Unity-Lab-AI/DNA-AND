# 📜 Builder Wiki Log

Append-only. Newest at the bottom.

## 2026-11-05 — Wiki created
- Scaffolded the builder wiki: `CLAUDE.md` (schema), `index.md`, `log.md`.
- Added one draft page per roadmap stage R1–R17 under `wiki/systems/`, based on `docs/DESIGN.md` rounds 1–4, `docs/ROADMAP.md` and `docs/TODO.md`.
- Every page is `status: draft`, `last-verified: none (plan only)`. No game code exists yet.
- Undecided design choices are marked 💬 and logged in `docs/suggestions/D.md`.

## 2026-11-05 — Decisions applied

- All 47 open suggestions were decided (see `docs/DESIGN.md`, round 5), and every 💬 mark across the wiki and docs was replaced with the final decision.
- The language is named **Helix**.
- Pages stay `status: draft` because no code exists yet.

## 2026-11-05 — Task ledger made proper

- `docs/ROADMAP.md`, `docs/TODO.md`, `docs/DECOMPOSED.md` and `docs/FINALIZED.md` now reflect every decision. Finished tasks moved to FINALIZED word for word. R1's first task is broken into 7 steps.

## 2026-11-05 — Art style samples planned

- Added R4.5: 3 samples × 3 styles for every subject before any style is locked in. The styles are RimWorld-like, Populous-like and Stardew-like, all light on the GPU.

## 2026-11-05 — Flocking and tree of life

- New player pages: groups-and-flocking.md (the 100-step grouping ladder) and tree-of-life.md. Life sim tasks added. The game is rated 18+.

## 2026-11-05 — Layered mini AIs

- Added the selfish-gene layered AI (chemical, individual, group, hive, species, galactic mind) to the life sim plan and the player page.

## 2026-11-05 — Full docs audit

- Cross-linked every player page, added glossary terms, and brought older pages up to rounds 6–8. Link and count checks pass.

## 2026-11-05 — Work laws

- Added `docs/WORK-LAWS.md`: the docs are the spec for all code. Every TODO section now lists its Spec pages. There is a review gate before the top-100 lists and before any code.

## 2026-11-05 — Round 12 regression sweep

- Gate order, R4.5 plan and species-driven assets brought in line across the docs.

## 2026-11-05 — Live generators

- Added R4.4: live generators, no pre-built assets (new page r04b-generators.md). Full docs correctness scan passed.

## 2026-11-05 — Content first

- Round 15: content comes before code. Added docs/CONTENT-FORMAT.md (our own text format, compiled to our own binary later). First content file: content/stars.dnat (20 real seeds, 5 rules).

## 2026-11-05 — Space in full

- Space page now has 100 space objects, impacts, the 10x10 world-condition tech matrix, a 100-step travel ladder (to hyperdrives and intergalactic), 100 space resources and 100 kinds of colony. r05 and r10 specs updated.

## 2026-11-05 — HUD

- New player page hud.md: header bar, build-your-own action bar, drag panels, element tree, painter brushes that grow as life spreads. r13 spec updated.

## 2026-11-05 — Public docs law

- LAW 15: public docs carry no work notes. Public docs cleaned, and the rule was added to wiki/CLAUDE.md, the TODO upkeep and the before-push check.

## 2026-11-05 — Research first

- LAW 16: research every topic before writing it. Added content/black-holes.dnat (researched). Re-researched stars, galaxies and solar systems, and filled the gaps.

## 2026-11-05 — Physics

- content/physics.dnat (researched): 4 forces, particles, atoms, planet interiors, tectonics, the dynamo, atmospheres. New public page forces-of-nature.md. r03 and r06 specs updated.

## 2026-11-05 — Content for every sample subject

- Researched content files: planets, gas giants, moons, small bodies, nebulas, resource fields, space tech, cities, elements (118), and life (viruses, bacteria, archaea, protists, fungi, plants, animals, extinct life, intelligent creatures).

## 2026-11-05 — Scope sync

- Every spec page lists its content files. Added 23 TODO build tasks, 6 new player pages, README scope sections, the LISTS "Seeded in" column and 4 generators.

## 2026-11-05 — First code

- Round 20: the game is written in C# and compiled by Windows' built-in csc.exe. build.cmd plus the first window (our own Windows calls) works. r01 is now verified.

## 2026-11-05 — Engine steps 1–5

- Input, clock, watchdog, test runner, math/RNG/noise, content loader, Direct3D 12, billboard pipeline, galaxy generator and the live sample viewer (set 1, galaxies).

## 2026-11-05 — Deep zoom

- Click a sample galaxy to open it full-window. Zoom from the whole galaxy down to the core black hole (shadow, photon ring, disk, jets), clusters and nebulas, and any star's generated system. Positions are kept relative to the focus (moving origin).

## 2026-11-05 — Calm motion

- Round 22: on-screen motion is capped while the sim runs at full speed, and fast bodies become trails. Time controls are in the deep zoom.

## 2026-11-05 — Sample sets 1–4 approved, art style locked

- Populous-like is the art style for everything. The RimWorld-like and Stardew-like styles were dropped, and their code removed.
- Sets 1–4 (galaxies, solar systems, stars, planets with gas giants) are approved. The keeper pictures are in `docs/samples/`.
- The renderer now has depth, solid faceted ground, glowing air, see-through clouds and water, and planets with detail from orbit down to micrometres.
- `r04-renderer.md` is verified against the code.

## 2026-11-05 — Sample sets 5 and 6 approved

- Set 5: resource fields placed by real geology on every world, with a resource view.
- Set 6: one Dyson sphere, with an industrial hull outside and a terraformed landscape inside around its star. You can go in and out, and fly freely inside.

## 2026-11-05 — Sets 7 and 8, and the journey

- Set 7: the elements as game icons, with melting and boiling points and temperature and pressure controls, plus the air of every world.
- Set 8: a live microbe ecosystem.
- The journey: one continuous zoom from galaxies to microbes and back. You can click any star and any planet. There are real sizes and no popping. Sea life is shown at true scale.

## 2026-11-05 — Staleness check

- The pages for the toolchain, Windows layer, engine core, universe, planet, life sim and god mode now record what is built. Status is partial where built, and verified against the current code.
- The public docs are updated too: controls (Ctrl + wheel, zoom where the mouse points, Tab, R, N, I, C, M), powers (lightning restarts life), elements (heat and pressure, the icon looks), resource fields (the resource view, species use resources, life-made fuels), Dyson spheres (the world inside), and the air of each world.

## 2026-11-05 — Body builder

- r04b generators: the body builder, bodies from genes, and what intelligent species build (`BodyGen`, `GeneBody`, `CivGen`). Status is now partial.

## 2026-11-05 — Planet life on land

- r08 life sim: one shared life sim per planet, the stage ladder with each stage competing only with its own kind, and the land life drawn from the evolved species.

## 2026-11-06 — Colours and thicker plant life

- r04b generators: species skin colour kept strong and bright; builds carry the builders' colour.
- r08 life sim: plants on land grow in thick stands.
- r08 life sim: the planet's own sea animals swim at true size from their genes (`SeaBodies`).
- r08 life sim: a test jump to land where an intelligent species lives; its village seen on the ground.
- r08 life sim: intelligent species go step by step after communication (wild, tribes, villages, towns, cities, space), shown on land.
- r04b generators: build sample keys 1-5 compare the five ways of building; everything is built on the fly when first seen.
- r05 universe: every shining star kind can appear; colour from surface temperature, brightness from size and temperature.
- r04 renderer: render scale, 50 to 200% of the window, from a menu.
- r04 renderer: our own GIF clip writer; a first journey clip saved.
- r04 renderer: the 10-second journey clip, galaxies to the ground.
- r03 engine core: the job system, one worker per core on a shared queue.
- r03 engine core: the save file format (versioned chunks) and the life chunk.
- r08 life sim: a food web per region (producers, grazers, hunters, nutrients); spreading follows conditions.
- r08 life sim: research review of what real life does; a real-life checklist added; TODO R8b; player page docs/wiki/ecology.md.
- r06 planet: the living planet written out in full (TODO R6b); table of what runs today and what does not.
- r06 planet: the planet's inside runs on its clock (heat, core, plates or lid, magnetic field and flips), and the field shields land life.
- r06 planet: plates, edges, earthquakes and eruptions on the planet's clock; disasters kill life.
- r06 planet: the air and climate (carbon-silicate cycle, great oxidation, methane, escape, runaway and snowball), feeding life.
- r06 planet: water and weather per region (temperature, rain belts, ice, ice ages, sea level, hurricanes, seasons), feeding life.
- r06 planet: ocean currents, upwelling and tides, feeding sea life.
- r06 planet: erosion, soil, river mud and the rock cycle per region, feeding life.
- r06 planet: impacts at real rates with the heavy bombardment, and the brightening star; deep microbes survive.
- r06 planet: researched content for geology (edges, faults, quakes, erosion, soil, rock cycle) and climate (water cycle, circulation, weather, currents, tides, ice ages, Köppen zones).
- r06 planet: the visible ground follows the sim (sea level, mountain heights, craters), remade a few pieces at a time.
- r06 planet: continents drift with their plates (ridges and mountains at edges). r04: --native checks at the screen's own resolution.
- r04 renderer: the coefficient waveform engine (CDF 9/7, from Uni Vs Matics) ported to the core; plan to use it everywhere (TODO R4.6).
- r04 renderer: mode-E endless detail ported (identical to the lab's fractalize.ts).
- r06 planet: whole-planet swap for ground changes at big moments; r04: ground detail true to screen resolution.
- r05/r04: dead stars kept out of young arms (pulsars rarest); the wheel glides; planet handoff at the planet view's own distance; skirts lit; planet changes as wavelet change fields.
- r04 renderer: the one-world camera begins (universe and galaxies, no handoff).
- r04 renderer: reversed float depth everywhere; the one world goes on into matter, down to atoms.
- r04 renderer: ground pieces built in the background on every core; the frame never waits.
- r04/r05/r06: everything drawn from the data, nothing named in code (crystals from published unit cells, lustre, surface things, element colours and sizes, world kinds and behaviours, star faces, galaxy layouts, Dyson builds); smooth shading; drag speed; a new universe each launch.
- r04: one grain field for the whole planet; the ground's finest detail is the top of its grains and the matter view goes into the same ones (no layer laid over the ground); nucleus by the strong and electric forces; bonds as shared electrons.
- r04/r05: 100 billion galaxies on the cosmic web with an edge; the waveform (CDF 9/7 + mode-E) drives every surface's detail; orbitals as clouds; gluon fields; star plasma; air over the ground; microbes; tours (content/tours); Universe.exe and its own project kit.
- r04/r05: atoms are never balls (nucleus, orbitals, electrons on tilted 3D paths); the 1s cloud inside the atom; tilted orbits for planets and moons; far galaxies clickable; sand grains per pixel on the card; zoom carries on into the matter.
- r04: the grain you are on keeps its 3D body; its fine face in round rings over it (no flat square slab).
- r04: one ground all the way down to a grain's face (the matter view's own grains removed); face detail in double precision on the card; every card wraps short and sits beside the pick; GPU buffers freed eight frames late (device-removed crash).
- r04: the tour told as a story with reading time, ending on a quark; tour on/off (View, T); screenshots (F12, PNG) and clips (F9, MP4 via Media Foundation); --record films the tour.
- r04/r05: atoms in a crystal are element-coloured nucleus points in their lattice places, electrons and orbitals only round the atoms near the view (no fog); the view rides with a picked electron; a grain's face from one coarse shape with all fine detail per pixel (no seams), no glints along triangle edges, no sheet among the atoms; ground colours shade into each other; tour: a second world and a second star system, looks own the view at the ground, pauses look around except deep in matter, captions with a live line; neutron stars with real masses and nuclear density; matter entered a third of a grain up.
- r04/r05: GPU first, the web's light sent once and sifted per point on the card; galaxies made on the background workers, drawn with as many stars as their size on screen, let go far behind you (55-58 fps on the pull out, from 4); the web's points as bright as the galaxies at the hand-over, kept by density squared (strands bright, voids dark); zoomed-out view size from how far out you have pulled; tour paced by frame rate and ground building, waits for the web, opens on home; start-up allowed a minute.
- r04/r05: black holes feeding (accretion disc from 3 to about 60 Schwarzschild radii, hot inside, some with jets) or quiet, by shares in content/bodies.dnat; tour visits a black hole (a feeding one first), an asteroid and a comet, circles in on star systems, looks up at the sky by the world's own horizon; no star called by our own star's name (stars are stars); viewer pages as formatted text; galaxies you go to made at once.
- release: Universe 0.1.0 as its own project (Forgejo GFourteen/Universe, GitHub Unity-Lab-AI/Universe, installer and zip); DNA-AND 0.1.0 (universe preview) on both its remotes; each project its own name; landing page before the universe is made.
- 2026-11-07: the content reader keeps an entry through its comment lines (a blank line ends it): the two black-hole places of bodies.dnat had lost every line to a comment under their header, so no black hole was ever made; the small bodies, rogue planets and stars carry their make-up (small-bodies.dnat mix/makeup/density/albedo/belt-share, stars.dnat makeup), dived into at their surfaces; the fall through a black hole's horizon to the singularity, its gravity well drawn and graphed; supernovae from physics.dnat (@law supernova, @supernova kinds) in every galaxy, with their shells, remnants, dead stars and scorched neighbours. Pages r05-universe, r10-space, r04b-generators: the small-bodies line updated.
- 2026-11-07 (later): the shot's departure pace keeps its start in the subject's own frame and paces from what is in view (the world flickered between the glide and the start); the grain's face at nanometres: the camera's height on the drawn coarse shape, a sheet in the face's plane under it, spikes thrown out, the face's spots clickable into the lattice; one picture a level (ball-and-stick to a nanometre, nucleus and electrons below); cards kept between the tour's bars; the clip's size a View menu choice.
- 2026-11-07 (the docs audit): every document read against the build; index legend now verified/partial/draft/stale per page (r01 verified, r01a stale, r02, r03, r04b, r05, r06, r08, r11, r16, r17 partial, r04 and r04.5 verified); r16 packaging written up as built; r04's universe row brought to the whole way (WorldView to UniverseView, the edge to a horizon, the universe's menu); r05's well graph noted dropped and the fall as travel; r02's menus; CLAUDE.md's "no game code exists yet" removed.
- 2026-11-07 (the two projects): the roadmap opens with DNA-AND and Universe side by side in the creator's words (what each is, what each entails, where each lives, which docs each carries, life DNA-AND's alone); R21 added, the Universe program's own road; the Universe readme's "Still to come" and How it works brought to the same list.
- 2026-11-07 (the Universe's own workflow): the `.claude/` template installed into `Universe/` and its own DESIGN, ROADMAP (U1 to U12), TODO, FINALIZED, DECOMPOSED, WORK-LAWS and builder wiki written; the sync leaves them alone; DNA-AND's TODO cleared of every finished item (371 moved to FINALIZED word for word), the Universe's TODO holding only its docs upkeep and its own road.
- 2026-11-07 (clocks in gravity): gravitational time dilation everywhere you are: the title's own-clock-against-far-clocks line, the world card's ground slowing and satellite gain, the fall card's far watcher (DESIGN 577); the well a complete dilation cone from the mouth to the point at every depth (DESIGN 576).
- 2026-11-07 (the word swept, the Universe's road built): our own star's name swept out of content, code, wikis and pages (DESIGN 578); every world's mass (579); the gravity switch (580); frame dragging, redshift and contraction (581); a star's layers (582); decay before your eyes (583); the tour's end figure (584); the clip's text scaled (585); duplicated code from reran scripts cut back (586).
- 2026-11-07 (the clip's size on the landing page, its text measured right): DESIGN 587; `Launcher.cs`, `Program.cs`.
- 2026-11-07 (the cards short again): every line added today one clause, no card more than two thirds of the screen (DESIGN 588).
- 2026-11-07 (the face and the ride, from the creator's shots): the spots on the face's coarse shape, the ridden electron drawn from the pick, the face matte and grey at the nanometres, spikes a step off (DESIGN 589).
- 2026-11-07 (no balls, no sticks): the lattice's level the quantum picture, electron clouds and shared electrons; the stick routine gone (DESIGN 590).
- 2026-11-07 (the well an inward spike, from the creator's shots): DESIGN 591.
- 2026-11-07 (looking round the well as you fall): DESIGN 592.
- 2026-11-07 (the patterning found; the way into a hole not its size): DESIGN 593, 594; `StarGen.cs`, `UniverseView.DrawFallGrid`, the fall's card.
- 2026-11-07 (the funnel's walls there to be seen): DESIGN 595.
- 2026-11-07 (the cone travelled down, the walls closing in): DESIGN 596.
- 2026-11-07 (millions of light-years from the horizon to the singularity, a law): DESIGN 597; `@law hole-inside`.
- 2026-11-07 (the way in by mass; the fall's pace): DESIGN 597, 598.
- 2026-11-07 (the tour looking round the funnel): DESIGN 599.
- 2026-11-07 (every hole's photon ring): DESIGN 600.
- 2026-11-07 (the well rings and straight spokes, the look up then down): DESIGN 601.
- 2026-11-07 (the fall's grid at every scale): DESIGN 602.
- 2026-11-07 (the disc's far side over the rim, the horizon stop pulled back): DESIGN 603.
- 2026-11-07 (the sheet round, the fall a travel first): DESIGN 604.
- 2026-11-07 (the tour's pans eased in, the grid gone after the hole, the text rimmed and warm): DESIGN 606-608.
- 2026-11-07 (the fireball as a star's face, every asteroid its own make-up and its elements' forges, the zoom from rest and the pan before the travel): DESIGN 609-611; `vary` on mixes, `made-in` on elements.
- 2026-11-07 (the lattice's clouds readable, the matter levels handing over by degrees): DESIGN 612.
- 2026-11-07 (every zoom ending at a crawl, chained zooms one movement): DESIGN 613.
- 2026-11-07 (the climb out of the hole looking up): DESIGN 614.
- 2026-11-07 (the remnant from the blast, every small body on its orbit, the tuner watching the processor): DESIGN 615-617.
- 2026-11-07 (the "never a ball" asides gone): DESIGN 618.
- 2026-11-07 (every nucleon its three quarks at every size, the nucleus arcs short): DESIGN 619.
- 2026-11-07 (every atom its electron shells at the lattice level): DESIGN 620.
- 2026-11-07 (the fall's card measured down the funnel): DESIGN 621.
- 2026-11-07 (the tour's fall half a minute): DESIGN 622.
- 2026-11-07 (the quarks tight from afar): DESIGN 623.
- 2026-11-07 (the singularity an uncountable mass, the nucleon's quarks never pulled apart): DESIGN 624.
- 2026-11-07 (the singularity five hundred glows): DESIGN 625.
- 2026-11-07 (the climb out as fast as the fall): DESIGN 626.
- 2026-11-07 (the asteroid's caption its own make-up, its dive to a quark, the supernova logged): DESIGN 627-629.
- 2026-11-07 (the faces before the glows, the picked atom in focus, the stats card's width, the inner cloud brighter): DESIGN 630-633.
- 2026-11-07 (the orbit lines continuous, no rows of dots): DESIGN 634.
- 2026-11-07 (one dive chain for any body, the supernova pick, the lattice probe, the levels' words): DESIGN 635.
- 2026-11-07 (the comet dived like the planet and the asteroid): DESIGN 636.
- 2026-11-07 (a dive onto a small body lands on the drawn stone): DESIGN 637.
- 2026-11-07 (a spot on any small body picked by a click): DESIGN 638.
- 2026-11-07 (small bodies from their precise offsets: the belt's grid gone): DESIGN 639.
- 2026-11-07 (the pick's aim at a small body its precise offset): DESIGN 640.
- 2026-11-07 (the dive firing where the zoom reaches, matter stops never in open space): DESIGN 641.
- 2026-11-07 (the ghost discs gone, the dive's state logged): DESIGN 642.
- 2026-11-07 (the near plane beside a body, every rock a kind): DESIGN 643.
- 2026-11-07 (a young supernova's ejecta a ballooning cloud): DESIGN 644.
- 2026-11-07 (the small bodies' descents in the world's layers, the atom chain once, the tour quicker): DESIGN 645.
- 2026-11-07 (the one zoom chain on every body, nothing left out): DESIGN 646.
- 2026-11-07 (a small body a world: one ground system for every body): DESIGN 647; `@terrain asteroid`, `relief-of-radius`.
- 2026-11-07 (the dive's arcs and orbits short, no pauses): DESIGN 648.
- 2026-11-07 (the crash at the asteroid's ground fixed; every small body its own size and shape): DESIGN 649, 650.
- 2026-11-07 (the landing page's seed field takes the typing, no refusing beep): DESIGN 651.
- 2026-11-07 (the small body's ground by distance, the supernova target live): DESIGN 652, 653.
- 2026-11-07 (the small body's ground centred after the ride, no shaking): DESIGN 654.
- 2026-11-07 (rule: the tour is a gimmick over the program's own code): DESIGN 655.
- 2026-11-07 (a spot on the face at height nothing: the lattice to the nucleus no longer black): DESIGN 656.
- 2026-11-07 (all bodies: the star's, the giant's, the neutron star's and the nebula's chains on the tour): DESIGN 657.
- 2026-11-07 (the public pages read in full and mended): DESIGN 658.
- 2026-11-07 (the dive atom drawn and pickable at every size in gas and plasma): DESIGN 659.
- 2026-11-07 (the shot's pace eased; the ground under the camera never culled, the ground probe): DESIGN 660, 661.
- 2026-11-07 (nebulae close up as clouds of their own gas): DESIGN 662.
