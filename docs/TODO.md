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
  - ✅ Written: README, player wiki (`docs/wiki/`, 35 pages), builder wiki (`wiki/`), `docs/SETUP.md`, `docs/HOW-TO-BUILD.md`, `docs/img/README.md`, `docs/WORK-LAWS.md`.
  - ✅ All 47 open questions decided and applied (`docs/DESIGN.md` round 5).
  - ⏳ Left: fill the 55 image slots once R4's 3D generator and capture tool exist.

---

## Pending

### 🚦 Gate: before the top-100 lists and before any code
> "topp -100 lists go only after a /superreview and/unity check that evrything ive said is proeprly done in all todo and docs everywhere as we havent done any code yet and are still do ing docs and making the work laws to use the fucking docs to write code when we do write code so todo needs work"

- [ ] Write the real seed entries for the top-100 lists (below). Moved after the samples: "Samples path now, lists later (Suggested)"

### 📦 Content (first, before code)
**Spec:** `docs/CONTENT-FORMAT.md`, `docs/LISTS.md`, `wiki/systems/r04b-generators.md`

Order: galactic scale down, the same order as the sample sets.
- [ ] Then: seeds and rules for the rest of the 100 categories: 31 not seeded and 42 partly seeded (see "Seeded in" in `docs/LISTS.md`)

### Lists
**Spec:** `docs/LISTS.md`

- [ ] Fill each approved category with its top 100 entries — "real and dynamic invented combo procedurally"
  - Real seed entries per category, written by hand (no fixed number): "Real seeds, game fills to 100 (Suggested)" (`docs/LISTS.md`).
  - The live generators invent the rest, so each category reaches 100 in play.
- [ ] Procedural generator that invents new entries on top of the real ones

### R1 — Build setup (C# built into Windows)
**Spec:** `wiki/systems/r01-toolchain.md`, `docs/HOW-TO-BUILD.md`

- [~] Source layout: `src/platform`, `src/core`, `src/render`, `src/gen` and the rest, one folder per system (started: `src/Program.cs`, `src/platform/`)

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

- [~] Memory and the giant data arrays for billions of things (started: `BigArray<T>`)
  - Big memory blocks reserved up front per system.
- [~] Multithreading so the simulation uses every CPU core
  - One worker per core, pulling jobs from a shared queue. Built: `src/core/Jobs.cs` (`Jobs.Run`, `Jobs.For`); used to measure a planet's regions and to build the land's new bodies all at once. Still to move onto it: the life sim step, terrain building, sea bodies.
- [~] Save and load file format
  - Our own versioned binary chunks: universe, planets, life, civilizations, player. Built: the format (`src/core/SaveFile.cs`) and the life chunk (`PlanetLife.Save`/`Load`). Still to do: universe, planet, civilization and player chunks, and Save and Load in the game's menu.

### R4 — Graphics
- [ ] Full resolution scaling and range: "its got to have full resolution scaling and range"
**Spec:** `wiki/systems/r04-renderer.md`, `docs/img/README.md`, `wiki/systems/r17-docs-and-capture.md`

- [ ] Our own 3D image generator for all game icons and art: "we will need to design the #D genrator and botting of them"
- [ ] Auto-capture tool that renders the doc images and fills every image slot in the docs
  - A mode of the game exe. It finds every `<!-- IMG: ... -->` marker and renders that image (`docs/img/README.md`).
- [~] (built in part: `GeneBody`, `LandLife`, `SeaBodies`) Drawing creatures built from their genes
- [ ] Top-down god camera
- [ ] Visual gene map for every trait and skill

### R4.4 — Live generators
**Spec:** `wiki/systems/r04b-generators.md`

- [ ] No pre-built assets: every asset built live from data arrays as life evolves: "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living"
- [ ] Shared generator core: data arrays in, shapes, surfaces, motion and sound out, cached and rebuilt when the data changes
- [~] Microbe & virus generator (built in part: `src/sim/MicrobeWorld.cs`)
- [~] Plant & fungus generator (built in part: `src/shell/LandLife.cs` from the genes)
- [~] Creature generator (built in part: `src/gen/GeneBody.cs`, `BodyGen.cs`)
- [~] Intelligent creature generator (built in part: `GeneBody`, `CivGen`)
- [~] City generator (built in part: `src/gen/CivGen.cs`)
- [ ] Tech & machine generator
- [~] Space tech generator (built in part: ships, stations and colonies in `CivGen`; the Dyson builds from `content/space-tech.dnat`)
- [~] Galaxy & system generator: galaxies and star systems done (`src/gen/GalaxyGen.cs`, `src/gen/SpaceGen.cs`, from the galaxy, star and system content)
- [ ] Weather & effect generator
- [ ] Sound generator
- [ ] Fossil & ruin generator

### R4.5 — Art style samples
- [ ] Sample sets, galactic scale down, one set at a time, waiting for the creator's input before the next: "and when we do the dsamples we show the non headless and do them galactic scale down one set of samples at a time to take input before doing the next set"
  - Life sets follow the full tree (`docs/wiki/tree-of-life.md`): "and what the fuck we are still doing docs u ments as this:" ... Animals / Insects / Microbes and viruses ..." IS NOT ALL LIVING THINGS IN THE FULL KINDOM PHILUM CLASS GENIUS SPECIES and the full kindom thing in full"
  - [ ] Set 7: Space stations, colonies (mining outposts, prison colonies and the rest), spaceships and hyperdrive ships
  - [ ] Set 8: Cities
  - [ ] Set 9: Intelligent creatures (any kingdom)
  - [ ] Set 10: Animals: every phylum, then its classes, orders, families, genera, species
  - [ ] Set 11: Plants: every phylum down to species
  - [ ] Set 12: Fungi: every phylum down to species
  - [ ] Set 13: Protists: every group down to species
  - [ ] Set 14: Archaea: every phylum down to species
  - [~] (the live microbe ecosystem approved; the full tree still to come) Set 15: Bacteria: every phylum down to species
  - [~] (viruses in the live microbe ecosystem; the full tree still to come) Set 16: Viruses: every group down to type

- [ ] Every sample generated procedurally from a seed: "and remember thses things are get dynamic procedural generations"
- [ ] The asset builder and its examples come before any play-testing of planet life: "yeah we are in no way ready to test it we havent even build the asset builder and done those examples for all the creatures life villages tiebes towns cities space stuff ect ect"
  - [~] Creatures and the rest of life (every kingdom): families of viruses, cells, plants, fungi, jellies, animals and intelligent creatures, all from genes (`--family`)
  - [~] Villages: from the builder species' genes (`--civ`)
  - [~] Tribes: a tribe camp from the builder species' genes (`--civ`)
  - [~] Towns: from the builder species' genes (`--civ`)
  - [~] Cities: from the builder species' genes (`--civ`)
  - [~] Space stuff (stations, colonies, ships and the rest): spaceship, hyperdrive ship, space station and colony from the builder species' genes (`--civ`)
- [ ] How long each step of an intelligent species lasts (wild, tribes, villages, towns, cities, space): not a set time, it follows how much the god helps; a player who keeps at it gets there quicker: "its not a set time... its all based on how the god helps the life to evolve if ur on it you can get there quicker". The god's ways of helping are not built yet; until then each step takes an even share of the last stage's hour. Asked about: "yeah intelligent species live on the land before they ever village and tribe up before going into theri tech and stuff"
**Spec:** `wiki/systems/r04-renderer.md#-style-samples-come-first`, `docs/DESIGN.md` round 6

- [~] Style samples, 3 samples × 3 styles: "remembr we arent using other peoples shit unless we absoulutley have to so plan on buiolding it all and things like ingame microbes viruses and intelligernt creature should all have samples 1,2,3 made in 3 diffeernt styles 1,2,3 for a hand ful of things in the game before we go with any one style and same for things like the galaxy layouts solarsystem layouts and planets and stars and dyson spheres and space ships and space colonires ands space stations and rouseource firleds and gas giants and athe whole periodic table of elements"
  - Styles: RimWorld-like, Populous-like, Stardew-like, all drawn originally by us: "try a rimworld like and a, populous like, and a stardew like".
  - Light on the GPU, built for living things that move and spread: "not one thats too gpu intense and these will be living things in the environment moving and spreading as told before".
  - Waits on R4's basic renderer: "we cant do em till you build tyhier egine and shit".
- [ ] Side-by-side comparison sheet of every subject, for the creator to pick one style

### R4.6 — The coefficient waveform engine everywhere (galaxies down to microbes)
**Asked:** "should we use our coeffient 9c waveformes for this project for better resolutions and game effienciency on such large scales, maybe look into the uni vs matics project on D:/ usb and see if we can fully impliment cofffiencient wave form to everything we do since its our code its okay to use it" / "option 2 everything from galaxies down to microbes and everything inbetween for the complete full game" / "with infinate zoom it should be fun re maping the zoom features"
**Source:** the lab's Uni Vs Matics engine (`UnityAILab/UniVsMatics`, `ftcore/reconstruct.py`, `docs/ARCHITECTURE.md`). Spec: `wiki/systems/r04-renderer.md`.

- [~] (built in part: the living planet's changes as wavelet terms over the six faces, `PlanetTerrain.BakeChanges`) Planet ground: the height and colour fields of each planet as wavelet equations per cube face; the living planet's changes (drift, erosion, craters, sea level) written as term edits; ground pieces rebuilt from the level that fits their size (fixes the coarse, blocky ground seen from orbit at native resolution)
- [ ] Planet saves: the changed ground saved as its wavelet terms
- [ ] Clouds, weather and air: cloud cover, rain and temperature fields as wavelet fields
- [ ] Resource fields and the element map as wavelet fields
- [ ] Galaxies: star density and gas as wavelet fields; zooming from the universe to a galaxy to a star system reads deeper levels
- [ ] Nebulas and the deep sky as wavelet fields
- [~] (built in part: the star's surface and its plasma dived into) Stars up close: surface plasma as a wavelet field with mode-E detail
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
- [~] (built in part: see R6b) Ground, plates, mountains, oceans, rivers, ice
- [~] (built in part: `PlanetCore`, `PlanetPlates`; see R6b) Planet interiors: core, mantle, crust, magma, convection, plate tectonics or stagnant lid, volcanoes, geodynamo and magnetic field, magnetosphere and auroras
- [~] (built in part: `PlanetAir`; see R6b) Atmospheres: layers, pressure, makeup, greenhouse and runaway greenhouse, and air escaping (thermal, hydrodynamic, non-thermal)
- [~] (built in part: `PlanetWeather`; true day and night with Always day, DESIGN 420) Air, weather, climate, seasons, day and night
- [~] (built in part: every mineral's real crystal and traces, every natural element placed, `content/minerals.dnat`) Chemistry and minerals that life can use
  - All 118 elements. Lab-made ones (93+) unlock once a civilization creates them.

### R6b — The living planet (it runs even with no life on it)
**Asked:** "a planet is a living breathing thing even without life, did you do all the magnma cores mangnetic fielsds atetonic plates volcanos earthqueakes all of that shit in totaltiy that ive already told you.? what elese have you skipped" Earlier: "dont forget the forces of nature weak and nuclear forces of atoms pysics electormagnatisim magma in plantes that arte tectonic with magnic fields and shit come one wwhat about gravity and atmosphers"
**Today in the code:** the ground is a fixed height map from noise; mountain belts, volcanoes, rivers and resource fields are placed once and never change; rain and snow are only pictures. Nothing below is simulated yet. Data: `content/physics.dnat` (interiors, processes, atmospheres), `content/resource-fields.dnat`. Spec: `wiki/systems/r06-planet.md`, `docs/wiki/forces-of-nature.md`.

- **Inside**
  - [ ] Mantle convection that drives everything above it
- **Plates**
  - [ ] Hot springs, geysers and deep-sea vents (where life can start)
- **The magnetic shield**
- **Air**
- **Water and weather**
- **Climate and seasons**
- **The surface changes**
  - [ ] Landslides, floods, sinkholes and coastlines that move
- **From outside**
- **Everything feeds everything**
  - [ ] Each process changes the others and the life on the world: plates make mountains, mountains change rain, rain feeds rivers, rivers carry nutrients to the sea; volcanoes and impacts cause die-offs; ice ages move life toward the warm middle
  - [ ] Resource fields form and change with this geology over time (magma makes ores, hot fluids make veins, seas lay down sediments)
- **Content still to write (researched, like `content/physics.dnat`)**

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

- [ ] (Later) Mac program
- [ ] (Later) Linux program (today: through Wine, with `linux/run.sh` in the zip)

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
  - Samples: Intelligent creatures (3 examples × 3 styles)
  - Samples: A handful of other living things (plants, insects, animals, fish, birds) (3 examples × 3 styles)
  - Samples: Space ships (3 examples × 3 styles)
  - Samples: Space colonies (3 examples × 3 styles)
  - Samples: Space stations (3 examples × 3 styles)
- **"Combined score from all the categories"** (R15). Replaced by the creator's round 4 answer: "life in your galaxy and life \"havested\"". The new task is under R15.
- **"Power list (`docs/POWERS.md`)"** (R11). The list itself is done (see FINALIZED). Replaced by "Build all 100 powers from the power list".
- [ ] **Remaining sample sets in the locked style only:** "yup same one the center one is the best, dont need to test them twice". Sets 4–17 show only the Populous-like style.
- [ ] **Stuck at the deepest zoom inside the Dyson sphere:** "when i zommed all all the way in to the ground as close as i could get, i got stuck in the ground and couldnt back back up(but that can be solved when we do micorscopic)". Fix with the microscopic set.
- [ ] **Sample sets in proper order:** "okay well we need to do everything in proper order for the full game". The order is elements → microscopic world (viruses, microbes) → kingdoms of life → intelligent creatures → cities → stations, colonies and ships (built from their species).
- [ ] **Elements into the in-game wiki:** "looks good that can go in the wiki once we flush it out with tool tips throughought and propelry gate it all off for balanced play and leveling". This needs tooltips throughout, and gating of everything for balanced play and levelling.
- [ ] **Life from more than one start:** "ur lighning bolt can start life again too u can start life in more than one place on one planet". The lightning-bolt power can start life again at other spots on one planet, each strike costing Souls ("not as many times as you like powers take life souls"). Each start is its own lineage in the planet's life simulation and on the tree of life.
- [ ] **Powers and abilities unlocked by the tech of life:** "like for example u shouldnt be able to use add elements and such until unlocked by life... so alot needs to go into the tech of life and how it all unlocks your powers and abilities". Decided: day one is Fire, cold, water and lightning only; each element unlocks once living things first use it. Still to design and build: what unlocks Add Genes, the Spark of Life power and every other power and ability, and the element-use tracking in the life sim.
- [ ] **Life evolves from the primordial ooze:** "and dont forget the whole dna thing.. life evolves we dont start with trees and animals, from the primordial ooze like". Already how the planet life sim works (one spark of simple replicators; everything else evolves); keep it so in every system.
- [~] **Full ecology: spread, prey, predators, herbivores:** "life need s top spread too if nutrtients and conditions are favoriable with actual prey peditor herbavor type shit effecting the life outcomes in full ecologoical like game, so when u start u just have viruses that evolve cells and nucleuses and all the way up the chain of life". Built: a food web per region (producers, grazers, hunters, nutrients) and spreading only where nutrients and conditions suit (`src/sim/PlanetLife.cs`). Still to do: a nucleus step (cells with a nucleus) between first cells and many-celled life.
- [ ] **Life waits for the genome, Genebank and heredity charts:** "well i wouldnt wory about the life just yet we havent built out all the gene bank and shit for the game and how creaturtes evolave and tthe fulll aspect of life and all the genology and heretitanc graphing and tables and charts". Do not tune or judge life outcomes until R7 (genomes), the Genebank, evolution, and the genealogy and heredity graphs, tables and charts are built.
- [~] **The ground you see follows the living planet:** seas, mountain heights, drifting continents and great craters, shown as a whole-planet swap at big moments under a short fade ("Whole-planet swap"; `PlanetTerrain.SwapGround`, `PlanetView.ReshapeGround`); ground pieces split by their size in pixels. Still to do: plate edges are straight lines that cut coasts straight (they should wind and break); new volcanic land; ice sheets drawn; old craters wearing away.
- [x] **One world, one camera, no handoffs (true infinite zoom at any angle):** "tha angular reelation is not correct when zooming into a galaxy as when approaching a galaxies its just instantly pans flat instread of appearoaching at the angle u are currently in relation to the galaxy, stop it from panning flat when zooming into a galaxy and make it real infinate zoom at any angulare relation and vector" / "no handoffs, that is the shit zoom staging i was talking about"
  - [x] One world holding every scale: the universe of galaxies, each galaxy's stars, each star's system, each planet's ground, its life and its microbes, all placed in one shared space (64-bit positions, a moving origin)
  - [x] One camera that flies through it at any angle and direction; zooming moves the camera toward what the mouse points at, never switches views (built the whole way, from the cosmic web to the quarks; it began with the universe and galaxies: `src/shell/UniverseView.cs`, `--world`; the wheel flies along the mouse's line by a share of the distance to the nearest thing, so it slows by itself and never ends; a galaxy fades from its far glow into its own 150,000 stars at whatever angle you come in)
  - [x] What is drawn is chosen by how big it looks from the camera, so things grow smoothly into view (a galaxy into its stars, a star into its system, a planet into its ground) with no pop
  - [x] Detail at every scale from the wavelet levels and mode-E (R4.6), so nothing loads in steps
  - [x] Below the ground's finest piece, the ground's own matter, all the way down: "i love the scale when u go down to um u should start to see atoms and shit not to mention microbes bacteria and viruses, but like i said life isnt built yet". Grains of sand and dust (mode-E), then the crystals of the real minerals that make the ground there (from its resource field and `content/elements.dnat`), then their molecules, then atoms of their real elements; microbes, bacteria and viruses join once life is built
  - [ ] Remove the separate views and their handoffs (GalaxiesView, DeepView, PlanetView switching) once the one world draws everything they do
- [~] **The engine must use the GPU properly:** "and is our engine beffy enough? i see im only using like 2G of GPU and the game is running kinda slow". Today the CPU and GPU take turns every frame (EndFrame waits for the GPU to finish), the ground is built vertex by vertex on the CPU, and points are re-sent each frame. To do: overlap frames (two or three in flight), build the ground and rebuild the wavelet fields on the GPU, keep point sets on the GPU and move them with constants. Built: the ground is built in the background by one worker per core and each frame takes in finished pieces (never waits); 37 to 49 fps at 3818x2074 on a 2 km view. Built since: a frame in flight (`Gpu.BeginFrame`, DESIGN 459), the grain's face and the sand per pixel on the card, the web's light sifted on the card. Still to do: the ground and wavelet rebuild on the GPU, points kept on the GPU.
- [~] **Everything on the GPU:** "why is it so hard for my GPU to simulate such a simple area zoomed in" / "nothing should run on cpu unless absolutely necessary". Move to shaders: the grain faces' shape detail, steps and stains (the CPU sends a coarse grid and the grain's numbers), the waveform (its stored CDF 9/7 octaves uploaded once), mode-E, the stones, the atoms' orbitals and electrons, the lattice; the CPU keeps only picking, the camera and the coarse shape it needs to agree with the GPU.
- [ ] (Later, DNA-AND only) **Habitable means carbon-based:** "and remember habital is only for carbon based life but thats nanother thing for later and for dna and only". The habitable zone is only where carbon-based life with liquid water can live; other chemistries of life have their own zones.
- [ ] (Later) **The rest of relativity:** length contraction, redshift, frame dragging.
- [ ] (Later) **Light rendered truly below its wavelength:** "later when ther is color properly do it"
- [~] **Every body landed on and zoomed into, with its real make-up:** (roadmap R18: DESIGN 486; the small bodies, the stars' surfaces and the neutron star's crust built, DESIGN 546; a star's layers to its core left) "ie i should be able to find a gold astroid or nickle being most common ect ect and zoom infinated on any body or abject" / "stars shoiuld be mostly hydrogen till getting to it heavey m,etal cores ect ect"
- [ ] **A gravity switch: bodies straight on, stars blown apart:** (open: DESIGN 519) "as i dont thinkg galxies are acutally being held togetther buy the laws of nature. like if i has a setting to turn off all gravity.. would stars explode pressures of all bodies zero and every body move to straight line travel... right?"
- [ ] **Screens of the landing page, the installer and the document viewer in the readmes:** (waits on the user's windows: open each and say so, and it is grabbed from the screen) "and we will need screen shots of like the docs viewer and install requirments and installer posints but not alot of screen shots just the main things" / "and landing page"
- [ ] **The Linux path run on a Linux desktop:** (waits on a Linux machine or a Linux user's report) "okay well make sure that path is real"
- [~] **Every document checked top to bottom, every single wrong word:** (done: DESIGN 573; the docs audit's fixes shipped) "make sure all docs are not stale, and when i say "all" i mean it all fucking documents, top to bottom look for single words that are wrong too" / "and we did the comparisons correct for other similar apps and games right? with the chekmarks and write up and stuff all proper and truthfull at how kick ass this application is beeing so samll and doing so much"
- [x] **The Universe's own .claude and workflow files, the ledgers proper to each project:** (DESIGN 575) "yeas the Universe need its own .claude and its own files in it file directory within the danand project" / "so pull the ual repo templet and buuild it all out correctly while kiiping its docs intact" / "not over write existing shit" / "and propelry finalize all the nuniverse todo work weve done in finalizxed makeing sure the todo is only certified still open items" / "proper to each project"
- [~] **The well a complete dilation cone from the mouth to the point at every depth, longer than the hole:** (built: DESIGN 576; waits on the test) "amnnd the black hole graph thing i think u are missing is space and distance in a black hole is strached to infinity so the grid funnel needs to be complete from top to bottom able to see the shole thing when looking down to the pin point or tit big opening looking up" / "its longer than the space that existes the black hole takes up... it stratches beyound the confines of space and time and is a complete funnel from top to bottom at all zoom scalled, and u actuall need to travel a long ways like a long long weays the funnel is a time and distance dialtion cone squeezing down to one point of non space existace"
- [~] **Your own clock against far clocks wherever you are; a world's pull on clocks and a satellite's gain on its card; the far watcher at the horizon:** (built: DESIGN 577; waits on the test) "rtemebre time speeds up further from gravity and slows whenin very high gravity and maybe even stops" / "its why we have time dialtion codes for satilights further from gravity time runs differnt"
- [~] **Every world's own mass, on its card and in its clocks:** (built: DESIGN 579; waits on the test) "mass is not in"
- [~] **The gravity switch:** (built: DESIGN 580; waits on the test) "as i dont thinkg galxies are acutally being held togetther buy the laws of nature. like if i has a setting to turn off all gravity.. would stars explode pressures of all bodies zero and every body move to straight line travel... right?"
- [~] **The rest of relativity: length contraction, redshift, frame dragging:** (built: DESIGN 581; waits on the test)
- [~] **A star's layers down through to the heavier elements at its core:** (built: DESIGN 582; waits on the test) "stars shoiuld be mostly hydrogen till getting to it heavey m,etal cores ect ect"
- [~] **Decay happening before your eyes:** (built: DESIGN 583; waits on the test; the fields particles are waves in stay open)
- [~] **The tour's end: a true figure of what it reached against everything there is:** (built: DESIGN 584; waits on the test) "and at the end of the tour say something acurat like Now youve seen .0000000000012% of the universe. but a real number of total number of destinmnations version the trip the tour takes" / "Vs* not version"
- [~] **The clip's text scaled to the clip's size:** (built: DESIGN 585; waits on the test) "and when we change resolution of the recorder the text needs to be proerply scales and sized to the resolutions"
- [~] **Clip size on the landing page, the clip's text measured right:** (built: DESIGN 587; waits on the test) "the text isnt sclaed correctly and i didnt have a resolution selection i could see for the recording"
- [~] **Every card line one short clause; no card more than two thirds of the screen:** (built: DESIGN 588; waits on the test) "like currently the text is full from top to bottom and takes up too much screen"
- [~] **The spots on the drawn face, the ridden electron always drawn, the face at the nanometres matte and whole:** (built: DESIGN 589; waits on the test) "i took sshots of issues and at near 100 micometers im seeing through the planet at certain zoom. so that needs fixed and we are good, and all the shots are obvious of the issue like when it says is following a electron and ther is no electon in the selection and the weird shit like complete darkness when its saying other things are happening"
- [~] **The lattice the quantum picture: electron clouds and shared electrons, never balls or sticks:** (built: DESIGN 590; waits on the test) "what about the pitch black right before atom view and wtf did i say about that damn bond bars being out dated and u fucking built them back" / "thats old scool understandings before quantum and elcton mapping"
- [~] **The well an inward spike: the wall four tenths of a view out, closing to a line before the tip, the sheet only near the mouth, rings lines at any width:** (built: DESIGN 591; waits on the test) "as u can see in screen shots the grid doesnt persists to the finate point of the cone in the singularity i took shots of the shit cone thats not a cone at all" / "it need to be more like a inward spike grid i say cone and u are making it wide it turning into a singel line before hitting the singularity it squezzes so small the tip of the cone in reality"
- [~] **Looking round the well as you fall, by hand and on the tour:** (built: DESIGN 592; waits on the test) "and as you fall u should be able to pan around slightly below and abow and to the sides to proply show the sturcture and shape of the grid u are falling into to is singularity"
- [~] **The patterning found: a star's surface no longer a lattice of dots:** (built: DESIGN 593; waits on the test) "please tell me u finally see the scren sot of the petterning of the solar systems ive talked about so much before that is a random chance to find them but they are out there patterned lines up planets and bodies"
- [~] **The way into a hole not its size: forty tenfolds for every hole, the way in on the card into the light-years:** (built: DESIGN 594; waits on the test) "and remeber traveling into a black hole is not relative to its size in a black hole there is near infinater space and no space at the same time. ie u cvasn trvel light years into ablack hole before reching the singularit at the internal funnel length is noit proptionasl to the size of the black hole space is infinate but at same time funnels down to no space at emense distances and scales"
- [~] **The funnel's walls there to be seen: every ring and spoke as dense as the sheet's lines:** (built: DESIGN 595; waits on the test) "same priblem ther is no funnel grid its just an opening i took scrteen shots"
- [~] **The cone fixed in the view and travelled down: the walls beside you closing in as you fall, a pin point below:** (built: DESIGN 596; waits on the test) "where is the funnel walls that should be besid me as i fall closing in and getting tighter as i fall" / "and should be a pin point below me,, and it look like u are still view thing the singularity on zoom in instead of correctly look out to the sky hurizons"
- [~] **Millions of light-years from the horizon to the singularity, by the hole's mass, a law of the content, counted on the card:** (built: DESIGN 597; waits on the test) "and like i said ther are millions of light years to the center of a black hole" / "from event to singularity" / "well not the smae based on mass"
- [~] **The tour's fall at a pace that does not take forever: the clock's stretch eight seconds, a tenfold and a half a second to the point:** (built: DESIGN 598; waits on the test) "so make sure the tore propley falls at speeds needed to not take forever"
- [~] **The tour's one look round the funnel as it falls, the mouth, the walls, then the point, about a minute:** (built: DESIGN 599; waits on the test) "and looks arounfd at the funnel to get its scope and shape and porints of refrence" / "as we fall in" / "not cycle just a once look around its not going to be a long process about a minute"
- [~] **The tour's hole a feeding one, the whole disc flyby; every hole's photon ring:** (built: DESIGN 600; waits on the test) "something broke black hole isnt there" / "i took screen shot" / "its suppose to be going to one thats feeding" / "the whole acrestion disk fly by"
- [~] **The well rings and straight spokes, no spiral; the sheet at the mouth from every depth; the tour looking up as it enters, then down the funnel to its point:** (built: DESIGN 601; waits on the test) "the funnel just looks like a trippy effect not a real grvity well and why the fuck and where did u decide to make it spiralled i told u to look up garvity well funnelks wtf ids so fuvcking ard to recreate exactly what it is and is disappaard at zoom in when it shall not, and ur llook around was fucked and too late to pan up at sky after already entering should be llooking up as u enter then look down along the funnel to is absoulte pooint, do u not undersastnd what it liooks like too be in a ubside down spike cone?"
- [~] **The fall's grid drawn at every scale, never faded with the sky:** (built: DESIGN 602; waits on the test) "anbd near atom scales the grid just stopped being projected"
- [~] **The disc's far side bent over the shadow's rim, never hidden; the horizon stop where the shadow is a round orb:** (built: DESIGN 603; waits on the test) "anmd the black hold has a none round shape like half the acretion disk is invisible as there is like a shost blackness that not a real orb black hole centered in the accrtion disk its like it has a nig block build to the top of it that makes it not look like a orb black hole"
- [~] **The sheet rings and spokes, round from the top; the fall a travel first and a zoom last:** (built: DESIGN 604; waits on the test) "okay the ggrid is square not correctl round at the top and its starts zooming in with never traveling the millions of light years to the singularity.. it only goes in like 200000m or something stufppid and starts zooming in way to quickly before the funnel even squeezes in" / "i toolk snaps"
- [~] **The fall's look the right way round: up through the mouth, then down to the point:** (built: DESIGN 605; waits on the test) "i didnt see the walls of the spike cone fuuneel thing.. i only saw an opening at the top i was falling into"
- [~] **The tour's pans eased in as well as out, no snap away from the last thing:** (built: DESIGN 606; waits on the test) "and the pan waway from objects the tour visits are too abrubts making it look jerky and laggy just instantly changing the view completely" / "ie the black hole just instantly flew out of view like disapperend in a snap" / "other objects do this too"
- [~] **The well's grid gone once you are out of the hole, no space kind outside space:** (built: DESIGN 607; waits on the test) "and im out of the black hole now and ime seeing the grid all over the universe" / "i took snaps"
- [~] **The text a warm ivory and gold with a dark rim under every letter, readable on black and white alike:** (built: DESIGN 608; waits on the test) "and we need a differnt color text that not gody as in the none light scale the white text on white and black environment make it impossible to read the text, but we need something that sint clashy but works for bioth black and white screen when in eveironments of the the universe"
- [~] **The supernova's fireball drawn as a star's face, full-bright while it shines:** (built: DESIGN 609; waits on the test) "check first shot of issue nothing is in focus so wtf is the text meaning something isnt built"
- [~] **Every asteroid its own shares of its kind's minerals, eight elements and their forges on the card:** (built: DESIGN 610; waits on the test) "and astroids still just genericly say rock and iron not theri actual varied and differnt vasness of compositions of all types and elements from a super nova"
- [~] **The zoom brought up from rest and slowed to a crawl on approach; the shot's pan before its travel:** (built: DESIGN 611; waits on the test) "and its still jerky at points like pan away from black hole and zoom to solar systems and when it first pans out the the galactic webbing it need s to pace slow down to a craw on appraches and properly pan away berfore travel away from a point body focus"
- [~] **The lattice's clouds bright enough to read; the hand-overs to the orbs and to the orbitals by degrees:** (built: DESIGN 612; waits on the test) "i took some screen shots of issues and at 200 picometers jsut befoer it there is a had off between orbs and elctrons and there is not middle ground it jsut instaly jusmps to orbs then instantly jusmps to our electron fields of yellow abnd blue elctrons, check clips"
- [~] **Every zoom ending at a crawl, the pull-outs from matter eased to the pop, chained zooms one movement:** (built: DESIGN 613; waits on the test) "u didnt fix the pull out to web it just instant stops i though i told u all movememnts need slow down a crawl once close to doing centematics"
- [~] **The climb out of the hole looking up at the mouth the whole way:** (built: DESIGN 614; waits on the test) "and the balck hole climb out needs to be looking up on climb out not down"
- [~] **The supernova's remnant from the blast, the tour's shots to what is there:** (built: DESIGN 615; waits on the test) "and it went to a focus on a super nova and nothing was there just empty space, where is its nurtron star or if bigger the balck holes?"
- [~] **Every belt, Kuiper and Oort body and every comet on its own Kepler orbit, the belts' haze turning:** (built: DESIGN 616; waits on the test) "and it appears astroid belts and comets and shit arnt even moving"
- [~] **The detail tuner watching the processor too; the frame's processor time by phase on the stats card:** (built: DESIGN 617; waits on the test) "and im only getting 5fps on 1m range so wtf is acually taking all th fps when is looking at a small spot on the ground"
- [~] **The "never a ball" asides gone from the code, the card, the tour and the docs:** (built: DESIGN 618; waits on the test) "get rid of the constant no ball shit you wrote everywhere into my project"
- [~] **Every proton and neutron its three quarks in its gluon glow at every size, no balls; the nucleus arcs short:** (built: DESIGN 619; waits on the test) "i took images of the issues and the protons and nutrons change from balls to quesrks with like no zooming at all is that right? do we need the balls can we jsut propely do them the nucleaus protons and nurtons as quarks better so that is not a instant jump to qwerks and the nucles pan around of an atom takes too long and it pans around liike two differnt times for like 30s in total which is way to long to look at a atom nucleus that is just red and white balls"
- [~] **Every atom its electron shells at the lattice level, the electrons' paths from seven spacings:** (built: DESIGN 620; waits on the test) "like i dont like the old school balls for atoms i would like it to propely be electron fields at higher levels as the zoom scale isnt much difference we can just propigate them a bit smaller and more consisous and accuracy to what the atoms actually look like so no ball atoms but atctual elctron fields at scale where balls appear the ball is suppose to represent the outer edges of the non valance rings of electons so they are totaly displayable as the elctron fields we have just at a little higher scale to see them a ligttle sooner berfoe the classic understanding of balls representing whole electrons"
- [~] **The fall's card measured from the horizon down the funnel, the singularity millions of light-years down by mass:** (built: DESIGN 621; waits on the test) "and when entering a black hole it says like 11 AU not the proper millions of light years to the center singularity" / "based on mass" / "no the singualrity is not the way in the event horizon is and the singularity is millions of light years down the funnel so it needs to be accurate to the  non pysics we dont understand of infanite space in a black hoole"
- [~] **The tour's fall half a minute, not a minute and a half:** (built: DESIGN 622; waits on the test) "let make it faster 1 minute of falling with nothing to see but a grid cone is too much bore"
- [~] **The quarks tight in one clump from afar, spreading only close in:** (built: DESIGN 623; waits on the test) "and it need to have the qwurks alot tighter currently i think i can count them"
- [~] **The singularity one uncountable mass of quarks, the nucleon's three never pulled apart:** (built: DESIGN 624; waits on the test) "but they dont pull apart they ar trillionX^e1000000000000 quarks" / "cramed into zero space"
- [~] **The singularity five hundred overlapping glows, not six thousand:** (built: DESIGN 625; waits on the test) "lets not have 6 thousand glow we can handle this without frying my gpu"
- [~] **The climb out of the hole as fast as the fall, about half a minute each way:** (built: DESIGN 626; waits on the test) "okay the fall in was much faster than the clibe out they need to be similar and not minutes and minutes of traveling up the tunnel used speed up"
- [~] **The asteroid's caption its own kind and minerals, the places' lines without stock make-up:** (built: DESIGN 627; waits on the test) "and astrodis astill only say rock and iron generically , i told you four times now to fix this"
- [~] **The asteroid's dive all the way down to a quark:** (built: DESIGN 628; waits on the test) "and the astroid zoom into the the material need to go to subatomic levels as it goes down the the white and black crystaline structures and dots but then backs off beforee ever getting to atoms and elctrons and nucleus and qwerks"
- [~] **The tour's supernova logged twice a second, so a blank stop can be read:** (built: DESIGN 629; waits on the test and its log) "the suprer novas still and blank focal points no black hole no nutron start no exploasion"
- [~] **The faces drawn before the glows, the lattice's level no longer black:** (built: DESIGN 630; waits on the test) "check the images the problems are obvious like pitch black while zooming into levels of the of attoms as all the near upper zooms ar all black and i tried to capture tem all in screen shots"
- [~] **The atom you picked the atom in focus, the proton and quark stops no longer skipped:** (built: DESIGN 631; waits on the test) "and it neverr got to querk level where did my planet zoom do with the qwerks"
- [~] **The stats card never wider than a wrapped line:** (built: DESIGN 632; waits on the test) seen in the shots: the stats card over the matter's card
- [~] **The inner cloud at a picometre bright enough to see:** (built: DESIGN 633; waits on the test) "and just above and at 1 picometer shit is too dim"
- [~] **The orbit lines faint continuous lines, never rows of evenly spaced dots:** (built: DESIGN 634; waits on the test) "im still seeing that patterning in solar syastem of planets lik being designed in straight lines"
- [~] **The asteroid's dive the planet's own stops, the tour's supernova a shining one with a remnant, the lattice level probed in the log, the levels' words:** (built: DESIGN 635; waits on the test and its log) "ive put in a bucnh of errors i saw in snaps pics of the zoom in to the astroid its fucked the astroid is invisible till it gets to crystal latuses and the elctrons never appear, wtf we need unification and unifying of the zoom ability to be like planet zoom for any thing or any body u zoom into. so in order there is the astroid images of its fucked up zoom levels then there is maybe the super nova again with no super nova and no nuston star and no black hole in error, so wtf wher is my super novas it keeps zooming to a focal point thats just empty space, and then i aslos took pics of the planet zoom wher shit is black and shit is all jagged and crazy like and at the atom level it need to not just appears from blackmness at a zoom to full on electon fields its a instant hand over ther is no scale in that proper of them being small and getting bigger and the description text needs to propely describe ththat atoms ares pure engergy basicly and classical balls for atoms is in no way accurate but no in so many words"
- [~] **The comet dived into on the tour the same way as the planet and the asteroid, one chain for any body:** (built: DESIGN 636; waits on the test) "and like i said unify all astral body zoom ins so they are all acruate based on elements zommed into as high scales"
- [~] **A dive onto a small body landing on the drawn stone's own surface, its spin held while you are on it:** (built: DESIGN 637; waits on the test) "okay but u fixed the invisible astroid as zoom rihgt?" / "what the fuck do u mean its visible ther in not a fucking thing but stars at all the xzoom levels i screen shotted when the sasteroid was selecterd"
- [~] **A spot on any small body picked by a click on its stone, the zoom flying down to it:** (built: DESIGN 638; waits on the test) "and should differnt spots on an astroid be selctabble just like all bodies" / "so maqke it so for all bodies"
- [~] **Every small body drawn from its precise offset from its star, never from a billion-light-year double:** (built: DESIGN 639; waits on the test) "that same solar system shit aint fixed i screen shot"
- [~] **The pick's aim at a small body its precise offset, the same place it is drawn at:** (built: DESIGN 640; waits on the test) "okay i took pics thios time it never found a astroid let alone zoomed in and it just kinada stalled and never made it to a comet either" / "i SAID I TOOK PICS"
- [~] **The dive onto a small body firing where the zoom can reach, the camera set over the drawn stone, the matter's stops never run in open space:** (built: DESIGN 641; waits on the test) "the astroid is still fucked read what it says its suppose to be looking at versus what on the screen.. wtf this is totally fucked and dont you dare say the astroid IS there becasue its not. i tookk about 100 pics ofevery second passing so u cant fuck it up" / "there like 4 or 5 things it says in theose pics that is not whats in view" / "why is the astorid disappearing when zooming into it?" / "and fuck if u look at the scale it never zoomed in"
- [~] **The ghost discs beside a rock gone (the far glows only the bodies not drawn as stone); the dive's state in the log:** (built: DESIGN 642; waits on the test and its log) "same fucking problem i got pics on first zoom in the astroid instanly disappears like it went zooming past it or astroids dont have something planets do to where the astroid is ghosting us on zoom in or it something about being too mall idk we need to build the astroids to proprly have the full zoom ability and not like ghost physics that doesnt hold up... i got pics"
- [~] **The near plane never leaping out past a body you are beside, so the stone is not clipped away; every rock with a kind and a ground to dive onto:** (built: DESIGN 643; waits on the test) "same issue and i got picks directly before i didappears and directly after not a framwe skipped" / "i got pics"
- [~] **A young supernova's ejecta a ballooning cloud round its face, bright while it shines, the shots framed on it:** (built: DESIGN 644; waits on the test) "one issue now when i told u to add super nova i see their gas explosions globes all over but every time the tour goes to a super nova its just a star with no cluad, wtf it even says it poped within the week so how the fuck and where is the balloning gas cloud of the super nova is not there whre the tours go to even tho it says it just blew a week ago which we can say as weeks fly by like seconds in this game" / "cant* say"
- [~] **The asteroid's and comet's descents in the world's own layers, the atom's long chain once on the world, the way out at the pace of the way in, less waiting and circling:** (built: DESIGN 645; waits on the test) "i tolkk lots of pics it never made it to ground level and whole zomme levels were zommed into well not on the surface and a bunch of layer zooms are are missing like grains and all their layers and even the atom level after 2nanometers i just see white dots at 2um and i dont remember seeing that on the planet zoom in" / "and we dont need the whole hour long atom waits when we have to do it all again on the planet" / "too mauch waiting around and pauses and too many orbits of objects" / "look at the pics i just took the whole zoom out skipped everything even tho the zoom in skipped it all too"
- [~] **The one zoom chain on every body, the world's own stops from a person's height to the quark and back, nothing left out on the asteroid or the comet:** (built: DESIGN 646; waits on the test) "what the fuck do u not undersatnad anbout unifying the zoom ins for all bodies" / "i dont believe you this is the 23rd time weve tried to fix it!"
- [~] **A small body a world: its ground made by the same ground system a planet's is, nothing special for rocks:** (built: DESIGN 647; waits on the test) "start it then !!! FUCK!!!!!!!!!! YOU!!!!!!! YOU ARRE WASTING MY TIME!!!!!!! FUCKING DO IT!!!! HJESUS FUCKING CHRIST ALMIGHTY WHAT THE FUCK DO I HAVE TO DO TO GET YOUR ASS CODING"
- [~] **The dive's looks, arcs and orbits three to six seconds each, part turns, no pauses, on every body alike:** (built: DESIGN 648; waits on the test) "and make sure the dip into the astroid is not jerky and quicvk but doesnt linger and rotate full orbits views of shit as that takes to long and no pauses"
- [~] **The crash at the first zoom toward the asteroid (the small body's world indexed as a planet's) fixed; its day passes like a planet's:** (built: DESIGN 649; waits on the test) "it crashed at zooming into the astroid" / "like theh first zoom incremment when it wasnt even close to the asteroid crashed it or it crashed trying to get close idk i have tno information on what happened"
- [~] **Every small body its own size (metres to the width gravity rounds a body at) and shape (lumpier the smaller), by data:** (built: DESIGN 650; waits on the test) "and we properly have diffetn astroid shapes and sizesd right as some are feet across and others can be a 1000 miles or more, any sized bascily before gravity pulls it into a sphere"
- [~] **Typing a seed on the landing page: the field has the focus, digits typed anywhere on the page go into it, never Windows' refusing beep:** (built: DESIGN 651; waits on the test) "something is up im trying to type a seed into the landing page and im gettth the error sound and cant type numbers into it" / "the windows error sound" / "the same sound that i used to get when trying to close out the top right x on this app" / "that we fixed"
- [~] **The small body's ground lives by distance like a planet's, the camera rides with it, the stand stop no longer drops it:** (built: DESIGN 652; waits on the test) "SAME FUCKING ASTROID PROBLEM I TOOK PICS JEXSUS CRISTE THE 40th fucking attempt at fixing one fucking thing" / "idk write some code to catch the fucking issue" / "id rather u figure it out tho"
- [~] **The tour's supernova target its live place (moved onto a real star once the galaxy is made), so the shots fly to the cloud, not to where it was picked from afar:** (built: DESIGN 653; waits on the test) "the super nova fly by went back to be just a star so u dint code it properly to persist the gas clauds correctly"
- [~] **The small body's ground centred where the body is now, after the orbital ride, so it no longer shakes and the landing comes:** (built: DESIGN 654; waits on the test) "something is uup, the astroid is shaking violently i tried to picture it shaking but without video its hard to capture but i tried to take pics u can see these were consectuve back to back pics and you can see the great variation in where it is at any one momment the astroid is just shaking violently and wmoving back and forth not keeping focus and the tour froze, i left it open if u can get anything from thew app, its still shaking"
- [~] **A spot picked on the face at height nothing, so the camera is on the face and the crystal, lattice, atom, electrons and nucleus show, and the way out too, on every body:** (built: DESIGN 656; waits on the test) "major problems and everyone of my pics documents one read the text and look at whats in focus and the zoom in was all funky and the zoom out was nothing but blackness all the way to the surface" / "ik dont know whats so hard about matching code we already have working" / "akay dont jsut fix it for astroids" / "fix it for all bodies,, you know what that means right?"
- [~] **All bodies: the tour's chain on the star, the giant to its core, the neutron star's crust and the nebula's gas, each the planet's own stops, by the one code; black holes kept as their fall:** (built: DESIGN 657; waits on the test) "well you obviously didnt do all bodies as u didnt list them all in what u said" / "what about stars what about nuetron stars what about gas giants metal core,, like i said u didnt do all bodies" / "ALL BODIES" / "so what do i mean by all bodies" / "black holes are differnt there is no matter to zoom into"
- [~] **The public pages read in full and mended: sizes, counts, the tour's bodies, the small bodies as worlds, the cloud, "sunlight", the decay note, the picks, the crash log, the doubled note:** (built: DESIGN 658; waits on the test) "full document push and update and check for stale or light wrong words used in the public facing documents i see things are minute ly not worded correctly in  some places im sure u can find them"
- [~] **The atom you dive at drawn and pickable at every size in a gas or a plasma, so the nebula's, the star's and the neutron star's chains go down; matter picks that find nothing given up at once:** (built: DESIGN 659; waits on the test) "wtf are these screen shots probelms its saying things that arent on screen"
- [~] **The shot's pace eased at both ends, so no crossing jerks away or slams to a halt:** (built: DESIGN 660; waits on the test) "and every tour movment need proper pand and gentle pan away shit like tleaving the focuus of the black hole jsut instantly shoots to another system like slide show the black hole si there then another system is instantly in view it look like im cheezing the video to make unreal thing look like they are real, like i mean the snap jerks everywhere on all motion start ups and stops need to be gradual to a halt and gradualy pan away and speed up not just jerk from one place to the next"
- [~] **The piece of ground under the camera never thrown out of the walk; the ground's log line with the height above bare rock and the piece under you:** (built: DESIGN 661; waits on the test and its log) "and if u check the new ones pic just made the astroid is showing stars through it" / "the application is still open pasued on the issue" / "at 1mm"
- [~] **Nebulae drawn close up as clouds of their own gas, each kind its own shape, from forty radii in:** (built: DESIGN 662; waits on the test) "it now zzoms to planetary nebula i think u added that but the nebulas are missing"
- [~] **The tour: the asteroid seen, not dived into; the nebula out of it:** (built: DESIGN 663; waits on the test) "what the fuck this thing was suuppose to max out at five minutes and u keep adding shit to the tour" / "kllets not dive into the astreoid that save alot of time" / "and i never said do a gas cloud fly by" / "m,ake those changes i said and dont worry about the time just make thosae adjustment"
- [~] **A star whose system is open is the target at its real place and radius, so the zoom ends at its face and the dive comes:** (built: DESIGN 664; waits on the test) "i took some pics when it failed and had nothing in view talking about shit that wasnt happening in view"
- [~] **The tour back to what it was: every chain added without being asked for (the comet's, the giant's, the star's, the neutron star's) out:** (built: DESIGN 665; waits on the test) "okay wtf !!! now u r zooming into the comet, wtf STOP FUCKING DOING SHIT I NEVER FUCKING TOLD YOU TO DO"
- [~] **The crash at a grain of water ice's lattice (the card stopped answering) fixed: a molecular crystal's sites capped and its atoms drawn as a crystal's, and a ceiling on the glows in a frame:** (built: DESIGN 666; waits on the test) "it got to 2um and then crashed,, and it was weird it was just side stepping for along ways during grain zoom levels and never propelry zoomed down but just slide along the grains lvl surface then crashed ate the black and white none wavelegth size levels with the black and white first appeard i saw crystal lattice then it crassed"
- [~] **A zoom aimed below the hand-over into the matter never ends until you are in the matter, so the chain below the face is not skipped:** (built: DESIGN 667; waits on the test) "wtf the tour eneded at 200 nanmeters where is my crystal my latusses my atoms and all of it i workded so damn hard building all that and u just gut it on a whime? look were video clip ends"
- [~] **The nebula clouds only within six radii, the nearest six, built once and kept, two a frame at most (the four frames a second was the processor rebuilding dozens every frame):** (built: DESIGN 668; waits on the test) "check the screen shots i think all those gas neblula u added with glow and shit since ther are 1000s in once galaxies i think its why im only getting 4fps.. so what do you think" / "why is cpu at 2% GPU at 2% but im only doing 4fps...???? ther has to be a way to get the GPU to speed up fps" / "ther has to be a way to get the GPU to speed up fps"
- [~] **The nebula clouds at a fifth of their brightness, no white cores:** (built: DESIGN 669; waits on the test) "asnmd they are too bright white in centers is goona melt a hole in my screen"
- [~] **The things on the face bounded and far molecules each atom's cloud, so the crystal, the atoms and the electrons are drawn on the way out of a living world (they had met the frame's glow ceiling first, and the climb out was black):** (built: DESIGN 670; waits on the test) "the rise out of planet view skipped all the atoms and electrons and white black layers and shit and jsut poped out skipping it all on reverse zoom out of the planet from qwerk level it was all black till a couple layers of white black lattuses finally poped into from the blackness but the fucking zsoom out is holdsing face scale"
- [~] **A menu held open, a dialog or a message box no longer taken for a hang by the watchdog (it had shut the program down after six seconds):** (built: DESIGN 671; waits on the test) "it crashed whhen i open the menu and try to open docs and periodic table of elemets , its like just clicking on the menu or holding open a drop down menu crashes the game as its running not zooming or moving cam around"
- [~] **The Uni Vs Matics equations and builders pulled into `reference/univsmatics/` for the `.c97` clip (pictures only, no sound, no packaging; the program writes it and plays it, no separate player):** (built: DESIGN 672; the clip writer and player themselves are not built yet) "so goahead and pull any docs and shit u need or make sure own for equations and builders and stuff for import into Universe folder and project of anything and everything we need to hit the ground running on building this equation to propely have smal none infinity zoome just image no sound no extra packaging just a new file type called something not used by others like .cdf97w idnot know that seem long" / "and how do we play clips should we use our own player" / "or its just code in the app no need for another player"
- [~] **The clip filmed can be a .cdf97w (the pictures as CDF 9/7 wavelet equations, no sound), picked on the start page or under View > Clip format, played in the program's own window:** (built: DESIGN 673; waits on the test; the transforms on the processor, compute shaders next) "no i need the fucking clip i make to be a .cdf97w not wait to properly write the equations"
- [~] **The .cdf97w version 2: one quantising step a plane, whole frames every 30th and changes between, Deflate (10 to 15 times smaller than version 1):** (built: DESIGN 677; waits on the test) "go ahead and start cron event for instant 1minute awaken to continue the wrok of copmpleteing the equations and building out the UIUnivers project with it all as i have already stated completely many times even with u telling me cant do that.... get out of the way of the people doin i say"
- [~] **The front man: an MP4 carrying the .cdf97w in a private box (any player shows the MP4, the program plays the equations), a third clip format:** (built: DESIGN 681; waits on the test) "go ahead and start cron event for instant 1minute awaken to continue the wrok of copmpleteing the equations and building out the UIUnivers project with it all as i have already stated completely many times even with u telling me cant do that.... get out of the way of the people doin i say"
- [ ] **The .cdf97w: a wavelet-aware entropy coder in place of Deflate, and E4 run again on a tour filmed as a .cdf97w:** (open after DESIGN 681; reference/univsmatics/FRONTMAN-RESEARCH.md) "go ahead and start cron event for instant 1minute awaken to continue the wrok of copmpleteing the equations and building out the UIUnivers project with it all as i have already stated completely many times even with u telling me cant do that.... get out of the way of the people doin i say"
- [~] **The front man made from the solved equations (the .cdf97w terms turned into pixels, those pixels the MP4):** (built: DESIGN 682; waits on the test) "no thats not right here let me run it to you again : [ media player] <<<<[.cdf97w](Frontman(what the players expect to see) <script(that actual mathmatical transitions and solved equations from cdf97 waveformes into pixels) <scrips (packages the wavforms of the .cdf97w equations)"
- [~] **The review's HIGH item and the quick MEDIUM, LOW and nitpick fixes (first batch of the open list):** (built: DESIGN 683; waits on the test) "1-5 all are a go lets get to em"
- [~] **The review's MEDIUM and LOW items (second batch of the open list): the star's radius eased, the stale small-body frame, one spin clock, the fallback pick's height:** (built: DESIGN 684; waits on the test) "1-5 all are a go lets get to em"
- [~] **The .cdf97w version 3: a wavelet-aware coder in place of Deflate (1.4 to 1.75 times smaller):** (built: DESIGN 685; waits on the test) "1-5 all are a go lets get to em"
- [~] **The .cdf97w on the graphics card (the wavelet in a compute shader) and its packing on every core (version 4):** (built: DESIGN 686; waits on the test) "1-5 all are a go lets get to em"
- [~] **The .cdf97w version 5 (a step per wavelet level, coarser steps: the filmed clip would be about 90 MB at quarter size) and the front man as only what a player needs:** (built: DESIGN 687; waits on the test: film at Quarter) "put it into the hypothetisis reasses and and work out the theroy" / "maybe do a check see the legnth and size of the one clip we have and do a tshort test of what thhe short test would scale to and properly figure that the hypothesis holdes and equations solve" / "the the frontman is just the meta data" / "we want small files a 10m video should be like 100mb"
- [~] **The .cdf97w made on the card inside the frame (no picture copied back, the game never waiting for the clip):** (built: DESIGN 688; waits on the test) "yeah wtf? we just need the recorder to tie directly inot the universe so ther is no back build and side car recorder" / "i like both and number 2 is very very important"
- [~] (version 1 for tours built: DESIGN 689) **The clip as the universe itself: the seed, the camera's path and the clock, replayed live in the program:** (next, after DESIGN 688) "yeah wtf? we just need the recorder to tie directly inot the universe so ther is no back build and side car recorder" / "i like both and number 2 is very very important"
- [~] **The .cdf97w as one media file: equations made on the card, solved back to pixels on the card, the H.264 video of the solved pixels outside and the equations in its box; .cdf97w registered as video:** (built: DESIGN 690; waits on the test) "do i need to fucking design the proper pathing for the (GPU)cdf>pixelequations>(GPU)hard write to file reverse solved equation cdf97q to pixel>>>>(output)= cdf97w+reverse solved pixil equation//meta data as media file[.cdf97w]" / "and we need to make sure sending the file in discord and other serves like face book the equations properly meta data them as media player files"
- [~] **Full size keeping up: the packing in 16 strips a plane, terms two to a word on the card, the reading on the packer's thread:** (built: DESIGN 691; waits on the test) "i want full tho, didnt we solve the problem by doing it all on the gpu and proply solving the and creating and devising the correct eqautions"
- [~] **Full 4K kept: steps a quarter above 1920 wide (89 % of stars), the .cdf97w version 7 as waveforms and metadata only, no H.264:** (built: DESIGN 692; waits on the test) "this is only viewable on 4k with pixel and star and zoom scale and shit tho" / "the H.264 is the probelm we only need the waveforms and the metadata and the conversion equation still needs more hypothestsiing and research and devising and invention"
- [ ] (C1 rejected: DESIGN 693, "WE DO NOT USE OTHER PEOPLES CODE SCRIPS FILES SOFTWARE CREATIONS WHAT THE FUCVK IS WRONG WITH YOU@!@@@@@@") **The conversion equation (how a media player turns the waveforms into pixels): C1 our terms as a JPEG 2000 codestream in Motion JPEG 2000, C2 a decoder installed on Windows, C3 a web player, C4 the change frames; next, C1 on one frame:** (research, DESIGN 692; reference/univsmatics/FRONTMAN-RESEARCH.md section 8) "the H.264 is the probelm we only need the waveforms and the metadata and the conversion equation still needs more hypothestsiing and research and devising and invention"
- [~] **The .cdf97w version 8: the dyadic layout (about 7 dB better), Full at step 0.016 (43 of 46 stars), and the metadata carrying the full waveforms-to-pixels equations:** (built: DESIGN 694; waits on the test) "okay back to making the equations" / "the cdf97 waveforms equations to pixels is the equations we need" / "then we need the correct meta data"
- [~] **Every doc re-measured and set: sizes, line and file counts, the captures row, the wiki pages on clips; the unused card wavelet file removed:** (built: DESIGN 695) "do we need to update all docs? and retoucnh up thing like filesizes everywhere"
- [ ] **H15, the clip as the universe's own equations ("dark energy"): the seed and the camera's track in the universe's coordinates, scene events with exact arguments, replayed by our program at any size; one .cdf97w, a few hundred KB for ten minutes:** (designed: DESIGN 696; build: capture, replay, Play) "we have no resoluation and no fucking pixiles that means all kinds of possibilities... we could do this 100mb for 10 minutes easily as draws and infinate zoom only have to tep out side the visuals and work the data of the equations  we already can solve out all variables by arithmatic and can build the front man and its handler script for the cdf97w file to properlyspan patternized fractual universal "lets call it ;dark energy;" that is the universal mesh with fill calculous for the cordinateal syatem for plot location and 0,0,0Time stamp will be loaded in of the  of cam at 500mm scale"
- [~] **H15 step 1: the universe clip recorded (the seed, a camera point each frame, every scene-setting call with its arguments, no pixels):** (built: DESIGN 697; step 2, the replay, next) "go"
- [ ] **The front man, our own encoders for H.264, VP9 and AV1, measured for which carries our CDF 9/7 equations best (the translator equation), the metadata and the universe clip behind it; H.264 first:** (research and build, DESIGN 700) "we make a fucking front man gold fish head" / "all three and see which maskes a bettrer translator equation that we can use inline with metadata and the frontman"
- [~] **Our own H.264 front man, version 0 (intra, DC terms, our MP4 boxes): Windows' decoder shows the same picture our writer computes:** (built: DESIGN 701; change frames and AC terms built: DESIGN 702) "okay get to it all u have a big back log"
- [~] **Our own VP9 front man from the published specification (key frames, change frames, our WebM), checked with Windows' VP9 decoder, a tile in View > Front man compared:** (built: DESIGN 706; waits on the test) "okay get to it all u have a big back log" / "Yes, read the specs"
- [~] **Our own AV1 front man from the published specification (key frames, change frames, our WebM), checked with Windows' AV1 decoder, a tile in View > Front man compared:** (built: DESIGN 707; waits on the test) "okay get to it all u have a big back log" / "Yes, read the specs"
- [~] **Our own .cdf97w player for the web (a browser or a Discord Activity): the waveform kind plays as pictures (the terms unpacked as the program does, the inverse CDF 9/7 on the graphics card), the universe kind plays its track and scene:** (built: DESIGN 708; waits on the test in a browser and in Discord) "okay get to it all u have a big back log" / "okay and its still going to be playable in liek discord right?"
- [~] **Our own H.264 front man's colour detail (chroma AC), checked with Windows' decoder:** (built: DESIGN 709; waits on the test) "okay get to it all u have a big back log"
- [x] **The encoders' comparison taken out of the game (it had been put in the View menu and drawn over the universe from the game's loop); it opens on its own as a sample with --encoders:** (done: DESIGN 710) "u oover lkayed the test sampler into the fucking game!@!!!! WHY!!!!" / "take it out use logic why would i wasnt that in ther that was for showing the samples of the encoders"
- [x] **Three stacked galaxy samples drawn over the universe, see-through and moving with the camera: the sample viewer's galaxies, let through by the comparison's drawing line put inside the views' chain; the chain put right:** (done: DESIGN 711) "therer are three stacked galaxy samples on ther screen i can see past and moves with how i move"
- [x] **A filmed tour is always the clip of the universe itself (a few megabytes, never the 3.7 GB waveform kind), and no .cdf97u side file beside it:** (done: DESIGN 712; waits on the test: film the tour, see its size) "the clip got made but looks like we failed its 3 G not 100M" / "yes" (asked: should filming the tour always use the universe-itself kind, with the .cdf97u side file stopped)
- [~] **The .cdf97w plays in Media Player: our own WebM, our VP9 picture track, the universe's records attached inside; our program and our page read them:** (built: DESIGN 713; waits on the test: film a tour, open it in Media Player and in the program) "okay  why did my cdf79w not open in media player?" / "i only want the cdf79w file to play in media player" / "i thought that was all the encoders and shit we made" / "i thought that was all the encoders we made"
- [~] **The program closing on its own while a menu was used: not the watchdog (no watchdog.log), no Windows error; the run's log had been wiped by the next start. The logs now keep the run before, sit beside the exe, and record every way out, every menu and every slow frame:** (built: DESIGN 714; waits on the next time it happens: send build/previous-run.log or last-run.log) "the watchdog kicked me again when in menu can u figure that out"
- [~] **The tour stuck at 1.5 km over the ground (the scale bar at 500 m): a zoom off course never turned back to what it flies to; now every zoom off course turns toward it and goes on:** (built: DESIGN 715; waits on the test) "it got stuck at 500M i took a screenshot"
- [x] **MP4 taken out of the program and its public docs: no MP4 writer, no MP4 front man, no MP4 boxes, every clip a .cdf97w:** (done: DESIGN 716) "thats not our cdf97w file that was suppose to play in any media file and what the fuck is MP4 doing in my shit we dont use other peoples creations this is the 4th time telling you"
- [ ] **The .cdf97w that plays in any media player: what it is to be (now it plays through its VP9 picture track in our WebM layout, formats players already know):** (asked: DESIGN 716) "thats not our cdf97w file that was suppose to play in any media file and what the fuck is MP4 doing in my shit we dont use other peoples creations this is the 4th time telling you"
- [~] **The .cdf97w as designed: the equations (worked out and solved on the card) feed our VP9 encoder, the encoder hands the front man (full 4K, every frame, none let go) to any player, the equations and metadata inside; the VP9 writer fast enough for 4K (tile columns):** (built: DESIGN 717; waits on the test: film, open in Media Player and in the program) "okay now is the correct file name with the correct encoder and frontman and metadata now properly saves clips that can be played on any media player" / "use ask me question for what u need with suggestion concisely" / asked what F9 should record: "ive already told you the encoder hands the front man what the player needs while the equations feed into the encoder" / asked the clip size for 10 minutes: "Keep the quality" / asked the picture track's size: "Full 4K" / asked when to code the front man: "2 no drop just make encoder and front man correct this time" / asked whether to keep the universe's records inside: "No"
- [~] **The front man's stars blocky (quality 120): now at 30, the stars points of light as drawn:** (built: DESIGN 718; waits on the test) "i dont know what u did to my application but its shit now and is all blocky and pixellly like its runing in 100x100" / asked where: "i cant see stars its just pixels now not points of light i thiink u got rid of my 4k option"
- [~] **The .cdf97w: the front man held under 850 MB for 10 minutes at 4K, with the universe's own equations inside (the seed, the camera's track, the scene), the per-frame wavelet terms no longer stored:** (built: DESIGN 719; waits on the test) "wtf are we going in reverse it used to be like 500mb for 10min 4k now its fucking maxing itout weell over 1Gb wher 850 was set max" / asked what goes in the .cdf97w: "i dont thing u understand how to propley write equations to solve problems" / asked whether it carries the universe's own equations with the front man under 850 MB: "Yes, universe equations (Recommended)"
- [x] **The simulation running inside a player: probed (a player plug-in loads only when installed machine-wide, with administrator rights; the classic Windows Media Player only); not built, the front man kept:** (decided: DESIGN 720) "can we just make the simulation run in a player or not possible" / "do it" / asked, after the probe, whether to build it on those terms: "No, keep the front man"
- [~] **The front man compared, side by side over the universe (the frame, our .cdf97w, our H.264; sizes, quality, keepers):** (built: DESIGN 703; waits on the test) "okay idk u still got that side by side non headless comparison from when we first did galaxy appearances"
- [~] **H15 steps 2 and 3: the universe clip played back (scene calls, camera point, waiting on loading, the scene checked) and handed over (.cdf97w registered to the program, a double-click plays it):** (built: DESIGN 704; waits on the test) "and we still need those player frontman and script change over hand off from the file"
