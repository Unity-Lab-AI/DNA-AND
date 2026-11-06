---
title: R4.4 · Live generators — every asset built on the fly
status: partial
sources: [docs/DESIGN.md#round-13-live-generators-verbatim, docs/ROADMAP.md, docs/TODO.md]
last-verified: 62c3cf6
---

# 🏭 R4.4 · Live generators

> "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living"

**Nothing in DNA-AND is pre-built.** No models, textures, sprites or sounds ship with the game. The game's own program builds every asset **live, on the fly**, from data arrays, while life evolves. A new species gets its body the moment it evolves. A new city gets its buildings the moment it's founded.

---

## 🧱 How every generator works

| | |
|--|--|
| 📥 **Input** | Arrays of data: the genome, the species' body plan, its society and age, its tech branches, the planet's chemistry, plus a seed |
| ⚙️ **Build** | The generator turns that data into shapes, surfaces, colors, motion and sound, right then, in the game |
| 💾 **Cache** | What's built is kept in memory and rebuilt only when its data changes (a mutation, a new age, a new tech) |
| 🔁 **Live** | When life changes, its assets change with it. Evolve wings and the creature's model grows wings |
| 🎨 **Style** | Every generator draws in the one art style chosen from the samples (R4.5) |
| 🪶 **Light** | Built to stay cheap with thousands of living things on screen |

---

## 🗂️ The generators

| Generator | Builds | From |
|---|---|---|
| 🦠 **Microbe & virus** | Cells, viruses, colonies, mats | Genome, chemistry |
| 🌿 **Plant & fungus** | Plants, trees, fungi, forests | Genome, climate, soil |
| 🐺 **Creature** | Every animal and insect: body, limbs, skin, eyes, how it moves | Genome, gene map, body plan |
| 🧠 **Intelligent creature** | Thinking species, their clothing, tools and gear | Genome, body plan, age, society |
| 🏘️ **City** | Camps, villages, towns, cities, megacities, buildings, roads, farms | Species body plan, society, age, tech branches, terrain |
| 🛠️ **Tech & machine** | Tools, weapons, vehicles, factories, machines | Species, tech branches, age, materials |
| 🚀 **Space tech** | Spaceships, space stations, colonies, Dyson swarms and spheres, world grinders | Species, tech branches, age, star and system |
| 🪐 **Planet & moon** | Terrain, oceans, clouds, ice, rings | Template, chemistry, plates, climate |
| ⭐ **Star** | Stars of every type, flares, death | Star type, age, mass |
| 🌌 **Galaxy & system** | Galaxy layouts, solar system layouts, nebulas, black holes | Seed, galaxy size |
| ⛏️ **Resource field** | Ore veins, asteroid belts, gas clouds, oil and mineral fields | Elements, geology |
| ⚗️ **Element** | How each of the 118 elements looks in the world | Element data |
| 🌦️ **Weather & effect** | Storms, fire, lava, explosions, souls rising | Climate, powers, events |
| ⚫ **Black hole & active core** | Black holes, accretion disks, jets, quasars, blazars | Mass, spin, feeding, view angle |
| 🌫️ **Nebula** | Emission, reflection, dark and planetary nebulas, pillars, supernova remnants | Stars nearby, gas, age |
| ☄️ **Small body** | Asteroids, comets and their tails, dwarf planets, belts and clouds, meteorites | Makeup, distance from the star |
| 🦴 **Fossil & ruin** | Fossils of extinct life, ruins of fallen civilizations | Extinct lines, collapsed cities |
| 🔊 **Sound** | Calls, footsteps, machines, weather | Species body plan, materials, events |

> 🐜 **Built in their own image.** The city, tech and space-tech generators read the **builder's** body plan and genes. Insect species get insect cities and insect spaceships, and wolf-folk get wolf-folk stations.

---

## 🧪 First job: the samples

The style samples (R4.5) are the generators' first output, generated live in the sample viewer. The style is locked to Populous-like, so the sets that remain show that style only. Built so far: galaxies (`GalaxyGen`), solar systems (`SpaceGen`), stars (`StarGen`), planets and giants (`PlanetTerrain`, rings from `PlanetGen`).

**Living and built things** use one body builder, in the smooth look:
- `src/gen/BodyGen.cs`: puts bodies together from blobs, boxes and cones. It can draw them smooth, chunky or blocky; smooth is the one used.
- `src/gen/GeneBody.cs`: builds a body from a genome. The stage of life decides the kind of thing it grows into (virus, cell, plant, fungus, jelly or animal), and the body-plan genes decide its shape: segments, legs, neck, tail, fins, wings, head, eyes, armour, poison, jaws, standing upright and branching. Intelligent creatures carry a tool and wear cloth. Skin colour comes from the colour genes, strengthened and kept bright enough to read, so families look apart and none come out black.
- `src/gen/CivGen.cs`: builds everything an intelligent species makes, from its own genes: a tribe camp, village, town, city, spaceship, hyperdrive ship, space station and colony. Their skin colour runs through all of it: ship hulls carry bands of it, and stations and colonies are tinted with it. The builders' body plan sets their way of building:
  - insect-like: mounds and hive spires
  - armoured: spiked forts
  - at home in water: stilts
  - round: domes
  - the rest: walls with pointed roofs
- Samples: `--style` (the three looks), `--family` (families of seven kinds of life; keys 1 to 7, E evolves, N new) and `--civ` (one species and all it builds; E evolves, N new).

## 🔗 Depends on
[R3 · Engine core](r03-engine-core.md) (arrays, seeded random, threads) · [R4 · Renderer](r04-renderer.md) (Direct3D 12 and our shader generator) · [R7 · Genome](r07-genome.md) (the data creatures are built from)

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/black-holes.dnat`: 35 entries (Kinds of black hole, Parts, Active galaxy cores (a feeding supermassive black hole), Events, Real examples) and 7 rules
- `content/cities.dnat`: 50 entries (Settlement sizes, Layouts, Districts, Infrastructure, in rough order of invention, Building materials by age) and 8 rules
- `content/elements.dnat`: 118 entries (Main) and 5 rules
- `content/galaxies.dnat`: 47 entries (Galaxy types (real), Extra galaxy types (from research), Real examples, Parts a galaxy is built from, Structures between galaxies) and 11 rules
- `content/gas-giants.dnat`: 30 entries (Kinds, Cloud classes (Sudarsky, by temperature), Layers, Features, Solar System examples) and 7 rules
- `content/life/animals.dnat`: 84 entries (Body-plan traits, Phyla, Arthropod classes, Insect orders (seeds), Fish classes, Land vertebrate classes, Bird orders (seeds), Mammal orders (seeds)) and 5 rules
- `content/life/archaea.dnat`: 12 entries (What makes them different, Extremophiles, Main groups (phyla)) and 3 rules
- `content/life/bacteria.dnat`: 31 entries (Shapes, Cell parts, How they get energy and food, Main groups (phyla), Group life, Swapping genes) and 5 rules
- `content/life/extinct.dnat`: 47 entries (Mass extinctions (the Big Five, and after), The first strange animals, Early fish and the walk onto land, Dinosaurs, Other Mesozoic giants, After the dinosaurs: the age of mammals, Extinct plants) and 5 rules
- `content/life/fungi.dnat`: 16 entries (Phyla, Body parts, Ways of living) and 5 rules
- `content/life/intelligent.dnat`: 29 entries (Real thinking animals (seeds), Signs of a mind, What pushes a line toward intelligence) and 5 rules
- `content/life/plants.dnat`: 36 entries (Groups (phyla and lines), Big splits, Organs and tissues, How they make food, How they breed) and 6 rules
- `content/life/protists.dnat`: 25 entries (Supergroups, Protist groups (seeds), How they move) and 4 rules
- `content/life/viruses.dnat`: 22 entries (Baltimore groups (how a virus's genes get read), Shapes and parts, Who they infect, Life cycles) and 6 rules
- `content/moons.dnat`: 18 entries (By how they formed, By surface and inside, By orbit) and 6 rules
- `content/nebulas.dnat`: 12 entries (The five main kinds, Star-forming structures) and 6 rules
- `content/physics.dnat`: 49 entries (The four forces of nature, Particles and atoms, Inside a planet, What moves inside a planet, Atmospheres) and 8 rules
- `content/planets.dnat`: 27 entries (By size, By makeup, By heat and orbit, Earth-family examples) and 6 rules
- `content/resource-fields.dnat`: 26 entries (Made by magma, Made by hot fluids, Made by water and time, Made by weather, Fields in space) and 6 rules
- `content/small-bodies.dnat`: 39 entries (Asteroids by makeup, Asteroids by orbit, Comets, Dwarf planets and far bodies, Meteors and meteorites) and 7 rules
- `content/solar-systems.dnat`: 31 entries (Kinds of systems (real), Real examples, Zones a system is built from) and 12 rules
- `content/space-tech.dnat`: 44 entries (How big a civilization's power is (Kardashev scale), Megastructures, Space habitats and stations, Ships by job, Drives) and 7 rules
- `content/stars.dnat`: 34 entries (Main-sequence stars (the seven classic types), Giants and supergiants, Young and failed stars, Dead and strange stars, Luminosity classes (how bright for its type), Extra star types) and 5 rules
