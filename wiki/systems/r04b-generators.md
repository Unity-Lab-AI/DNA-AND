---
title: R4.4 · Live generators — every asset built on the fly
status: draft
sources: [docs/DESIGN.md#round-13-live-generators-verbatim, docs/ROADMAP.md, docs/TODO.md]
last-verified: none (plan only)
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
| 🔊 **Sound** | Calls, footsteps, machines, weather | Species body plan, materials, events |

> 🐜 **Built in their own image.** The city, tech and space-tech generators read the **builder's** body plan and genes. Insect species get insect cities and insect spaceships, and wolf-folk get wolf-folk stations.

---

## 🧪 First job: the samples

The style samples (R4.5) are the generators' first output: 3 examples × 3 styles for each subject, generated live in the sample viewer.

## 🔗 Depends on
[R3 · Engine core](r03-engine-core.md) (arrays, seeded random, threads) · [R4 · Renderer](r04-renderer.md) (Direct3D 12 and our shader generator) · [R7 · Genome](r07-genome.md) (the data creatures are built from)
