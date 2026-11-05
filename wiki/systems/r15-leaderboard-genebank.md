---
title: R15 · Leaderboard + Genebank
status: draft
sources: [docs/DESIGN.md (rounds 1-4), docs/ROADMAP.md#R15]
last-verified: none (plan only)
---

# 🏆 R15 · Leaderboard + Genebank

> "everyoner that plays attribute to the leader boards and be listed in the Genebank with maybe the top three or so being able to uplaod a genome" · "https://jsonbin.io/app/dashboard"

## 🧮 Score (decided)
> "life in your galaxy and life \"havested\""

**Score = life in your galaxy + life harvested.**

## 🧬 Genebank
- Every player gets listed.
- Roughly the **top 3** can upload a genome.
- Other players add it to their world, and it arrives **by meteorite** in an ocean or on land: "no its added to the oceans or land via meteorite".

## 🔌 The side car
> "it only thing allowed but lets keep it as a side car so if needed we can pop something else in there."

jsonbin is the **only outside service**. The game never talks to it directly. Everything goes through one swappable piece:

| Game asks | Side car does |
|---|---|
| Send my score | Writes it to jsonbin |
| Get the top list | Reads it from jsonbin |
| Upload a genome | Stores it in the Genebank |
| Get a genome | Fetches it |

To swap services, replace only the side car.
✅ The side car runs as its own small program that we write, and the game talks to it on the same computer. The Genebank top three is locked in weekly ("Genome of the Week").

## 🔒 Secure connection
Reaching jsonbin needs encrypted web traffic (HTTPS). That's **written from scratch**, which was approved in round 2.
✅ TLS 1.3.

## 🚫 Cheating
> "no cheats will NOT work its fine."
