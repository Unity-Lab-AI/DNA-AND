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
| 🎨 **Style hooks** | Per-style values for the 3 sample styles | RimWorld-like, Populous-like, Stardew-like |

---

## ✍️ How a file looks

```
# DNA-AND content · stars
# Lines starting with # are comments.

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
example: the Sun
...
```

| Piece | Means |
|---|---|
| `@kind id` | Starts an entry. `kind` is the category, and `id` is a unique short name |
| `key: value` | One property |
| `a..b` | A range the generator picks from |
| `a, b, c` | A list |
| `1e6` | 1,000,000 (shorthand for big numbers) |
| `real: yes` | A real-world seed. `real: rule` marks a generator rule |
| `# …` | A comment, ignored |

**Rules:**
- One entry per `@` block. A blank line ends the block.
- Keys are lowercase words joined with `-`.
- Every file starts with a `# DNA-AND content · <subject>` line.
- Units go in the key name (`temp-k`, `mass-sol`, `radius-km`).
- Facts marked `real: yes` must be real. If a fact isn't certain, it's left out (LAW 7).
- Our star is always "the Sun". Every other star is a star (LAW 9).

---

## 🗃️ Files

One file per subject, in `content/`, named `<subject>.dnat` (**DNA**-AND **t**ext).

The sample subjects come first (round 15): planets, stars, gas giants, Dyson spheres, space stations, space colonies, spaceships, galaxy layouts, solar system layouts, resource fields, cities, microbes, viruses, insects, animals, intelligent creatures and elements.

---

## ⚙️ Text to binary (later)

Our own toolchain (R1) gets a content compiler. It reads every `.dnat` file, checks it against this page, and writes the binary the game loads. Until that exists, these text files are the content.

🔗 [Design record](DESIGN.md) · [Work Laws](WORK-LAWS.md) · [Live generators](../wiki/systems/r04b-generators.md) · [Top-100 lists](LISTS.md)
