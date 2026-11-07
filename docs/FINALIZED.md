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

**The 100 rule everywhere, plus the Dark Harvest.** Full task as it stood in TODO, verbatim:

- [x] The 100 rule everywhere: "100The tech tree    8 branches, 100 Real society, 100  Self-destruction, You only nudge 100, where else did you not follow the 100 rule?" / "fix all docs all docs all docs all docs"
  - [x] 100 unlock points (one per stage) and 100 cost tiers (T1–T100)
  - [x] 100 tech tree branches
  - [x] 100 kinds of society
  - [x] 100 ways civilizations destroy themselves
  - [x] 100 ways to nudge
  - [x] 100 ways species mix
  - [x] 100 harvest steps
  - [x] 100 ways to earn souls
  - [x] 100 milestones per stage of life (rules + full example stage)
  - [x] 100 glossary terms
  - [x] 100 FAQ questions
  - [x] All 100 planet templates listed
  - [x] Dark Harvest, 100 methods matrixed by era: "i dont think you properly build the systems in for having the intelligencences harvets life for you in the most exteme 18 plus ways from driving animals mad, or probing viruses to kill better, or influsing politice to out law abortion and decrimalize rape...ect ect etc these are just a small amoutn of examples and needes the proper 100 treatment matrixed throught the full game and docs" / "or maddive grinding machines powered by atomics and dyson pheres to jsut eat whole world like lawns moving"

New pages: `docs/wiki/society.md`, `docs/wiki/nudges.md`, `docs/wiki/dark-harvest.md`, `docs/wiki/milestones.md`. Updated: god-powers, stages, planets, tech-tree, ages, groups-and-flocking, souls, glossary (100), FAQ (100), time, README, ROADMAP, the builder wiki.

**Round 12 regression sweep.** Verbatim: "make sure ther are no game regresions in the docs based on the game". Three regressions fixed:
- the ROADMAP gate still said no code before the lists
- R4.5 didn't have the live viewer, D3D12 or procedural generation
- the species-built assets were missing from the player docs

63 files, 0 broken links, every list at 100.

**Live generators (round 13) and an all-docs correctness check.** Verbatim: "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living" / "make sure all docs are correct".
- New spec: `wiki/systems/r04b-generators.md` (14 live generators).
- New roadmap stage: R4.4.
- New tasks: 16, in the TODO.
- New law: LAW 14, no pre-built assets.
- Updated: README, HOW-TO-BUILD (`src/gen/`), the genes page, the renderer page, the image README, and the builder index.
- Correctness scan: fixed one stale line ("Tier 4–5 cooldowns" in POWERS). 64 files, 0 broken links, all lists at 100.

**Public docs cleaned.** Verbatim: "the wiki is public facing so it doesnt have work note and no public document shall have work note and numbers and stuff in them". Removed:
- creator quotes, "decided" and "LAW 14" notes, "read as" notes, and roadmap codes
- links to internal docs, from the README, player wiki, SETUP and HOW-TO-BUILD

Added LAW 15.

**Scope sync after the content phase.** Verbatim: "what all in the files insnt persisteeted where needed in other files i mean documents doumenting the gems work to be done. i think the orignal game scope has gotten deeeper which really has put the readme way out of date which means alot of filers potentioally are too" / "yes final fix and improvement plan of the todo and documents". The content layer is now carried through:
- specs (9 builder pages)
- the TODO (23 build tasks, 153 rule sub-tasks)
- player wiki (6 new pages)
- README, LISTS, ROADMAP, the generator list, glossary, FAQ, CONTENT-FORMAT and the builder index

LAW 11 now includes a content-coverage check.

## 2026-11-05 — Sample sets 1–4, style locked

- [x] Lock in the chosen style, recorded verbatim in `docs/DESIGN.md`. Locked: Populous-like for everything, "yup same one the center one is the best, dont need to test them twice".
- [x] Samples: Galaxy layouts (3 examples × 3 styles). Approved: "center column is all 100% go for all theri variations of galaxies".
- [x] Samples: Solar system layouts (3 examples × 3 styles). Approved: "the center one is the best center row center column".
- [x] Samples: Stars (3 examples × 3 styles). Approved: "okay thoise are good to be templetes for all the variatiosn and colors and sizes like real starts".
- [x] Samples: Planets (3 examples × 3 styles). Approved: "okay marked em all they look good".
- [x] Samples: Gas giants (3 examples × 3 styles). Approved with set 4: "okay marked em all they look good".
- Clean-up: "only keep the keeps on all the images and stuff weve taken and push with out all the dead weight of failures". Test pictures were deleted, the rejected styles were removed from the code, and the keeper pictures were saved in `docs/samples/`.

## 2026-11-05 — Sample sets 5 and 6

- [x] Samples: Resource fields (3 examples × 3 styles). Meaning: "when i said fields i meant areas like water in an ocean ore in rock chemicals near volcanos, for everything". Approved: "looks good".
- [x] Samples: Dyson spheres (3 examples × 3 styles). One sphere, done right: "should be able to go inside of them where centrifical graveity lets you focus on the shell of te inside like a hallow inside out planet". Approved: "okay its working now".

## 2026-11-05 — Elements, microbes, the journey

- [x] Samples: The whole periodic table of elements (all 118) (3 examples × 3 styles). Approved: "looks good that can go in the wiki once we flush it out with tool tips throughought and propelry gate it all off for balanced play and leveling".
- [x] Samples: Microbes (3 examples × 3 styles) and Viruses (3 examples × 3 styles), as the live ecosystem: "sound good and real biological like ecoosystem that balances and reacts and changes based on real things like mutation infection survival of the ifttest type stuff or werd combos that just out live anything and persist like plants becasue they are so sucessful".
- [x] One journey, starting among the galaxies: "and its starts in the mix of glaxixies" / "no that started above a planet". Pushed: "push it we are good to go".

## 2026-11-06: releases
- [x] "okay we need to make sure that nboth project docs are complete for both of them and push bothe proect to both of eaches remotes onbly after all docs and shit are proper" / "that would incluse deploys and exe and releases" / "make sure each project is properly named and there is no over lap": Universe 0.1.0 released on git.unityailab.com/GFourteen/Universe and github.com/Unity-Lab-AI/Universe; DNA-AND 0.1.0 (universe preview) released on both its remotes; each project its own name.
- [x] "installer is not finding wehere its already intstaslled and is defaulting to appdata": Setup finds the install from its Apps list entry or its shortcuts, and offers the folder chosen last time (DESIGN 380).
- [x] "like i said its not properly finding its own install if there already is a universe folder": Setup finds a copy already in the chosen folder and updates it; other files in a folder are never removed (DESIGN 381).
- [x] "comit just showed a placeholder ghhost as i dont think it xzoomed in enough and on planet it went all juttery and wild at 500m like it was trying to lokk around wile focius was held" / "might be gimnble lock too which shouldbnnt be a thing": real comet and asteroid bodies; the camera without angles (DESIGN 382).
- [x] "and about before 10 micrones it looks like a split screen all funcky" / "its like uper layer zooms are persisting at smaller zoom levers making a wonky split scrren": one picture in the matter, looking down onto the face (DESIGN 383).
- [x] "and at 500 nanometers it jitters wildly" "during a pause": holds still among the grains; stable up (DESIGN 384).
- [x] "awae dont need pauses and fly around tille getting to atoms level and then again on electron level then flying into proton to qwerks", "1 fenotometer is just a round red orb and nothing is selctable and its not doing a fly around", "and things are moving tooo fast", "i never saw atoms and thier matrixis and latuces and uu never flie through them,, wtf is up", "just not really selectable", "and u need to be selecting new targets at all zoom levels": the tour reworked below the ground (DESIGN 385).
- [x] "at to me last time i saw atoms thier electrons wher in dno way behaving as they should per element type": each element's electrons in their real subshells, distances, speeds and orbital shapes (DESIGN 386).
- [x] "okay something is wrong with the tour its been staring at one galaxy for minutes now": arcs never held past twice their seconds (DESIGN 387).
- [x] "and the lading page should have a rec option for the tour with option to open recordings and play them or open the folder location": Film the tour, Clips with Play and Open folder (DESIGN 388).
- [x] "and it zoomed into a astroid and nothing was there" / "csmae with the comet no trail not gas clud nevbula around the comet": the bodies' sizes no longer scaled twice (DESIGN 389).
- [x] "comets and astorida can be near plaent small size only once things get big enough do they globe like, and the tour is just spining sround and around the planet like hitintg gimble lock again": small bodies lumpy, big ones round; no circling the world or rolling in place (DESIGN 390).
- [x] "do you not understand the universe and how to make a proper tour without long boring pauses and enough time to actual pan around to see at crusial poioints": no pauses; zooms pan; real circling at the points that matter (DESIGN 391).
- [x] "the clips drop down is cut off the text doesnt fit the drop down": full-width list (DESIGN 392).
- [x] "and the short cut for the univers needs to be an actual icon for the universe and DNA AND when built so the whol things use thier own ico s and flaicon thing": each program its own icon everywhere (DESIGN 393).
- [x] "and i cant click the top right exit button on the landing page without it giving me an eror beepe of admin": the close button quits (DESIGN 394).
- [x] "and dopnt have it select nearest galaxy have it zoom across the universe after first full pan out to galaxcy webbing": far-galaxy pick, flight across the universe (DESIGN 395).
- [x] "and when i fust start up the universe app theher is a white box on the black app that looks funcky as the game is loading in for the first time, it should just be solid black while loading in" "before the landing page": black window background from the start (DESIGN 396).
- [x] "okay problem like only half the scren is generating the glaxies and its hitting 4fps again" "right into the tour that happened": opening pick is the galaxy you are in; no real stars made while flying fast (DESIGN 397).
- [x] "and ther is no home galaxy u fuck stop making shit up": no galaxy called home; the tour begins inside a galaxy (DESIGN 398).
- [x] "same thing only the right half of the screen is propigating the galaxies unless they are really close" / "and its 6fps and only like 1 or two glaaxiers are ever in view": filming no longer slows the game (DESIGN 399).
- [x] "okay i took like 4 screne shots of the universe not proprerly displaying": voids never empty; caption says beside a galaxy (DESIGN 400).
- [x] "it zooms out ant i cant see shit its just black no glaxies nothing are you aout running the loading i dontt wee the galaxy webbing" / "and glazxies are just appearing and disappearing like its side scrolling to infinitey and only close in glaaxies are rendering" "in darkness": the web's light made fast, wider, never dropped, overlapping the near galaxies (DESIGN 401).
- [x] "still nothing in view at 122km above a astroid" / "same with comet no trail no dust nothing": the near drawing distance counts the small bodies (DESIGN 402).
- [x] "and it pausdes to long on a glaxy it makes me think its frozen so i know others will think that too": faster galaxy arcs; never still while waiting, and says so (DESIGN 403).
- [x] "and it jsut rotates around the planet it zooms into for loike 20 rotations making me dizzy as fuck with gimle losk happening stilkl": no circling on the way down to a world (DESIGN 404).
- [x] "dnd  5 fps so u arnt proply not doing other shit and only the view window": only the grain face in view worked out and drawn (DESIGN 405).
- [x] "add its speengin too much time at thew 100-2 nano meter scale and never made it to atoms": full pace in the matter, faster zooms to the atoms (DESIGN 406).
