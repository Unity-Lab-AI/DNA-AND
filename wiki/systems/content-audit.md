---
title: Content audit · which entries the program reads
status: verified
sources: [content/*.dnat, content/life/*.dnat, content/tours/*.dnat, src/**/*.cs]
last-verified: 1366298
---

# Content audit: which entries the program reads

Every kind of entry in every content file, and whether the program reads it (a lookup by its file and kind, a reader that walks every entry of a kind, or one that takes the kind as a name: the layers' reader, a body's `entry:` line, the world kinds' rules). Made by reading the code for each kind, 2026-11-10.

## The groups that are words only, and why

- **`@rule`, `@example`, `@part`** in every file: design rules (most built as code, not read as entries), real examples (the Milky Way, Jupiter, our own system) and named parts, for the reader.
- **`content/life/*`, `content/cities.dnat`, `content/space-tech.dnat`**: DNA-AND's own game, its life, cities and ships; the universe has no life yet.
- **`content/climate.dnat`, `content/geology.dnat`, `content/physics.dnat` (`@atmosphere`, `@atom`, `@force`, `@interior`, `@particle`, `@process`)**: the climate, the air by height, the seas, tides, currents, the core, plates and the forces are built as code (src/gen/Climate.cs, src/shell/UniverseView.Climate.cs, src/core/Physics.cs, src/sim/PlanetCore.cs); these entries describe them and are not read.
- **`content/moons.dnat` `@moon`**: a moon takes its kind from content/terrain.dnat; these eighteen kinds are words only.
- **`content/nebulas.dnat`**, **`content/small-bodies.dnat` `@dwarf`, `@far`, `@meteor`, `@meteorite`**, **`content/stars.dnat` `@class`**, **`content/black-holes.dnat` `@active`, `@event`**, **`content/galaxies.dnat` `@structure`**, **`content/gas-giants.dnat` `@cloudclass`, `@feature`**: descriptions; the things themselves are made from the kinds that are read (content/bodies.dnat places, the supernova kinds, the galaxy and star kinds).

## Every kind

| File | Kind | Entries | Read by the program |
|---|---|---|---|
| `content/black-holes.dnat` | `@active` | 9 | **no: words only** |
| `content/black-holes.dnat` | `@blackhole` | 7 | yes |
| `content/black-holes.dnat` | `@event` | 5 | **no: words only** |
| `content/black-holes.dnat` | `@example` | 4 | **no: words only** |
| `content/black-holes.dnat` | `@part` | 10 | **no: words only** |
| `content/black-holes.dnat` | `@rule` | 7 | **no: words only** |
| `content/bodies.dnat` | `@place` | 10 | yes |
| `content/cities.dnat` | `@district` | 11 | **no: words only** |
| `content/cities.dnat` | `@infra` | 13 | **no: words only** |
| `content/cities.dnat` | `@layout` | 8 | **no: words only** |
| `content/cities.dnat` | `@material` | 9 | **no: words only** |
| `content/cities.dnat` | `@rule` | 8 | **no: words only** |
| `content/cities.dnat` | `@settlement` | 9 | **no: words only** |
| `content/climate.dnat` | `@circulation` | 3 | **no: words only** |
| `content/climate.dnat` | `@cycle` | 3 | **no: words only** |
| `content/climate.dnat` | `@ice` | 2 | **no: words only** |
| `content/climate.dnat` | `@ocean` | 4 | **no: words only** |
| `content/climate.dnat` | `@rule` | 4 | **no: words only** |
| `content/climate.dnat` | `@star` | 1 | yes |
| `content/climate.dnat` | `@water` | 4 | **no: words only** |
| `content/climate.dnat` | `@weather` | 5 | **no: words only** |
| `content/climate.dnat` | `@zone` | 6 | yes |
| `content/elements.dnat` | `@element` | 118 | yes |
| `content/elements.dnat` | `@family` | 10 | yes |
| `content/elements.dnat` | `@rule` | 5 | **no: words only** |
| `content/galaxies.dnat` | `@example` | 4 | **no: words only** |
| `content/galaxies.dnat` | `@galaxy` | 27 | yes |
| `content/galaxies.dnat` | `@part` | 10 | **no: words only** |
| `content/galaxies.dnat` | `@rule` | 11 | **no: words only** |
| `content/galaxies.dnat` | `@structure` | 6 | **no: words only** |
| `content/gas-giants.dnat` | `@cloudclass` | 5 | **no: words only** |
| `content/gas-giants.dnat` | `@example` | 4 | **no: words only** |
| `content/gas-giants.dnat` | `@feature` | 6 | **no: words only** |
| `content/gas-giants.dnat` | `@giant` | 9 | yes |
| `content/gas-giants.dnat` | `@layer` | 6 | yes |
| `content/gas-giants.dnat` | `@rule` | 7 | **no: words only** |
| `content/geology.dnat` | `@balance` | 1 | **no: words only** |
| `content/geology.dnat` | `@edge` | 4 | **no: words only** |
| `content/geology.dnat` | `@erosion` | 5 | **no: words only** |
| `content/geology.dnat` | `@fault` | 3 | **no: words only** |
| `content/geology.dnat` | `@hazard` | 2 | **no: words only** |
| `content/geology.dnat` | `@quake` | 5 | **no: words only** |
| `content/geology.dnat` | `@rock` | 3 | **no: words only** |
| `content/geology.dnat` | `@rule` | 4 | **no: words only** |
| `content/geology.dnat` | `@soil` | 2 | **no: words only** |
| `content/life/animals.dnat` | `@class` | 13 | **no: words only** |
| `content/life/animals.dnat` | `@family` | 1 | yes |
| `content/life/animals.dnat` | `@order` | 41 | **no: words only** |
| `content/life/animals.dnat` | `@phylum` | 17 | **no: words only** |
| `content/life/animals.dnat` | `@rule` | 5 | **no: words only** |
| `content/life/animals.dnat` | `@trait` | 12 | **no: words only** |
| `content/life/archaea.dnat` | `@extreme` | 5 | **no: words only** |
| `content/life/archaea.dnat` | `@phylum` | 4 | **no: words only** |
| `content/life/archaea.dnat` | `@rule` | 3 | **no: words only** |
| `content/life/archaea.dnat` | `@trait` | 3 | **no: words only** |
| `content/life/bacteria.dnat` | `@behavior` | 4 | **no: words only** |
| `content/life/bacteria.dnat` | `@genes` | 3 | **no: words only** |
| `content/life/bacteria.dnat` | `@metabolism` | 4 | **no: words only** |
| `content/life/bacteria.dnat` | `@oxygen` | 3 | **no: words only** |
| `content/life/bacteria.dnat` | `@part` | 6 | **no: words only** |
| `content/life/bacteria.dnat` | `@phylum` | 6 | **no: words only** |
| `content/life/bacteria.dnat` | `@rule` | 5 | **no: words only** |
| `content/life/bacteria.dnat` | `@shape` | 5 | **no: words only** |
| `content/life/extinct.dnat` | `@dinosaur` | 9 | **no: words only** |
| `content/life/extinct.dnat` | `@extinct` | 28 | **no: words only** |
| `content/life/extinct.dnat` | `@extinction` | 6 | **no: words only** |
| `content/life/extinct.dnat` | `@extinctplant` | 4 | **no: words only** |
| `content/life/extinct.dnat` | `@rule` | 5 | **no: words only** |
| `content/life/fungi.dnat` | `@life` | 6 | **no: words only** |
| `content/life/fungi.dnat` | `@part` | 5 | **no: words only** |
| `content/life/fungi.dnat` | `@phylum` | 5 | **no: words only** |
| `content/life/fungi.dnat` | `@rule` | 5 | **no: words only** |
| `content/life/intelligent.dnat` | `@driver` | 7 | **no: words only** |
| `content/life/intelligent.dnat` | `@mind` | 12 | **no: words only** |
| `content/life/intelligent.dnat` | `@rule` | 5 | **no: words only** |
| `content/life/intelligent.dnat` | `@sign` | 10 | **no: words only** |
| `content/life/plants.dnat` | `@breed` | 5 | **no: words only** |
| `content/life/plants.dnat` | `@group` | 10 | **no: words only** |
| `content/life/plants.dnat` | `@part` | 11 | **no: words only** |
| `content/life/plants.dnat` | `@photosynthesis` | 5 | **no: words only** |
| `content/life/plants.dnat` | `@rule` | 6 | **no: words only** |
| `content/life/plants.dnat` | `@split` | 5 | **no: words only** |
| `content/life/protists.dnat` | `@move` | 4 | **no: words only** |
| `content/life/protists.dnat` | `@protist` | 16 | **no: words only** |
| `content/life/protists.dnat` | `@rule` | 4 | **no: words only** |
| `content/life/protists.dnat` | `@supergroup` | 5 | **no: words only** |
| `content/life/viruses.dnat` | `@cycle` | 3 | **no: words only** |
| `content/life/viruses.dnat` | `@host` | 5 | **no: words only** |
| `content/life/viruses.dnat` | `@part` | 3 | **no: words only** |
| `content/life/viruses.dnat` | `@rule` | 6 | **no: words only** |
| `content/life/viruses.dnat` | `@shape` | 4 | **no: words only** |
| `content/life/viruses.dnat` | `@virusgroup` | 7 | **no: words only** |
| `content/minerals.dnat` | `@mineral` | 32 | yes |
| `content/minerals.dnat` | `@mix` | 25 | yes |
| `content/minerals.dnat` | `@surface` | 9 | yes |
| `content/moons.dnat` | `@moon` | 18 | **no: words only** |
| `content/moons.dnat` | `@rule` | 6 | **no: words only** |
| `content/nebulas.dnat` | `@cloud` | 7 | **no: words only** |
| `content/nebulas.dnat` | `@nebula` | 5 | **no: words only** |
| `content/nebulas.dnat` | `@rule` | 6 | **no: words only** |
| `content/physics.dnat` | `@atmosphere` | 13 | **no: words only** |
| `content/physics.dnat` | `@atom` | 7 | **no: words only** |
| `content/physics.dnat` | `@force` | 4 | **no: words only** |
| `content/physics.dnat` | `@gas` | 17 | yes |
| `content/physics.dnat` | `@interior` | 7 | **no: words only** |
| `content/physics.dnat` | `@law` | 15 | yes |
| `content/physics.dnat` | `@particle` | 10 | **no: words only** |
| `content/physics.dnat` | `@process` | 8 | **no: words only** |
| `content/physics.dnat` | `@rule` | 8 | **no: words only** |
| `content/physics.dnat` | `@supernova` | 8 | yes |
| `content/planets.dnat` | `@example` | 4 | **no: words only** |
| `content/planets.dnat` | `@layer` | 12 | yes |
| `content/planets.dnat` | `@planet` | 23 | yes |
| `content/planets.dnat` | `@rule` | 6 | **no: words only** |
| `content/resource-fields.dnat` | `@deposit` | 16 | yes |
| `content/resource-fields.dnat` | `@field` | 11 | yes |
| `content/resource-fields.dnat` | `@rule` | 6 | **no: words only** |
| `content/small-bodies.dnat` | `@asteroid` | 12 | yes |
| `content/small-bodies.dnat` | `@comet` | 4 | yes |
| `content/small-bodies.dnat` | `@dwarf` | 5 | **no: words only** |
| `content/small-bodies.dnat` | `@far` | 5 | **no: words only** |
| `content/small-bodies.dnat` | `@layer` | 4 | yes |
| `content/small-bodies.dnat` | `@meteor` | 5 | **no: words only** |
| `content/small-bodies.dnat` | `@meteorite` | 4 | **no: words only** |
| `content/small-bodies.dnat` | `@part` | 5 | **no: words only** |
| `content/small-bodies.dnat` | `@rule` | 8 | **no: words only** |
| `content/solar-systems.dnat` | `@example` | 4 | **no: words only** |
| `content/solar-systems.dnat` | `@rule` | 12 | **no: words only** |
| `content/solar-systems.dnat` | `@system` | 16 | yes |
| `content/solar-systems.dnat` | `@zone` | 12 | yes |
| `content/space-tech.dnat` | `@build` | 9 | yes |
| `content/space-tech.dnat` | `@drive` | 12 | **no: words only** |
| `content/space-tech.dnat` | `@habitat` | 7 | **no: words only** |
| `content/space-tech.dnat` | `@mega` | 10 | yes |
| `content/space-tech.dnat` | `@rule` | 7 | **no: words only** |
| `content/space-tech.dnat` | `@scale` | 3 | **no: words only** |
| `content/space-tech.dnat` | `@ship` | 12 | **no: words only** |
| `content/stars.dnat` | `@class` | 7 | **no: words only** |
| `content/stars.dnat` | `@layer` | 26 | yes |
| `content/stars.dnat` | `@rule` | 5 | **no: words only** |
| `content/stars.dnat` | `@star` | 27 | yes |
| `content/terrain.dnat` | `@terrain` | 15 | yes |
| `content/tours/grand-tour.dnat` | `@stop` | 78 | yes |
| `content/universe.dnat` | `@universe` | 1 | yes |
