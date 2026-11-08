# 🗂️ DNA-AND Content Format

> "well i thought we needed to write the games content before we could have something to generate in the game engine"

All game content is written by us, in **our own text format**, inside `content/`. Later, our own toolchain compiles every content file into **our own binary format**, and the game reads only the binary (`docs/DESIGN.md`, round 15). No outside format, parser or tool is used.

---

## 🎯 What content is

Content is **what the live generators work from**. It is never a finished asset (LAW 14, no pre-built assets).

| Kind | What it holds | Example |
|---|---|---|
| 🌱 **Seeds** | Real-world entries per category, as many as it takes to teach its generator (round 14) | A real star type: an O-type blue star |
| 📏 **Ranges** | The limits a generator may vary within | Surface temperature 30,000–50,000 K |
| 🧩 **Parts** | Building blocks a generator combines | Leg, wing, eye, dome, spire, hull |
| 📐 **Rules** | How the generator puts parts together | Insect species build with segments and hex cells |
| 🎨 **Style hooks** | Values for the game's one art style | Populous-like (locked) |

---

## ✍️ How a file looks

```
# DNA-AND content · stars
# Lines starting with # are comments (inside an entry too: a comment never ends it; a blank line does).

@star o-type
name: O-type blue star
real: yes
color: blue
temp-k: 30000..50000
mass-sol: 16..90
life-years: 1e6..1e7
rarity: 1 in 3000000
death: supernova
tags: hot, bright, short-lived

@star g-type
name: G-type yellow star
real: yes
example: our own star
...
```

| Piece | Means |
|---|---|
| `@kind id` | Starts an entry. `kind` is the category, and `id` is a unique short name |
| `key: value` | One property |
| `a..b` | A range the generator picks from |
| `a, b, c` | A list |
| `1e6` | 1,000,000 (shorthand for big numbers) |
| `real: yes` | A real-world seed. `real: rule` marks a generator rule. `real: hypothetical` marks something science predicts but hasn't confirmed. `real: game` marks something DNA-AND invents (hyperdrives, world grinders) |
| `# researched:` | Under the title line: the sources the topic was researched from (LAW 16) |
| `# …` | A comment, ignored |

**Rules:**
- One entry per `@` block. A blank line ends the block.
- Keys are lowercase words joined with `-`.
- Every file starts with a `# DNA-AND content · <subject>` line, followed by `# researched:` and its sources.
- **Research first** (LAW 16): every topic is researched before its file is written, so no kind, part, event or example is left out.
- Units go in the key name (`temp-k`, `mass-sol`, `radius-km`).
- Facts marked `real: yes` must be real. If a fact isn't certain, it's left out (LAW 7).
- Our own star is "our own star", never its name: the word is not used in these projects. Every other star is a star (LAW 9). Masses, radii and brightness are in solar units (solar masses, solar radii, solar luminosities), the correct terminology.

---

## 🗃️ Files

One file per subject, in `content/`, named `<subject>.dnat` (**DNA**-AND **t**ext). In the Universe project the same files are `.unt`, renamed by `universe-sync.cmd`. In the Universe project the same files are `.unt`, renamed by `universe-sync.cmd`.

Life follows the full tree, one folder per kingdom and one file per phylum or class:

```
content/life/viruses/   content/life/bacteria/   content/life/archaea/
content/life/protists/  content/life/fungi/      content/life/plants/
content/life/animals/   (e.g. animals/arthropods-insects.dnat, animals/chordates-mammals.dnat)
```

Each entry names its full path: `domain`, `kingdom`, `phylum`, `class`, `order`, `family`, `genus`.

The sample subjects come first (round 15): planets, stars, gas giants, Dyson spheres, space stations, space colonies, spaceships, galaxy layouts, solar system layouts, resource fields, cities, intelligent creatures, every kingdom of life (animals, plants, fungi, protists, archaea, bacteria, viruses), and the elements.

---

### Current files

- `content/black-holes.dnat`
- `content/bodies.dnat` (`@place` entries: every kind of body made round you, black holes, neutron stars, rogue planets, asteroids, Kuiper bodies and comets, with their shares and where they live; a `size-km` range with `size-power` gives each body its own size, `round-above-km` the size gravity rounds a body at, below which they are lumpy, the smallest the most)
- `content/cities.dnat`
- `content/climate.dnat`
- `content/elements.dnat` (each `@element` also carries `colour`, `covalent-a`, `atomic-a`, `made-in` (where its atoms were forged); the unstable ones `half-life` and `decays`)
- `content/galaxies.dnat`
- `content/gas-giants.dnat`
- `content/minerals.dnat` (`@mineral` with `lustre`, `clarity` and `trace` lines, `@mix` with `vary` (each body's own shares of the kind's minerals, scattered by its seed), `@surface`)
- `content/life/animals.dnat`
- `content/life/archaea.dnat`
- `content/life/bacteria.dnat`
- `content/life/extinct.dnat`
- `content/life/fungi.dnat`
- `content/life/intelligent.dnat`
- `content/life/plants.dnat`
- `content/life/protists.dnat`
- `content/life/viruses.dnat`
- `content/geology.dnat`
- `content/moons.dnat`
- `content/nebulas.dnat`
- `content/physics.dnat` (`@law` entries, among them `gravity` with `g-m3-kg-s2`, `frame-dragging` with `spin-range`, `gravity-off` with `swell-over-freefall`, `hole-inside` with `way-in-ly`; `@law supernova` and the `@supernova` kinds)
- `content/planets.dnat`
- `content/resource-fields.dnat`
- `content/small-bodies.dnat` (each kind with `mix`, `makeup`, `density`, `albedo`; asteroids with `belt-share`)
- `content/terrain.dnat` (`@terrain`: each kind of world's size, `density-g-cc` (its mass follows), ground, water, air, clouds, rock and base temperature)
- `content/tours/grand-tour.dnat` (`@stop` entries: the grand tour's camera moves and captions; `{seen}` in a caption is how much of the universe the tour reached)
- `content/universe.dnat` (`@universe`: how far you see, how many galaxies, the cosmic web, how finely galaxies and real stars are made round you)
- `content/solar-systems.dnat`
- `content/space-tech.dnat`
- `content/stars.dnat` (each `@star` with `makeup`, by count of atoms, and `makeup-note`; the `@layer` entries, a star's layers from its surface to its core, each with `from-r`, `to-r`, `temp-k`, `density-g-cc`, `makeup` and `in`)

`real:` values in use: `yes`, `hypothetical`, `game` (for example hyperdrives and world grinders), and `rule`.

## ⚙️ Text to binary (later)

Our own toolchain (R1) gets a content compiler. It reads every `.dnat` file, checks it against this page, and writes the binary the game loads. Until that exists, these text files are the content.

🔗 [Design record](DESIGN.md) · [Work Laws](WORK-LAWS.md) · [Live generators](../wiki/systems/r04b-generators.md) · [Top-100 lists](LISTS.md)
