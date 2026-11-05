# ⚖️ DNA-AND Work Laws

> "we havent done any code yet and are still do ing docs and making the work laws to use the fucking docs to write code when we do write code"

These laws are binding for **every** piece of DNA-AND work: every code task, every doc change, every person, and every AI session. The docs are written first, and the code follows them.

---

## 📜 The laws

### LAW 1 · The docs are the spec
Code gets written **from the docs**. Every system is already specified in the player wiki (`docs/wiki/`), the builder wiki (`wiki/`) and the decisions in `docs/DESIGN.md`. If it isn't in the docs, it doesn't get built.

### LAW 2 · Every task names its spec
Every task in `docs/TODO.md` names the doc pages it's built from (**Spec:**). Before writing code for a task, read every spec page in full. The code must do exactly what those pages say. No more, no less.

### LAW 3 · Gaps get asked, never guessed
If the code needs a detail the docs don't give, **stop**:
1. Ask the creator, plainly, as multiple choice with suggestions.
2. Record the answer word for word in `docs/DESIGN.md`.
3. Update every affected doc page.
4. Only then write the code.

### LAW 4 · Code and docs move together
Any change in behavior updates its docs **in the same batch**. When a system's code matches its builder wiki page, set that page's `status: verified` and `last-verified: <commit>`. Docs and code never drift apart.

### LAW 5 · No dependencies
> "No dependancies!!!! WE DO NOT USE ANOYONE ELSE CODE< SCRIPTS< PROGERMAES< BINARIES <EXECULATEBLES< TANBLES< DATA< FILES< FOLDERS< DIRECTORES< OR COMPOLIERS"

Everything is built from scratch in **Helix**, starting from the hand-typed hex loader. The only outside pieces allowed:
- Windows itself
- the graphics card driver
- jsonbin, behind the swappable side car

### LAW 6 · Build only what was asked, and all of it
> "YOU do NOT add shit that you were not told to add, but you DO build out everything required to make what i say a reality."

### LAW 7 · Never make things up
> "YOU do not make shit up! you always use ask me question and you leave the tecnial bullshit out of it and talk plain english and to the point."

### LAW 8 · Everything is 100
Every game-content list has 100 entries. Controls and settings keep their decided counts. The elements stay at all 118. See `docs/DESIGN.md`, rounds 7 and 9.

### LAW 9 · Words matter
- A star is a star. "The Sun" is only our star's name.
- The creator's words are quoted exactly in DESIGN, TODO and FINALIZED.

### LAW 10 · Git flow
Every batch goes `feature/*` → `develop` → `main`. Every branch is pushed to both `origin` (git.unityailab.com, over HTTPS) and `github`.

### LAW 11 · Regression check before every push
Before every push:
- every link and anchor works
- no stale "suggestion" or "undecided" wording
- every list still has 100 entries
- no removed line loses information
- no generic "sun"

### LAW 12 · Decisions stay in the main session
> "stop u dont outsource importatn decistions to a fuucking agent thhat knows nothing of the project!"

### LAW 13 · Review gates
Big phases end with a `/super-review` and a `/unity` check that everything the creator said is in the TODO and docs. The next phase only starts after both pass.

---

## ✅ Before writing any code

| | Check |
|:-:|---|
| 1 | The task is in `docs/TODO.md`, with its **Spec:** pages |
| 2 | Every spec page is read in full |
| 3 | Every detail the code needs is in those pages, or LAW 3 was followed |
| 4 | Nothing outside the project is used (LAW 5) |
| 5 | You're on a `feature/*` branch (LAW 10) |

## ✅ Before every push

| | Check |
|:-:|---|
| 1 | The docs match the code (LAW 4) |
| 2 | The regression check passes (LAW 11) |
| 3 | Finished tasks are moved to `docs/FINALIZED.md`, word for word |
| 4 | Pushed feature → develop → main, to both remotes |

---

🔗 [Design record](DESIGN.md "Every decision, word for word") · [To-do](TODO.md "Every task and its spec") · [Roadmap](ROADMAP.md "The big stages") · [Builder wiki](../wiki/index.md "How every system is built") · [Player wiki](wiki/README.md "Every system, as the player sees it")
