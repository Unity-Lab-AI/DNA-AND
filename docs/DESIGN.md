# DNA-AND — Game Design (from the creator's own words)

This file records what the game is, in the creator's exact words. Nothing here is invented. Where something hasn't been decided yet, it says **OPEN** and goes on the question list at the bottom.

---

## The pitch (verbatim)

> "okay we are going to be making a full 3-d world game where u start on a random procedurally generated full massive wolrd with everything(when i say everything i fucking mean everything) that a wrold has on it like earth and we can build some other templete so these world will be mine craft big will be a game where u build life and we have a fully dynamic ggenology with microbes and viruses and plants and animals and insects and intelliegents life and civilizastion and space expansion to dysonsphere all with dynmaic play and genology and genome of all creatures liveing and even viruses all doing what they need to do and shit like the more dadvanced they get the more things they can sdo and this is like a simulation but with god mode to where you can progressively unlock things to help life or hinder it or help you manage it better and it has a leader board in some whay so that once we build the exe like the past project like gladiator we can have everyoner that plays attribute to the leader boards and be listed in the Genebank with maybe the top three or so being able to uplaod a genome for other players to add to theri game when playing and its like a simulation with time and pasue all balances to run like reall time scales and can back off on the speed once certain mile stones are met like walking or fire making or agriculture or space age or dyson sphere all with other planets capable to spead life to but to get to other planbets intelligent life has to get there first and if all life dies maybe you lose idk maybe not i like it open ended."

## The rules of the project (verbatim)

> "No dependancies!!!! WE DO NOT USE ANOYONE ELSE CODE< SCRIPTS< PROGERMAES< BINARIES <EXECULATEBLES< TANBLES< DATA< FILES< FOLDERS< DIRECTORES< OR COMPOLIERS<<< SECOND RULE: YOU do NOT add shit that you were not told to add, but you DO build out everything required to make what i say a reality. Rule THREE(THE MOST IMPORTANT!): YOU do not make shit up! you always use ask me question and you leave the tecnial bullshit out of it and talk plain english and to the point."

The workflow template (`.claude/`, the template docs and installers) is project tooling, not part of the game: *"thats the templete stuff and out side of the project"*.

---

## Decisions so far

| Topic | Decision (verbatim) |
|---|---|
| How the exe gets made | "we make it" |
| Platforms | "all three" (Windows, Mac, Linux) |
| Leaderboard / Genebank host | "https://jsonbin.io/app/dashboard" |
| Size | "obviously galaxy size to buidl dyson spheres and ingalactic war and technology trees for the life" |
| The world | "EARTH(but other templets too you can unlock after reaching certain points on Earth." |
| Graphics | "ut will be a AAA game graphics that can do all the microbes and viruses an d gene mapings and peices for every traight and skill and evolutionary thing like breathing air to walking upright to photosynethesiis" |
| Other worlds | "solarsystems in the galaxy." |
| How life thinks | "they will be like ants ais with genes that use thatev eveold or been infected with u know EVOLUTION" |
| Space layout | "stars+planets>solar sytems> galaxys" |
| DNA | "Real-style" |
| Crowds of creatures | "wher they are on the planet can be animate by what they are living in that area liek 1 worm can be 4396 worms and i suppose its possible to cover the planet win worms but unlickely with mutation and viruses and breeding if not asexual and such but that all needs worked out." |
| How life starts | "life starts base on your first action. AND THIS IS NOT A MOBILE CLICKER  bullshit" |
| God powers | "basic god stuff that can effect life u know im sure youll figure it out all scaled for the entire game think populous form back in the day but gneological twists" |
| Points | "points are amount of life in your galaxie(S!)" |
| Editing creatures | "no direct edit besids using powers to influence things to happen" |
| Gene powers | "maybe yea a inset ones uve unlocked but has differnt outcomes good and bad depending." |
| Game length | "i dont know a full game should take about 4-6 hours to get a civ to space im thinking" |
| Milestones | "well for each stage of life all has its miles stones and intelligent life has technology and culture and war and tribalisze and pollitics and stuff like real life does" |
| Control of civilizations | "only every nudge evrything but u get the power of god to basicly do everything peopel attribute god doing." |
| Self-destruction | "yes thats the whole point alla life can die and intelligent life has amazing ways of killing its self" |
| Scoring | "combination of all of them." |
| Cheating | "no cheats will NOT work its fine." |
| Genebank genomes | "no its added to the oceans or land via meteorite" |
| Menus / saving | "yeas a normal game landing a nd wiki of everything setting quit." |
| Players | "single/coop/multi player see each others cursors all working to one goal." |
| View | "3d top down world with god mode space mechanics" |
| Runs as | "nothing of this game runs in a html its its own system and program" |
| Scope of the plan | "the todo for all of this shall be emense as all of the systems and trees and proceduaral dynmamic templets all nedd to be figured and the arrays and the graphics and 3d top down world with god mode space mechanics. dso the todo needs made and all the docs and wikis and how to and setup and readmes" |

---

## Open questions

None right now. Rounds 1–5 below answer everything asked so far. New questions go to the creator as multiple choice, and the answers get added here word for word.

---

## Answers, round 2 (verbatim)

| # | Question | Answer (verbatim) |
|---|---|---|
| Q1 | Build tools / which system first | "windows only for now" |
| Q2 | Use only the operating system and graphics driver | "sure" |
| Q3 | jsonbin as the only outside thing | "it only thing allowed but lets keep it as a side car so if needed we can pop something else in there." |
| Q4 | Gladiator | "look on desktop it in the folder called gladiators" |
| Q5 | The "everything" list | "do top 100 for everything from viruse types, aminals, insects, fish, birds, environmets, senses, abilities, and not limited to thed bit to incluse the top one hundered top onehundreds" |
| Q6 | One player hosts multiplayer | "yes" |
| Q7 | Losing | "open ended till u resarts to get a new world and galaxy" |
| Q8 | God powers | "they all need their usesas and pluses and minuses in some cases raise temp with fire lower with cold add water to flood sun to dry it up solar flare to fuck with tech, ,ect ect ect ect ect ect ectr x100" |
| Q9 | Wikis | "both" |
| Q10 | Fix commit history / credits | "yes and this deserves the unity ai lab team credits and website .com with the git and github links" |

### What these mean for the build
- **Windows only for now.** Mac and Linux stay on the roadmap for later.
- **The only outside software:** Windows itself and the graphics card driver.
- **The only outside service:** jsonbin. It plugs in as a swappable piece (a "side car"), so another service can replace it later without touching the rest of the game.
- **Gladiators** is a web page inside an outside program (Electron) with many outside packages. DNA-AND can't be built that way. What carries over is how it ships: a single setup exe on a release page, and a README with a banner and screenshots.
- **The lists:** the top 100 categories, each holding its top 100 entries (`docs/LISTS.md`).
- **God powers:** 100 of them, each with uses, pluses and minuses (`docs/POWERS.md`).
- **Multiplayer:** one player's computer hosts.
- **The game never ends** until the player restarts with a new world and galaxy.
- **Two wikis:** one inside the game, one for us builders.

---

## Answers, round 3 (verbatim)

| Question | Answer (verbatim) |
|---|---|
| Real or invented list entries | "real and dynamic invented combo procedurally" |
| How powers are paid for | "i like that maybe u have to harvet life and later on u get the intelligent sure intelligences using their powers to harvest life for you endgame kinda wiked liek the intelligences goal is to harvest life for its needs and since all life is like a virus advanced forms seprad and use up avasil nutrientrs and resources and have to spread. jsut liek real life" |
| Mills in the credits | "Alfreddo is Mills but he goes by Alfreddo so use that." |

### What these mean for the build
- **Entries are a mix:** real-world things, plus new ones the game invents as it runs.
- **Powers are paid for by harvesting life.** Later, intelligent life uses its own powers to harvest life for you. In the endgame, intelligent life harvests life for its own needs. Like a virus, advanced life spreads, uses up nutrients and resources, and has to keep spreading.
- **Credits:** Alfreddo stays as Alfreddo (he is also Mills).

---

## Answers, round 4 (verbatim)

The ask: "okay we need to write all the wikis and docs and readmes for the entire project in full to complete final endgame and play through completely in full no text wealls easy and fun to read beautifully written and well designed and formated with tool tips and everything like a AAA game using all the icons and images of the game in the docs with links and relseas link at top of readme and everything as if the game was full and complete wee will make everything but the geam itself yet"

| Question | Answer (verbatim) |
|---|---|
| Undecided details | "option 2. but use this way to give me suggestions" (ask first, using multiple-choice suggestions) |
| Images | "well obviousle we will need to design the #D genrator and botting of them". Confirmed: "Yes, exactly". Image slots now, filled later by our own 3D generator and an auto-capture tool. |
| Release link | "should be a release page on both with link to it in the readme first thing" |
| Lists and powers | "Use them as is" |
| Controls | "Mouse + keys (Suggested)": WASD or screen edges to pan, scroll wheel to zoom from microbe to galaxy, right-drag to rotate, left-click to use a power |
| Time | "Pause + 5 speeds (Suggested)": pause, 1x, 10x, 100x, 1,000x and max, with an automatic slow-down at each milestone |
| Main menu | "all of the above and wiki and settings, i already told you this and credits": New World, Continue/Load, Multiplayer, Genebank + Leaderboard, Wiki, Settings, Credits, Quit |
| Stages of life | "More, smaller stages", then "option two, but i think i told you 100x everything, did you not properly understand what everything means and need to reassess what ive already told you?" (100 stages; Earth follows them, and other planet templates can evolve in other orders) |
| Civilization ages | "Real history, fine-grained", then "see prvious answer" (100 ages) |
| Harvesting | "option 1 but harvet has negatives like its slow and freaks out intelligent life if seen but things like a volcano kicking off a city will make lots of souls" |
| Starting powers | "option one but things like add genes add elements ect ect real life so full perioditic table of elements" |
| Currency | "Souls" |
| Leaderboard score | "life in your galaxy and life \"havested\"" |

### What these mean for the build
- **Every list has 100 entries:** stages of life, civilization ages, powers, and every category.
- **Souls are the currency.** Living things slowly give them off. Harvesting by hand is slow and scares intelligent life if they see it. Disasters that kill many, like a volcano hitting a city, give lots of souls.
- **Starting tools:** a few starter powers (lightning, heat, cold, water, spark of life), plus adding genes and adding any element from the full periodic table (all 118).
- **Leaderboard score** = life in your galaxy + life harvested.

---

## Round 5: suggestions decided

The ask, verbatim: "you need to answer all the aBCD ..mds for best answers for everything based on beding A AAAA Game like nothing ever seen on earth before"

Gee handed these choices over, so Claude picked the best AAA option for each. Every pick can be changed at any time.

| ID | Topic | Decision |
|---|---|---|
| A1 | Galaxy sizes | Small, Medium, Large, Massive |
| A2 | Hotkeys | Space = pause, 1–5 = speeds, Esc = menu, a wiki hotkey. **Every key can be rebound** |
| A3 | Souls | Intelligent deaths give more souls. Harvesting shows as a visible glow that nearby intelligent life can spot |
| A4 | Lab-made elements | Elements 93+ unlock once a civilization first creates them |
| A5 | Planet templates | Ocean, Desert, Ice, Jungle, Volcanic, Tidally locked and Gas-giant moon come first. The full set is 100 (LISTS #62) |
| A6 | Multiplayer | The host shares a join code. Up to 8 players |
| A7 | Genebank top three | **Counted weekly: "Genome of the Week"** |
| A8 | Saving | Autosave every 10 minutes and at every milestone. Unlimited slots |
| A9 | Settings | Graphics, Sound, Controls, Gameplay, Online, **plus Accessibility** (colorblind modes, text size, subtitles, motion) |
| B1 | Stage unlocks | Each stage unlocks the powers that fit it. Stronger "+" versions unlock later |
| B2 | Milestone slow-down | Time drops to 1x with a short cinematic popup. Can be turned off in Gameplay settings |
| B3 | Souls from milestones | None. Souls come only from life, deaths and harvesting |
| B4 | Collapse | Civilizations can fall back to earlier ages |
| B5 | Tech needs | Each discovery needs the one before it on its branch, plus links across branches |
| B6 | Harvest steps | Farming 14 → Spirit/sacrifice 11 + Kingdoms 26 → Factories 64 → Writing DNA 85 → Terraforming 94 → Intergalactic 100 |
| B7 | Ages on other planets | **Other templates can run the ages in their own order**, like the stages |
| C1 + S13 | Power costs | **Base tier (💠 to 💠💠💠💠💠) × area × strength.** Six unlock points: Start, Life, Land, Mind, Civ, Space |
| C2 | Cooldowns | **Bigger powers (tiers 4–5) have cooldowns**, smaller ones don't |
| C3 | Chapter timings | Menu 5m, dead planet 20m, spark 15m, microbes 40m, seas 40m, land 45m, mind 30m, civilization 60m, space at about 4–6 h |
| C4 | Example run | Yes, clearly labelled as one example run |
| C5 | Fire and Heat | One power: Fire is the toolkit's heat power. The list stays at 100 |
| C6 | Spark of Mind | Unlocks when life reaches land. Only works on species close to using tools |
| C7 | Genebank meteorite | Available from the first-life stage on |
| S1 | Bootstrap seed | A tiny hex loader typed in as raw bytes |
| S2 | Language style | Low-level and C-like, with manual memory and no garbage collector |
| S3 | Language name | **Helix** |
| S4 | Memory | Big blocks reserved up front per system |
| S5 | Threading | One worker per core, pulling from a shared job queue |
| S6 | Precision | 64-bit numbers plus a moving origin |
| S7 | Random numbers | Our own seeded generator |
| S8 | Graphics | Direct3D 12 (part of Windows) |
| S9 | Planet grid | Equal-size cells covering the sphere |
| S10 | DNA storage | 2 bits per letter |
| S11 | Save format | Our own versioned binary chunks |
| S12 | Population groups | One group per cell and species. A group splits when its genes drift, and individuals appear when you zoom in |
| S14 | Time scale | Each stage has its own years-per-second, tuned so a game reaches space in 4–6 hours |
| S15 | In-game wiki | Reads the same pages as `docs/wiki/` |
| S16 | Network | Our own reliable messaging over UDP |
| S17 | Side car | Its own small program on the same computer |
| S18 | Secure connection | TLS 1.3 |
| S19 | Installer | Our own `DNA-AND-Setup.exe` |
| S20 | Capture tool | A mode of the game exe |
| S21 | Requirements | Min: Win 10/11 64-bit, 4 cores, 8 GB RAM, D3D12 GPU with 4 GB. Recommended: 8+ cores, 16 GB RAM, 8 GB GPU |
| S22 | Source layout | One folder per roadmap system (`src/toolchain`, `src/platform`, …) |
| S23 | Disk space | Measured once the first build exists |
| S24 | Uninstall | Keep saves and ask whether to delete them |

---

## Round 6: art style samples (verbatim)

The ask: "remembr we arent using other peoples shit unless we absoulutley have to so plan on buiolding it all and things like ingame microbes viruses and intelligernt creature should all have samples 1,2,3 made in 3 diffeernt styles 1,2,3 for a hand ful of things in the game before we go with any one style and same for things like the galaxy layouts solarsystem layouts and planets and stars and dyson spheres and space ships and space colonires ands space stations and rouseource firleds and gas giants and athe whole periodic table of elements"

| Question | Answer (verbatim) |
|---|---|
| How many pictures per thing | "3 samples × 3 styles = 9 (Suggested)" |
| Which 3 styles | "not one thats too gpu intense and these will be living things in the environment moving and spreading as told before, so come up with the best options idk try a rimworld like and a, populous like, and a stardew like, idk figure it out, but we cant do em till you build tyhier egine and shit" |
| When | "Right after basic graphics (Suggested)" |

### What these mean for the build
- **No art style gets locked in until the samples are compared.** For each subject there are 3 examples, each drawn in all 3 styles, so 9 pictures per subject.
- **The 3 styles,** all drawn originally by us and never copied:
  1. **RimWorld-like:** clean, flat, top-down, readable shapes with simple shading.
  2. **Populous-like:** low-poly 3D terrain seen from the god's view, with chunky sculpted land.
  3. **Stardew-like:** warm pixel art, cozy and colorful.
- **Light on the graphics card.** Thousands of living things move and spread on screen at once, so whichever style wins has to stay cheap to draw. "AAA" means the polish and the feel, not heavy graphics.
- **Everything is built by us.** The samples come from our own 3D generator ("we cant do em till you build tyhier egine"), right after the basic renderer works.
- **Subjects:** microbes, viruses, intelligent creatures, plus a handful of other living things, galaxy layouts, solar system layouts, planets, stars, gas giants, Dyson spheres, spaceships, space colonies, space stations, resource fields, and all 118 elements.

---

## How we work (verbatim)

These are the creator's working rules for this repo, each in his own words.

| Rule | Verbatim |
|---|---|
| Branch flow | "and when u do stiuff in batches u need to do the full feature, develop, main cascade" |
| Both remotes | "on boith remotes" |
| Branch order | "use the right proceude freature>develop>main and the bolock should be fine" |
| Push order | "fix them so we can make the pushes like i said feature>develop>cascade" |
| `.claude/` is tracked | "this .claude is allowed to be pushed jsut like every project we put it in" |
| Git over HTTPS | "u have to use https im signed in right now" / "try again use https im signed in now" |
| Decisions stay in the main session | "stop u dont outsource importatn decistions to a fuucking agent thhat knows nothing of the project!" |
| Suggestions as multiple choice | "option 2. but use this way to give me suggestions" |
| Nothing from outside | "remembr we arent using other peoples shit unless we absoulutley have to so plan on buiolding it all" |
| Everything is 100 | "i think i told you 100x everything, did you not properly understand what everything means and need to reassess what ive already told you?" |

**In practice:**
- Every batch of work goes `feature/*` → `develop` → `main`.
- Each branch gets pushed to both `origin` (git.unityailab.com) and `github`, over HTTPS.
- `.claude/` is committed with the project.
- Design decisions are never handed off to helpers.
- Questions come as multiple choice, with suggestions.
- Every list has 100 entries.

---

## Conversation record (every message, verbatim)

Everything the creator has said about this project, in order. Short process replies are included too, so nothing is missing.

1. **Project setup:** "import the :https://git.unityailab.com/UnityAILab/UAL-ClaudeWorkflow so we have the templet files heree in this project, i havent told you what the project is about yet, but go ahead and set up the import and here will be the repo for the project: https://git.unityailab.com/GFourteen/DNA-AND and you will make one on git hub in the Unity ai lab account as Gfourteen for DNA-AND as well"
2. "u have to use https im signed in right now"
3. "and when u do stiuff in batches u need to do the full feature, develop, main cascade"
4. "on boith remotes"
5. **The rules:** see "The rules of the project" above.
6. "1. im not ther yet 2. thats the templete stuff and out side of the project". This answered "what is DNA-AND?" and "keep the template's atree.exe?"
7. **The pitch:** see "The pitch" above. It ended with: "now you shoudl have a million questions for me"
8. "to write up the todo". This explained what the questions were for.
9. **Round 1 answers, in full:** "1. we make it 2. all three 3.https://jsonbin.io/app/dashboard 4. (thats a stuppid question) obviously galaxy size to buidl dyson spheres and ingalactic war and technology trees for the life 5. EARTH(but other templets too you can unlock after reaching certain points on Earth. ut will be a AAA game graphics that can do all the microbes and viruses an d gene mapings and peices for every traight and skill and evolutionary thing like breathing air to walking upright to photosynethesiis 6. solarsystems in the galaxy. they will be like ants ais with genes that use thatev eveold or been infected with u know EVOLUTION 7.stars+planets>solar sytems> galaxys 8. Real-style 9. wher they are on the planet can be animate by what they are living in that area liek 1 worm can be 4396 worms and i suppose its possible to cover the planet win worms but unlickely with mutation and viruses and breeding if not asexual and such but that all needs worked out. 10. life starts base on your first action. AND THIS IS NOT A MOBILE CLICKER  bullshit 11. basic god stuff that can effect life u know im sure youll figure it out all scaled for the entire game think populous form back in the day but gneological twists 12. points are amount of life in your galaxie(S!), 13. no direct edit besids using powers to influence things to happen 14. maybe yea a inset ones uve unlocked but has differnt outcomes good and bad depending. 15. i dont know a full game should take about 4-6 hours to get a civ to space im thinking 16. well for each stage of life all has its miles stones and intelligent life has technology and culture and war and tribalisze and pollitics and stuff like real life does 17. only every nudge evrything but u get the power of god to basicly do everything peopel attribute god doing. yes thats the whole point alla life can die and intelligent life has amazing ways of killing its self 19. combination of all of them.20. no cheats will NOT work its fine. 21.no its added to the oceans or land via meteorite 22. yeas a normal game landing a nd wiki of everything setting quit. single/coop/multi player see each others cursors all working to one goal. the todo for all of this shall be emense as all of the systems and trees and proceduaral dynmamic templets all nedd to be figured and the arrays and the graphics and 3d top down world with god mode space mechanics. dso the todo needs made and all the docs and wikis and how to and setup and readmes and nothing of this game runs in a html its its own system and program"
10. **Round 2 answers:** see "Answers, round 2" above.
11. "use the right proceude freature>develop>main and the bolock should be fine"
12. **Round 3 answers, in full:** "1what categories. 2. real and dynamic invented combo procedurally 3. i like that maybe u have to harvet life and later on u get the intelligent sure intelligences using their powers to harvest life for you endgame kinda wiked liek the intelligences goal is to harvest life for its needs and since all life is like a virus advanced forms seprad and use up avasil nutrientrs and resources and have to spread. jsut liek real life 4. Alfreddo is Mills but he goes by Alfreddo so use that. and those are not the only two fuckign option fix the .claude dumb ass u wront and imported the fucking hooks just fix them so we can make the pushes like i said feature>develop>cascade and this .claude is allowed to be pushed jsut like every project we put it in"
13. "how do i edit it then"
14. "okay shit go". This came after adding the force-push permission.
15. "try again use https im signed in now"
16. **The docs ask, and round 4 answers:** see "Answers, round 4" above.
17. **Round 5 ask:** "you need to answer all the aBCD ..mds for best answers for everything based on beding A AAAA Game like nothing ever seen on earth before"
18. "stop u dont outsource importatn decistions to a fuucking agent thhat knows nothing of the project!"
19. "you need to make the todo and docs all of them proper"
20. **Review ask:** "then /unity so she(our in house special info handler codex) can make sure nothing waas left out in all the verbatium words and answers given and shew knows me best"
21. **Style samples ask:** see "Round 6" above. Answers: "3 samples × 3 styles = 9 (Suggested)" / "not one thats too gpu intense and these will be living things in the environment moving and spreading as told before, so come up with the best options idk try a rimworld like and a, populous like, and a stardew like, idk figure it out, but we cant do em till you build tyhier egine and shit" / "Right after basic graphics (Suggested)"
