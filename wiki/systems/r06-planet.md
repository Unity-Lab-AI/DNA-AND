---
title: R6 · Planet
status: partial
sources: [content/geology.dnat, content/climate.dnat, docs/DESIGN.md (rounds 1, 2, 4), docs/LISTS.md, docs/ROADMAP.md#R6]
last-verified: fdaea69
---

# 🌍 R6 · Planet

> "EARTH(but other templets too you can unlock after reaching certain points on Earth." · "everything(when i say everything i fucking mean everything) that a wrold has on it like earth"

## 🧱 Layers
| Layer | Simulates |
|---|---|
| 🪨 Ground | Plates, rock, minerals, soil, volcanoes, quakes |
| 🌊 Water | Oceans, currents, rivers, lakes, ice, salt |
| 🌬️ Air | Gases, oxygen, greenhouse gases, ozone |
| ⛅ Weather | Temperature, rain, wind, storms, seasons, day and night |
| ⚗️ Chemistry | **All 118 elements** and the compounds life uses |

> "add elements ect ect real life so full perioditic table of elements"

## 📋 What "everything" means
The planet draws on the top-100 lists in [docs/LISTS.md](../../docs/LISTS.md "100 categories × 100 entries"): environments, ocean zones, rocks, minerals, weather, disasters and more. Entries are "real and dynamic invented combo procedurally".

## 🗺️ Templates
Earth comes first. Other templates (list 62, "Planet templates to unlock after Earth") unlock as you reach points on Earth. On other templates, life can evolve in a different order from Earth's.

## 🧮 Grid
✅ The planet's surface is split into equal-size cells on a sphere. Each cell stores its ground, water, air, weather and chemistry.

## 📌 Decided specs (build from these)

- 100 planet templates, listed in `docs/wiki/planets-and-templates.md`. Earth is first, then 2–8 unlock first. Life on a template never exists before the player's first action.
- All 118 elements (`docs/wiki/elements.md`). Lab-made 93+ unlock once a civilization creates them.
- Interiors, convection, tectonics or stagnant lid, volcanism, geodynamo, magnetosphere, auroras, atmosphere layers, greenhouse effect and air escape, from `content/physics.dnat`.

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/planets.dnat`: 27 entries (By size, By makeup, By heat and orbit, Earth-family examples) and 6 rules
- `content/gas-giants.dnat`: 30 entries (Kinds, Cloud classes (Sudarsky, by temperature), Layers, Features, Solar System examples) and 7 rules
- `content/moons.dnat`: 18 entries (By how they formed, By surface and inside, By orbit) and 6 rules
- `content/physics.dnat`: 49 entries (The four forces of nature, Particles and atoms, Inside a planet, What moves inside a planet, Atmospheres) and 8 rules
- `content/resource-fields.dnat`: 26 entries (Made by magma, Made by hot fluids, Made by water and time, Made by weather, Fields in space) and 6 rules
- `content/elements.dnat`: 118 entries (Main) and 5 rules

## 🔨 Built so far

- **Ground** (`src/gen/PlanetTerrain.cs`): a cube-sphere whose pieces split down to about a micrometre. Height and colour come from our own noise.
- **Sky and water:** an air glow, turning clouds, weather near the ground, see-through water with a sea floor, and light fading with depth.
- **Resource fields:** volcanoes, rivers and 16 kinds of field placed by geology, plus the resource view.
- **Air:** each world kind's air make-up is in content (`air:`).
- **The fly-down** (`src/shell/PlanetView.cs`):
  - from orbit to the ground, into the ground (solid colour darkening with depth), and under the sea
  - sea life at true scale (`src/sim/SeaLife.cs`)
  - on land, the view fades to black before the microbes

## 🌋 The living planet: what runs today and what does not

| Process | In the code now | To build (TODO R6b) |
|---|---|---|
| Inside (core, mantle, crust, heat) | `src/sim/PlanetCore.cs`: heat that runs out (Earth today = 1, bigger planets hold it longer), magma ocean when young, molten outer core and growing solid inner core, plates or a stagnant lid; shown in the title, saved as the `CORE` chunk | Layers from size and makeup; heat that cools over billions of years |
| Plates | `src/sim/PlanetPlates.cs`: 8 to 15 plates turning at 1 to 10 cm a year; every edge spreading, crashing or sliding (the ground does not move yet) | Moving plates, drift, ridges, trenches, faults, supercontinents |
| Earthquakes | Along edges, real magnitude counts (one magnitude 8 a year on Earth), tsunamis on oceanic crashing edges, magnitude 9 harms nearby life | Quakes along edges and faults, tsunamis |
| Volcanoes | Placed once on the ground; erupting over time on edges and hot spots (VEI 7 per 1,000 years, VEI 8 per 50,000), killing nearby life; super-eruptions cool the world | Eruptions over time, ash, new land, island chains |
| Magnetic field | Geodynamo from a liquid outer core and spin; random flips about every 450,000 years; the shield sets how much radiation reaches dry land (harming land life) | Geodynamo, magnetosphere, air stripping, auroras, flips |
| Air | `src/sim/PlanetAir.cs`: pressure, carbon dioxide, oxygen, methane and temperature on the planet's clock; volcanoes in, rain on rock out (settling over 400,000 years); oxygen from plant-like life after the seas rust (the great oxidation, near 21%); methane from early microbes; air stripped once the field dies; runaway greenhouse and snowball; the world's warmth and oxygen feed back into life. On the test planet: methane-rich at 1 billion years, 21% oxygen by 2.5 billion, 12 °C at 5.6 billion | Layers, greenhouse, escape, gases from volcanoes, carbon-silicate cycle |
| Water and weather | `src/sim/PlanetWeather.cs`: per region temperature, rain belts and ice; ice ages from orbit wobbles, seas up to 130 m lower; hurricanes over warm seas; seasons at slow speeds; ocean currents that even out sea and coast temperatures; upwelling and tides that bring nutrients to coastal seas; all fed into life. Seas on the ground and rain pictures are still fixed | Water cycle, winds, storms, currents, tides, ice ages, sea level |
| Surface | The ground you see is still a fixed height map. `src/sim/PlanetSurface.cs` runs per region: mountains pushed up at crashing edges and worn down by rain and ice, soil from worn rock (faster under plants), river mud carried to the seas, sediment buried into stone; soil and mud feed life | Erosion, the rock cycle, soil, landslides |
| From outside | `src/sim/PlanetImpacts.cs`: the 10 impact sizes at real rates (a house-size rock every 60 years to a 10 km one every 100 million), many times more in the young heavy bombardment, harm to life by size, dust that darkens and cools; deep microbes always survive. The star brightens with age (about 30% dimmer when young), warming the world | Impacts, star flares, the star ageing |

**The ground you see** now follows the sim: `PlanetTerrain` takes a sea drop, a mountain scale per region and a crater list, and every ground piece made before a change is remade a few at a time (a `Generation` number). Continents do not drift yet.
