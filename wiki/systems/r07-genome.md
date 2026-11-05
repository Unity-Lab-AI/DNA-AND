---
title: R7 · Genome
status: draft
sources: [docs/DESIGN.md (rounds 1, 4), docs/ROADMAP.md#R7]
last-verified: none (plan only)
---

# 🧬 R7 · Genome

> "Real-style" · "genome of all creatures liveing and even viruses"

## 🔤 DNA
Every living thing carries real-style DNA written in **A, C, G and T**, viruses included. Genes are stretches of that DNA, and they build the body and its behavior.

✅ Each letter is stored in 2 bits, so 4 letters fit in one byte.

## 🔄 How genes change
| Way | What happens |
|---|---|
| 🎲 Mutation | Random letter changes, copies and cuts |
| 💞 Sexual breeding | Two parents mix their DNA |
| 🌱 Asexual breeding | One parent copies itself, with mutations |
| 🦠 Viruses | Carry genes from one creature into another |
| ☢️ God powers | Gene powers raise or lower how fast genes change. They never edit DNA directly |

> "no direct edit besids using powers to influence things to happen"

## 🗺️ The gene map
The gene map shows which gene pieces give which trait or skill, "like breathing air to walking upright to photosynethesiis". Players see it with the **Gene scan** power. Traits come from the abilities, senses and body lists in [docs/LISTS.md](../../docs/LISTS.md).

## 🌠 Genebank genomes
A genome uploaded by a top player reaches other players' worlds by meteorite ([R15](r15-leaderboard-genebank.md)).

## 📦 Content (build from these)

This system is built from these researched content files (`docs/CONTENT-FORMAT.md`). Every entry and every rule in them is part of the spec (WORK-LAWS LAW 1).

- `content/life/viruses.dnat`: 22 entries (Baltimore groups (how a virus's genes get read), Shapes and parts, Who they infect, Life cycles) and 6 rules
- `content/life/bacteria.dnat`: 31 entries (Shapes, Cell parts, How they get energy and food, Main groups (phyla), Group life, Swapping genes) and 5 rules
- `content/life/archaea.dnat`: 12 entries (What makes them different, Extremophiles, Main groups (phyla)) and 3 rules
- `content/life/protists.dnat`: 25 entries (Supergroups, Protist groups (seeds), How they move) and 4 rules
- `content/life/fungi.dnat`: 16 entries (Phyla, Body parts, Ways of living) and 5 rules
- `content/life/plants.dnat`: 36 entries (Groups (phyla and lines), Big splits, Organs and tissues, How they make food, How they breed) and 6 rules
- `content/life/animals.dnat`: 84 entries (Body-plan traits, Phyla, Arthropod classes, Insect orders (seeds), Fish classes, Land vertebrate classes, Bird orders (seeds), Mammal orders (seeds)) and 5 rules
