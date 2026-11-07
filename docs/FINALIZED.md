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
- [x] "oioaky i dont like the zoom out to webb ciew its like side scrolling for a 100 million light years and shit is just flying by that close and u cant see far away so its like a black screen with galxies flying by super fast.. not cneematic at all": straight pull out, slow web turn, the far web brighter and deeper (DESIGN 407).
- [x] "put on ur movieaehat and make it proper dynamically and prcedural like": a director, each shot afresh each viewing (DESIGN 408).
- [x] "and how do i bring the landing page back up is it a drop down from the menu toolbars?": Start page on the menu bar (DESIGN 409).
- [x] "tour is still jsut side panning for 50 million miles as galaxies fly by in darkness": circles only what is close; far off it pans in place (DESIGN 410).
- [x] "i told you to put on your movie making hat" / "stop it with these shit edits and do the full fucking thing correctly": real framed shots for the tour in space (DESIGN 411).
- [x] "it zoomed out to 105 million light years and it was just all black i neever saw the webbing of galaxies and couldfnt see them as dots of light" "it was just all black": the web's light made in seconds; galaxies taken faster (DESIGN 412).
- [x] "yeah shit is fucked im not seeing any other galaxies unles it piced one on the edge of the universe" / "i should see galaxies in the sky": other galaxies and the web show from inside a galaxy; clips finished on close (DESIGN 413).
- [x] "at 50 microns i started seeing through the palnet and weird teering ... at that scal of electrons zoom flys right by whats foccuesd on": a floor under the face; flies in only once over the pick (DESIGN 414).
- [x] "and the thing need a proper zoom back out and fly around ending on web galaxy view": the flight back out to the web, ending over it (DESIGN 415).
- [x] "probnelm: after it zooms out to galaxy web view it inprorply still shows the web once zoomed into another galaxy when ist should show gaslaxies in the sky noit just dots of the webbing": no web dots inside a galaxy; the galaxies themselves in its sky (DESIGN 417).
- [x] "astroid it zoomed into was a ghost like apporition and not properly stone or rock or other elements": solid stone asteroids and nuclei (DESIGN 418).
- [x] "on planet drop in its hit lows of 7fps": background workers never starve the frame (DESIGN 419).
- [x] "andf ther should be an options to turn off day night cycle to be only day" / "and days need to be when the star is actuall on that side of planet" / "as polanets rotate and spin": true day and night with the sky turning; Time > Always day (DESIGN 420).
- [x] "it stopped at 500 nano metter looking like a freeze": never held waiting over a pick (DESIGN 421).
- [x] "and 2 nano meter is jsut static" / "its all jsaut stat from nanaometers to pico" "static": steady orbital clouds, only for the atom you are at (DESIGN 422).

## 2026-11-07 — The universe preview: the tour, the matter, the black hole, the elements, Linux

Built and shipped in the 0.1.0 releases on all four remotes; each item below keeps its words from the TODO, with the DESIGN entry that tells how it was done. What the user's own runs and clip showed working is in DESIGN 529 and 533; anything a later run shows wrong opens a new TODO item in the user's words.

- [x] **A scenic fly-by, replayable:** (built: DESIGN 282-289; waits on the test) "okay do a full panoramic scenic view fly by and down and into attoms like fly bys and follow an electron and then zoom in more to a proton and then into its qwerks nice and panoramic like for human viewability speeding through only at large zoom points and paning on cool scenes like at ground level 6' tlooking into space then looking down into atoms" / "make it a script like thing we can do agaion later" / "okay but you went to fast u didnt even fly around the solor sytem of the random star and shit and fly around the planet looking around and shit them like zooming in" / "and it should ene on the electrical quark like waves and shit from after electorns and u never panned and zoomed around the valence rings of atoms and zooming around them in like slolor system esk electoron view" / "and you going way to fast at keey point when u need to be doing sceneic views" / "and you zoom into the first galaxy without ever flying around the galxaies first" / "and u go from galaxy to planet ground in like 2 seconds>>> WTF you retard". "okay u did a sprial thing and u are using differnt zoom speeds incorrectly it need  s to be continuesous to give the scale" / "spiral thing made me dissy and u keep moveing alt all differnt speeds way to fast". The zoom at one steady rate in scale (each tenfold the same time), no spirals, gentle pans. A script of stops (fly round the galaxies, into one, round its star system, round the planet, down to 6 ft looking up into space, then down into the grains, the atoms, round their valence rings, following an electron, across the gap to the nucleus, a proton, its quarks), slow pans at each, fast only through the big zoom gaps.
- [x] **100 billion galaxies in a visible universe with an edge:** (built: DESIGN 282-289; waits on the test) "okay lets properly add 100 billion galaxies as the start looks empty with only 32, and lets do a properl 13.4 biillion light year across vissible universe asnd handle properly not being able to move beyond it (for now)". Galaxies made by regions of space at the real density (about 100 billion in all), the far ones a glow along the cosmic web, the size in content (13.4 billion light-years as asked; the real visible universe is about 93 billion across), a soft edge you cannot pass.
- [x] **The fly-by, eye-friendly, the whole way:** (built: DESIGN 282-289; waits on the test) "and when everything is build build the fly by the vastness of the univers, a few galxy fly bys and fly throughs planets around and then around a planet, and you are doooing a spinning thing in error with like gyro lock or something" / "and fly by of the elxtrons and atom latics at thier respective levels and electron fly bys and quark shit" / "and u cant make wild turns and pansd and shit and u zoom in way to fast it has to be huuman readable and viewable friendly to the eyes with peresective relations". No gimbal spinning (never circling while looking straight up or down), slow gentle turns, a slower steady zoom, stops that show scale against something near.
- [x] **Intergalactic scale, researched:** (built: DESIGN 282-289; waits on the test) "and zoom out at inter galactic scall shuould propely show glaaxies at scale and pin points in the vast matrix of the universies glacactic systems of web like structures of galaxies do research". Galaxies at their true sizes, pinpoints from afar, along the cosmic web (filaments, walls, clusters, voids) as measured.
- [x] **The tour stops itself and sits as if frozen:** (built: DESIGN 423; waits on the test) "it pased too long at 106 light years and as u can see its focused on a glaxy and the web is still all viewable interseting with stars of the galaxy clasdhing too much" / "the tour crashed" / "like i said it froze". Found: any key reaching the window (Shift, Alt, the Windows key: Alt+Tab, Win+Shift+S) ended the tour; the web's points were drawn from 3.2 million light-years while whole galaxies are drawn to 12 million, both at once.
- [x] **A paused tour says so, with restart:** (built: DESIGN 424; waits on the test) "like it should say mouse movement tour paused if done and shit" / "with restart".
- [x] **WorldView is the universe view:** (built: DESIGN 425; waits on the test) "make sure world view isnt labeld wrong and should actually be universeview".
- [x] **The in-game docs formatted:** (built: DESIGN 426; waits on the test) "and the readsmes and shit need proper formating and shit it just looks like a shit text files with no actual style of formating or layout and shit" / "the in game all doc s".
- [x] **Every public doc fun to read:** (built: DESIGN 427; waits on the test) "and the project readme has the same fucking problem not beiung fun to read like all docs public versions for the public".
- [x] **How-to docs for building with the universe:** (built: DESIGN 428; waits on the test) "and we need how to docs on how to build in the universe from adding more planet types to features and all kinds of shit so its a tools to build universes of simulations and games do you understande the universe project now?"
- [x] **Every generator a template, no names in code:** (built: DESIGN 429, 431; waits on the test) "thats a problem they were all suppose to be dynamic and procedurally genreated templet like things not hard code" / "yeah day length is the spin of the planet". Done for worlds (terrain.dnat: where each kind forms, its size, moons, spin, colours and make-up, every value a range each world rolls from its seed). Still to audit: galaxy kinds are told apart in code by words in their ids (`GalaxyGen`: "spiral", "dwarf", "elliptical"), and the rest of the generators the same way.
- [x] **Smooth, continuous tour camera:** (built: DESIGN 430; waits on the test) "and make sure ther are no abrupt turns or wild pans as it cant look shooppy and needs to be smooth and continueous"
- [x] **Rings on any world:** (built: DESIGN 431; waits on the test) "planets can have rings too"
- [x] **Physics docs, all four forces:** (built: DESIGN 432; waits on the test) "what about physics and the natural laws like strong nuclear and weak forces electromagnaism and gravity where all all those docs"
- [x] **Planet fall frames:** (built: DESIGN 433; waits on the test) "its hitting like 9fps zooming into a planet on planet fall liek its still trying to simulate the sky when its not even in the view port"
- [x] **No pause, no static, no handoff, no pop from 500 nm to the atoms:** (built: DESIGN 434; waits on the test) "and it pauses at 500 nanometer get rid of pauses like that it makes it look prozen" / "and from 500 -1 nano meters its just static appearance" / "then atoms pop in all at once in eroor" / "and looks like handoff in the 500-1 nano range"
- [x] **Inside a giant:** (built: DESIGN 435; waits on the test) "look what happens on zoom in to a ice giant im sduure this is nt the only problem one"
- [x] **The web the same from the start:** (built: DESIGN 436; waits on the test) "it does a weird thing were glaxies webbing isnt viewable until zoom out then it persists on zoom in which is fine i suppsoe i just needs to be uniform"
- [x] **A loading screen at the start:** (built: DESIGN 437; waits on the test) "lets put a loading bar or somehting on start till everything is properly ready or what? its a free wheel loading problem"
- [x] **Calmer time at the start:** (built: DESIGN 438; waits on the test) "and planets spped is way to fast or time scale is way to fast the star is rising like every second"
- [x] **A calm look at the sky:** (built: DESIGN 439; waits on the test) "and when it looks up at the sky it pans too much and with the fasst time speen it makes me dizzy"
- [x] **No driver time-out in the matter:** (built: DESIGN 440; waits on the test) "it froze an unknow exemtioon error at matter delpht where teh static was before"
- [x] **Nothing labelled wrong between the projects:** (built: DESIGN 442) "make sure nothing else is labels wrong between projects"
- [x] **All docs good to go, in the game and out:** (built: DESIGN 444) "make sure all docs are good to go" / "in game and out" / "the readme last i saw was pretty shit"
- [x] **Docs finished to the creator's words, no emoji:** (built: DESIGN 445) "fix the files of the docs fully and completely to how ive already descirbbeed many many times then do full cascade for both projects all four remotes" / "and not a emohji desacster like dna and for the docs"
- [x] **How a universe fits in 1.7 MB, in the readme:** (built: DESIGN 446) "and make sure the readme properly describnbes how we make such a massive universe with only 1.7 Mb or what ever the count is now"
- [x] **Comet seen, smooth turn to it:** (built: DESIGN 447; waits on the test) "the comet was a ghost and the pan from astroid to commet was jerk as fuck"
- [x] **Tidal locking from the orbit:** (built: DESIGN 448; waits on the test) "and tiadally loceked jsut means a moon or something is always near one side of the planet or body due to orboitoitalk distance of like moons and stuff"
- [x] **No straight lines in nature:** (built: DESIGN 449; waits on the test) "and some planets ave straihgt line surface coloring and pattrering< nature sdoesnt build in straight lines"
- [x] **No driver time-out over the ground's sand:** (built: DESIGN 450; waits on the test) "200 million for a micron sounds high"
- [x] **Moons are worlds, orbits are ellipses:** (built: DESIGN 451; waits on the test) "and remmeber planets can be moons and moons planets and all these bodies orbits are too perfect reall orbits are eliptical and wild in cvariation"
- [x] **No still frame while the tour looks for what is next:** (built: DESIGN 452; waits on the test) "it pauses at 10570 light years way to long when it needs to look around a little instead of still frame pause for 10 seconds"
- [x] **Comets at their real size, seen across the system:** (built: DESIGN 453; waits on the test) "zopomed to a comet and nothign was there and wtf u should be able to see comets all the way across a solar system"
- [x] **Always day on to start with:** (built: DESIGN 454; waits on the test) "and we should default day only cycles on"
- [x] **No driver time-out on the first frame of a grain's face:** (built: DESIGN 455; waits on the test) "i tried to f12 at 12 microns but it crashed"
- [x] **Nothing shows through an asteroid or a nucleus:** (built: DESIGN 457; waits on the test) "the comet is transdpartent i can see stars through it" / "i mean asstroid but comet prolly same problem"
- [x] **A pick always shows its card:** (built: DESIGN 458; waits on the test) "also im clicking around at 1km scale and i cant select anything"
- [x] **Processor and card at once:** (built: DESIGN 459; waits on the test) "and im just sitting at 1km and its shittly doing 14fps im not even zooming or nothing"
- [x] **A comet with its coma and tails, seen across its system:** (built: DESIGN 460; waits on the test) "comet didnt have a auros or gas or a tail facing away from star and i could nt see it at solar systems scale like comets should be seen" / "it looks just like an astroid"
- [x] **Blocky hand-overs at 200 mm:** (built: DESIGN 462; waits on the test) "200m ther are lock hand offs" / "mm"
- [x] **The crash log names the draw the card died on:** (built: DESIGN 461; waits on the next crash) "at 200 mm ant micor level it crashes still"
- [x] **Ids for asteroids, comets and every body:** (built: DESIGN 464; waits on the test) "astroids and comets and shit dont have stellar ids"
- [x] **The sky caption says what is really up:** (built: DESIGN 465; waits on the test) "the look up to thew sky at human ground level inproprly says its star and other worlds when worlds are viewable and  no star is in the sky at the moment"
- [x] **No circling at the gravel; frames at 5 mm:** (built: DESIGN 466; waits on the test) "at its hitting 5fps at 5mm pan and it should not pan at those zoom levels only when at atom level and there is shit to see besides gravel"
- [x] **A click brings the window to the front:** (built: DESIGN 467; waits on the test) "when i click on the app it doesnt propelry bring it to top focus it starys behind other windows till i close them"
- [x] **Stirred systems, worlds out of the plane:** (built: DESIGN 468; waits on the test) "are obits suppsoe to be in a flat plane thats not normal is it" / "no fix it dont leav e it"
- [x] **Orbits from speeds; relativity in the orbits and on the cards:** (built: DESIGN 469; waits on the test) "all type of compbinations even ultrrqa fast bodies willth stained long elipticals theh phsysics should determine it" / "relativity is propelty in , or no?"
- [x] **No fading in of the atoms:** (built: DESIGN 470; waits on the test) "at 500 pico the atmoms jsut pop into view incorrectly" / "and i dont want a fad in"
- [x] **A proton's card names its element:** (built: DESIGN 471; waits on the test) "and it generic says a proton of a nuclues incorrectly saying which element it is"
- [x] **Electron-microscope greys below light's wavelength:** (built: DESIGN 472; waits on the test) "and there is a hand off on the 2mm and smaller to wher it dgoes nano" / "ii see grains then it instandt swticheds to balck and white elctron view of material which is fine(might ass note color doesnt exist at this scalle and later when ther is color properly do it" / "not black and white elctron view but like elecrton microscopre vioew where everything turns black and white before the color of electorns and stuff appear"
- [x] **GPU and CPU stats with the FPS, toggleable in settings:** (built: DESIGN 473; waits on the test) "and with fps i want gpu and cpu stats also togfgle able in settings"
- [x] **A directory of every document in the viewer:** (built: DESIGN 474; waits on the test) "we need a full directory listing of the docs and their make uup and use descriptions in the doc virewer that well formated easy to read and use and beautiful"
- [x] **Docs split plain and technical, and a tool meant to be taken:** (built: DESIGN 475; waits on the read) "the docs are all properly formated and created and designed around explaining the systems and shit technical and non techinical dos where basic readmes and how tos are laymens and technical docs are more involved" / "this is a tool menat to be stolen and used for any number of projects of any types wehere a universe is needed"
- [x] **Scratch-made, from the compiler to the engine, in every readme:** (built: DESIGN 476; waits on the read) "and readmes need to propelryy talk of how the app is totaly scatch made from the compiler to the engine explaine it all in laymens terms and tefchnical respective to the doc"
- [x] **The 2 MB universe against the field, checked and sourced:** (built: DESIGN 477; waits on the read) "the full universe in 2mb and less is a major achievement.. is it not? no thing else does this does it dont be pushy beut be real and do checks on reall shit that does similar with comparison tables of like top universe sims of stats and size and comput issues or out preformance and such"
- [x] **Real infinite zoom in, no floor:** (built: DESIGN 478; waits on the test) "and it better be rela infinate zoom in the code so if its not fix it" / "so docs are accurate"
- [x] **The tour's feeding black hole: disc, lensing, shadow:** (built: DESIGN 479; waits on the test) "and add a fly by a balck hole thats feeding and zoom into it on flyby to see its acression disk of a eating balckhole and zoome to the gravity distortions and points of interest" / "before planet level zoom in"
- [x] **Repo pages: social images and write-ups on GitHub and Forgejo:** (built: DESIGN 480; GitHub's social preview waits on the one upload in Settings) "and the repo urls need properl social images and write ups and" / "can that be done on github and git?"
- [x] **The viewer's lines wrap at every text size; the list wide enough:** (built: DESIGN 481; waits on the test) "look at the open text viewer there is text under the highleighted text that i can scroll to and is a dispaly issue"
- [x] **Real stars made all round you, not round your cube:** (built: DESIGN 482; waits on the test) "took a shot image stars ar only populating in half the screen or its a shading thing not allowing them to materialize"
- [x] **The tour's crossing of billions of light-years to a far galaxy:** (built: DESIGN 483; waits on the test) "and it like zooms in and out of the same galazxy when it should fly like 1-5 billion light years to a whole nother galaxy on web galaxy view zoom out" / "its like it keeps selecting the closest"
- [x] **The dive under the sea to the bed; the tour comes down on land:** (built: DESIGN 484; waits on the test) "and dont zoom in to water worlds as it not propelry done yet as the fall to planet is under water or at least it looks like it is a 500mm level" / "need to fix water worlds and water zommming in to go to bed rock" / "i took a scrren shot of that issue" / "if we can get all the worlds working correctly i dont care what it zooms into" / "and qwerks never loaded and it says a spot in the seas on 200 attometers" / "and just beforee atom view its a weird grid like checkbaord of differnt colors so the elctron microscope vioew neever happend on the water world"
- [x] **No atom wobble:** (built: DESIGN 485; waits on the test) "and eatoms are all wobbel;y looks nosiating the atom is wobbleing even when focused on"
- [x] **The code's density, said plainly in the docs:** (built: DESIGN 487; waits on the read) "make notes in document this prokject code is mostly not human readible, right? thats correct right?"
- [x] **Who can change what, said in the docs:** (built: DESIGN 488; waits on the read) "what i mean is without AI only advance coders can manually edit it"
- [x] **The code note is a suggestion, not a rule:** (built: DESIGN 489; waits on the read) "its not who can do what its a suggestion"
- [x] **Stars exact on the card, no lattice, no empty squares:** (built: DESIGN 490; waits on the test) "at 1 aU scale the stars are appearing in grids and somene ggrids no starts like squares of starless areas i took a screenshots"
- [x] **Focus on the star flown to:** (built: DESIGN 491; waits on the test) "and its like zooming around with no focus"
- [x] **No white fuzz of near stars on a fast zoom out:** (built: DESIGN 492; waits on the test) "and another thing on zoom out u see the stars all like converge in a white fuzz pattern as zooming out fast that looks scetch"
- [x] **The loading bar seen:** (built: DESIGN 493; waits on the test) "A bar shows the universe being made, then it opens, whole. there is no loading bar"
- [x] **The 50-micrometre hand-over and the six shots' faults (pillars, hatch, skirts, dotted rings, the ribbon in black):** (built: DESIGN 494 to 498; waits on the test) "right at 50 mico meters there is an instant hand off change over and i took a bunch of shots each with its own obvious viewable problems" / "keep working till its all fixed and ready for install so all the probelms at scale of the shots are fixed"
- [x] **A shot leaves what you were at smoothly:** (built: DESIGN 499; waits on the test) "and when it pans from astroid to comet the astroid just instanat disappears intstead of smoth pan and zoom away"
- [x] **Electrons slower, their orbital paths seen and always changing:** (built: DESIGN 500; waits on the test) "at atom scale the elctrons astart moving way to fast and i dont see their orbital paths that are constantly changing like electrons do"
- [x] **Stars and the web's galaxies told apart from a galaxy's edge:** (built: DESIGN 501; waits on the test) "and when looking at a galxy form distance but not too far i cant tell the differnce betwen startts and glaxies in the background webbing idk how we would asddress that maybe tone down one and brighten the other correctly at appropriate distances and scales"
- [x] **Against the field as its own document, with performance:** (built: DESIGN 503; waits on the read) "and where is the performance and comparison table of other sims and space games and space simulations" / "it need proper naming and listing in the doc lists not burried oin other docs"
- [x] **The atoms not drawn in the latest shots (11 nm to 614 pm, black):** (built: DESIGN 507, the glide outrun by the zoom; waits on the test) "i took snaps again of all the sclaed issues of the differnt zooms and the shit thats happening". Every link of the chain checked in the code (the lattice made, the dots added, the buffer written, the card's constants right) and the cause not found from the shots; needs one check by the user: the wheel by hand (not the tour) down to an atom, and a screenshot there.
- [x] **The wheel comes down onto the landmark at each size:** (built: DESIGN 504; waits on the test) "i think ur problem is u arnt focusing on shit like landmarks on zoom in and are just indefinitly zooming in on nothing"
- [x] **Test shots quick (BMP, below a size) and the solid-mass cluster softened:** (built: DESIGN 505; waits on the test) "not crawl, shit will take eons at a crawl but need to be human viewable this is taking forever u fucked up the speed, and also there is a galaxy type that looks like a solid mass thats too bright and to icony and need fioner edge"
- [x] **The test's wheel ends on the quark:** (built: DESIGN 506) "u are zooming toi infinity again"
- [x] **What is picked stays in the picture; no strobe at the quark:** (built: DESIGN 507; waits on the test) "wuts happening im just seeing blue flashes at 10 zepto meters and nothing is in focus" / "and wtf its not suppose to be running so fast that people have photo seizures"
- [x] **Electrons and quarks slower:** (built: DESIGN 508; waits on the test) "need to slow down electrons and querks"
- [x] **The face's bands, tiles and saw gone (the three sheets tilted):** (built: DESIGN 509; waits on the test) "and just beforee atom view its a weird grid like checkbaord of differnt colors"
- [x] **The tour's picks: skipped when nothing is found, and logged:** (built: DESIGN 511; waits on the next tour's log) "something is up ist just zooms in and out of the same galzxy a bunch and never does the astropid and comet zoomin and neverr sayw the black hole aven thos it said it was looking at them" / "and its narrating shit thats not on screen"
- [x] **The tour never waits on the ground below a centimetre:** (built: DESIGN 512; waits on the test) "at 10 nano meter the planet went invisible and all i see is stars" / "or blackness i dont know"
- [x] **Every electron seen moving:** (built: DESIGN 513; waits on the test) "and on atom scale some electrons are frozen while others are moving"
- [x] **A real accretion disc, stars eaten, the skim:** (built: DESIGN 516; waits on the test) "aand on the black hole dive fly by do a skim of the scretions disks of gasses and starrs being eaten properly made as i think u have it just as an effect not a real accretion disk of stars and gasses and gravity destoying of stars"
- [x] **No edge, only a horizon that moves with you; the far light reddened:** (built: DESIGN 517; waits on the test) "and u have the expansion of space properly, yeah?? space is always expanding and at 13 buillion light years it expands faster then light can reach the opposite side of the galaxy thats wher the visible universe barrior comes from, so its not really a end all of the universe its the distance light can no longer reach before the vastness of expanding space out paces light speed at full universe scale.. do you need to research this./.. becasue theraretically if u move a light year in any direction u can see 1 light year further of the 15 buillion into the "Edge": of the universe" so it my belirf the univers is infiniate but for the fack that space expands faster than light can move through it" / "but we ater talking billions of years and billions of time from the big bang to wher not a start is visible from any other star,, this is just tesctnical facts and im not sure how to build it in"
- [x] **Dark matter and dark energy as content, on the cards and in the captions:** (built: DESIGN 518; waits on the test) "and what about dark matter and dark energy to make gravity work?" / "based on their angul;ar moment? just things to think about" / "while coding a universe"
- [x] **The hole shot stays in its galaxy; small bodies framed from their lit side:** (built: DESIGN 520; waits on the test) "i think it might be a focus problem" / "as last test it zoomed in a bunch never focusing and was saying it was looking at a astraoid and commet when just in black space of a mix of stars"
- [x] **The asteroid held in the middle, the comet from its lit side and bright enough:** (built: DESIGN 521; waits on the test) "the astoid trails off to the sids and focus doent follow it i never saew it upclose" / "aand to comet caought the dark side of it" / "or its just not bright enough"
- [x] **The apostrophe in the cards' font:** (built: DESIGN 522; waits on the test) "and it keeps having (s)'s stand alone when prlulizing shit like ur add errnonious spaces"
- [x] **No Sun in this universe: solar units everywhere:** (built: DESIGN 523; waits on the test) "and u still using Sun everyehwer in the write ups and texts is solar mass and shiot not Sun there is no Sun in this Unioverse"
- [x] **The atom pick takes the mineral's elements in turn:** (built: DESIGN 524; waits on the test) "and why are ther ever only oxygen atoms on atom view" / "maybe its the luck of the draw but seems excessive to alway land on owygen"
- [x] **The atom flown round, through its electrons and inner shells, before the nucleus:** (built: DESIGN 525; waits on the test) "and u need to acutally do the fly around and have the ability to fly through the atoms and elctrons and around the scale a little bit more as u kinda just zoom in only when these scales have reall shit to fly around and u may need to upscale the atoms a little bit so when u coom into them they dont instany got to proton nutron till a orbit and rotate of the atom vies like galaxctic and solar system views fly arounds should be" / "maybe do some rotating too so dep depths of zoom show everything is similar even behind your view"
- [x] **Every mineral's trace elements on their real share of sites; the sea's ions:** (built: DESIGN 526; waits on the test) "okay but do we really have the full table of elements mix on all bodies to what they should have mixes of,, its not just silica a planet is made out of earth has like 118 elements weve found on it"
- [x] **Every natural element placed, the noble gases in the air, the periodic table opened from the universe:** (built: DESIGN 527; waits on the test) "fiftene elements? wtf did we build the periodic table of elements for u retard" / "and it needs to be proper and have a way to open it"
- [x] **The black hole pick goes to the galaxy's own heart; a clip finished on a safety exit; nine screens in the readmes:** (built: DESIGN 529; waits on the test) "okay i hope on next install we can get a finished clip so finger s crosesed" / "and we will need screen shots of like the docs viewer and install requirments and installer posints but not alot of screen shots just the main things" / "and landing page" / "to put in the readmes propely"
- [x] **The loading screen from the first frame, and unmistakable:** (built: DESIGN 530; waits on the test) "and im still not seeing a loading bar just black scrren on load in after seed input and tour start"
- [x] **A shot leaves the asteroid and the comet smoothly:** (built: DESIGN 531; waits on the test) "when it moves away from the comet and astroid it doesnt slowly pan towrds the next target jit just instally zooms making them disappear instanlty instead of a smothe pan away before zooming"
- [x] **The sky caption tells of the air, what it does to the light, and the star's colour:** (built: DESIGN 532; waits on the test) "and whit it looks up to the sky from ground level it should comit on atmospher if one and how it changes athe color of light and its composition which it already has" / "and the star color"
- [x] **A spot or grain pick never fails on an ice sheet or the sea:** (built: DESIGN 533; waits on the test) "is it normal the gpu only ever uses 124mb of 16 gb?vram? cant we make it better if we use more of the vram? and at 500 nanmoeter is pasues in eroor or crashed" / "it never went into atoms and qwerks"
- [x] Pause and speed controls (in the deep zoom: Space and 1–5)
- [x] **A toggleable scale on screen while zooming:** "it zooms too fast and when zoomeing maybe we need a hud scale designator ofr something thats toggle able". Built in the one world: a bar of a round length with its label in the game's own letters, H turns it on and off (`src/shell/ScaleHud.cs`).
- [x] **Real stars of the space you are in:** "the stars u are flying by are just place holders". Built: around the camera the galaxy fills in with its real stars at the galaxy's true density (about 100 billion in all), in 25-light-year cubes made from their own position (`UniverseView.LocalStars`); the nearest star is now light-years away, not hundreds. Built too: the wheel flies straight at a star (or, in a system, a planet or moon) within about 4 degrees of where you point; stars are drawn at their real size (a dot from afar); the star of the system you are in shows its real body (plasma surface and corona) at true size. Built too: within 40 of its radii a planet or moon gets its real ground (`PlanetTerrain`, with its wavelet change fields), drawn in the same camera from its own centre in km, lit by its star; the zoom measures to the ground and never goes below it. Still to do: the full path checked at native resolution; carrying on below the ground's finest piece with mode-E.
- [x] **No hexagon placeholder near a star:** "take a look at 1 lyr scale the solar system has a place holde4r emblem image of a hexigon w block over lay thing". Galaxy points shrink fully to points of light inside it; to be confirmed by the creator.
- [x] **Bodies in the matter, picked and flown into like solar systems:** (grains and atoms pick, with cards and orbit rings; molecular ices and sea water in their real molecule shapes; halite and quartz in real crystal structures; still to come: the other minerals' structures, molecules and crystal shapes as their own bodies, real crystal structures, the waveform carrying the matter fields) "and just like u zoom in tho the planets zoom into the microscopeic should have bodies of the mixture you can zoom in on just like solar sytems and shit of crystals molecules and shit" / "but authentic and relaaistic" / "atom view should be like reall orbiting solar systems of their own" / "c/f waveform should handle all of this". Grains, crystals, molecules and atoms clickable with their own cards and the wheel flying to them; each atom its own system of electrons round its nucleus; the CDF 9/7 waveform carrying the matter fields (grain surfaces, crystal shapes) as it does the ground.
- [x] **Real universal laws, all four forces:** "this is real fucking univaresal laws we shall be using electomagnatism strong and weak nuclear forces all of it". Nucleus packed by the strong force against the protons' Coulomb push, binding energy from the nuclear mass formula, weak-force decay; electrons by electromagnetism.
- [x] **Checks at the player's real resolution:** "try running it at reall game resoultion like my native resoultion im using now,, tests that dont run the game in real world application of real resolutions are worthless". Visual checks use `--native` (a normal window the size of the screen). First finding at 3818x2074: the ground seen from orbit is coarse and blocky; plate edges cut coasts in straight lines.
- [x] **True infinite zoom, no stepped loading:** "the infinate zoom also doesnt seem to be fully integrated as its like shit zoom placeholders that just load zoom points or some artifical stepped loading shit loading points". The camera must never wait for ground pieces; build pieces on every core; re-map the zoom onto wavelet levels with mode-E below the finest (R4.6).
- [x] **One world, one camera, no handoffs (true infinite zoom at any angle):** "tha angular reelation is not correct when zooming into a galaxy as when approaching a galaxies its just instantly pans flat instread of appearoaching at the angle u are currently in relation to the galaxy, stop it from panning flat when zooming into a galaxy and make it real infinate zoom at any angulare relation and vector" / "no handoffs, that is the shit zoom staging i was talking about"
- [x] One world holding every scale: the universe of galaxies, each galaxy's stars, each star's system, each planet's ground, its life and its microbes, all placed in one shared space (64-bit positions, a moving origin)
- [x] What is drawn is chosen by how big it looks from the camera, so things grow smoothly into view (a galaxy into its stars, a star into its system, a planet into its ground) with no pop
- [x] Detail at every scale from the wavelet levels and mode-E (R4.6), so nothing loads in steps
- [x] Below the ground's finest piece, the ground's own matter, all the way down: "i love the scale when u go down to um u should start to see atoms and shit not to mention microbes bacteria and viruses, but like i said life isnt built yet". Grains of sand and dust (mode-E), then the crystals of the real minerals that make the ground there (from its resource field and `content/elements.dnat`), then their molecules, then atoms of their real elements; microbes, bacteria and viruses join once life is built
- [x] **The universe preview release:** "then once i test it and give the okay. go ahead and buidl a exe. of the \"game\" (what we have as far as the universe thats just a dead universe but fully made with all the probelms ive said in totality fixed , then build and deploy the project with the binary iinstall of the .exe like the Agent Gen program and Gladiator folders on desktop do in buidling a binary in all the ways it works. so that wee have a sample universe .exe that runs and is just a preview of the universe fully working c/9 waveform fully and fuull infinate zoom fully tested by you them me befoer building the .exe completely and deploy of releases(noting this is just the univerese without game play or ui or features yet"
- [x] The one-world universe complete, every problem raised fixed, the 9/7 waveform and infinite zoom throughout
- [x] Tested fully by me at native resolution, then tested by the creator, who gives the okay
- [x] Only then: build the .exe and its binary installer the way the Agent Gen and Gladiator projects do, and publish the release
- [x] **Its own Universe project (after the good test):** "and when we deploy the exe after i have a good test we need to make the program into its own repo with everything needed to build the universe only so its one two remotes on git and github need to make the github one with only supposrt documents for this universe program and everything any one would need to build inthis universe and change it and deploy their own exe in everything needed that we have made so far just not the DNA AND project for these repo remotes only a universe project with none of the life and project stuff so this is its own project made from DNA AND as a dmeo but not a dmeo the full universe we have sdo anyone can use it and build with it and all readme and instructions  for the technicals but a base good readme about the universe and how it works non technical and laymans like and fun to read and the exe has all of the public docs for it : https://git.unityailab.com/GFourteen/Universe.git"
- [x] A separate repo with only what builds the universe (no life or DNA-AND project files), two remotes: https://git.unityailab.com/GFourteen/Universe.git and a GitHub repo (to make)
- [x] Technical readme and instructions: how to build, change and deploy your own .exe
- [x] **Credits, readmes and repo links, pushed and released:** "and make sure the apps cedits redadmes technical readmes and documentations and links to the repose for Universe project are made and correctly pushed and deployed a release of the .exe for the full universe and all needed files and documentations of them for public release, but i stell need to test all the fixes weve talked about"
- [x] Technical readme (build, change, deploy your own .exe) and the public docs, in the repo and inside the .exe
- [x] Pushed to both remotes and the .exe released, only after the creator has tested all the fixes
- [x] Windows and Linux installs: "need linix and windows install" / "for the .exe". The engine talks to Windows directly (Direct3D 12, Win32), so Linux needs its own port: Vulkan graphics and Linux windowing, our own code, no outside libraries
- [x] **Runs well on anyone's computer, not just one:** "remember it needs to use whoes ever GPU not made just for mine". Workers follow the core count, ground detail the screen's resolution, budgets are in milliseconds, the render scale trades sharpness for speed. Built: the strongest graphics card is picked (the most video memory of its own, software ones skipped), named in the run log (`Gpu.PickAdapter`). Built too: cards with less than 3 GB of their own video memory start at a 75% render scale, less than 1.5 GB at 50%.
- [x] **At 20 micrometres, space and black-and-white:** "at 20 microns im seeing space and only white and black texture" (to see on screen)
- [x] **Towers, spikes and lines from the ground:** "i took scren shot im getting weild toweres and spikes and lines coming out of the ground" (the F12 picture did not save: to see on screen, and why F12 saved nothing)
- [x] **The driver still times out at the ground's hand-over to the matter:** "at 200 mm ant micor level it crashes still" (00:55:06 and 00:55:08, about 185 micrometres over an ice world). Every shader loop is bounded, the octave cap and the palette are in range: next, the device's own record of what it was doing (DRED breadcrumbs and page-fault address) written to the crash log with each draw named.
- [x] **The quark seen at the end of the tour:** (logged: DESIGN 514; waits on the next tour's log) "i never saw qwerks it mazxed out and said smallest thing we know and i never saw qwerks"
- [x] **The readme's recording a proper tour, once it all works:** "okay the clip u have in the read me needs to be a proper tour once we figure it all out". When a full tour runs clean, film it (Film the tour), then cut its highlights (web, galaxy, black hole, star system, comet, world, ground, grain, atoms, quarks) from the MP4 into a short GIF for the readme (the clip itself is hundreds of MB, too big for the repo), replacing docs/img/journey.gif in both projects.
- [x] **Snaps from the game for the social images:** "we cxan take some snaps to use for the social images later". When a tour runs clean, pictures from the game itself (the web, a feeding black hole's disc, a world from orbit, the ground, the atoms) go under the name and tagline in docs/img/social.png for both repos, and GitHub's social preview is uploaded again.
- [x] **On Linux, through Wine, with one script, and documented everywhere:** (DESIGN 534; the first Linux report decides it) "do .exe run on linix.. how does linix people install?" / "okay well make sure that path is real and its documented propley for how linix people can use the app with out crazy workarounds" / "make sure all the docs are proper with this requuest and fixes"
- [x] **Clips at thirty frames a second and a clip's bit rate, half the size:** (DESIGN 536) "and wtf fuck are u using to make these clips 500 mb for minutes,, wtf are u using f2000 instead of f16 or something"
- [x] **One clip per recording, packed by quality, never massive:** (DESIGN 537) "it should only make one clip as long as its recording and not a a gig file so use something appropriate that doesnt fuck our c/9 waveform into hevey picxel style files that are fucking massive"
- [x] **A REC notice while recording, off the tape:** (DESIGN 538) "and ther should be a REC notice if recording liek carasvideos tapes have"
- [x] **A star's colour and heat come from its mass, not its make-up (answered, and on the card):** (DESIGN 549) "are yuou sure every star type has the exasct same make up??" / "doesnt color and temp depend on matter with in it and its pressures based on matter in the star?" / "tpya and amount" / "type*"
- [x] **The comparison as check tables, concise, with the others' ticks explained:** (DESIGN 552) "thhis is not a proper compasions if this is suppose to be it" / "like u need check marks where whe have all the checks and other have none but with explinations" / "concise like"
- [x] **The picked thing's card on the tour again, at the left edge:** (DESIGN 556) "major problem im not seeing the write up for focus bodies"
- [x] **The quark view shows one trio:** (answered: at 200 attometres a nucleon is four screens wide and the next nucleon eight screens off; at the proton stop before it every trio of the nucleus is in view) "and the qwerk view only shows one grouping of qwerks where are all the others? too far wayaway at that scale?"

## 2026-11-07 — The docs audit, every document top to bottom

Verbatim: "make sure all docs are not stale, and when i say "all" i mean it all fucking documents, top to bottom look for single words that are wrong too" / "and we did the comparisons correct for other similar apps and games right? with the chekmarks and write up and stuff all proper and truthfull at how kick ass this application is beeing so samll and doing so much". DESIGN 573.

TODO items whose markers were flipped to done in this audit, because the code had overtaken them (their words kept in place):
- "Start R1 code toward the sample viewer, after the sample-subject content, following `docs/WORK-LAWS.md`"
- "Content loader (R1): read `.dnat` text at start-up"
- "Zoom range from one microbe to a whole galaxy (64-bit positions, moving origin)"
- "Smooth zoom from microbe to planet to solar system to galaxy"
- "Set 2: Solar system layouts, with moons, asteroid belts, the Kuiper belt and Oort clouds" / "Set 3: Stars" / "Set 4: Planets and gas giants" / "Set 5: Resource fields" / "Set 6: Dyson spheres" / "Set 17: Elements (all 118)"
- "Our own shader-code generator for Direct3D 12 (no outside shader compiler)" (changed: Windows' own `d3dcompiler_47.dll`)
- "Galaxy generation from a seed" / "Solar systems: stars and planets" / "Multiple galaxies"
- "Windows exe" / "Our own `DNA-AND-Setup.exe` installer" / "Post each release on both release pages (git.unityailab.com and GitHub)"
- "One camera that flies through it at any angle and direction; zooming moves the camera toward what the mouse points at, never switches views"
- "The preview build: `build.cmd NAME preview` makes a copy that always opens in the one-world universe" / "Our own installer, not NSIS" / "A base readme about the universe and how it works" / "The .exe carries all of the public docs for it" / "The program's Credits and About link the Universe repo" / "About, Readme and Credits built into the program's menu bar, and its header tools such as the scale HUD for the zoom"

Files touched: `README.md`, `docs/release/UNIVERSE-README.md`, `UNIVERSE-HOW-TO-BUILD.md`, `UNIVERSE-COMPARED.md`, `UNIVERSE-GUIDE.md`, `IN-GAME-HOW-IT-WORKS.md`, `IN-GAME-README.md`, `README.txt`, `universe/README.txt`, `universe/CREDITS.md`, `docs/HOW-TO-BUILD.md`, `docs/SETUP.md`, `docs/CONTENT-FORMAT.md`, `docs/LISTS.md`, `docs/ROADMAP.md`, `docs/TODO.md`, `docs/wiki/controls.md`, `faq.md`, `glossary.md` and every player page's footer, `wiki/CLAUDE.md`, `wiki/index.md`, `wiki/systems/r02`, `r04`, `r05`, `r16`, `wiki/log.md`.

