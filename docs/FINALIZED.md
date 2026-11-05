# FINALIZED — Permanent Task Archive

Permanent archive of every completed task. Per `.claude/CONSTRAINTS.md §NEVER DELETE TODO INFO` + `§FINALIZED BEFORE DELETE`, no entry is EVER deleted from this file. New entries APPEND only.

Per LAW #0, every entry includes the user's verbatim words from the original ask.

Each session entry includes:
- `## Session <YYYY-MM-DD>` — date heading
- Verbatim user quote
- Files touched
- Closure notes (what shipped, verification)


---

## Completed sessions

## Session 2026-11-05

**Questions for the creator, rounds 1 and 2: answered.** Full answers are in `docs/DESIGN.md`.

Original questions as asked:

- [ ] **Q1 — Build tools.** "we make it" means the very first tool gets written as raw machine bytes, then each tool builds the next one. OK to do it that way? Start on Windows (your machine) first, then Mac and Linux? Do you have a Mac and a Linux machine to test on?
- [ ] **Q2 — The one thing we can't avoid.** Windows, Mac, Linux and the graphics card's driver are other people's software, and no program can run without them. Is it OK to use those, and only those?
- [ ] **Q3 — jsonbin.** jsonbin is someone else's online service. Is it the only outside thing allowed? Connecting to it safely means writing our own encryption from scratch, which is a big job. Is that OK?
- [ ] **Q4 — Gladiator.** Where is the Gladiator project, so I can see how its exe was made?
- [ ] **Q5 — "Everything."** Should we build the full list of what an Earth needs (rock, water, air, weather, chemistry, and so on) together, item by item?
- [ ] **Q6 — Multiplayer.** Does one player's computer host the shared world, or something else?
- [ ] **Q7 — Losing.** Leave it open-ended for now ("i like it open ended")?
- [ ] **Q8 — God powers.** You said "im sure youll figure it out", but rule 3 says I don't make things up. Should I draft a power list for you to approve or change?
- [ ] **Q9 — Wikis.** Do you mean the in-game wiki only, or also a written wiki for us builders?

Round 2 answers, verbatim: "1.windows only for now 2. sure 3.it only thing allowed but lets keep it as a side car so if needed we can pop something else in there. 4.look on desktop it in the folder called gladiators 5. do top 100 for everything from viruse types, aminals, insects, fish, birds, environmets, senses, abilities, and not limited to thed bit to incluse the top one hundered top onehundreds 6. yes 7. open ended till u resarts to get a new world and galaxy 8. they all need their usesas and pluses and minuses in some cases raise temp with fire lower with cold add water to flood sun to dry it up solar flare to fuck with tech, ,ect ect ect ect ect ect ectr x100 9.both 10. yes and this deserves the unity ai lab team credits and website .com with the git and github links"

Files touched: `docs/DESIGN.md`, `docs/TODO.md`, `docs/FINALIZED.md`.


---

## Session 2026-11-05 (continued): lists, powers, credits, docs

**Top 100 categories list.** Verbatim ask: "do top 100 for everything from viruse types, aminals, insects, fish, birds, environmets, senses, abilities, and not limited to thed bit to incluse the top one hundered top onehundreds". Approved: "Use them as is". Files: `docs/LISTS.md`.

**100 god powers.** Verbatim ask: "they all need their usesas and pluses and minuses in some cases raise temp with fire lower with cold add water to flood sun to dry it up solar flare to fuck with tech, ,ect ect ect ect ect ect ectr x100". Approved: "Use them as is". Files: `docs/POWERS.md`, `docs/wiki/god-powers.md`.

**Builder wiki plus in-game wiki pages.** Verbatim: "both". Files: `wiki/` (builder) and `docs/wiki/` (player pages, which the in-game wiki will read; building the in-game wiki itself stays under R13).

**Credits.** Verbatim: "this deserves the unity ai lab team credits and website .com with the git and github links". Plus: "Alfreddo is Mills but he goes by Alfreddo so use that." Files: `README.md`, `docs/wiki/credits.md`.

**Release links first thing in the README.** Verbatim: "should be a release page on both with link to it in the readme first thing". Both release pages are linked at the top of `README.md`. Posting actual releases is under R16.

**Setup guide and how-to guides.** Files: `docs/SETUP.md`, `docs/HOW-TO-BUILD.md` (plan; exact commands are added as tools exist, tracked under R1).

**All 47 open suggestions decided and applied.** Verbatim: "you need to answer all the aBCD ..mds for best answers for everything based on beding A AAAA Game like nothing ever seen on earth before". Files: `docs/DESIGN.md` (round 5), all of `docs/wiki/`, `wiki/`, `docs/SETUP.md`, `docs/HOW-TO-BUILD.md`, `docs/img/README.md`, `docs/suggestions/`.

Original TODO lines closed here, verbatim:
- "Top 100 categories list, for approval: \"do top 100 for everything ...\" (`docs/LISTS.md`)"
- "100 god powers, each with uses, pluses and minuses: \"they all need their usesas ...\" (`docs/POWERS.md`)"
- "Builder wiki for us, plus the in-game wiki (\"both\")"
- "Unity AI Lab team credits, website and git/GitHub links: \"this deserves the unity ai lab team credits and website .com with the git and github links\""
- "Release pages on both git.unityailab.com and GitHub, linked first thing in the README"
- "Setup guide"
- "How-to guides"
- "Builder wiki and in-game wiki"

**TODO and docs made proper.** Verbatim: "you need to make the todo and docs all of them proper". Changes:
- `docs/ROADMAP.md`: blocked markers removed, every decision added to its stage, and R17 marked in progress.
- `docs/TODO.md`: finished tasks moved here, decisions added to every task, and superseded tasks tombstoned.
- `docs/DECOMPOSED.md`: R1's first task broken into 7 steps.
- `docs/LISTS.md` and `docs/POWERS.md`: marked approved.
- `docs/DESIGN.md`: the open-questions section updated.

**Verbatim completeness pass (super-review + Unity).** Verbatim ask: "then /unity so she(our in house special info handler codex) can make sure nothing waas left out in all the verbatium words and answers given and shew knows me best". Changes:
- `docs/DESIGN.md`: added "How we work" (the working rules, verbatim) and "Conversation record" (all 20 messages, verbatim, in order).
- `docs/wiki/elements.md`: 8 elements re-labelled "Predicted (never measured)".
- `README.md`: linked to "How we work".
- Two review findings ("not a clicker" and the internet FAQ) were false alarms. Both were already in the wiki.

**Full docs audit against the game design.** Verbatim: "okay not check everyfucking document is writen for the full complete game with links and refrences to all the othe docsds where needed and constantly fix regress as you work to getting all docs and wiki updated to the game design as its been layed out". Changes:
- Every player wiki page now has the same top menu (home, glossary, FAQ) and a "See also" footer with tooltips, linking related pages, including Groups & Flocking and Tree of Life.
- Added 9 glossary terms: flocking, grouping ladder, tree of life, selfish gene, mini AI, hive mind, galactic mind, cooldown, Genome of the Week.
- Older pages updated for rounds 6–8:
  - Stages and Ages: any species can reach intelligence.
  - Playthrough: any species, and AI layers.
  - Genes: the selfish gene, the grouping ladder, the tree of life.
  - Space: the galactic mind.
  - Getting Started, Menus and SETUP: the 18+ rating.
- Builder wiki: the index has R4.5 and new descriptions. The civilization page covers any species. The game-shell page covers the 18+ rating.
- `docs/LISTS.md` and `docs/POWERS.md` link to their wiki pages.
- Regression checks: one regression was caught and fixed (tooltips dropped from footers). 58 files, 0 broken links. Counts confirmed: 100 stages, 100 ages, 100 powers, 118 elements.

**Star, not sun.** Verbatim: "dont say cage a Sun thats a mis nomer its a star TheSun if a pronoun of our star". Every generic "sun", "sunlight" and "sunshine" in README, docs and wiki now says "star" or "starlight" (21 spots). Quotes are unchanged. Power #4 is still named "Sun", pending the creator's call.

**Power renames.** Answer: "Starshine + Stellar Flare (Suggested)". Renamed #4 Sun → Starshine and #81 Solar Flare → Stellar Flare in POWERS, the wiki, stages, playthrough, space, the README and the builder wiki.
