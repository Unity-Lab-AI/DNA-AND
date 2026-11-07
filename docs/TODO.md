# TODO — Minor Task List (Active Tasks)

**Tier 2 of 3** — the MINOR task list. Holds active tasks (pending + in_progress) at the day-to-day work grain. Each minor task lives under a major milestone in `docs/ROADMAP.md` and decomposes further into entries in `docs/DECOMPOSED.md` when YOLO mode picks it up.

Completed tasks move to `docs/FINALIZED.md` per `.claude/CONSTRAINTS.md §FINALIZED BEFORE DELETE`. Never delete a task description; only flip status (LAW: NEVER DELETE TODO INFO).

Status markers:
- `[ ]` pending
- `[~]` in_progress
- `[x]` complete (move to FINALIZED.md immediately, never leave here)

LAW #0 reminder: every task description preserves the user's verbatim words.

⚖️ **Work laws:** every task is built from its **Spec:** pages, per `docs/WORK-LAWS.md`. If a spec is missing a detail, ask the creator first (LAW 3).

**Three-tier cascade:** ROADMAP.md (major) → TODO.md (minor, this file) → DECOMPOSED.md (decomposed). YOLO mode reads all three and works the cascade — see `.claude/commands/yolo.md` and `.claude/WORKFLOW.md §YOLO MODE`.


---

## In progress

### R17 — Docs
- [~] Write all wikis, docs and readmes as if the game were complete: "okay we need to write all the wikis and docs and readmes for the entire project in full to complete final endgame and play through completely in full no text wealls easy and fun to read beautifully written and well designed and formated with tool tips and everything like a AAA game using all the icons and images of the game in the docs with links and relseas link at top of readme and everything as if the game was full and complete wee will make everything but the geam itself yet"
  - ✅ Written: README, player wiki (`docs/wiki/`, 26 pages), builder wiki (`wiki/`), `docs/SETUP.md`, `docs/HOW-TO-BUILD.md`, `docs/img/README.md`, `docs/WORK-LAWS.md`.
  - ✅ All 47 open questions decided and applied (`docs/DESIGN.md` round 5).
  - ⏳ Left: fill the 55 image slots once R4's 3D generator and capture tool exist.

---

## Pending

### 🚦 Gate: before the top-100 lists and before any code
> "topp -100 lists go only after a /superreview and/unity check that evrything ive said is proeprly done in all todo and docs everywhere as we havent done any code yet and are still do ing docs and making the work laws to use the fucking docs to write code when we do write code so todo needs work"

- [x] Write the work laws: code is written from the docs (`docs/WORK-LAWS.md`)
- [x] Every TODO section names its **Spec:** pages
- [x] `/super-review`: everything the creator said is properly done in every TODO and doc
  - First pass found 7 issues (2 high, 4 medium, 1 low). All 7 are fixed: the missing messages are recorded, the builder specs have decided-spec sections, the playthrough covers the new systems, Heat became Fire, the LISTS index is complete, and the README numbers and TODO milestone line are updated.
- [x] `/unity` check: nothing the creator said is left out, word for word
  - All 30 recent messages are checked and recorded verbatim in `docs/DESIGN.md`. One gap was fixed: the milestone answer is now quoted in its task. **Gate passed.**
- [ ] Write the real seed entries for the top-100 lists (below). Moved after the samples: "Samples path now, lists later (Suggested)"
- [ ] Start R1 code toward the sample viewer, after the sample-subject content, following `docs/WORK-LAWS.md`: "okay now we need to do everything we need to do so you can get me those 9 none headless samples of everything from the planets to the microbes to the animals insects cities spaces stations stars and dyson spheres"

### 📦 Content (first, before code)
**Spec:** `docs/CONTENT-FORMAT.md`, `docs/LISTS.md`, `wiki/systems/r04b-generators.md`

- [x] Content first (sample subjects done): "well i thought we needed to write the games content before we could have something to generate in the game engine" / "Content first, then code (Suggested)"
- [x] Our own content text format (`docs/CONTENT-FORMAT.md`), compiled to our own binary later: "option one and and whats the most dynamic code to use? rust or type script?"
Order: galactic scale down, the same order as the sample sets.
- [x] Content: Galaxy layouts (seeds, ranges, parts, rules) in `content/` · `content/galaxies.dnat`: 20 types, 4 real examples, 10 parts, 6 structures, 10 rules. Galaxy sizes decided: 100 / 500 / 2,000 / 10,000 systems
- [x] Content: Solar system layouts (seeds, ranges, parts, rules) in `content/` · `content/solar-systems.dnat`: 14 kinds, 4 real examples, 10 zones, 10 rules
- [x] Content: Stars (seeds, ranges, parts, rules) in `content/` · `content/stars.dnat` started: 20 real seeds, 5 rules
- [x] Content: Black holes and quasars in `content/black-holes.dnat`, researched: "dont forget black holes and quasars,, i think maybe you need to research topics as you are leaving out all kinds of thhigns maybe make that a rule"
- [x] Re-research the files written before LAW 16 (stars, galaxies, solar systems) and add anything missing. Added: 7 luminosity classes, 7 star types, 7 galaxy types and a rule, 1 system type, 2 zones and 2 rules
- [x] Content: Physics (4 forces, particles and atoms, planet interiors, tectonics, magnetic fields, atmospheres) in `content/physics.dnat`, researched: "dont forget the forces of nature weak and nuclear forces of atoms pysics electormagnatisim magma in plantes that arte tectonic with magnic fields and shit come one wwhat about gravity and atmosphers"
- [x] Content: Planets in `content/planets.dnat` (researched: 23 types, 4 examples, 6 rules)
- [x] Content: Gas giants in `content/gas-giants.dnat` (researched: 9 kinds, 5 cloud classes, 6 layers, 6 features, 4 examples, 7 rules)
- [x] Content: Moons in `content/moons.dnat` (researched)
- [x] Content: Small bodies in `content/small-bodies.dnat` (researched: asteroids, comets, dwarf planets, Kuiper belt, Oort cloud, meteors, meteorites)
- [x] Content: Nebulas in `content/nebulas.dnat` (researched)
- [x] Content: Resource fields in `content/resource-fields.dnat` (researched: 16 planet deposits, 10 space fields, 6 rules)
- [x] Content: Space resources and colonies (`content/resource-fields.dnat`, `content/space-tech.dnat`)
- [x] Content: Dyson spheres, megastructures, stations, ships, drives in `content/space-tech.dnat` (researched)
- [x] Content: Space stations (in `content/space-tech.dnat`)
- [x] Content: Space colonies (100 kinds on the space page, generator rules in `content/space-tech.dnat`)
- [x] Content: Spaceships and drives (in `content/space-tech.dnat`)
- [x] Content: Cities in `content/cities.dnat` (researched)
- [x] Content: Intelligent creatures (any kingdom) in `content/life/intelligent.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Animals: every phylum → class → order → family → genus → species in `content/life/animals.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Plants: every phylum down to species in `content/life/plants.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Fungi: every phylum down to species in `content/life/fungi.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Protists: every group down to species in `content/life/protists.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Archaea: every phylum down to species in `content/life/archaea.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Bacteria: every phylum down to species in `content/life/bacteria.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Viruses: every group down to type in `content/life/viruses.dnat` (researched seeds; the game fills each rank to 100)
- [x] Content: Elements, all 118, in `content/elements.dnat`
- [ ] Then: seeds and rules for the rest of the 100 categories: 31 not seeded and 42 partly seeded (see "Seeded in" in `docs/LISTS.md`)
- [x] Content: Extinct life, dinosaurs and mass extinctions in `content/life/extinct.dnat`: "dont forget extinct animals" / "dinosars...ectectect"
- [x] Content: Physics, black holes, moons, small bodies, nebulas, resource fields, space tech, cities and elements (all researched)
- [ ] Content loader (R1): read `.dnat` text at start-up. Binary packing, if ever needed, is done by our own C# code

### Lists
**Spec:** `docs/LISTS.md`

- [ ] Fill each approved category with its top 100 entries — "real and dynamic invented combo procedurally"
  - Real seed entries per category, written by hand (no fixed number): "Real seeds, game fills to 100 (Suggested)" (`docs/LISTS.md`).
  - The live generators invent the rest, so each category reaches 100 in play.
- [ ] Procedural generator that invents new entries on top of the real ones

### R1 — Build setup (C# built into Windows)
**Spec:** `wiki/systems/r01-toolchain.md`, `docs/HOW-TO-BUILD.md`

- [x] `build.cmd`: run Windows' own `csc.exe` over `src/` and write `build/DNA-AND.exe`: "Use C# built into Windows". Builds clean with warnings treated as errors
- [~] Source layout: `src/platform`, `src/core`, `src/render`, `src/gen` and the rest, one folder per system (started: `src/Program.cs`, `src/platform/`)
- [x] First proof: the exe opens its own window and closes cleanly (`DNA-AND.exe --smoke 1500` exits 0)
- [x] Content loader: read the `content/*.dnat` text files at start-up (`src/core/Content.cs`, loads 1,007 entries)

### R2 — Talking to Windows
**Spec:** `wiki/systems/r02-windows-layer.md`

- [~] Windows: window, keyboard, mouse, sound, files, clock, threads, network
  - [x] Window: our own Windows calls (`src/platform/Win32.cs`, `src/platform/Window.cs`)
  - [x] Keyboard and mouse, frame clock, file reading (`src/platform/Input.cs`)
  - [x] Safety: in-game watchdog (`src/platform/Watchdog.cs`) and safe test runner (`test.cmd`)
  - [x] Keep-or-revert prompt for look-around tests (`--try`, `src/shell/KeepBar.cs`): "well i cant reall use the tests because you never put a revert keep option with tine out they just have  a 3 sec timer out so i dont hev time to judge anything"
  - Only Windows itself and the graphics card driver may be used ("sure").
- [ ] (Later) Mac: same list
- [ ] (Later) Linux: same list

### R3 — Engine core
- [ ] Physics core: all four forces drive the sim (gravity, electromagnetism, strong, weak), per `content/physics.dnat`
**Spec:** `wiki/systems/r03-engine-core.md`
- [ ] Build from `content/elements.dnat`: all 118 entries (Main) and these rules:
  - [ ] element-protons: an atom's proton count is its element number; neutrons give isotopes (content/physics.dnat)
  - [ ] element-paint: any natural element can be painted into the world from the element tree; lab-made ones (93 and up) unlock once a civilization first creates them
  - [ ] element-chemistry: elements combine by their chemistry into compounds, minerals, air and the molecules of life
  - [ ] element-abundance: hydrogen and helium are by far the most common elements in stars and giants; rocky worlds are mostly oxygen, silicon, iron and magnesium
  - [ ] element-heavy: heavy elements are made in stars and supernovas and spread by them; young galaxies have fewer of them
- [ ] Build from `content/physics.dnat`: all 49 entries (The four forces of nature, Particles and atoms, Inside a planet, What moves inside a planet, Atmospheres) and these rules:
  - [ ] physics-forces: all four forces run the simulation: gravity for orbits, tides, air and collapse; electromagnetism for light, chemistry and magnetism; the strong force for fusion and fission; the weak force for radioactive decay
  - [ ] physics-elements: an atom's protons decide its element (all 118); neutrons decide its isotope; unstable isotopes decay over time
  - [ ] physics-gravity-air: a planet keeps its air if its gravity is strong and its air is cold; small, hot worlds lose it
  - [ ] physics-interior: every rocky planet gets layers (core, mantle, crust); heat from inside drives convection, then plate tectonics or a stagnant lid, then volcanoes
  - [ ] physics-dynamo: a liquid metal core plus spin makes a magnetic field; it can switch off and back on; the field shapes auroras and radiation at the surface
  - [ ] physics-tectonics: moving plates build mountains, trenches, volcano chains and quakes, recycle the crust, and help keep the climate steady
  - [ ] physics-greenhouse: greenhouse gases warm a planet; too much can run away into a Venus-like world
  - [ ] physics-tech: civilizations climb the forces in order: fire and chemistry (electromagnetism), electricity and magnetism, then fission and fusion (the strong force), then mastery of gravity for space travel

- [x] Math: vectors, matrices, noise, seeded random numbers (`src/core/MathCore.cs`)
  - Our own seeded random generator. 64-bit positions with a moving origin.
- [~] Memory and the giant data arrays for billions of things (started: `BigArray<T>`)
  - Big memory blocks reserved up front per system.
- [~] Multithreading so the simulation uses every CPU core
  - One worker per core, pulling jobs from a shared queue. Built: `src/core/Jobs.cs` (`Jobs.Run`, `Jobs.For`); used to measure a planet's regions and to build the land's new bodies all at once. Still to move onto it: the life sim step, terrain building, sea bodies.
- [~] Save and load file format
  - Our own versioned binary chunks: universe, planets, life, civilizations, player. Built: the format (`src/core/SaveFile.cs`) and the life chunk (`PlanetLife.Save`/`Load`). Still to do: universe, planet, civilization and player chunks, and Save and Load in the game's menu.

### R4 — Graphics
- [ ] Full resolution scaling and range: "its got to have full resolution scaling and range"
  - [x] Any window size, rebuilt on resize; per-monitor DPI aware (sharp on high-DPI screens, display settings untouched)
  - [x] Render-scale setting (draw at 50–200% of the window, then scale to fit): the Render scale menu, and `--scale <percent>`
  - [ ] Zoom range from one microbe to a whole galaxy (64-bit positions, moving origin)
**Spec:** `wiki/systems/r04-renderer.md`, `docs/img/README.md`, `wiki/systems/r17-docs-and-capture.md`

- [ ] Our own 3D image generator for all game icons and art: "we will need to design the #D genrator and botting of them"
- [ ] Auto-capture tool that renders the doc images and fills every image slot in the docs
  - A mode of the game exe. It finds every `<!-- IMG: ... -->` marker and renders that image (`docs/img/README.md`).
- [x] Talk to the graphics card: Direct3D 12 device, swap chain, frames (`src/render/Gpu.cs`), windowed, vsync, Alt+Enter blocked
- [x] Billboard pipeline: shaders compiled by Windows' own d3dcompiler_47.dll (`src/render/Pipeline.cs`, `src/render/Draw.cs`)
  - Direct3D 12 (part of Windows).
- [ ] Terrain, water, sky, weather, lighting, shadows
- [ ] Drawing creatures built from their genes
- [ ] Smooth zoom from microbe to planet to solar system to galaxy
- [ ] Top-down god camera
- [ ] Visual gene map for every trait and skill

### R4.4 — Live generators
**Spec:** `wiki/systems/r04b-generators.md`

- [ ] No pre-built assets: every asset built live from data arrays as life evolves: "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living"
- [ ] Shared generator core: data arrays in, shapes, surfaces, motion and sound out, cached and rebuilt when the data changes
- [ ] Microbe & virus generator
- [ ] Plant & fungus generator
- [ ] Creature generator
- [ ] Intelligent creature generator
- [ ] City generator
- [ ] Tech & machine generator
- [ ] Space tech generator
- [ ] Planet & moon generator
- [ ] Star generator
- [~] Galaxy & system generator: galaxies done (`src/gen/GalaxyGen.cs`, from the galaxy and star content)
- [ ] Resource field generator
- [ ] Element generator
- [ ] Weather & effect generator
- [ ] Sound generator
- [ ] Black hole & active core generator
- [ ] Nebula generator
- [ ] Small body generator
- [ ] Fossil & ruin generator

### R4.5 — Art style samples
- [x] Live sample viewer, its own window, moving and rotatable: "Live viewer, moving (Suggested)"
- [ ] Sample sets, galactic scale down, one set at a time, waiting for the creator's input before the next: "and when we do the dsamples we show the non headless and do them galactic scale down one set of samples at a time to take input before doing the next set"
  - Life sets follow the full tree (`docs/wiki/tree-of-life.md`): "and what the fuck we are still doing docs u ments as this:" ... Animals / Insects / Microbes and viruses ..." IS NOT ALL LIVING THINGS IN THE FULL KINDOM PHILUM CLASS GENIUS SPECIES and the full kindom thing in full"
  - [x] Set 1: Galaxy layouts. **LOCKED: Populous-like for all galaxy types**: "center column is all 100% go for all theri variations of galaxies". Barred spiral rebuilt after "middle row is 100% trash, center column are best". Pick corrected: center column, Populous-like: "they jsut timed out i could NOT look at them all and clicking brout it up and no way back but the center column on the first test looked the best"
  - [x] Deep zoom built (`src/shell/DeepView.cs`, `src/gen/SpaceGen.cs`): galaxy (150,000 stars, clusters, nebulas) → core supermassive black hole (shadow, photon ring, accretion disk, jets) → any star's full system (planets, moons, belt, Kuiper belt, Oort cloud), with a moving origin. Deep zoom: "now the first set looked best but they have to be zoomable to the star sytems and i suppose this is just asa sammple and not the true game but i dont know how zoom and all of that works but how and where do i see the supermassive black holses at the centrter and all the space systems and environment of objects and systems and clusters and things" / "Yes, build deep zoom now (Suggested)"
  - [~] Set 2: Solar system layouts, with moons, asteroid belts, the Kuiper belt and Oort clouds
  - [ ] Set 3: Stars
  - [ ] Set 4: Planets and gas giants
  - [ ] Set 5: Resource fields
  - [ ] Set 6: Dyson spheres
  - [ ] Set 7: Space stations, colonies (mining outposts, prison colonies and the rest), spaceships and hyperdrive ships
  - [ ] Set 8: Cities
  - [ ] Set 9: Intelligent creatures (any kingdom)
  - [ ] Set 10: Animals: every phylum, then its classes, orders, families, genera, species
  - [ ] Set 11: Plants: every phylum down to species
  - [ ] Set 12: Fungi: every phylum down to species
  - [ ] Set 13: Protists: every group down to species
  - [ ] Set 14: Archaea: every phylum down to species
  - [ ] Set 15: Bacteria: every phylum down to species
  - [ ] Set 16: Viruses: every group down to type
  - [ ] Set 17: Elements (all 118)

- [ ] Every sample generated procedurally from a seed: "and remember thses things are get dynamic procedural generations"
- [x] Samples rendered with Direct3D 12: "Straight to Direct3D 12"
- [ ] Our own shader-code generator for Direct3D 12 (no outside shader compiler)
  - Changed: shaders are compiled by `d3dcompiler_47.dll`, which ships with Windows (same reasoning as round 20)
- [ ] The asset builder and its examples come before any play-testing of planet life: "yeah we are in no way ready to test it we havent even build the asset builder and done those examples for all the creatures life villages tiebes towns cities space stuff ect ect"
  - [x] Style sample first, three body looks: "all of the above but we need to find a set style first". **Smooth chosen**: "those will work the smooth and remmebr what i said about them probecedurally generated and dynamic to the life in the geame"
  - [x] Bodies built from the genes of the species in the running game, changing as they evolve: body-plan genes, the gene body builder, and the planet's own land life drawn from its evolved species (`src/shell/LandLife.cs`), and its sea animals swimming from their genes (`src/shell/SeaBodies.cs`)
  - [~] Creatures and the rest of life (every kingdom): families of viruses, cells, plants, fungi, jellies, animals and intelligent creatures, all from genes (`--family`)
  - [~] Villages: from the builder species' genes (`--civ`)
  - [~] Tribes: a tribe camp from the builder species' genes (`--civ`)
  - [~] Towns: from the builder species' genes (`--civ`)
  - [~] Cities: from the builder species' genes (`--civ`)
  - [~] Space stuff (stations, colonies, ships and the rest): spaceship, hyperdrive ship, space station and colony from the builder species' genes (`--civ`)
- [ ] How long each step of an intelligent species lasts (wild, tribes, villages, towns, cities, space): not a set time, it follows how much the god helps; a player who keeps at it gets there quicker: "its not a set time... its all based on how the god helps the life to evolve if ur on it you can get there quicker". The god's ways of helping are not built yet; until then each step takes an even share of the last stage's hour. Asked about: "yeah intelligent species live on the land before they ever village and tribe up before going into theri tech and stuff"
- [x] Species-driven asset generation (five ways of building from the body plan, ships shaped like their builders; compared in `--civ` with keys 1-5): cities, ships and stations built from the species' own genes and body plan: "ie insect species has insect spaceships ansd insect cities"
**Spec:** `wiki/systems/r04-renderer.md#-style-samples-come-first`, `docs/DESIGN.md` round 6

- [~] Style samples, 3 samples × 3 styles: "remembr we arent using other peoples shit unless we absoulutley have to so plan on buiolding it all and things like ingame microbes viruses and intelligernt creature should all have samples 1,2,3 made in 3 diffeernt styles 1,2,3 for a hand ful of things in the game before we go with any one style and same for things like the galaxy layouts solarsystem layouts and planets and stars and dyson spheres and space ships and space colonires ands space stations and rouseource firleds and gas giants and athe whole periodic table of elements"
  - Styles: RimWorld-like, Populous-like, Stardew-like, all drawn originally by us: "try a rimworld like and a, populous like, and a stardew like".
  - Light on the GPU, built for living things that move and spread: "not one thats too gpu intense and these will be living things in the environment moving and spreading as told before".
  - Waits on R4's basic renderer: "we cant do em till you build tyhier egine and shit".
- [ ] Side-by-side comparison sheet of every subject, for the creator to pick one style
- [x] Lock in the chosen style, recorded verbatim in `docs/DESIGN.md`
  - Locked: Populous-like for everything, "yup same one the center one is the best, dont need to test them twice" (DESIGN.md item 67).

### R4.6 — The coefficient waveform engine everywhere (galaxies down to microbes)
**Asked:** "should we use our coeffient 9c waveformes for this project for better resolutions and game effienciency on such large scales, maybe look into the uni vs matics project on D:/ usb and see if we can fully impliment cofffiencient wave form to everything we do since its our code its okay to use it" / "option 2 everything from galaxies down to microbes and everything inbetween for the complete full game" / "with infinate zoom it should be fun re maping the zoom features"
**Source:** the lab's Uni Vs Matics engine (`UnityAILab/UniVsMatics`, `ftcore/reconstruct.py`, `docs/ARCHITECTURE.md`). Spec: `wiki/systems/r04-renderer.md`.

- [x] Port the CDF 9/7 engine into the core: forward and inverse lifting, energy-target term selection, Int16 values, dv1 positions (LEB128 deltas), save and load, a strongest-share rebuild dial (`src/core/Wavelet97.cs`). Checked against the reference engine: coefficients match to 3e-17; dv1 round-trips exactly
- [x] Mode-E: the endless self-similar detail stage (Newton on z³ − K, the 3D cubic continuation, decimal-lattice cellular texture, basin colouring), ported from Uni Vs Matics / Deviant 3D, for detail that never runs out as you zoom (`src/core/ModeE.cs`; per-point `At` and grid `Solve`; checked against Deviant 3D's fractalize.ts: all 12,288 colour values of a 64×64 picture identical)
- [ ] Re-map the zoom features onto wavelet levels for infinite zoom: each zoom band reads its own level of terms, and mode-E carries on past the finest stored level
- [ ] Planet ground: the height and colour fields of each planet as wavelet equations per cube face; the living planet's changes (drift, erosion, craters, sea level) written as term edits; ground pieces rebuilt from the level that fits their size (fixes the coarse, blocky ground seen from orbit at native resolution)
- [ ] Planet saves: the changed ground saved as its wavelet terms
- [ ] Clouds, weather and air: cloud cover, rain and temperature fields as wavelet fields
- [ ] Resource fields and the element map as wavelet fields
- [ ] Galaxies: star density and gas as wavelet fields; zooming from the universe to a galaxy to a star system reads deeper levels
- [ ] Nebulas and the deep sky as wavelet fields
- [ ] Stars up close: surface plasma as a wavelet field with mode-E detail
- [ ] Gas giants: cloud bands and storms as wavelet fields
- [ ] Life on the ground: where each species lives (population density) as wavelet fields per planet
- [ ] Microbes and the micro world: the soil and water seen at microbe size from mode-E detail
- [ ] Bodies of creatures and builds: surface detail and colour as small wavelet fields
- [ ] Doc pictures and clips: stored as wavelet equations, not images, where that is smaller
- [ ] Sound: the master music equation (each field sings its own coefficients), as in Uni Vs Matics

### R5 — Universe
**Spec:** `wiki/systems/r05-universe.md`, `docs/wiki/space-and-dyson-spheres.md`
- [ ] Build from `content/small-bodies.dnat`: all 39 entries (Asteroids by makeup, Asteroids by orbit, Comets, Dwarf planets and far bodies, Meteors and meteorites) and these rules:
  - [ ] small-seed: every asteroid, comet and dwarf planet is generated live from the world seed; nothing is pre-built
  - [ ] small-makeup-by-distance: stony asteroids are more common close to the star, dark carbon-rich ones farther out, icy bodies beyond the frost line
  - [ ] small-comet-source: short-period comets come from the Kuiper belt and scattered disk; long-period comets fall in from the Oort cloud
  - [ ] small-comet-tails: comets grow a coma and two tails only near their star; the ion tail always points away from the star
  - [~] small-impacts: near-planet asteroids and falling comets can hit worlds; impact size and effect follow the 10 impact sizes on the space page (built on the planet's clock: `src/sim/PlanetImpacts.cs`; not yet from the real asteroids and comets of the system)
  - [ ] small-meteorites: a meteorite's type follows the asteroid it came from; carbon-rich ones can deliver the chemistry of life
  - [ ] small-resources: metal asteroids are mined for metals, icy bodies and comets for water and fuel, carbon-rich asteroids for life chemistry
- [ ] Build from `content/nebulas.dnat`: all 12 entries (The five main kinds, Star-forming structures) and these rules:
  - [ ] nebula-seed: every nebula is generated live from the world seed; nothing is pre-built
  - [ ] nebula-light: a cloud glows red if hot stars light it from inside, blue if it reflects starlight, and black if there's no light behind it
  - [ ] nebula-birth: molecular clouds and their globules and pillars collapse into new stars and solar systems over time
  - [ ] nebula-death: dying Sun-like stars leave planetary nebulas; exploding heavy stars leave supernova remnants that seed new clouds with heavy elements
  - [ ] nebula-place: star-forming nebulas sit mostly in a galaxy's spiral arms
  - [ ] nebula-travel: thick nebulas block sight and slow travel; they are also rich in gas resources
- [ ] Build from `content/black-holes.dnat`: all 35 entries (Kinds of black hole, Parts, Active galaxy cores (a feeding supermassive black hole), Events, Real examples) and these rules:
  - [ ] bh-seed: every black hole is generated live from the world seed; nothing is pre-built
  - [ ] bh-mass-kind: mass decides the kind: stellar from dead heavy stars, intermediate from mergers, supermassive at galaxy centers
  - [ ] bh-feeding: a black hole with gas falling in grows a hot disk and may fire jets; a starved one stays dark
  - [ ] bh-view-angle: one feeding core looks different from different angles: a blazar seen down the jet, a quasar or Seyfert 1 seen from above, a Seyfert 2 seen through the dust
  - [ ] bh-spin-energy: energy can be drawn from a spinning black hole's ergosphere; advanced civilizations can tap it
  - [ ] bh-danger: close in, tides tear apart stars, planets and ships; jets sterilize anything in their path
  - [ ] bh-hypothetical: primordial black holes are hypothetical; the game may generate them, marked as such
- [ ] Build from `content/stars.dnat`: all 34 entries (Main-sequence stars (the seven classic types), Giants and supergiants, Young and failed stars, Dead and strange stars, Luminosity classes (how bright for its type), Extra star types) and these rules:
  - [ ] star-color: color follows surface temperature, from red (coolest) to blue (hottest)
  - [ ] star-life: heavier stars burn hotter and die sooner, lighter stars burn cooler and live longer
  - [ ] star-pick: when a galaxy is generated, red dwarfs are by far the most common and blue stars the rarest
  - [ ] star-vary: every generated star picks its values from inside its type's ranges, from the world seed
  - [ ] star-name: only our own star is called the Sun; every other star is a star
- [ ] Build from `content/solar-systems.dnat`: all 31 entries (Kinds of systems (real), Real examples, Zones a system is built from) and these rules:
  - [ ] system-resonance: young planets often drift into locked resonant chains; most chains later break apart
  - [ ] system-hill-sphere: a moon can only stay with a planet inside that planet's sphere of pull; too far out and the star takes it
  - [ ] system-seed: every system is generated live from the world seed; nothing is pre-built
  - [ ] system-star-first: the star is picked first; its type sets where the habitable zone and frost line fall
  - [ ] system-zones: rocky planets mostly form inside the frost line, giants outside it, belts sit in the gaps, and an Oort cloud surrounds it all
  - [ ] system-migration: giant planets can wander inward after forming, making hot Jupiters and scattering smaller planets and comets
  - [ ] system-moons: giants collect many moons; rocky planets have few or none; big impacts can make a large moon
  - [ ] system-comets-fall: comets from the Oort cloud and Kuiper belt sometimes fall inward, and some hit planets
  - [ ] system-multiple-stars: in two- and three-star systems, planets orbit one star or circle the whole group, never in between
  - [ ] system-life-zone: life is most likely in the habitable zone, but ice-shell moons and other strange worlds can hold life too
  - [ ] system-age: young systems still have dust disks and forming planets; old systems have settled orbits
  - [ ] system-name: only our own system is the Solar System and only our own star is the Sun; generated ones get generated names
- [ ] Build from `content/galaxies.dnat`: all 47 entries (Galaxy types (real), Extra galaxy types (from research), Real examples, Parts a galaxy is built from, Structures between galaxies) and these rules:
  - [ ] galaxy-hubble-stage: galaxies run on a scale from smooth ellipticals, to lenticulars, to tight spirals, to loose spirals, to irregulars; any of them can be barred, ringed or peculiar
  - [ ] galaxy-seed: every galaxy is generated live from the world seed (LAW 14); nothing is pre-built
  - [ ] galaxy-size: the player picks Small, Medium, Large or Massive; size sets how many star systems the galaxy holds, every one fully simulated all the time
  - [ ] galaxy-type-pick: the type decides which parts are used; spirals get arms, ellipticals don't, irregulars get patches
  - [ ] galaxy-gas: gas-rich galaxies keep forming new stars; gas-poor galaxies hold mostly old stars
  - [ ] galaxy-arms: young blue stars, nebulas and star-forming regions sit in the arms; old red stars sit in the bulge and halo
  - [ ] galaxy-center: big galaxies get a supermassive black hole at the center; some feed it and become active
  - [ ] galaxy-neighbors: a world can hold more than one galaxy; extra galaxies sit in pairs, groups or clusters along filaments, with voids between
  - [ ] galaxy-collisions: galaxies that pass close pull out tidal tails, may trigger a starburst, and can merge into one
  - [ ] galaxy-satellites: big galaxies are circled by dwarf and satellite galaxies
  - [ ] galaxy-name: only our own galaxy is the Milky Way and only our own star is the Sun; generated ones get generated names

- [ ] Galaxy generation from a seed
  - Sizes: Small 100, Medium 500, Large 2,000, Massive 10,000 star systems, every one fully simulated: "Fewer, every one detailed".
- [ ] Solar systems: stars and planets
- [ ] Multiple galaxies

### R6 — Planet
**Spec:** `wiki/systems/r06-planet.md`, `docs/wiki/planets-and-templates.md`, `docs/wiki/elements.md`
- [ ] Build from `content/resource-fields.dnat`: all 26 entries (Made by magma, Made by hot fluids, Made by water and time, Made by weather, Fields in space) and these rules:
  - [ ] deposit-geology: deposits follow the planet's geology: magma bodies make magmatic deposits, plate edges and volcanoes make hydrothermal ones, seas and rivers make sedimentary ones, tropical weather makes laterites
  - [ ] deposit-life: some resources need life to have existed first: banded iron needs oxygen makers, coal needs forests, oil and gas need ancient sea life
  - [ ] deposit-tectonics: planets with plate tectonics make far more ore deposits than stagnant-lid worlds
  - [ ] deposit-depth: deeper deposits need better mining tech; placers and surface ores come first
  - [ ] field-space: space fields follow what the bodies are made of (small bodies, giants, nebulas, rings)
  - [ ] field-deplete: every deposit and field can run out; civilizations that strip them must move on, as the endgame harvest says
- [ ] Build from `content/moons.dnat`: all 18 entries (By how they formed, By surface and inside, By orbit) and these rules:
  - [ ] moon-seed: every moon is generated live from the world seed; nothing is pre-built
  - [ ] moon-count: giants collect many moons, both regular and captured; rocky planets have few or none
  - [ ] moon-tides: tides from the planet and from neighboring moons heat moons from inside, powering volcanoes and keeping oceans liquid
  - [ ] moon-hill-sphere: a moon only stays inside its planet's sphere of pull (content/solar-systems.dnat)
  - [ ] moon-capture: passing asteroids and comets can be captured as irregular moons
  - [ ] moon-life: ocean moons and thick-air moons can hold life; a moon's local conditions use the same 10-condition world matrix as planets
- [ ] Build from `content/gas-giants.dnat`: all 30 entries (Kinds, Cloud classes (Sudarsky, by temperature), Layers, Features, Solar System examples) and these rules:
  - [ ] giant-seed: every giant is generated live from the world seed; nothing is pre-built
  - [ ] giant-zone: giants form beyond the frost line; some wander inward to become hot Jupiters and hot Neptunes
  - [ ] giant-clouds: a giant's temperature picks its cloud class, from ammonia clouds when cold to rock clouds when hottest
  - [ ] giant-layers: gas giants get clouds, molecular hydrogen, metallic hydrogen and a core; ice giants get clouds, an ice mantle and a core
  - [ ] giant-weather: fast spin makes belts, zones and long-lived giant storms
  - [ ] giant-moons-rings: giants collect many moons and often rings; some moons can hold life
  - [ ] giant-resources: civilizations skim giants for hydrogen, helium-3 and deuterium fuel
- [ ] Build from `content/planets.dnat`: all 27 entries (By size, By makeup, By heat and orbit, Earth-family examples) and these rules:
  - [ ] planet-seed: every planet is generated live from the world seed; nothing is pre-built
  - [ ] planet-size-air: bigger, colder planets keep more air; small hot ones lose it (content/physics.dnat)
  - [ ] planet-zone: where a planet forms (inside or outside the frost line, in or out of the habitable zone) sets its makeup and temperature
  - [ ] planet-interior: every rocky planet gets core, mantle and crust; heat decides tectonics or a stagnant lid, volcanoes, and a magnetic field
  - [ ] planet-conditions: every planet is rated on the 10 world conditions (temperature, gravity, air pressure, air makeup, radiation, water, day length, surface, magnetic shield, local life)
  - [ ] planet-hypothetical: hypothetical planet types can be generated but are rare, and they're marked as such

- [ ] Earth template: the full "everything" list (top 100 lists)
  - Surface split into equal-size cells on a sphere.
- [ ] Unlockable other planet templates and what unlocks them
  - 100 templates. The first to unlock are Ocean, Desert, Ice, Jungle, Volcanic, Tidally locked and Gas-giant moon. Stages and ages can come in their own order on these.
- [ ] Ground, plates, mountains, oceans, rivers, ice
- [ ] Planet interiors: core, mantle, crust, magma, convection, plate tectonics or stagnant lid, volcanoes, geodynamo and magnetic field, magnetosphere and auroras
- [ ] Atmospheres: layers, pressure, makeup, greenhouse and runaway greenhouse, and air escaping (thermal, hydrodynamic, non-thermal)
- [ ] Air, weather, climate, seasons, day and night
- [ ] Chemistry and minerals that life can use
  - All 118 elements. Lab-made ones (93+) unlock once a civilization creates them.

### R6b — The living planet (it runs even with no life on it)
**Asked:** "a planet is a living breathing thing even without life, did you do all the magnma cores mangnetic fielsds atetonic plates volcanos earthqueakes all of that shit in totaltiy that ive already told you.? what elese have you skipped" Earlier: "dont forget the forces of nature weak and nuclear forces of atoms pysics electormagnatisim magma in plantes that arte tectonic with magnic fields and shit come one wwhat about gravity and atmosphers"
**Today in the code:** the ground is a fixed height map from noise; mountain belts, volcanoes, rivers and resource fields are placed once and never change; rain and snow are only pictures. Nothing below is simulated yet. Data: `content/physics.dnat` (interiors, processes, atmospheres), `content/resource-fields.dnat`. Spec: `wiki/systems/r06-planet.md`, `docs/wiki/forces-of-nature.md`.

- [x] **A planet clock:** the planet's own processes run on the game clock from the moment it forms, before and after life, at every time speed (the planet's age runs with the life clock; the game starts about 200 million years after it formed)
- **Inside**
  - [~] Core, mantle and crust from the planet's size and makeup; a molten or solid inner and outer core (built: the core's solid share and molten outer core from its heat, `src/sim/PlanetCore.cs`; makeup still to come)
  - [~] Heat inside: from forming, from radioactive decay, from tides; it cools over billions of years, and as it cools tectonics and the magnetic field can die (a Mars-like end) (built: heat that runs out, held longer by bigger planets, ending in a stagnant lid and a dead field; tides still to come)
  - [ ] Mantle convection that drives everything above it
  - [~] Magma: where it forms, rises and pools; magma oceans on young or very hot worlds (built: the magma-ocean age; where magma pools still to come)
- **Plates**
  - [x] Plate tectonics or a stagnant lid, decided by the heat inside (and size and water)
  - [~] Plates that move over time: continents drift, split, crash together and form supercontinents (built: 8 to 15 plates, each turning about its own axis at 1 to 10 cm a year, `src/sim/PlanetPlates.cs`; the ground you see drifts with them, with sea-floor ridges where plates pull apart and mountains where they crash; climate follows the true latitude. Still rough: plate edges are straight lines and cut coasts straight)
  - [~] Plate edges: spreading ridges, trenches where one plate dives under another, mountain ranges where they crash, faults where they slide past (built: every edge is spreading, crashing or sliding from how its plates move; not yet shaping the ground)
  - [x] Earthquakes along plate edges and faults, with real sizes (magnitude) and tsunamis from undersea quakes (counts fall tenfold per magnitude, about one magnitude 8 a year on Earth, up to 9.5 on crashing edges; magnitude 9 quakes harm life nearby)
  - [~] Volcanoes at plate edges and hot spots that erupt over time: lava, ash clouds that cool the world, new land, island chains (built: great eruptions once in 1,000 years and super-eruptions once in 50,000, more on a hotter world, killing life nearby and, for super-eruptions, cooling the whole world; new land and island chains still to come)
  - [ ] Hot springs, geysers and deep-sea vents (where life can start)
- **The magnetic shield**
  - [x] The geodynamo: a molten, churning outer core makes the magnetic field (needs a liquid outer core and spin; stronger with plates)
  - [~] The magnetosphere shields the air and life from the star's wind and radiation; without it the air is stripped away over time (built: the shield sets the radiation that reaches dry land, which harms land life; air stripping waits for the air sim)
  - [~] Auroras near the poles; the field wanders and flips now and then (built: flips at random, about every 450,000 years; auroras still to draw)
- **Air**
  - [~] Air layers (troposphere to exosphere), pressure and makeup, from `content/physics.dnat` (built: pressure, carbon dioxide, oxygen, methane and surface temperature, `src/sim/PlanetAir.cs`; the layers still to come)
  - [~] The greenhouse effect and runaway greenhouse; air escaping to space (thermal, hydrodynamic, non-thermal) (built: greenhouse warming from pressure, carbon dioxide and methane; runaway greenhouse above 67 °C; snowball below -23 °C; air stripped fast once the magnetic field dies, faster on small worlds; the three escape kinds still one rate)
  - [x] Volcanoes add gases; rock and sea take carbon back out over millions of years (the carbon-silicate cycle that keeps a world steady): the air settles toward the level where rain on rock takes out what volcanoes put in, faster when warm and with plates
- **Water and weather**
  - [~] The water cycle: seas evaporate, clouds form, rain and snow fall, rivers run back to the sea, water soaks into the ground (built: rain per region, more from warmer air, deciding how wet the land is for life, `src/sim/PlanetWeather.cs`; rivers and ground water still fixed)
  - [~] Real weather: winds from heat and spin, storms, hurricanes, monsoons, drought (built: the rain belts of the air's circulation, wet equator, dry 30 degrees, wet 60, dry poles; hurricanes over seas above 26.5 °C, 5 to 20 degrees out; winds, monsoons and droughts still to come)
  - [x] Ocean currents carrying heat around the world; upwelling that brings nutrients up (seas swing less than the world, coasts are milder than inland; upwelling 15 to 35 degrees out along coasts and at the equator adds nutrients for sea life)
  - [~] Tides from moons and the star (built: a tide range per world that stirs nutrients on coasts; worked out from the real moons and star still to come)
  - [x] Ice: glaciers and ice caps that grow and shrink, ice ages, snowball worlds, sea levels rising and falling with them (ice per region from its temperature; ice ages from the orbit's 100,000, 41,000 and 23,000 year wobbles; seas up to 130 m lower; snowball from the air; ice is hard on land life, sea life lives on under it)
- **Climate and seasons**
  - [~] Climate zones from heat, water and spin; they shift as continents move and the air changes (built: each region's temperature from the world's air, its latitude and height, and its rain belt; shifting with the air; continents do not move yet)
  - [~] Seasons from the planet's tilt, day and night from its spin, years from its orbit (built: seasons from a 23.4 degree tilt, opposite in each half, shown at slow game speeds; day and night still to come)
  - [~] Long climate swings: orbit wobbles, the star brightening over billions of years (built: the orbit wobbles; the star brightening still to come)
- **The surface changes**
  - [x] Erosion: rain, rivers, wind and ice wear mountains down and carry the pieces to the sea (per region, faster the higher, wetter and icier; mountains pushed up where plates crash; rivers carry the worn rock to the seas next door, `src/sim/PlanetSurface.cs`; wind still to come)
  - [~] The rock cycle: rock melts to magma, cools to new rock, wears to sand and mud, is buried and pressed into stone, and is lifted up again (built: wearing down, sea-floor mud, burial into stone, uplift at crashing edges; melting back into magma still to come)
  - [x] Soil forms from worn rock (and later from life): faster when warm and wet, five times faster under plants, thin on steep and icy ground; soil feeds land life and river mud feeds coastal seas
  - [ ] Landslides, floods, sinkholes and coastlines that move
- **From outside**
  - [x] Impacts: meteorites, asteroids and comets hit, leaving craters and changing the climate (sizes from the space page): the 10 sizes at real rates for Earth today, far more in the young heavy bombardment, bigger worlds hit more; each size does its harm to life (to a mass extinction), great ones darken and cool the world; microbes deep in rock and at vents always survive (`src/sim/PlanetImpacts.cs`; craters on the ground still to draw)
  - [~] The star's flares and wind; the star ageing (built: the star brightens with age, about 30% dimmer when young than at 4.6 billion years, warming the world; flares and wind still to come)
- **Everything feeds everything**
  - [ ] Each process changes the others and the life on the world: plates make mountains, mountains change rain, rain feeds rivers, rivers carry nutrients to the sea; volcanoes and impacts cause die-offs; ice ages move life toward the warm middle
  - [ ] Resource fields form and change with this geology over time (magma makes ores, hot fluids make veins, seas lay down sediments)
- **Content still to write (researched, like `content/physics.dnat`)**
  - [x] Earthquakes and faults, erosion and the rock cycle, ocean currents and tides, the water cycle, glaciers and ice ages, weather systems, climate zones and cycles: `content/geology.dnat` and `content/climate.dnat`

### R7 — Genomes
**Spec:** `wiki/systems/r07-genome.md`, `docs/wiki/genes-and-dna.md`

- [ ] Real-style DNA format
  - 2 bits per letter (A, C, G, T).
- [ ] How DNA builds a body and its behavior
- [ ] Mutation and breeding, with and without sex
- [ ] Viruses changing genes
- [ ] Map of gene pieces to traits ("breathing air to walking upright to photosynethesiis")

### R8 — Life
**Spec:** `wiki/systems/r08-life-sim.md`, `docs/wiki/stages-of-life.md`, `docs/wiki/milestones.md`, `docs/wiki/groups-and-flocking.md`, `docs/wiki/tree-of-life.md`
- [ ] Build from `content/life/intelligent.dnat`: all 29 entries (Real thinking animals (seeds), Signs of a mind, What pushes a line toward intelligence) and these rules:
  - [ ] mind-any-line: any kingdom can reach intelligence if evolution builds the drivers: animals most often, but hive insects, fungal networks and even plants on other templates can climb the grouping ladder
  - [ ] mind-signs-ladder: a species earns signs of a mind one by one (tool use, culture, language…); together they unlock the thinking steps (31+) on the grouping ladder
  - [ ] mind-own-image: a thinking species' clothing, tools, cities and ships are generated from its own body plan (LAW 14)
  - [ ] mind-ai-layers: thinking species get the species AI layer, and can later grow a galactic mind
  - [ ] mind-spark: the Spark of Mind power pushes a species toward intelligence, but only works on one already close to it
- [ ] Build from `content/life/extinct.dnat`: all 47 entries (Mass extinctions (the Big Five, and after), The first strange animals, Early fish and the walk onto land, Dinosaurs, Other Mesozoic giants, After the dinosaurs: the age of mammals, Extinct plants) and these rules:
  - [ ] extinct-seeds: extinct real life is seed data: on Earth-like worlds, evolution can produce dinosaur-like, trilobite-like or mammoth-like life again, in new forms
  - [ ] extinct-forever: once a line dies out it never returns as the same line; it stays greyed out on the Tree of Life
  - [ ] extinct-triggers: mass extinctions follow real causes: impacts, supervolcanoes, warming, cooling, oxygen loss, and thinking species
  - [ ] extinct-recovery: after a mass extinction, survivors spread fast into the emptied world and new groups rise
  - [ ] extinct-fossils: extinct life leaves fossils; thinking species can dig them up and learn from them
- [ ] Build from `content/life/animals.dnat`: all 84 entries (Body-plan traits, Phyla, Arthropod classes, Insect orders (seeds), Fish classes, Land vertebrate classes, Bird orders (seeds), Mammal orders (seeds)) and these rules:
  - [ ] animal-body-plan: every animal's body comes from its genes: symmetry, layers, body cavity, segments, skeleton, limbs, eyes, skin
  - [ ] animal-tree: new animals branch from real lines: phylum, then class, order, family, genus, species, each filling to 100 in play with invented ones
  - [ ] animal-convergence: unrelated lines can evolve the same answer (wings, eyes, fins), because the same problems shape them
  - [ ] animal-intelligence: any line can climb to intelligence: primates, wolves, crows, parrots, octopuses, whales, social insects (the grouping ladder)
  - [ ] animal-extinction: lines die out; extinct groups like trilobites and dinosaurs stay greyed out on the tree forever
- [ ] Build from `content/life/plants.dnat`: all 36 entries (Groups (phyla and lines), Big splits, Organs and tissues, How they make food, How they breed) and these rules:
  - [ ] plant-land: plants climb onto land from green algae, with root-partner fungi helping; veins let them grow tall; seeds free them from water
  - [ ] plant-climate: climate decides the kind: C3 in mild damp places, C4 in hot grasslands, CAM in deserts
  - [ ] plant-forests: forests make soil and air, and when buried they become coal
  - [ ] plant-partners: flowering plants and pollinators evolve together; fruit evolves with the animals that spread it
  - [ ] plant-defense: plants fight back with thorns, poisons, bark and chemical warnings to their neighbors
  - [ ] plant-farming: thinking species farm, breed and engineer plants (Farming, Selective Breeding, Genetic Engineering)
- [ ] Build from `content/life/fungi.dnat`: all 16 entries (Phyla, Body parts, Ways of living) and these rules:
  - [ ] fungi-recyclers: fungi rot what dies and return it to the soil; without them dead matter piles up
  - [ ] fungi-networks: root-partner fungi link plants underground, sharing food and warnings (the root-network step on the grouping ladder)
  - [ ] fungi-spread: fungi spread by spores on wind and water, and by growing their mycelium through soil and wood
  - [ ] fungi-land: root-partner fungi help the first plants move onto land
  - [ ] fungi-farming: fungi can be farmed: by ants and termites, and by thinking species for food, drugs and fermenting
- [ ] Build from `content/life/protists.dnat`: all 25 entries (Supergroups, Protist groups (seeds), How they move) and these rules:
  - [ ] protist-complex: protists are the first complex cells, with a nucleus and parts inside; they appear after the swallowed-partners stage
  - [ ] protist-roots: protist lines lead on: green algae to land plants, choanoflagellate-like cells to animals, opisthokont cells to fungi
  - [ ] protist-food: drifting protists are the base of the sea's food chain; their blooms feed everything, and some blooms poison it
  - [ ] protist-rock: chalk and glass shells of dead protists pile up into rock on the sea floor
- [ ] Build from `content/life/archaea.dnat`: all 12 entries (What makes them different, Extremophiles, Main groups (phyla)) and these rules:
  - [ ] archaea-extremes: archaea fill the worst places first (vents, acid, salt, ice), so they're often the first life on hard worlds
  - [ ] archaea-swallowed: an archaean swallowing a bacterium is one route to complex cells (the "swallowed partners" stage)
  - [ ] archaea-methane: methane makers warm young planets by filling the air with methane
- [ ] Build from `content/life/bacteria.dnat`: all 31 entries (Shapes, Cell parts, How they get energy and food, Main groups (phyla), Group life, Swapping genes) and these rules:
  - [ ] bacteria-fast: bacteria split in two fast; populations explode when food is there and crash when it runs out
  - [ ] bacteria-gene-swap: bacteria trade genes sideways, so useful traits like drug resistance spread fast between species
  - [ ] bacteria-everywhere: bacteria live everywhere life can, from vents to guts, and recycle everything that dies
  - [ ] bacteria-oxygen: cyanobacteria fill the air with oxygen over time, triggering the great oxidation and poisoning oxygen-hating life
  - [ ] bacteria-partners: bacteria live inside other life as gut partners, root partners and parasites
- [ ] Build from `content/life/viruses.dnat`: all 22 entries (Baltimore groups (how a virus's genes get read), Shapes and parts, Who they infect, Life cycles) and these rules:
  - [ ] virus-not-alive: viruses have no cells and no metabolism; they only copy themselves inside a host
  - [ ] virus-chemistry-ai: viruses run on the chemistry layer of the mini AI only: latch on, inject, copy, burst
  - [ ] virus-mutation: RNA viruses mutate fastest; viruses jump between species when they mutate to fit a new host's cells
  - [ ] virus-gene-carry: viruses can carry genes from one species into another (Viral Gene Carry power, and natural events)
  - [ ] virus-host-range: a virus can only infect hosts whose cells its spikes can latch onto
  - [ ] virus-plagues: dense populations let viruses spread fast; immunity, isolation and medicine slow them

- [ ] Viruses
- [ ] Microbes
- [ ] Plants
- [ ] Insects
- [ ] Animals
- [ ] Gene-driven ant-like behavior for each creature
- [ ] Layered mini AIs: "and we will have to make the mini ai's that are individual, species, hive, glactic mind like typ dynamic control ai of what they are suppose to so selfish gene like all life tries to get more and do more before death but life is life death comes"
  - [ ] Individual AI: selfish gene, wants to get more, do more and breed before death. Death always comes
  - [ ] Chemical AI for brainless life: "option 2 but real life non brian things still do thing sin life do you need to research?" (bacteria quorum sensing and moving toward food, slime-mold pathfinding, plants growing toward light and warning neighbors, viruses that only latch, inject and copy)
  - [ ] Group AI (pack, flock, school, colony)
  - [ ] Hive AI
  - [ ] Species AI
  - [ ] Galactic mind AI
  - [ ] Bigger steers smaller, and members can rebel, cheat or split off: "Bigger steers smaller (Suggested)"
  - [ ] Each layer appears only when evolution builds the brain or bond it needs
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
- [ ] Milestones for every stage of life: 100 per stage (10,000), built from the lists (`docs/wiki/milestones.md`)
  - All 100 stages (`docs/wiki/stages-of-life.md`). Each stage is one of the 100 unlock points. Every power unlocks at exactly one stage.

### R8b — What real life does, needs and is (research review)
**Asked:** "wtf we are making a true life game do reaseach and super -review of what the todo is missing that life does and needs and is" / "update the todo and docs where they are all lacking \"all\" means all of them"
**Sources:** the properties of life (OpenStax biology: order, sensitivity, reproduction, adaptation, growth and development, regulation, homeostasis, metabolism, energy), the six kinds of species interaction (competition, predation including herbivory, mutualism, commensalism, parasitism, amensalism), the major transitions in evolution (Maynard Smith and Szathmáry, 1995). Spec: `docs/wiki/ecology.md`, `wiki/systems/r08-life-sim.md`.

- [ ] **The life sim runs all 100 stages, not 7:** the code's ladder (replicators, single cells, many-celled life, nerves, brains, big-brained hunters, communication) is a stand-in. Every stage in `docs/wiki/stages-of-life.md` must be a real step, from the first chemistry through the nucleus (stage 25), swallowed partners, sex, colonies and many-celled life, each reached by evolution.
- **What every living thing is and does (the properties of life)**
  - [ ] Cells and order: every living thing is one or more cells, or (viruses) uses another's cells
  - [ ] Metabolism and energy: every species gets energy one real way (light, chemicals from vents, eating others, fermenting, breathing oxygen) and pays for its body, its moving and its breeding out of it
  - [ ] Homeostasis: each species keeps its body within limits (heat, water, salt, acid); outside them it weakens and dies
  - [ ] Growth and development: life grows from small to adult, and its body is built step by step from its genes (embryo, young, adult)
  - [ ] Response to stimuli: life senses and reacts: moving toward food and light, away from poison and danger (chemotaxis, phototropism), up to eyes, ears, smell and electric senses
  - [ ] Reproduction: with and without sex, each line's own way and rate (splitting, budding, spores, seeds, eggs, live birth)
  - [ ] Heredity: offspring carry their parents' genes, with mutation
  - [ ] Adaptation and evolution: populations change over generations by natural selection; one living thing never "adapts" its genes in its own lifetime
- **Life cycles, ageing and death**
  - [ ] Life cycles: eggs, larvae, metamorphosis (caterpillar to butterfly), seeds and seedlings, spores, alternation of generations in plants
  - [ ] Lifespan and ageing: every species has its own lifespan from its genes and size; old age always comes
  - [ ] Parental care: from none (millions of eggs) to long care (few young), set by the genes
  - [ ] Death feeds life: what dies is eaten by scavengers and rotted by fungi and bacteria back into nutrients
- **Every kind of species interaction**
  - [~] Predation and herbivory (+/−): hunters eat prey, grazers eat plants (first pass built: the food web per region)
  - [~] Competition (−/−): for food, light, space and mates, within a species and between species (first pass built: crowding per stage, sharing nutrients)
  - [ ] Mutualism (+/+): pollinators and flowers, root fungi and plants, gut bacteria, nitrogen-fixing bacteria, cleaner fish
  - [ ] Commensalism (+/0): one gains, the other is untouched (remoras, plants growing on trees)
  - [ ] Parasitism (+/−): parasites living on or in a host for a long time, unlike a hunter's quick kill
  - [ ] Amensalism (−/0): one harms another for nothing in return (a mould's poison killing bacteria)
  - [ ] Relationships shift: a partnership can turn parasitic and back as genes change
- **Ecosystems**
  - [ ] Trophic levels: producers, grazers, hunters, top hunters, scavengers and decomposers, with energy lost at each step so there are always fewer hunters than prey
  - [ ] Nutrient cycles: carbon, nitrogen, phosphorus and water cycle through air, sea, rock and life (`content` top-100 nutrients list)
  - [ ] Carrying capacity: each place holds only so much life; boom and crash when food comes and goes
  - [ ] Niches: each species fills its own way of living; two cannot share one for long
  - [ ] Keystone species: some species hold a whole system together; lose them and it collapses
  - [ ] Invasive species: life carried to a new place can take over where nothing eats it
  - [ ] Succession: bare rock, then lichen and moss, grass, bushes, forest; after a fire or flood it starts again
  - [ ] Biomes: deserts, grasslands, forests, tundra, reefs, deep sea and the rest, set by heat, water and light
  - [ ] Seasons, day and night: life times its breeding, growing, sleeping, hibernating and migrating to them
- **How species change and split**
  - [ ] Natural selection: the ones that fit best leave the most young
  - [ ] Sexual selection: mates choose, so displays, songs, horns and colours evolve
  - [ ] Genetic drift: in small groups, genes change by chance
  - [ ] Gene flow: groups that meet mix their genes
  - [ ] Speciation: groups cut off by seas, mountains or distance become new species (allopatric), or split in one place
  - [ ] Hybrids: close species can cross; sometimes a new species comes of it
  - [ ] Convergence and arms races: the same answers evolve again (wings, eyes); hunters and prey, parasites and hosts push each other
  - [ ] Mass extinction and recovery: after a die-off, survivors spread into the emptied world
- **Bodies, behaviour and minds**
  - [ ] Body systems from the genes: breathing (gills, lungs, skin), blood and hearts, guts, skeletons, nerves, hormones
  - [ ] Warm and cold bodies: warm-blooded life needs more food but lives in the cold; cold-blooded life needs the sun
  - [ ] Camouflage, mimicry and warning colours
  - [ ] Behaviour: territory, migration, hibernation, sleep, courtship, nest building, hunting together
  - [ ] Learning and memory: from simple habits up to teaching the young, then culture
  - [ ] Signals: scents (pheromones), calls, song, colour, light, dances
  - [ ] Kin and helping: helping relatives, colonies with workers that do not breed (ants, bees, termites, naked mole rats)
- **The great steps of life (the major transitions)**
  - [ ] Molecules that copy themselves gather in bubbles (protocells)
  - [ ] Genes join into chromosomes
  - [ ] RNA gives way to DNA and proteins (the genetic code)
  - [ ] Simple cells become complex cells with a nucleus, by swallowing partners that become their power plants and chloroplasts
  - [ ] Sex: mixing genes from two parents
  - [ ] Many-celled life with cells that take on different jobs
  - [ ] Colonies with castes that do not breed
  - [ ] Societies held together by language
- **Life changes its world, and the world changes life**
  - [~] Life makes the air: oxygen from photosynthesis, the great oxidation, the ozone shield, methane from archaea (built: plant-like life makes oxygen, the seas rust first, then the great oxidation fills the air to about 21%; early microbes make methane that oxygen destroys; animals with nerves need oxygen; the ozone shield still to come)
  - [ ] Life makes rock and soil: reefs, chalk, coal, soil from roots and rot
  - [ ] The world shapes life: plate movement splits and joins lands, ice ages, warming, impacts and volcanoes

### R9 — Intelligent life
**Spec:** `wiki/systems/r09-civilization.md`, `docs/wiki/ages-of-civilization.md`, `docs/wiki/tech-tree.md`, `docs/wiki/society.md`
- [ ] Build from `content/cities.dnat`: all 50 entries (Settlement sizes, Layouts, Districts, Infrastructure, in rough order of invention, Building materials by age) and these rules:
  - [ ] city-seed: every settlement, building and road is generated live; nothing is pre-built
  - [ ] city-own-image: cities follow the builder species' body plan and genes: insect species build hive cities of segmented cells, burrowers build underground, fliers build tall towers and roosts, swimmers build reef and underwater cities, giants build huge, small species build dense
  - [ ] city-grows: a settlement climbs the 100 grouping steps (camp, hamlet, village, town, city…), growing districts and infrastructure as it goes
  - [ ] city-age: the civilization's age decides its materials and infrastructure, from mud and paths to composites and spaceports
  - [ ] city-terrain: cities grow along rivers, coasts and trade routes; hills get defenses; layouts bend around the land
  - [ ] city-society: the city's society shapes it: temples tower in theocracies, palaces in monarchies, factories in industrial economies (100 kinds of society)
  - [ ] city-conditions: on hostile worlds cities need the tech for every world condition: domes, shielded roofs, floating platforms (the 10 × 10 world matrix)
  - [ ] city-fall: collapsed civilizations leave ruins that decay and can be resettled

- [ ] Tribes and culture
- [ ] Politics
- [ ] War
- [ ] Technology tree
- [ ] 100 tech branches in 10 families (`docs/wiki/tech-tree.md`)
- [ ] 100 kinds of society: rule, trade, family, belief, law (`docs/wiki/society.md`)
  - 100 ages in real-history order. 100 branches in 10 families. Each discovery needs the one before it in its family, plus links across families.
- [ ] Ways civilizations destroy themselves
  - A collapse can send a civilization back to earlier ages.

### R10 — Space
**Spec:** `wiki/systems/r10-space.md`, `docs/wiki/space-and-dyson-spheres.md`
- [ ] Build from `content/space-tech.dnat`: all 44 entries (How big a civilization's power is (Kardashev scale), Megastructures, Space habitats and stations, Ships by job, Drives) and these rules:
  - [ ] tech-seed: every station, ship and megastructure is generated live; nothing is pre-built
  - [ ] tech-own-image: built things follow the builder species' genes and body plan: an insect species builds insect ships, stations and cities
  - [ ] tech-ladder: what a civilization can build follows its place on the 100-step travel ladder and its tech branches
  - [ ] tech-kardashev: Dyson swarms and spheres mark a stellar (Type II) civilization; galactic megastructures mark Type III
  - [ ] tech-conditions: colonies need the tech for every condition of their new world (the 10 × 10 world matrix)
  - [ ] tech-resources: every build costs resources from the 100 space resources; megastructures can strip whole planets
  - [ ] tech-hypothetical: real-but-unbuilt designs are allowed in the game; they unlock late and are marked hypothetical in content

- [ ] 100 kinds of space objects, generated live (Oort clouds, Kuiper belt, comets, asteroid belts, nebulas, moons and more): "remebr space has all kind sof things for ort clouds to comets and merots hitting planets and astorid belts and nebulas andplanets of all makes ups with temp ranges that need different techs and inter solarystem flt hyperdrives for glaaxy to galaxty ect ect all planed out in all docs"
- [ ] Impacts: comets and meteorites hitting planets, 10 sizes
- [ ] World-condition matrix: 10 conditions × 10 levels, each needing its own tech, per species
- [ ] 100-step travel ladder: in-system flight → interstellar → hyperdrives → galaxy to galaxy
- [ ] 100 space resources
- [ ] 100 kinds of colony, including mining outposts and prison colonies: "resources and colinatzitions of other planets mining outsposert prison coloniers all of it"

- [ ] Space travel and reaching other planets
- [ ] Spreading life and colonies
- [ ] Dyson spheres
- [ ] War across galaxies

### R11 — God mode
**Spec:** `wiki/systems/r11-god-mode.md`, `docs/wiki/god-powers.md`, `docs/wiki/souls-and-harvesting.md`, `docs/wiki/nudges.md`, `docs/wiki/dark-harvest.md`, `docs/POWERS.md`

- [ ] Build all 100 powers from the power list (`docs/POWERS.md`, `docs/wiki/god-powers.md`)
  - Names as decided, including #4 Starshine and #81 Stellar Flare ("Starshine + Stellar Flare (Suggested)").
  - No direct DNA editing: "no direct edit besids using powers to influence things to happen".
- [ ] 100 nudges, each driven by its powers (`docs/wiki/nudges.md`)
- [ ] Unlocking powers with points from the amount of life in your galaxies
  - Each power unlocks at exactly one stage. The 5 starters are at stage 1. The table is in `docs/wiki/god-powers.md` and `docs/wiki/stages-of-life.md`.
  - 100 unlock points = the 100 stages of life: "Stages + ranked tiers (Suggested)".
- [ ] Paying for powers by harvesting life: "maybe u have to harvet life and later on u get the intelligent sure intelligences using their powers to harvest life for you"
  - The currency is **Souls**. Cost = tier × area × strength. 100 tiers, T1–T100. T81–T100 have cooldowns.
  - Harvesting is slow and shows as a glow that scares intelligent life. Mass deaths give lots of souls.
- [ ] Build the Dark Harvest system: 100 methods, each gated by age, pushed by powers, paying souls per death (`docs/wiki/dark-harvest.md`)
- [ ] Endgame: intelligent life harvests life for its own needs, spreads and uses up nutrients and resources: "since all life is like a virus advanced forms seprad and use up avasil nutrientrs and resources and have to spread. jsut liek real life"
  - 100 harvest steps, one per age, with the six big jumps marked.
- [ ] Gene powers with good and bad outcomes
- [ ] Life starting from the player's first action
  - Starting toolkit: lightning, heat (fire), cold, water, spark of life, add genes, and add elements: "option one but things like add genes add elements ect ect real life so full perioditic table of elements".

### R12 — Time
**Spec:** `wiki/systems/r12-time.md`, `docs/wiki/time.md`, `docs/wiki/milestones.md`

- [x] Pause and speed controls (in the deep zoom: Space and 1–5)
- [x] Calm motion: on-screen motion capped, faster bodies shown as soft trails: "we cant have real motion with speed uup time and shit" / "people would get dizzy" / "Speed cap + trails (Suggested)"
  - Pause plus 1×, 10×, 100×, 1,000× and max.
- [ ] Automatic speed jumps at headline milestones only
- [ ] Milestone generator: 100 milestones per stage, built from the top-100 lists (10 families × 10). Milestone 100 is the headline (`docs/wiki/milestones.md`)
  - "Built from the 100 lists" / "Only each stage's big one (Suggested)"
  - Drops to 1× with a short cinematic. Can be turned off.
- [ ] Balancing so a game reaches space in about 4 to 6 hours
  - Each stage has its own years-per-second.

### R13 — Game shell
**Spec:** `wiki/systems/r13-game-shell.md`, `docs/wiki/hud.md`, `docs/wiki/menus-and-settings.md`, `docs/wiki/controls.md`

- [ ] 18+ rating shown on the store/release page, installer and main menu: "this is a 18 plus game"
- [ ] Open-ended play: "open ended till u resarts to get a new world and galaxy"
- [ ] Non-intrusive HUD, world fills the screen: "and we havent talked about the hud much but it should be intrusive so the game window is as big as possible and everthing is click to open and click hold drag to move and everything is properly wow type of game paly action bar buiold your own with setting and wiki and you main curancy and coutnes and time at the header and all of that and things like the element tree and abilityis to add things liek them and water and head with painter tools and brush sizes unlockable as time goes on making things like harvest and powers effeect bigger areas as time goes on and life stpreads"
  - [ ] Header bar: settings, wiki, souls, life, harvested, score, time controls, date
  - [ ] Build-your-own WoW-style action bar, drag-to-slot, rebindable hotkeys, cooldown sweeps
  - [ ] Click-to-open panels, click-hold-drag to move
  - [ ] Element tree panel (periodic table)
  - [ ] Painter tools: element, water, heat, cold, gene, power, harvest brushes
  - [ ] Brush sizes unlocking as time goes on and life spreads, with bigger areas for harvest and powers
- [ ] Main screen
  - New World, Continue/Load, Multiplayer, Genebank + Leaderboard, Wiki, Settings, Credits, Quit.
- [ ] In-game wiki of everything
  - Reads the same pages as `docs/wiki/`.
- [ ] Settings
  - Graphics, Sound, Controls (every key can be rebound), Gameplay, Online, Accessibility.
- [ ] Save, load and quit
  - Autosave every 10 minutes and at every headline milestone. Unlimited slots.
- [ ] Controls: mouse + keys
  - WASD/edge pan, scroll-wheel zoom, right-drag rotate, left-click power, Space pause, 1–5 speeds, Esc menu, F1 wiki.

### R14 — Multiplayer
**Spec:** `wiki/systems/r14-multiplayer.md`, `docs/wiki/multiplayer.md`

- [ ] Single player
- [ ] Co-op and multiplayer in one shared world (one player's computer hosts)
  - Join code, up to 8 players, our own reliable messaging over UDP.
- [ ] Seeing each other's cursors

### R15 — Leaderboard and Genebank
**Spec:** `wiki/systems/r15-leaderboard-genebank.md`, `docs/wiki/leaderboard-and-genebank.md`

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
**Spec:** `wiki/systems/r16-packaging.md`, `docs/SETUP.md`

- [ ] Windows exe
- [ ] Our own `DNA-AND-Setup.exe` installer
  - Uninstall keeps saves and asks whether to delete them.
- [ ] Post each release on both release pages (git.unityailab.com and GitHub)
- [ ] (Later) Mac program
- [ ] (Later) Linux program

### R17 — Docs (upkeep)
**Spec:** `wiki/systems/r17-docs-and-capture.md`, `wiki/CLAUDE.md`, `docs/WORK-LAWS.md`

- [ ] Fill all 55 image slots with the R4 capture tool
- [ ] Keep README and DESIGN up to date
- [ ] Every push: public docs (README, player wiki, SETUP, HOW-TO-BUILD) carry no work notes, per WORK-LAWS LAW 15: "the wiki is public facing so it doesnt have work note and no public document shall have work note and numbers and stuff in them" / "and make that a law and build it inbto the docs"
- [ ] Keep every wiki page true as code lands: update `last-verified` and flip `status` from draft to verified

---

## TOMBSTONES

- **The self-built Helix toolchain (R1)** was retired in round 20 ("Use C# built into Windows"). The original tasks, verbatim:
  - [ ] Design the tiny first tool, written directly as raw machine bytes, that can turn simple text instructions into machine code
  - Changed: the first tool is a PowerShell assembler (`bootstrap/asm0.ps1`): "Start from PowerShell (Suggested)"
  - Decided: a **hex loader** typed in as raw bytes. It turns a text file of hex digits into a Windows exe. Broken down in `docs/DECOMPOSED.md`.
  - [x] Exact spec written: `wiki/systems/r01a-seed-spec.md` (exe layout, hex format, imports, proofs)
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
- **Old per-subject sample tasks (round 6)** were replaced by the ordered "Sample sets" list (galactic scale down, full tree of life). Original text, verbatim:
  - [x] Samples: Microbes (3 examples × 3 styles)
  - [x] Samples: Viruses (3 examples × 3 styles)
  - Samples: Intelligent creatures (3 examples × 3 styles)
  - Samples: A handful of other living things (plants, insects, animals, fish, birds) (3 examples × 3 styles)
  - [x] Samples: Galaxy layouts (3 examples × 3 styles)
  - [x] Samples: Solar system layouts (3 examples × 3 styles)
  - [x] Samples: Planets (3 examples × 3 styles)
  - [x] Samples: Stars (3 examples × 3 styles)
  - [x] Samples: Gas giants (3 examples × 3 styles)
  - [x] Samples: Dyson spheres (3 examples × 3 styles)
  - Samples: Space ships (3 examples × 3 styles)
  - Samples: Space colonies (3 examples × 3 styles)
  - Samples: Space stations (3 examples × 3 styles)
  - [x] Samples: Resource fields (3 examples × 3 styles)
  - [x] Samples: The whole periodic table of elements (all 118) (3 examples × 3 styles)
- **"Combined score from all the categories"** (R15). Replaced by the creator's round 4 answer: "life in your galaxy and life \"havested\"". The new task is under R15.
- **"Power list (`docs/POWERS.md`)"** (R11). The list itself is done (see FINALIZED). Replaced by "Build all 100 powers from the power list".
- [x] **Stars from the templates:** "okay thoise are good to be templetes for all the variatiosn and colors and sizes like real starts". Build every star kind in content/stars.dnat from the approved set-3 templates, with real colours and real sizes. Done: all 26 shining kinds have real temperatures and shares, colour from temperature, brightness from size and temperature (`content/stars.dnat`, `GalaxyGen.ColorFor`).
- [ ] **Remaining sample sets in the locked style only:** "yup same one the center one is the best, dont need to test them twice". Sets 4–17 show only the Populous-like style.
- [ ] **Stuck at the deepest zoom inside the Dyson sphere:** "when i zommed all all the way in to the ground as close as i could get, i got stuck in the ground and couldnt back back up(but that can be solved when we do micorscopic)". Fix with the microscopic set.
- [ ] **Sample sets in proper order:** "okay well we need to do everything in proper order for the full game". The order is elements → microscopic world (viruses, microbes) → kingdoms of life → intelligent creatures → cities → stations, colonies and ships (built from their species).
- [ ] **Elements into the in-game wiki:** "looks good that can go in the wiki once we flush it out with tool tips throughought and propelry gate it all off for balanced play and leveling". This needs tooltips throughout, and gating of everything for balanced play and levelling.
- [x] **One journey, starting among the galaxies:** "and its starts in the mix of glaxixies" / "no that started above a planet". A view of many galaxies, then zoom into one, then its solar system, a planet, the surface, and the microbes.
- [x] **A 10-second clip of the journey:** "make it a 10sec clip" / "save it". Saved at `docs/img/journey.gif`: the mix of galaxies, into one, its stars, a temperate planet, its surface, down to the ground. Made by our own clip writer (`--clip <start s> <seconds> <file.gif>`, `src/render/GifWriter.cs`) with `--journey --aimcentre --wheel 0.5 7.2 --wheelevery 3 --journeyat 2.5 --clip 1 10 docs/img/journey.gif`.
- [ ] **Life from more than one start:** "ur lighning bolt can start life again too u can start life in more than one place on one planet". The lightning-bolt power can start life again at other spots on one planet, each strike costing Souls ("not as many times as you like powers take life souls"). Each start is its own lineage in the planet's life simulation and on the tree of life.
- [ ] **Powers and abilities unlocked by the tech of life:** "like for example u shouldnt be able to use add elements and such until unlocked by life... so alot needs to go into the tech of life and how it all unlocks your powers and abilities". Decided: day one is Fire, cold, water and lightning only; each element unlocks once living things first use it. Still to design and build: what unlocks Add Genes, the Spark of Life power and every other power and ability, and the element-use tracking in the life sim.
- [ ] **Life evolves from the primordial ooze:** "and dont forget the whole dna thing.. life evolves we dont start with trees and animals, from the primordial ooze like". Already how the planet life sim works (one spark of simple replicators; everything else evolves); keep it so in every system.
- [~] **Full ecology: spread, prey, predators, herbivores:** "life need s top spread too if nutrtients and conditions are favoriable with actual prey peditor herbavor type shit effecting the life outcomes in full ecologoical like game, so when u start u just have viruses that evolve cells and nucleuses and all the way up the chain of life". Built: a food web per region (producers, grazers, hunters, nutrients) and spreading only where nutrients and conditions suit (`src/sim/PlanetLife.cs`). Still to do: a nucleus step (cells with a nucleus) between first cells and many-celled life.
- [ ] **Life waits for the genome, Genebank and heredity charts:** "well i wouldnt wory about the life just yet we havent built out all the gene bank and shit for the game and how creaturtes evolave and tthe fulll aspect of life and all the genology and heretitanc graphing and tables and charts". Do not tune or judge life outcomes until R7 (genomes), the Genebank, evolution, and the genealogy and heredity graphs, tables and charts are built.
- [~] **The ground you see follows the living planet:** seas, mountain heights, drifting continents and great craters, shown as a whole-planet swap at big moments under a short fade ("Whole-planet swap"; `PlanetTerrain.SwapGround`, `PlanetView.ReshapeGround`); ground pieces split by their size in pixels. Still to do: plate edges are straight lines that cut coasts straight (they should wind and break); new volcanic land; ice sheets drawn; old craters wearing away.
- [x] **Checks at the player's real resolution:** "try running it at reall game resoultion like my native resoultion im using now,, tests that dont run the game in real world application of real resolutions are worthless". Visual checks use `--native` (a normal window the size of the screen). First finding at 3818x2074: the ground seen from orbit is coarse and blocky; plate edges cut coasts in straight lines.
- [x] **Pulsars no longer turn up in almost every system:** "and something is up it has a pulsar like jet image on almost every solar system i zoom in on, thats not acurat pulsars are their opwn thing not a setellar object that u see zooming into stars". Dead stars lean to old regions; pulsars rarest (`GalaxyGen.Biased`, `content/stars.dnat`).
- [x] **Smooth zoom, no planet jump:** "and the zoom in looks okay but it goes to fast on the solar system and its like boom theres the planet it just kinda appears with skipping the infinate zoom". The wheel glides (`Input.Glide`), notches count 0.7; a planet pick keeps the distance; handoff at 3.3 radii (`DeepView`).
- [x] **A toggleable scale on screen while zooming:** "it zooms too fast and when zoomeing maybe we need a hud scale designator ofr something thats toggle able". Built in the one world: a bar of a round length with its label in the game's own letters, H turns it on and off (`src/shell/ScaleHud.cs`).
- [x] **True infinite zoom, no stepped loading:** "the infinate zoom also doesnt seem to be fully integrated as its like shit zoom placeholders that just load zoom points or some artifical stepped loading shit loading points". The camera must never wait for ground pieces; build pieces on every core; re-map the zoom onto wavelet levels with mode-E below the finest (R4.6).
- [x] **No pulsar jets over every star:** "zooming in too fast and get rid of the pulsar jet thing on all systems of stars". The core black hole's jets show only at the core (`DeepView.Draw`); slower zoom steps.
- [x] **Stop at the solar system, then on to a planet:** "you keep zooming althe way to the star instead of pausing on solar system view and zooming into a planet". (`DeepView`: floor at the whole system; `PickNearestBody`.) Interim, until the one-world zoom replaces it.
- [x] **One world, one camera, no handoffs (true infinite zoom at any angle):** "tha angular reelation is not correct when zooming into a galaxy as when approaching a galaxies its just instantly pans flat instread of appearoaching at the angle u are currently in relation to the galaxy, stop it from panning flat when zooming into a galaxy and make it real infinate zoom at any angulare relation and vector" / "no handoffs, that is the shit zoom staging i was talking about"
  - [x] One world holding every scale: the universe of galaxies, each galaxy's stars, each star's system, each planet's ground, its life and its microbes, all placed in one shared space (64-bit positions, a moving origin)
  - [~] One camera that flies through it at any angle and direction; zooming moves the camera toward what the mouse points at, never switches views (built for the universe and galaxies: `src/shell/UniverseView.cs`, `--world`; the wheel flies along the mouse's line by a share of the distance to the nearest thing, so it slows by itself and never ends; a galaxy fades from its far glow into its own 150,000 stars at whatever angle you come in)
  - [x] What is drawn is chosen by how big it looks from the camera, so things grow smoothly into view (a galaxy into its stars, a star into its system, a planet into its ground) with no pop
  - [x] Detail at every scale from the wavelet levels and mode-E (R4.6), so nothing loads in steps
  - [x] Below the ground's finest piece, the ground's own matter, all the way down: "i love the scale when u go down to um u should start to see atoms and shit not to mention microbes bacteria and viruses, but like i said life isnt built yet". Grains of sand and dust (mode-E), then the crystals of the real minerals that make the ground there (from its resource field and `content/elements.dnat`), then their molecules, then atoms of their real elements; microbes, bacteria and viruses join once life is built
  - [ ] Remove the separate views and their handoffs (GalaxiesView, DeepView, PlanetView switching) once the one world draws everything they do
- [x] **Real stars of the space you are in:** "the stars u are flying by are just place holders". Built: around the camera the galaxy fills in with its real stars at the galaxy's true density (about 100 billion in all), in 25-light-year cubes made from their own position (`UniverseView.LocalStars`); the nearest star is now light-years away, not hundreds. Built too: the wheel flies straight at a star (or, in a system, a planet or moon) within about 4 degrees of where you point; stars are drawn at their real size (a dot from afar); the star of the system you are in shows its real body (plasma surface and corona) at true size. Built too: within 40 of its radii a planet or moon gets its real ground (`PlanetTerrain`, with its wavelet change fields), drawn in the same camera from its own centre in km, lit by its star; the zoom measures to the ground and never goes below it. Still to do: the full path checked at native resolution; carrying on below the ground's finest piece with mode-E.
- [~] **The engine must use the GPU properly:** "and is our engine beffy enough? i see im only using like 2G of GPU and the game is running kinda slow". Today the CPU and GPU take turns every frame (EndFrame waits for the GPU to finish), the ground is built vertex by vertex on the CPU, and points are re-sent each frame. To do: overlap frames (two or three in flight), build the ground and rebuild the wavelet fields on the GPU, keep point sets on the GPU and move them with constants. Built: the ground is built in the background by one worker per core and each frame takes in finished pieces (never waits); 37 to 49 fps at 3818x2074 on a 2 km view. Still to do: frames in flight, the ground and wavelet rebuild on the GPU, points kept on the GPU.
- [x] **The universe preview release:** "then once i test it and give the okay. go ahead and buidl a exe. of the \"game\" (what we have as far as the universe thats just a dead universe but fully made with all the probelms ive said in totality fixed , then build and deploy the project with the binary iinstall of the .exe like the Agent Gen program and Gladiator folders on desktop do in buidling a binary in all the ways it works. so that wee have a sample universe .exe that runs and is just a preview of the universe fully working c/9 waveform fully and fuull infinate zoom fully tested by you them me befoer building the .exe completely and deploy of releases(noting this is just the univerese without game play or ui or features yet"
  - [x] The one-world universe complete, every problem raised fixed, the 9/7 waveform and infinite zoom throughout
  - [x] Tested fully by me at native resolution, then tested by the creator, who gives the okay
  - [~] The preview build: `build.cmd NAME preview` makes a copy that always opens in the one-world universe
  - [~] Our own installer, not NSIS: "Our own installer (Recommended)". Prepared, not yet built or released: `installer/Setup.cs` (installs to the user's Programs folder, Start menu and desktop shortcuts, Apps list entry, uninstaller) and `release.cmd` (preview build, zip, installer). Run only after the creator has tested and given the okay: "i havent tested yet, so dont build the exe yet"
  - [x] Only then: build the .exe and its binary installer the way the Agent Gen and Gladiator projects do, and publish the release
  - [x] **Its own Universe project (after the good test):** "and when we deploy the exe after i have a good test we need to make the program into its own repo with everything needed to build the universe only so its one two remotes on git and github need to make the github one with only supposrt documents for this universe program and everything any one would need to build inthis universe and change it and deploy their own exe in everything needed that we have made so far just not the DNA AND project for these repo remotes only a universe project with none of the life and project stuff so this is its own project made from DNA AND as a dmeo but not a dmeo the full universe we have sdo anyone can use it and build with it and all readme and instructions  for the technicals but a base good readme about the universe and how it works non technical and laymans like and fun to read and the exe has all of the public docs for it : https://git.unityailab.com/GFourteen/Universe.git"
    - [x] A separate repo with only what builds the universe (no life or DNA-AND project files), two remotes: https://git.unityailab.com/GFourteen/Universe.git and a GitHub repo (to make)
    - [x] Technical readme and instructions: how to build, change and deploy your own .exe
    - [~] A base readme about the universe and how it works, non-technical, layman-like and fun to read. Drafted: `docs/release/UNIVERSE-README.md`
    - [~] The .exe carries all of the public docs for it. Prepared: `release.cmd` puts the plain readme and the build guide in the package's `docs` folder, with `README.txt`; the Help menu carries About, Readme, How it works and Credits
    - [x] **Credits, readmes and repo links, pushed and released:** "and make sure the apps cedits redadmes technical readmes and documentations and links to the repose for Universe project are made and correctly pushed and deployed a release of the .exe for the full universe and all needed files and documentations of them for public release, but i stell need to test all the fixes weve talked about"
      - [~] The program's Credits and About link the Universe repo (git.unityailab.com/GFourteen/Universe); the GitHub link once that repo is made
      - [x] Technical readme (build, change, deploy your own .exe) and the public docs, in the repo and inside the .exe
      - [x] Pushed to both remotes and the .exe released, only after the creator has tested all the fixes
  - [~] About, Readme and Credits built into the program's menu bar, and its header tools such as the scale HUD for the zoom: "make sure the e.xe has a proper about and readme and credits built into the program header tools like the sclae hud for zoom relation" Built: the one world's menu bar has Exit, View (scale on screen, render scale) and Help (Readme, Credits, About) (`Window.SetWorldMenu`, `src/shell/PreviewText.cs`); the release build still to come.
  - [x] Windows and Linux installs: "need linix and windows install" / "for the .exe". The engine talks to Windows directly (Direct3D 12, Win32), so Linux needs its own port: Vulkan graphics and Linux windowing, our own code, no outside libraries
- [x] **No tearing at full zoom:** "at full zoom it like screen tears do u see that". The camera stays above the finest ground made under it.
- [x] **No shimmering ghosts on the planet:** "and planet has static ghost like marages on surface at planet zoom". A tight depth range for the ground.
- [x] **Runs well on anyone's computer, not just one:** "remember it needs to use whoes ever GPU not made just for mine". Workers follow the core count, ground detail the screen's resolution, budgets are in milliseconds, the render scale trades sharpness for speed. Built: the strongest graphics card is picked (the most video memory of its own, software ones skipped), named in the run log (`Gpu.PickAdapter`). Built too: cards with less than 3 GB of their own video memory start at a 75% render scale, less than 1.5 GB at 50%.
- [x] **Drag directions:** "the conrols are reverese need to have defult left right drag direction reverse and y axis inverted too by default with ooption in settings". Reversed by default both ways; View menu: Invert left-right drag, Invert up-down drag; kept in settings (`src/platform/Settings.cs`).
- [x] **Click to focus:** "iots not focusing on wherte i click, when i zoom its not focusing where i click it just keeps zooming in straight line where ever i am". (`UniverseView.Pick`, `TargetFromCamera`.)
- [x] **How it works, in the program:** "and i didnt see the about of the readme explaining c/9 waveform and infinate zoom and all of that". Help > How it works (`PreviewText.HowItWorks`), pointed to from About and Readme; the same in `docs/release/README.txt`.
- [x] **See the pick, click any star:** "yeah im clicking on stars and im not seeing my selection any more and i cant click on every star". Screen-distance picking over every star; a dashed ring marks the pick (`ScaleHud.DrawRing`).
- [x] **Reach the picked star:** "when i zoom in trying to reach a star i think fly by stars are fucking with the zoom because im zooming in but it keeps jumping weteen 5 and 2 lightyears never getting to the star even after a shit tone of zooms in". Steps go by the distance to the pick only.
- [x] **No handoff into a galaxy:** "there are still hand offs on the galazy view i told u no hand offs real infinate zoom". One continuous star field per galaxy (`UniverseView.StarsShown`, `InsideShare`, background extra stars).
- [x] **No hexagon placeholder near a star:** "take a look at 1 lyr scale the solar system has a place holde4r emblem image of a hexigon w block over lay thing". Galaxy points shrink fully to points of light inside it; to be confirmed by the creator.
- [x] **Planets no longer outrun the zoom:** "i cant zoom in beyone 200000 km it keeps doing that thing again where i cant zoom in all the way aits like the planets are our running my zoom". The camera rides along with the picked body from any distance (`UniverseView.FollowBody`).
- [x] **Zoom reaches the ground:** "same thing cant get close thatn 200000km". The ground floor only slows the descent (never pushes back; each notch closes a tenth) so finer ground catches up (`UniverseView.Update`).
- [x] **Planets reached, no hexagons:** "still cant correctly zoom to planets and they are still showing hex grid placeholders when zoomed out". At the picked planet the wheel steps by the height above the ground (`UniverseView.Update`); points are round, not hexagons (`BillboardPipeline` shader).
- [x] **Drag circles the pick:** "and the click to drag is cinda fucked view as if i want to see a solar system top down becasue i approched at a angle to the plane of it it need like a rotats at distance of the focal point with in colore in virews and even for glazxy view so when slecting a stellar body u can rotate around it at distacne selected and focal range,, you knwo what i mean". Dragging circles round the picked galaxy, star or body at your distance (`UniverseView.Update`); readmes updated.
- [x] **W A S D Q E fly at scale:** "and wasd qe should properly allow moving at scale like fisrt person shoooter like with scaled movememnts". Speed is half the distance to the nearest thing a second, Shift four times (`UniverseView.Update`); readmes and title updated.
- [x] **Information cards on every pick:** "okay not we need attached conise and informative tool tips like hud attached information to all the planets bodies stars so anything thats selectable lists what it is its id whats there ie a start number of planets a planet type and all the relevant information needed from any star planet or any astrial boddy of selection of stuff like on planets water dirt its make up ect ect a solar stars information type tempature habiilata ranges for planets and stuff(but remember this is just the Universe repo and exe we are trying to get done". `UniverseView.Card` and `ScaleHud.DrawCard`; star temperature and mass kept in `StarSystem`; the font has colon, percent, brackets, plus and tilde.
- [x] **Ride-along kept far out:** "i still cant zoom in to planet level it maxes zoom at 1M km" / "same problem!". `DropPlanet` no longer resets the ride-along; only `CloseSystem` does (`UniverseView`). Found with the zoom log (`%TEMP%dna_zoom.log`).
- [x] **Into a gas or ice giant, down to its core:** (down through its layers to its core, and into each layer's own matter part way down, DESIGN 311-312) "something is wrong i zoomed to planet now but the planet never rendered its world" / "even gas giants have a core of solid material" / "do they not?>". The zoom stops at the cloud tops now. To come: on down through the deepening clouds and hotter, denser gas (for Jupiter, to metallic hydrogen) to a core of rock and ice, a hot dense core with no real surface.
- [x] **Thrown out of a system at a planet:** (found: zooming out right at the ground stepped about 9 light-years; fixed) found in the zoom log, at 0 km above an Ice giant the camera ended up 3 light-years from any star. Logging system open/close reasons and any camera jump over a million km (`UniverseView.ZoomLog`) to find which step does it.
- [x] **Level to the horizon, see at night:** "do you see what im seeing the planet view never rotated to the horizon and its too dark ass u can see im 4km from a planet and i cant see shit". `UniverseView` (levelling, `ViewUp`), night light in `MeshPipeline`; ring hidden when it would fill the view.
- [x] **Ghost moons, seeing through planets, thrown out at 0 km:** "take a look i found planet moons that like are ghosts and cant zoom into" / "yeah zoom is still fucked at 0km i jsut see through the planets". Ride along before choosing the body (`UniverseView.Update`); outward wheel step floor no longer about 9 light-years.
- [x] **Click lands where you click on the ground:** "im clicking all over the planet suface at 500km and its only letting me select a single point on the planet not correctly where ever i want". `UniverseView.MouseRay` and W A S D use the drawn view's right and up.
- [x] **Right click clears the pick:** "and i need a way like right click to clear sleection". `UniverseView.Update`; readmes and title updated.
- [x] **Not through the ground at 100 m:** "at 100m the planet started disapearing and i could see through it". Floor of 0.6 of the finest drawn piece for the wheel and W A S D Q E (`UniverseView.Update`).
- [x] **Down to the real ground below sea level:** "zoom maxes out at 200M,,.... cant get any close so u didnt fix it" / "and iots taking way too long for the terrain to load in like minutes and some times not at all" / "like i only see one green color on the whole screen at 200M" / "maxed out at 200 mm and land never spawnsed in" / "but after i zoomed way in". `UniverseView.Surface`: sea level only where there is sea.
- [x] **Night never pitch black:** "and look at night time for planets its so dark u cant see the land or water or environment,, i know how light works but we cant have pitch blackness.. ur a god so just a dark slight tint". Half brightness at night with a slight cool tint (`MeshPipeline` shader).
- [x] **Light from the star and the air:** "not a blue teint light is based off the atmospher and start tempature". `UniverseView.GroundLight`, `PlanetTerrain.SunColour/NightColour`, `MeshPipeline` (sunCol, nightCol).
- [x] **Fine ground arrives fast:** "shit is way to blocky at 1m scale". `PlanetTerrain`: pieces no longer wanted are neither built nor uploaded (`Wanted`), 12 ms of uploads a frame.
- [x] **Only what is in view loads:** "its like its trying to load everything instead of whats in view as i cant zoom in at the zoom spped i want and am having to wait for it to load when this didnt use to be the case". `PlanetTerrain.ViewDir` / `ViewHalfAngle`, set by `UniverseView`; most-needed-first building and uploading.
- [x] **Not seeing through the ground on high land:** "im seeing through the palnets at 50M" / "take a look". Nearest drawing distance from the height above the real ground (`UniverseView.Draw`).
- [x] **Clicks land where you click, at every scale:** "at 100M its not letting me click where i want, all scales need to allow clicking where u actually click not predefined click ponts". Ground: line of sight to the real ground (`UniverseView.Pick`); matter: any spot on the surface (`MatterView.Pick`).
- [x] **GPU use, a real share of the card (never over 90%):** "and we need a infinate scaled for all kinds of GPUS a setting of how much of ur GPU to use" / "as right now it says 60^ but only useing 2G so idnt know whats up with it" / "400%%?? no!!!! no gpu goes over 90%". View > GPU use; card time measured by timestamps (`Gpu.GpuMs`); detail tuned to it (`Program`, `Window.Detail`). Still to tune with it: galaxies, stars, matter.
- [x] **Time controls, and days on every planet:** "and we need time controls" / "so i can speed up or pause during day". `UniverseView.TimeSpeeds`, Space and , . keys, Time menu; each planet's own day (`DayHours`, `Spin`); local time in the title.
- [x] **Grains as real stones, no pop, no fighting the zoom:** "something is up at 100 mico meters its just matrix of 2d white ghosts,,, and im still having a hard time zooming where its fighting me on zoom as it tried to load in the view, take a look at my screen... they just instantly pop in at 100 micrometers" / "i like how it keeps the textures tho as u zoom in, but that really isnt accurat". `MatterView.Stone`, overlay from 1 cm (`MatterView.OverlayKm`), floor lifted below it.
- [x] **Click a grain:** "and i cant click on individual grains". Clicks under a centimetre go to the grains (`UniverseView.Update`), on into the matter at the same scale; the way out is where you came in (`_matterTop`).
- [x] **Real sizes and structures in the matter:** "okay the grains are sleectable and then i keep seeing them when zoomed in to molecule level and then element level its just a generic matrix and ther is only one atom electons and the scalle isnt right i think u need to do actual research for these zoom levels on what they should be" / "they are not sleectable i mena and need to be". Grain sizes per ground, terraces, structures for every mineral, electrons on every near atom, the pick ring (`MatterView`, `content/minerals.dnat`).
- [x] **Looking at the grains:** "that grain bvoiew needs figured out im still looking at hroizon and selected grains are out of view". View tilts about 70 degrees down among the grains (`UniverseView.Update`).
- [x] **Drag follows the mouse at every scale; picks centred:** "the view at all zooms is not correctly following the mouse... they are all fucking differnt and the select grain or atom should center on the screen". One grab rule (`UniverseView.Update`), straight down among the grains.
- [x] **No switching layers: every layer grows out of the one before:** "and this better not be bullshit handoffs at zoom levels this is full fledge infinate zoom". Atoms now grow up out of the grain's face in its colours; the nucleus is always its nucleons, and the grain becomes its face once it fills the view, in its own colour.
- [x] **Into electrons, protons and neutrons:** "even electrons are zommable to". Electrons clickable with their own card (a point as far as measured, smaller than 1e-22 m, its probability cloud round it); protons and neutrons zoomable into their three quarks (up and down, held by gluons, about 0.84 fm across).
- [x] **Every atom its own system:** "okay look at atom view i can still see orb atoms when the elcrons are in view and thats not a real valance shell like electron and orbital paths shit properly as i want it" / "it should be like star systems like view but for every atom and of atom type its size and number of electorns". `MatterView.Cloud`: nucleus, every electron on its path, valence shell in gold; real atomic radii.
- [x] **Everything pickable at every level, with proper tags:** "and u fixed it so all levels of xzoom all things are selectable properly" / "with tags properly made". Pickable: galaxies, stars, planets and moons, ground spots, grains, face spots, the top layer of atoms, every near atom's electrons, the nucleons and quarks of the atom you are in. Also belts, molecules, every atom layer, every nucleus, and the near galaxy's star clusters and nebulae.
- [x] **Into a nebula's gas, down to atoms:** "i can only get to 2 light yrs of a nebula instad of the infinate zoom" / "stop starting it till its fixed still 2 l yrs is as close to nebulas as i can get instead of correclt down to atoms". Flown into, not stopped at (`UniverseView`); its gas at real densities (`MatterView.SetSpace`).
- [x] **The zoom holds its precision in a nebula, and the scale is right:** "make sure of this" / "its at light year scale saying next atom 1mm away wtf" / "and it never gets the scale right" / "fixed means everything ive said in totality". Anchored on the nebula point (`UniverseView.ChooseSystem`); scale and title from `ViewDistanceKm`; test path `--autonebula`.
- [x] **Space between the stars, and inside clusters:** "i clicked on random galaxy no stellar object like nebula, and a globula cluster i zoomed in on was just empty space". Empty space picks a point (kind 5, no feature); cluster stars at real density round you (`UniverseView.ClusterStars`). A cluster star opens into its solar system too.
- [x] **Nebulae in every kind of galaxy:** "only one galaxy is fully built out" / "n o nebula shit". Star-forming kinds get nebulae and young clusters by how much they form stars (`SpaceGen.Structures`); every galaxy within about 60 of its sizes shows and picks its own (`UniverseView.EnsureStruct`).
- [x] **Every kind of body, in real numbers, coloured by its make-up:** "im seeing galaxies with only one nebula yuou didnt propely put in all the stewllar bodies" / "and thir colors should match what they are made off. there arnt only hydrogen nebulas" / "there all all kinds of things in alcohol nebulasd" / "i think ur research is failing". `SpaceGen.Structures`, `Composition`, `Colour`; planetary nebulae, supernova remnants, dark clouds added.
- [x] **Atom-scale picks centred, time freezes the matter:** "it not focusing what i click on on atom scale and time is not freexziong electrons". Focus point in 3D (`MatterView._fy`); `UniverseView._matterClock`.
- [x] **The last switches made smooth:** "and everyhting is fixed so no hand offs on zooming right?". Still switching: a planet's glow to its ground (at about 40 of its widths); a galaxy's clusters and nebulae showing once it is among the 8 nearest; the stars behind cut below a tenth of a millimetre; a nebula's gas atoms appearing within about ten spacings.
- [x] **Every zoom level of the ground filled:** "okay zooming in on a land of a planet gets to grains but themn its just levels of the grain surface no molecules or anything and nothing is clickable at all so there are whole zoom levels not complete and not built yet" / "and at fentometers i see the surface terrain again". `MatterView.SurfaceThings` (inclusions, clay, coatings, nanoparticles, the water film); ground mesh not drawn below 0.1 mm.
- [x] **Small ground features at metre scale:** (done: boulders, cobbles, pebbles and granules from each ground's clasts, GrainField, DESIGN 272) "do you see what i see a 2m scale weird cliffs that dont look correct but i suppose when we had features to planets dynamicly this will be fixed". At a few metres the ground is only its big shapes magnified (a smooth wall); to add: rocks, stones, pebbles, soil and scree grown by mode-E below about 10 m, and a look at the vertical streaks (likely the crack-hiding skirts and stretched pieces on steep slopes).
- [x] **Real matter, real atoms, every element:** "okay i only see atoms fade in not accuralty scale into view and no other depth layers exist and atoms need to be the material thats zoomed in on u know how atoms work right? if i zoom in on one and the electrons obiting nucleasue of protons and nuetons based on the element zoomed in on silicon and what not" / "but for all elements an planet is not only silicon" / "they are all differnt". `MatterView` rewritten at real sizes; `content/minerals.dnat` (new); `mass-number` for all 118 in `content/elements.dnat`.
- [x] **A real document viewer:** "and wtf the help and about and readme and technicals are fucking popup warnings you fuck! wtf are you thinking not putting a reall viewer for douments with all the bells and whistles". `DocViewer`: contents of every document and its headings, formatted pages (headings, bold, lists, code, tables), clickable links, search across all documents (Ctrl+F, Enter, F3), text size (A-, A+, Ctrl+wheel), Back, copy; the plain-words guide and the build guide are built into the .exe from docs/release.
- [x] **Bodies in the matter, picked and flown into like solar systems:** (grains and atoms pick, with cards and orbit rings; molecular ices and sea water in their real molecule shapes; halite and quartz in real crystal structures; still to come: the other minerals' structures, molecules and crystal shapes as their own bodies, real crystal structures, the waveform carrying the matter fields) "and just like u zoom in tho the planets zoom into the microscopeic should have bodies of the mixture you can zoom in on just like solar sytems and shit of crystals molecules and shit" / "but authentic and relaaistic" / "atom view should be like reall orbiting solar systems of their own" / "c/f waveform should handle all of this". Grains, crystals, molecules and atoms clickable with their own cards and the wheel flying to them; each atom its own system of electrons round its nucleus; the CDF 9/7 waveform carrying the matter fields (grain surfaces, crystal shapes) as it does the ground.
- [x] **No bolted-on code, everything from the data:** "and why the fuck are u using a clay fix instead of a full fucking game fix" / "check for other shit bolt ones youve done instead of making the full fucking real thing". Done: mineral lustre and clarity, surface things, every element's colour and sizes (content/minerals.dnat, content/elements.dnat). Crystal structures: done, every mineral's published unit cell and sites in content/minerals.dnat, one engine (`MatterView.Crystal`). Planet kinds' sizes, ground, water, air, clouds, rock and temperatures: done (content/terrain.dnat). Then: world kinds' behaviours: done (content/terrain.dnat); star kinds' faces, spots and remnants: done (content/stars.dnat); galaxy shapes by kind: done (content/galaxies.dnat layout:); the Dyson builds: done (content/space-tech.dnat @build); the element looks and family colours: done (content/elements.dnat).
- [x] **A new universe every launch:** "and is the univers properly procedurally generated every time?". `Program.Seed` from the clock and a GUID, shown in the title; `--seed N` plays one again.
- [x] **The land's zoom into atoms fills the view with the real lattice:** "the nebula to atom view was best ive seen the land needs work going through more zooms to atoms properly as it needs to fill the view at proeprly matrixing and latucces and shit" / "NOT just 3 orbs lists as chuncks of atoms" / "and make sure the grain shit isd proper scale into on zooming it currently just pops into view with no like element chunking or bonds or material spacing thats proper". `MatterView.Lattice`, `Crystal`; DESIGN 255.
- [x] **Drag turns further, with a setting:** "and make the drag pan and look do more i find my self having to move the mouse a foot just to change the angle 90 degrees" / "with setting adjuestments". View > Drag speed, 1x to 10x (setting `drag-speed`, 4x usual); `UniverseView` drag rule.
- [x] **Small-scale faults on screen:** "whoah grains are black and white checker board shit like hypotize material" / "and ther is nothing to click on at 20 micorns" / "like i said all zoome levels need to be figured out for the small shit" / "bonds are just atoms". Smooth shading unpacked per corner (`MeshPipeline`); plates and skins with thickness clear of the face (`MatterView.Plate`); silt specks (`@surface silt`); bonds as sticks (`MatterView.Stick`). DESIGN 259.
- [x] **The face between 50 um and 1 nm, its make-up and its clicks:** "50 micro meters to 1 nano meter is just endless hypnotize black and white shader clipping and tearing" / "and nothing at all to click on for the crystla matirxes and shit for that whole randge and cpomplexity". DESIGN 262.
- [x] **Electron focus, nucleus, bonds as shared electrons, picking in the lattice:** "zoom on electron scall isnt focusing correctl on whats selected and the nuclesu of attoms isnt properly a nutron and protons count based on element and the girid matrix of the bonds and shit doesnt really look like that that just how human make it observable with stick bonds and shit they are acually electons being shared at that scale which form bonds not stick connectors to robs" / "and i was having trouble selecting an atom in the bond matrix it would click through the ones i was trying to select". DESIGN 266.
- [x] **Real universal laws, all four forces:** "this is real fucking univaresal laws we shall be using electomagnatism strong and weak nuclear forces all of it". Nucleus packed by the strong force against the protons' Coulomb push, binding energy from the nuclear mass formula, weak-force decay; electrons by electromagnetism.
- [x] **The grains are the land going on down:** "and are grains a bullshit 2d layer bolt on oor the real zoom into the land?". Done: the grain bed on the land's real slope (`UniverseView.MatterFrame`). Then: one grain field for the whole planet (`GrainField`), the ground's finest detail is the top of its grains, the matter view reads the same grains; DESIGN 269. "yup its jsut a bolt on, u need to fix grain level" / "not true infinate zoom" / "yet so get to work".
- [x] **No tearing or two-tone flicker as you zoom:** "do you not see this terring and hypnotizing multi two toned geometry tearing and shit as u zoom its annoying and hard on the eyes". DESIGN 270.
- [x] **The grain face has detail at every size:** "from 50 mico meters to one nano meter there is nothing but the color of the grain". DESIGN 271.
- [x] **Every zoom continuous, application wide:** "come on now dont short change me only a bolt on peice of shit you dont fix full application wide". Done: loose rock at every size on every world (DESIGN 272). Done: the sea's water as a real liquid (DESIGN 273). Done: molten lava (DESIGN 274). Done: the air of giants (DESIGN 275). Done since: snow and ice are water-ice grains with ice Ih's real crystal; the giants' air (DESIGN 275); stars' plasma and the air over every ground (DESIGN 291). And microbes on a living world's grains (DESIGN 292). The zoom carries on the same way everywhere it reaches.
- [x] **The grain carries on round you at every zoom; fast:** "yup still turns into a cloth a 2nano meters and ther is nothing but white and black texture between those ranges i gave you". DESIGN 277.
- [x] **FPS on screen, with a setting:** "and put a fps count on the display with option to turn off" / "in settings". DESIGN 278.
- [x] **A scenic fly-by, replayable:** (built: DESIGN 282-289; waits on the test) "okay do a full panoramic scenic view fly by and down and into attoms like fly bys and follow an electron and then zoom in more to a proton and then into its qwerks nice and panoramic like for human viewability speeding through only at large zoom points and paning on cool scenes like at ground level 6' tlooking into space then looking down into atoms" / "make it a script like thing we can do agaion later" / "okay but you went to fast u didnt even fly around the solor sytem of the random star and shit and fly around the planet looking around and shit them like zooming in" / "and it should ene on the electrical quark like waves and shit from after electorns and u never panned and zoomed around the valence rings of atoms and zooming around them in like slolor system esk electoron view" / "and you going way to fast at keey point when u need to be doing sceneic views" / "and you zoom into the first galaxy without ever flying around the galxaies first" / "and u go from galaxy to planet ground in like 2 seconds>>> WTF you retard". "okay u did a sprial thing and u are using differnt zoom speeds incorrectly it need  s to be continuesous to give the scale" / "spiral thing made me dissy and u keep moveing alt all differnt speeds way to fast". The zoom at one steady rate in scale (each tenfold the same time), no spirals, gentle pans. A script of stops (fly round the galaxies, into one, round its star system, round the planet, down to 6 ft looking up into space, then down into the grains, the atoms, round their valence rings, following an electron, across the gap to the nucleus, a proton, its quarks), slow pans at each, fast only through the big zoom gaps.
- [x] **Below the electrons and the nucleus, built for real:** (orbitals as probability clouds by their real shapes; gluon flux tubes and virtual quark pairs; DESIGN 290) "you went zommie way past electorm n level and the levels past it arent correct you never focused on atoms and flew around u never focused on electrons you never zoomed in the massive distance between the electrons and protons and nutroons in the center of atoms" / "and past nutrons i dont think anything is built" / "should be all waves and electrical and shit maybe do some research". The real emptiness between the electron cloud and the nucleus (about 100,000 times smaller), electrons as waves (orbitals), and inside nucleons the quark and gluon fields: research and build.
- [x] **The GPU reset on entering a star system:** "okay fix that gpu issue first". Cards shared one GPU buffer (ScaleHud slots now); DESIGN 281.
- [x] **100 billion galaxies in a visible universe with an edge:** (built: DESIGN 282-289; waits on the test) "okay lets properly add 100 billion galaxies as the start looks empty with only 32, and lets do a properl 13.4 biillion light year across vissible universe asnd handle properly not being able to move beyond it (for now)". Galaxies made by regions of space at the real density (about 100 billion in all), the far ones a glow along the cosmic web, the size in content (13.4 billion light-years as asked; the real visible universe is about 93 billion across), a soft edge you cannot pass.
- [x] **The fly-by, eye-friendly, the whole way:** (built: DESIGN 282-289; waits on the test) "and when everything is build build the fly by the vastness of the univers, a few galxy fly bys and fly throughs planets around and then around a planet, and you are doooing a spinning thing in error with like gyro lock or something" / "and fly by of the elxtrons and atom latics at thier respective levels and electron fly bys and quark shit" / "and u cant make wild turns and pansd and shit and u zoom in way to fast it has to be huuman readable and viewable friendly to the eyes with peresective relations". No gimbal spinning (never circling while looking straight up or down), slow gentle turns, a slower steady zoom, stops that show scale against something near.
- [x] **Intergalactic scale, researched:** (built: DESIGN 282-289; waits on the test) "and zoom out at inter galactic scall shuould propely show glaaxies at scale and pin points in the vast matrix of the universies glacactic systems of web like structures of galaxies do research". Galaxies at their true sizes, pinpoints from afar, along the cosmic web (filaments, walls, clusters, voids) as measured.
- [~] **Everything on the GPU:** "why is it so hard for my GPU to simulate such a simple area zoomed in" / "nothing should run on cpu unless absolutely necessary". Move to shaders: the grain faces' shape detail, steps and stains (the CPU sends a coarse grid and the grain's numbers), the waveform (its stored CDF 9/7 octaves uploaded once), mode-E, the stones, the atoms' orbitals and electrons, the lattice; the CPU keeps only picking, the camera and the coarse shape it needs to agree with the GPU.
- [x] **Every kind of body, everywhere, clickable:** (content/bodies.dnat; DESIGN 307) "okay why cant i sleect obejct in ort clouds and astorid belsts and rouge planets and stars inbetween glaxies and where is the super massive black holdes and all the minor ones that are still massive pepered throught galaxies and the universe and all the crazy astrinomical bodies". Supermassive black holes at every galaxy's heart; stellar and intermediate black holes, neutron stars and the other strange bodies through galaxies; stars and rogue planets between galaxies; single asteroids and Oort-cloud comets; all from the content, all clickable with cards.
- [x] **The tour stops itself and sits as if frozen:** (built: DESIGN 423; waits on the test) "it pased too long at 106 light years and as u can see its focused on a glaxy and the web is still all viewable interseting with stars of the galaxy clasdhing too much" / "the tour crashed" / "like i said it froze". Found: any key reaching the window (Shift, Alt, the Windows key: Alt+Tab, Win+Shift+S) ended the tour; the web's points were drawn from 3.2 million light-years while whole galaxies are drawn to 12 million, both at once.
- [x] **A paused tour says so, with restart:** (built: DESIGN 424; waits on the test) "like it should say mouse movement tour paused if done and shit" / "with restart".
- [x] **WorldView is the universe view:** (built: DESIGN 425; waits on the test) "make sure world view isnt labeld wrong and should actually be universeview".
- [x] **The in-game docs formatted:** (built: DESIGN 426; waits on the test) "and the readsmes and shit need proper formating and shit it just looks like a shit text files with no actual style of formating or layout and shit" / "the in game all doc s".
- [x] **Every public doc fun to read:** (built: DESIGN 427; waits on the test) "and the project readme has the same fucking problem not beiung fun to read like all docs public versions for the public".
- [x] **Fresh clips:** "and go ahead and delete all my clips so its fresh and the broken aqttempts are gone". The ten tour clips in the installed copy's captures folder deleted; screenshots kept.
- [x] **How-to docs for building with the universe:** (built: DESIGN 428; waits on the test) "and we need how to docs on how to build in the universe from adding more planet types to features and all kinds of shit so its a tools to build universes of simulations and games do you understande the universe project now?"
- [x] **Every generator a template, no names in code:** (built: DESIGN 429, 431; waits on the test) "thats a problem they were all suppose to be dynamic and procedurally genreated templet like things not hard code" / "yeah day length is the spin of the planet". Done for worlds (terrain.dnat: where each kind forms, its size, moons, spin, colours and make-up, every value a range each world rolls from its seed). Still to audit: galaxy kinds are told apart in code by words in their ids (`GalaxyGen`: "spiral", "dwarf", "elliptical"), and the rest of the generators the same way.
- [ ] (Later, DNA-AND only) **Habitable means carbon-based:** "and remember habital is only for carbon based life but thats nanother thing for later and for dna and only". The habitable zone is only where carbon-based life with liquid water can live; other chemistries of life have their own zones.
- [x] **Smooth, continuous tour camera:** (built: DESIGN 430; waits on the test) "and make sure ther are no abrupt turns or wild pans as it cant look shooppy and needs to be smooth and continueous"
- [x] **Rings on any world:** (built: DESIGN 431; waits on the test) "planets can have rings too"
- [x] **Physics docs, all four forces:** (built: DESIGN 432; waits on the test) "what about physics and the natural laws like strong nuclear and weak forces electromagnaism and gravity where all all those docs"
- [x] **Planet fall frames:** (built: DESIGN 433; waits on the test) "its hitting like 9fps zooming into a planet on planet fall liek its still trying to simulate the sky when its not even in the view port"
- [x] **No pause, no static, no handoff, no pop from 500 nm to the atoms:** (built: DESIGN 434; waits on the test) "and it pauses at 500 nanometer get rid of pauses like that it makes it look prozen" / "and from 500 -1 nano meters its just static appearance" / "then atoms pop in all at once in eroor" / "and looks like handoff in the 500-1 nano range"
- [x] **Inside a giant:** (built: DESIGN 435; waits on the test) "look what happens on zoom in to a ice giant im sduure this is nt the only problem one"
- [x] **The web the same from the start:** (built: DESIGN 436; waits on the test) "it does a weird thing were glaxies webbing isnt viewable until zoom out then it persists on zoom in which is fine i suppsoe i just needs to be uniform"
- [x] **A loading screen at the start:** (built: DESIGN 437; waits on the test) "lets put a loading bar or somehting on start till everything is properly ready or what? its a free wheel loading problem"
- [x] **Calmer time at the start:** (built: DESIGN 438; waits on the test) "and planets spped is way to fast or time scale is way to fast the star is rising like every second"
- [x] **A calm look at the sky:** (built: DESIGN 439; waits on the test) "and when it looks up at the sky it pans too much and with the fasst time speen it makes me dizzy"
- [x] **No driver time-out in the matter:** (built: DESIGN 440; waits on the test) "it froze an unknow exemtioon error at matter delpht where teh static was before"
- [x] **Logs named for their program:** (DESIGN 441) "asnd wehy does it say dna log when we are working on the universe" / "oh we are doing both"
- [x] **Nothing labelled wrong between the projects:** (built: DESIGN 442) "make sure nothing else is labels wrong between projects"
- [x] **Universe content its own file type:** (DESIGN 443) "Universe gets its own (Recommended)"
- [x] **All docs good to go, in the game and out:** (built: DESIGN 444) "make sure all docs are good to go" / "in game and out" / "the readme last i saw was pretty shit"
- [x] **Docs finished to the creator's words, no emoji:** (built: DESIGN 445) "fix the files of the docs fully and completely to how ive already descirbbeed many many times then do full cascade for both projects all four remotes" / "and not a emohji desacster like dna and for the docs"
- [x] **How a universe fits in 1.7 MB, in the readme:** (built: DESIGN 446) "and make sure the readme properly describnbes how we make such a massive universe with only 1.7 Mb or what ever the count is now"
- [x] **Comet seen, smooth turn to it:** (built: DESIGN 447; waits on the test) "the comet was a ghost and the pan from astroid to commet was jerk as fuck"
- [x] **Tidal locking from the orbit:** (built: DESIGN 448; waits on the test) "and tiadally loceked jsut means a moon or something is always near one side of the planet or body due to orboitoitalk distance of like moons and stuff"
- [x] **No straight lines in nature:** (built: DESIGN 449; waits on the test) "and some planets ave straihgt line surface coloring and pattrering< nature sdoesnt build in straight lines"
- [x] **No driver time-out over the ground's sand:** (built: DESIGN 450; waits on the test) "200 million for a micron sounds high"
- [x] **At 20 micrometres, space and black-and-white:** "at 20 microns im seeing space and only white and black texture" (to see on screen)
- [x] **Moons are worlds, orbits are ellipses:** (built: DESIGN 451; waits on the test) "and remmeber planets can be moons and moons planets and all these bodies orbits are too perfect reall orbits are eliptical and wild in cvariation"
- [x] **No still frame while the tour looks for what is next:** (built: DESIGN 452; waits on the test) "it pauses at 10570 light years way to long when it needs to look around a little instead of still frame pause for 10 seconds"
- [x] **Comets at their real size, seen across the system:** (built: DESIGN 453; waits on the test) "zopomed to a comet and nothign was there and wtf u should be able to see comets all the way across a solar system"
- [x] **Always day on to start with:** (built: DESIGN 454; waits on the test) "and we should default day only cycles on"
- [x] **No driver time-out on the first frame of a grain's face:** (built: DESIGN 455; waits on the test) "i tried to f12 at 12 microns but it crashed"
- [x] **Old clips cleared; the installer offers to clear them:** (DESIGN 456) "go ahead and clear all the old clips" / "and the installer should have option to delete all the old clips on new install/update" / "no it need delet options for all of them" / "not just clips"
- [x] **The readme's recording a proper tour, once it all works:** "okay the clip u have in the read me needs to be a proper tour once we figure it all out". When a full tour runs clean, film it (Film the tour), then cut its highlights (web, galaxy, black hole, star system, comet, world, ground, grain, atoms, quarks) from the MP4 into a short GIF for the readme (the clip itself is hundreds of MB, too big for the repo), replacing docs/img/journey.gif in both projects.
- [x] **Nothing shows through an asteroid or a nucleus:** (built: DESIGN 457; waits on the test) "the comet is transdpartent i can see stars through it" / "i mean asstroid but comet prolly same problem"
- [x] **A pick always shows its card:** (built: DESIGN 458; waits on the test) "also im clicking around at 1km scale and i cant select anything"
- [x] **Towers, spikes and lines from the ground:** "i took scren shot im getting weild toweres and spikes and lines coming out of the ground" (the F12 picture did not save: to see on screen, and why F12 saved nothing)
- [x] **Processor and card at once:** (built: DESIGN 459; waits on the test) "and im just sitting at 1km and its shittly doing 14fps im not even zooming or nothing"
- [x] **A comet with its coma and tails, seen across its system:** (built: DESIGN 460; waits on the test) "comet didnt have a auros or gas or a tail facing away from star and i could nt see it at solar systems scale like comets should be seen" / "it looks just like an astroid"
- [x] **The driver still times out at the ground's hand-over to the matter:** "at 200 mm ant micor level it crashes still" (00:55:06 and 00:55:08, about 185 micrometres over an ice world). Every shader loop is bounded, the octave cap and the palette are in range: next, the device's own record of what it was doing (DRED breadcrumbs and page-fault address) written to the crash log with each draw named.
- [x] **Blocky hand-overs at 200 mm:** (built: DESIGN 462; waits on the test) "200m ther are lock hand offs" / "mm"
- [x] **The crash log names the draw the card died on:** (built: DESIGN 461; waits on the next crash) "at 200 mm ant micor level it crashes still"
- [x] **Ids for asteroids, comets and every body:** (built: DESIGN 464; waits on the test) "astroids and comets and shit dont have stellar ids"
- [x] **The sky caption says what is really up:** (built: DESIGN 465; waits on the test) "the look up to thew sky at human ground level inproprly says its star and other worlds when worlds are viewable and  no star is in the sky at the moment"
- [x] **No circling at the gravel; frames at 5 mm:** (built: DESIGN 466; waits on the test) "at its hitting 5fps at 5mm pan and it should not pan at those zoom levels only when at atom level and there is shit to see besides gravel"
- [x] **A click brings the window to the front:** (built: DESIGN 467; waits on the test) "when i click on the app it doesnt propelry bring it to top focus it starys behind other windows till i close them"
- [x] **Stirred systems, worlds out of the plane:** (built: DESIGN 468; waits on the test) "are obits suppsoe to be in a flat plane thats not normal is it" / "no fix it dont leav e it"
- [x] **Orbits from speeds; relativity in the orbits and on the cards:** (built: DESIGN 469; waits on the test) "all type of compbinations even ultrrqa fast bodies willth stained long elipticals theh phsysics should determine it" / "relativity is propelty in , or no?"
- [ ] (Later) **The rest of relativity:** length contraction, redshift, frame dragging.
- [x] **No fading in of the atoms:** (built: DESIGN 470; waits on the test) "at 500 pico the atmoms jsut pop into view incorrectly" / "and i dont want a fad in"
- [x] **A proton's card names its element:** (built: DESIGN 471; waits on the test) "and it generic says a proton of a nuclues incorrectly saying which element it is"
- [x] **Electron-microscope greys below light's wavelength:** (built: DESIGN 472; waits on the test) "and there is a hand off on the 2mm and smaller to wher it dgoes nano" / "ii see grains then it instandt swticheds to balck and white elctron view of material which is fine(might ass note color doesnt exist at this scalle and later when ther is color properly do it" / "not black and white elctron view but like elecrton microscopre vioew where everything turns black and white before the color of electorns and stuff appear"
- [ ] (Later) **Light rendered truly below its wavelength:** "later when ther is color properly do it"
- [x] **GPU and CPU stats with the FPS, toggleable in settings:** (built: DESIGN 473; waits on the test) "and with fps i want gpu and cpu stats also togfgle able in settings"
- [x] **A directory of every document in the viewer:** (built: DESIGN 474; waits on the test) "we need a full directory listing of the docs and their make uup and use descriptions in the doc virewer that well formated easy to read and use and beautiful"
- [x] **Docs split plain and technical, and a tool meant to be taken:** (built: DESIGN 475; waits on the read) "the docs are all properly formated and created and designed around explaining the systems and shit technical and non techinical dos where basic readmes and how tos are laymens and technical docs are more involved" / "this is a tool menat to be stolen and used for any number of projects of any types wehere a universe is needed"
- [x] **Scratch-made, from the compiler to the engine, in every readme:** (built: DESIGN 476; waits on the read) "and readmes need to propelryy talk of how the app is totaly scatch made from the compiler to the engine explaine it all in laymens terms and tefchnical respective to the doc"
- [x] **The 2 MB universe against the field, checked and sourced:** (built: DESIGN 477; waits on the read) "the full universe in 2mb and less is a major achievement.. is it not? no thing else does this does it dont be pushy beut be real and do checks on reall shit that does similar with comparison tables of like top universe sims of stats and size and comput issues or out preformance and such"
- [x] **Real infinite zoom in, no floor:** (built: DESIGN 478; waits on the test) "and it better be rela infinate zoom in the code so if its not fix it" / "so docs are accurate"
- [x] **The tour's feeding black hole: disc, lensing, shadow:** (built: DESIGN 479; waits on the test) "and add a fly by a balck hole thats feeding and zoom into it on flyby to see its acression disk of a eating balckhole and zoome to the gravity distortions and points of interest" / "before planet level zoom in"
- [x] **Repo pages: social images and write-ups on GitHub and Forgejo:** (built: DESIGN 480; GitHub's social preview waits on the one upload in Settings) "and the repo urls need properl social images and write ups and" / "can that be done on github and git?"
- [x] **Snaps from the game for the social images:** "we cxan take some snaps to use for the social images later". When a tour runs clean, pictures from the game itself (the web, a feeding black hole's disc, a world from orbit, the ground, the atoms) go under the name and tagline in docs/img/social.png for both repos, and GitHub's social preview is uploaded again.
- [x] **The viewer's lines wrap at every text size; the list wide enough:** (built: DESIGN 481; waits on the test) "look at the open text viewer there is text under the highleighted text that i can scroll to and is a dispaly issue"
- [x] **Real stars made all round you, not round your cube:** (built: DESIGN 482; waits on the test) "took a shot image stars ar only populating in half the screen or its a shading thing not allowing them to materialize"
- [x] **The tour's crossing of billions of light-years to a far galaxy:** (built: DESIGN 483; waits on the test) "and it like zooms in and out of the same galazxy when it should fly like 1-5 billion light years to a whole nother galaxy on web galaxy view zoom out" / "its like it keeps selecting the closest"
- [x] **The dive under the sea to the bed; the tour comes down on land:** (built: DESIGN 484; waits on the test) "and dont zoom in to water worlds as it not propelry done yet as the fall to planet is under water or at least it looks like it is a 500mm level" / "need to fix water worlds and water zommming in to go to bed rock" / "i took a scrren shot of that issue" / "if we can get all the worlds working correctly i dont care what it zooms into" / "and qwerks never loaded and it says a spot in the seas on 200 attometers" / "and just beforee atom view its a weird grid like checkbaord of differnt colors so the elctron microscope vioew neever happend on the water world"
- [x] **No atom wobble:** (built: DESIGN 485; waits on the test) "and eatoms are all wobbel;y looks nosiating the atom is wobbleing even when focused on"
- [ ] **Every body landed on and zoomed into, with its real make-up:** (roadmap: DESIGN 486) "ie i should be able to find a gold astroid or nickle being most common ect ect and zoom infinated on any body or abject" / "stars shoiuld be mostly hydrogen till getting to it heavey m,etal cores ect ect"
- [x] **The code's density, said plainly in the docs:** (built: DESIGN 487; waits on the read) "make notes in document this prokject code is mostly not human readible, right? thats correct right?"
- [x] **Who can change what, said in the docs:** (built: DESIGN 488; waits on the read) "what i mean is without AI only advance coders can manually edit it"
- [x] **The code note is a suggestion, not a rule:** (built: DESIGN 489; waits on the read) "its not who can do what its a suggestion"
- [x] **Stars exact on the card, no lattice, no empty squares:** (built: DESIGN 490; waits on the test) "at 1 aU scale the stars are appearing in grids and somene ggrids no starts like squares of starless areas i took a screenshots"
- [x] **Focus on the star flown to:** (built: DESIGN 491; waits on the test) "and its like zooming around with no focus"
- [x] **No white fuzz of near stars on a fast zoom out:** (built: DESIGN 492; waits on the test) "and another thing on zoom out u see the stars all like converge in a white fuzz pattern as zooming out fast that looks scetch"
- [x] **The loading bar seen:** (built: DESIGN 493; waits on the test) "A bar shows the universe being made, then it opens, whole. there is no loading bar"
- [x] **The 50-micrometre hand-over and the six shots' faults (pillars, hatch, skirts, dotted rings, the ribbon in black):** (built: DESIGN 494 to 498; waits on the test) "right at 50 mico meters there is an instant hand off change over and i took a bunch of shots each with its own obvious viewable problems" / "keep working till its all fixed and ready for install so all the probelms at scale of the shots are fixed"
- [x] **A shot leaves what you were at smoothly:** (built: DESIGN 499; waits on the test) "and when it pans from astroid to comet the astroid just instanat disappears intstead of smoth pan and zoom away"
- [x] **Electrons slower, their orbital paths seen and always changing:** (built: DESIGN 500; waits on the test) "at atom scale the elctrons astart moving way to fast and i dont see their orbital paths that are constantly changing like electrons do"
- [x] **Stars and the web's galaxies told apart from a galaxy's edge:** (built: DESIGN 501; waits on the test) "and when looking at a galxy form distance but not too far i cant tell the differnce betwen startts and glaxies in the background webbing idk how we would asddress that maybe tone down one and brighten the other correctly at appropriate distances and scales"
- [x] **Against the field as its own document, with performance:** (built: DESIGN 503; waits on the read) "and where is the performance and comparison table of other sims and space games and space simulations" / "it need proper naming and listing in the doc lists not burried oin other docs"
- [x] **The atoms not drawn in the latest shots (11 nm to 614 pm, black):** (built: DESIGN 507, the glide outrun by the zoom; waits on the test) "i took snaps again of all the sclaed issues of the differnt zooms and the shit thats happening". Every link of the chain checked in the code (the lattice made, the dots added, the buffer written, the card's constants right) and the cause not found from the shots; needs one check by the user: the wheel by hand (not the tour) down to an atom, and a screenshot there.
- [x] **The wheel comes down onto the landmark at each size:** (built: DESIGN 504; waits on the test) "i think ur problem is u arnt focusing on shit like landmarks on zoom in and are just indefinitly zooming in on nothing"
- [x] **Test shots quick (BMP, below a size) and the solid-mass cluster softened:** (built: DESIGN 505; waits on the test) "not crawl, shit will take eons at a crawl but need to be human viewable this is taking forever u fucked up the speed, and also there is a galaxy type that looks like a solid mass thats too bright and to icony and need fioner edge"
- [x] **The test's wheel ends on the quark:** (built: DESIGN 506) "u are zooming toi infinity again"
- [x] **What is picked stays in the picture; no strobe at the quark:** (built: DESIGN 507; waits on the test) "wuts happening im just seeing blue flashes at 10 zepto meters and nothing is in focus" / "and wtf its not suppose to be running so fast that people have photo seizures"
- [x] **Electrons and quarks slower:** (built: DESIGN 508; waits on the test) "need to slow down electrons and querks"
- [x] **The face's bands, tiles and saw gone (the three sheets tilted):** (built: DESIGN 509; waits on the test) "and just beforee atom view its a weird grid like checkbaord of differnt colors"
- [x] **No runs by me, ever again; the user installs and tests:** (DESIGN 510) "i SAID: next time ill install and test!"
- [x] **The tour's picks: skipped when nothing is found, and logged:** (built: DESIGN 511; waits on the next tour's log) "something is up ist just zooms in and out of the same galzxy a bunch and never does the astropid and comet zoomin and neverr sayw the black hole aven thos it said it was looking at them" / "and its narrating shit thats not on screen"
- [x] **The tour never waits on the ground below a centimetre:** (built: DESIGN 512; waits on the test) "at 10 nano meter the planet went invisible and all i see is stars" / "or blackness i dont know"
- [x] **Every electron seen moving:** (built: DESIGN 513; waits on the test) "and on atom scale some electrons are frozen while others are moving"
- [x] **The quark seen at the end of the tour:** (logged: DESIGN 514; waits on the next tour's log) "i never saw qwerks it mazxed out and said smallest thing we know and i never saw qwerks"
- [x] **Why the card's memory stays small:** (DESIGN 515) "and why is GPU using 40% at times but only using 124MD of the 16G of vram"
- [x] **A real accretion disc, stars eaten, the skim:** (built: DESIGN 516; waits on the test) "aand on the black hole dive fly by do a skim of the scretions disks of gasses and starrs being eaten properly made as i think u have it just as an effect not a real accretion disk of stars and gasses and gravity destoying of stars"
- [x] **No edge, only a horizon that moves with you; the far light reddened:** (built: DESIGN 517; waits on the test) "and u have the expansion of space properly, yeah?? space is always expanding and at 13 buillion light years it expands faster then light can reach the opposite side of the galaxy thats wher the visible universe barrior comes from, so its not really a end all of the universe its the distance light can no longer reach before the vastness of expanding space out paces light speed at full universe scale.. do you need to research this./.. becasue theraretically if u move a light year in any direction u can see 1 light year further of the 15 buillion into the "Edge": of the universe" so it my belirf the univers is infiniate but for the fack that space expands faster than light can move through it" / "but we ater talking billions of years and billions of time from the big bang to wher not a start is visible from any other star,, this is just tesctnical facts and im not sure how to build it in"
- [x] **Dark matter and dark energy as content, on the cards and in the captions:** (built: DESIGN 518; waits on the test) "and what about dark matter and dark energy to make gravity work?" / "based on their angul;ar moment? just things to think about" / "while coding a universe"
- [ ] **A gravity switch: bodies straight on, stars blown apart:** (open: DESIGN 519) "as i dont thinkg galxies are acutally being held togetther buy the laws of nature. like if i has a setting to turn off all gravity.. would stars explode pressures of all bodies zero and every body move to straight line travel... right?"
- [x] **The hole shot stays in its galaxy; small bodies framed from their lit side:** (built: DESIGN 520; waits on the test) "i think it might be a focus problem" / "as last test it zoomed in a bunch never focusing and was saying it was looking at a astraoid and commet when just in black space of a mix of stars"
- [x] **The asteroid held in the middle, the comet from its lit side and bright enough:** (built: DESIGN 521; waits on the test) "the astoid trails off to the sids and focus doent follow it i never saew it upclose" / "aand to comet caought the dark side of it" / "or its just not bright enough"
- [x] **The apostrophe in the cards' font:** (built: DESIGN 522; waits on the test) "and it keeps having (s)'s stand alone when prlulizing shit like ur add errnonious spaces"
- [x] **No Sun in this universe: solar units everywhere:** (built: DESIGN 523; waits on the test) "and u still using Sun everyehwer in the write ups and texts is solar mass and shiot not Sun there is no Sun in this Unioverse"
- [x] **The atom pick takes the mineral's elements in turn:** (built: DESIGN 524; waits on the test) "and why are ther ever only oxygen atoms on atom view" / "maybe its the luck of the draw but seems excessive to alway land on owygen"
- [x] **The atom flown round, through its electrons and inner shells, before the nucleus:** (built: DESIGN 525; waits on the test) "and u need to acutally do the fly around and have the ability to fly through the atoms and elctrons and around the scale a little bit more as u kinda just zoom in only when these scales have reall shit to fly around and u may need to upscale the atoms a little bit so when u coom into them they dont instany got to proton nutron till a orbit and rotate of the atom vies like galaxctic and solar system views fly arounds should be" / "maybe do some rotating too so dep depths of zoom show everything is similar even behind your view"
- [x] **Every mineral's trace elements on their real share of sites; the sea's ions:** (built: DESIGN 526; waits on the test) "okay but do we really have the full table of elements mix on all bodies to what they should have mixes of,, its not just silica a planet is made out of earth has like 118 elements weve found on it"
- [x] **Every natural element placed, the noble gases in the air, the periodic table opened from the universe:** (built: DESIGN 527; waits on the test) "fiftene elements? wtf did we build the periodic table of elements for u retard" / "and it needs to be proper and have a way to open it"
- [x] **The readmes open with the release, its links and its write-up:** (DESIGN 528) "asnd the readme for the repos need a proper first thing releases link and wording and write ups"
- [x] **The black hole pick goes to the galaxy's own heart; a clip finished on a safety exit; nine screens in the readmes:** (built: DESIGN 529; waits on the test) "okay i hope on next install we can get a finished clip so finger s crosesed" / "and we will need screen shots of like the docs viewer and install requirments and installer posints but not alot of screen shots just the main things" / "and landing page" / "to put in the readmes propely"
- [ ] **Screens of the landing page, the installer and the document viewer in the readmes:** (waits on the user's windows: open each and say so, and it is grabbed from the screen) "and we will need screen shots of like the docs viewer and install requirments and installer posints but not alot of screen shots just the main things" / "and landing page"
- [x] **The loading screen from the first frame, and unmistakable:** (built: DESIGN 530; waits on the test) "and im still not seeing a loading bar just black scrren on load in after seed input and tour start"
- [x] **A shot leaves the asteroid and the comet smoothly:** (built: DESIGN 531; waits on the test) "when it moves away from the comet and astroid it doesnt slowly pan towrds the next target jit just instally zooms making them disappear instanlty instead of a smothe pan away before zooming"
- [x] **The sky caption tells of the air, what it does to the light, and the star's colour:** (built: DESIGN 532; waits on the test) "and whit it looks up to the sky from ground level it should comit on atmospher if one and how it changes athe color of light and its composition which it already has" / "and the star color"
- [x] **A spot or grain pick never fails on an ice sheet or the sea:** (built: DESIGN 533; waits on the test) "is it normal the gpu only ever uses 124mb of 16 gb?vram? cant we make it better if we use more of the vram? and at 500 nanmoeter is pasues in eroor or crashed" / "it never went into atoms and qwerks"
- [x] **On Linux, through Wine, with one script, and documented everywhere:** (DESIGN 534; the first Linux report decides it) "do .exe run on linix.. how does linix people install?" / "okay well make sure that path is real and its documented propley for how linix people can use the app with out crazy workarounds" / "make sure all the docs are proper with this requuest and fixes"
- [ ] **The Linux path run on a Linux desktop:** (waits on a Linux machine or a Linux user's report) "okay well make sure that path is real"
- [x] **Clips at thirty frames a second and a clip's bit rate, half the size:** (DESIGN 536) "and wtf fuck are u using to make these clips 500 mb for minutes,, wtf are u using f2000 instead of f16 or something"
- [x] **One clip per recording, packed by quality, never massive:** (DESIGN 537) "it should only make one clip as long as its recording and not a a gig file so use something appropriate that doesnt fuck our c/9 waveform into hevey picxel style files that are fucking massive"
- [x] **A REC notice while recording, off the tape:** (DESIGN 538) "and ther should be a REC notice if recording liek carasvideos tapes have"
- [~] **The face's rain of lines and its floating slabs, from the user's shots:** (built: DESIGN 539; waits on the test) "okay i took alot of image shghotts of all the fproblems" / "check images for scalle and visual issues" / "to fix issues at those scales"
- [~] **A new path every time round; the electron ridden beside like a comet, with the view circling:** (built: DESIGN 540; waits on the test) "another thing on atom scall the orbits of attoms ar never constant they make one orbit then the orbitapaths changes.. do you not know this did you do your electro research" / "and on atom view i want you to dive into a electrons travels folling one on its orbiit at close randge, might need to effect scale so getting to thhat zoom actual makes electrons bigger like planets on view up close befoe wtitching to the nucleus" / "with pan and rotate properly" / "liek its a solar system of its own and like u are following a comet but an electron"
- [x] **The quark view shows one trio:** (answered: at 200 attometres a nucleon is four screens wide and the next nucleon eight screens off; at the proton stop before it every trio of the nucleus is in view) "and the qwerk view only shows one grouping of qwerks where are all the others? too far wayaway at that scale?"
- [~] **From the clip: the ring of dashes, the warp of stars, the black between the face and the atoms, the invisible electron ride:** (built: DESIGN 541; waits on the test) "look at all the scrreen shots all the shit above 2 nano meter dirrectly is all fucked and i took a bunch of pictures, and check the clip i made that the issue i saw and if its better u can use both clip and shots, and the zoom to an electro shows nothingtill the nucleas in erorr. the clip is done and the shots are waiting they need massive work to rectify the shot issues at their scale they show and the clip of the patterneing in the first solar system fly by and do you not see the warp like effect of stars at a focal point as it zooms out.. it looks trashy but i know its accurate just looks weird maybe do something smart so that warping focal effect isnt so heavy" / "they almost look like a grid not natural solar system"
- [~] **The start page in the same window:** (built: DESIGN 542; waits on the test) "and pressing the start page button in the menu in correctly closes and restart s the whole application"
- [~] **The belt a haze from afar, never a white annulus:** (built: DESIGN 543; waits on the test) "fix it all and did you see the solar system patterning" / "something is up im seeing solar system like patterned layout of items bodies or something idk whats up but solar systems have a weird patterning now that i wasnt quick enough to take a picture of" / "they almost look like a grid not natural solar system"
- [~] **The tour stood four minutes at a centimetre over the ground (the activating click's pick, the spot off its line of sight); the ground no longer split under the grain overlay:** (built: DESIGN 544; waits on the test) "somethnig is up it paused at 2mm" / "i might of clicked but it didnt say tour interupted" / "grain overlay "turning on"" / "sounds like a handoff" / "and at 2mm scale not even moveing it at 12fps thats not normal for such a eazy 2mm view"
- [~] **A click in the matter always picks something:** (built: DESIGN 545; waits on the test) "at at 200 micro meters i cant click anyhting, i should always be able to click on new landmarks and bodies at all zoom levels"
- [~] **Every body its own make-up (the small bodies' kinds, minerals and elements; the stars' plasma; the neutron star's crust), dived into at its surface:** (built: DESIGN 546; waits on the test) "astroid ar just generic and not showing the complete varition of all elements bodies need real compitions planets show alot not all bodies do the sdsame" / "not just astreriods stars comets black holes all of it" / "and get the element stuff for all bodies finally done and the black hole stuff ive said in totality all the univer project still has open need to be compelted before next run"
- [~] **Black holes exist (the content reader's comment bite); the disc flyby runs; several stars eaten at once:** (built: DESIGN 547; waits on the test) "and im still never seeing the black hole acretion disk fly by" / "so we need the fly by of a real acrestion disk of a black howle to where u get close enough to see lensing as you robit and see the starts being consumed in the disk" / "that can have many stars on super massive feeding black holes in the acttrtion disk feeding it"
- [~] **The fall through the horizon to the singularity, the gravity well drawn and graphed:** (built: DESIGN 548; waits on the test) "black holes tho will be a special case to fall into event horzon in pitch black till u get to the singularity of near infinate quarks in one smallest space singularity" / "and zooming through the black hole should show the gavity well grid out line that black holes have to a pin point making a sharp step garavityu well so that we can see the fall grafeded in a event horzon and to the singularity as i have described before the event horizon is just the point of speed of light cant exscape deeper in theratically the speed is faster  to escape and beond the event horsive the speed for escape velocity is less that the speed of light and distances off"
- [x] **A star's colour and heat come from its mass, not its make-up (answered, and on the card):** (DESIGN 549) "are yuou sure every star type has the exasct same make up??" / "doesnt color and temp depend on matter with in it and its pressures based on matter in the star?" / "tpya and amount" / "type*"
- [~] **Supernovae, researched and built: their rates, kinds, stages, brightness by physics, the fireball, the shells, the remnants born, the stars that died and their worlds, the neighbours scorched; the tour's flyby:** (built: DESIGN 550; waits on the test) "and what about super novas and nurton star creation and black hole creations and shit should super novas be very bright seen far away like 100s of time brighter than stars, check the math do the research , make it happen" / "from what i read a super nova in the universe happens every day" / "like i heard somehwere in the universe there is a super nova every day...stellar mass ones any ways" / "do research make it acurate" / "but we cant have supernovas shining through all the stars" / "would look crazy if at distance they need to appear propely and brighter where actually phisics says" / "and add a suprnova like nutron star creation in mid progress as supernoves explode for millions and billions of years as ther satr matter expands out in space ad near speed of light" / "do reasarch for what super novas look like in ther variations and time scales" / "and when u build all ive said add the acretion disk fly by and the super novea of some variation and in some variations of exploding, as we dont want the fly by to be finished until all i have said from the star eating acrretion disks that can have many stars on super massive feeding black holes in the acttrtion disk feeding it. so we need the fly by of a real acrestion disk of a black howle to where u get close enough to see lensing as you robit and see the starts being consumed in the disk and also fly by a super nove in stom stat of exploding as there should be all types of variations of supernova in all different episodes of activitity not generic all the same" / "ther should be a whole process of stars supernova and shock wave ablitering evrything into dust gas and plasma colling off and condensing over time like real body formation in space" / "like solar systems should be wiped out if a super nova"
- [x] **The comparison as check tables, concise, with the others' ticks explained:** (DESIGN 552) "thhis is not a proper compasions if this is suppose to be it" / "like u need check marks where whe have all the checks and other have none but with explinations" / "concise like"
- [~] **The picked body held as the body itself (the fall into the wrong hole, the asteroid's dive that never came); the approach to the horizon with the disc all round; the well drawn only from afar and its graph always; the disc a thin sheet with no arms; the supernova stop's young remnant:** (built: DESIGN 553; waits on the test) "the black hole dive ended the tour prematurely after zooming out and the black hole acrestion disk is not planulare and real looking it has all kinds of string arms and shit effects and shit and doent propely ave a planer disk" / "and the tour fucked up after the blasck hole dive and i never saw athe huds up display of the conular gravity well grid when entering the event horizon and fall into the steep cone down to the singularity, i only see the grid out side of the event horizon in error and it doesnt properly turn rpan and relation like it needs to like it does when falling onto a planet so it can see the horizon" / ".. and space" / "up till event horizon cross" / "and the balck hole accrtion disk is just shit effects pasted in"
- [~] **The shadow by the law, the disc's own bending, the card off the view:** (built: DESIGN 554; waits on the test) "do you see this shit i took screen shots the balck hole and actrtion disk are still just gimic effects with werild while traileas and not all spining the correct dirrection and the disk is like half there and there is no lensing" / "and u didnt propley cactly the curvature of the event horizson on zoom in and out" / "and wtf are u seeing this pop up text wall bullshit this is un forgive able i took screneshot"

