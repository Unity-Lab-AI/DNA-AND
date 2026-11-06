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
