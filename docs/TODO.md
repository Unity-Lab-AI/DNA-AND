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

### R5 — Universe
**Spec:** `wiki/systems/r05-universe.md`, `docs/wiki/space-and-dyson-spheres.md`
- [ ] Build from `content/small-bodies.dnat`: all 39 entries (Asteroids by makeup, Asteroids by orbit, Comets, Dwarf planets and far bodies, Meteors and meteorites) and these rules:
  - [ ] small-seed: every asteroid, comet and dwarf planet is generated live from the world seed; nothing is pre-built
  - [ ] small-makeup-by-distance: stony asteroids are more common close to the star, dark carbon-rich ones farther out, icy bodies beyond the frost line
  - [ ] small-comet-source: short-period comets come from the Kuiper belt and scattered disk; long-period comets fall in from the Oort cloud
  - [ ] small-comet-tails: comets grow a coma and two tails only near their star; the ion tail always points away from the star
  - [ ] small-impacts: near-planet asteroids and falling comets can hit worlds; impact size and effect follow the 10 impact sizes on the space page
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
  - [~] Plates that move over time: continents drift, split, crash together and form supercontinents (built: 8 to 15 plates, each turning about its own axis at 1 to 10 cm a year, `src/sim/PlanetPlates.cs`; the ground itself does not move yet)
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
  - [ ] The water cycle: seas evaporate, clouds form, rain and snow fall, rivers run back to the sea, water soaks into the ground
  - [ ] Real weather: winds from heat and spin, storms, hurricanes, monsoons, drought
  - [ ] Ocean currents carrying heat around the world; upwelling that brings nutrients up
  - [ ] Tides from moons and the star
  - [ ] Ice: glaciers and ice caps that grow and shrink, ice ages, snowball worlds, sea levels rising and falling with them
- **Climate and seasons**
  - [ ] Climate zones from heat, water and spin; they shift as continents move and the air changes
  - [ ] Seasons from the planet's tilt, day and night from its spin, years from its orbit
  - [ ] Long climate swings: orbit wobbles, the star brightening over billions of years
- **The surface changes**
  - [ ] Erosion: rain, rivers, wind and ice wear mountains down and carry the pieces to the sea
  - [ ] The rock cycle: rock melts to magma, cools to new rock, wears to sand and mud, is buried and pressed into stone, and is lifted up again
  - [ ] Soil forms from worn rock (and later from life)
  - [ ] Landslides, floods, sinkholes and coastlines that move
- **From outside**
  - [ ] Impacts: meteorites, asteroids and comets hit, leaving craters and changing the climate (sizes from the space page)
  - [ ] The star's flares and wind; the star ageing
- **Everything feeds everything**
  - [ ] Each process changes the others and the life on the world: plates make mountains, mountains change rain, rain feeds rivers, rivers carry nutrients to the sea; volcanoes and impacts cause die-offs; ice ages move life toward the warm middle
  - [ ] Resource fields form and change with this geology over time (magma makes ores, hot fluids make veins, seas lay down sediments)
- **Content still to write (researched, like `content/physics.dnat`)**
  - [ ] Earthquakes and faults, erosion and the rock cycle, ocean currents and tides, the water cycle, glaciers and ice ages, weather systems, climate zones and cycles

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

- [~] Pause and speed controls (in the deep zoom: Space and 1–5)
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
