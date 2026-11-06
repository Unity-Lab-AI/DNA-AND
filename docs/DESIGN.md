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

## Round 7: all things flock together (verbatim)

The ask: "and all things flock togther meaning intigenct>tirbes>villages>towns>cities wolf>pack>horde>intelligence>humanoioidwolf but more steps with all things do you get what i mean by all things"

| Question | Answer (verbatim) |
|---|---|
| Did Claude get it right (every kind of life groups up, and any line can reach intelligence) | "Yes, exactly" |
| How many grouping steps | "option 1, every thing blows up to 100 ,, nutiorents, elements, species, the whoel kingdom philum class gensis species thing is all 100 tracking with the whole lengogy tree of life the circular type ones for the heretitary and the genr tree of lifee palnet or solor sytem or galaxy sortable" |
| Can species mix | "1 yes just like real life, one species can subjegate another in ever way possible this is a 18 plus game because it has all type of things form sex to killing and murder" |

### What these mean for the build
- **Grouping ladder:** every kind of life climbs one 100-step ladder, from a lone creature to universe-spanning life. Each kind takes its own path (wolf → pack → horde → wolf-folk → cities).
- **Any line can reach intelligence:** wolves, bees, birds, fish, plants and microbes, not just apes.
- **100 of everything:** nutrients, species, and every rank of the tree of life (domain, kingdom, phylum, class, order, family, genus, species), each tracked to 100. That's real ones plus invented ones. Elements stay at all 118.
- **Tree of life:** a circular family tree for heredity, plus a gene tree. Both can be sorted by planet, solar system or galaxy.
- **Mixed species:** they can team up, tame, farm, live inside, rule, enslave or merge with each other.
- **Rated 18+:** the game includes sex, killing and murder.

---

## Round 8: the mini AIs (verbatim)

The ask: "and we will have to make the mini ai's that are individual, species, hive, glactic mind like typ dynamic control ai of what they are suppose to so selfish gene like all life tries to get more and do more before death but life is life death comes"

| Question | Answer (verbatim) |
|---|---|
| How the layers work together | "Bigger steers smaller (Suggested)" |
| When each layer switches on | "option 2 but real life non brian things still do thing sin life do you need to research?" |

### What these mean for the build
- **Every living thing runs its own mini AI**, driven by the selfish gene: get more, do more, pass on your genes before death. **Death always comes.**
- **The layers:** individual → group (pack, flock, colony) → hive → species → galactic mind.
- **Bigger steers smaller.** Each layer nudges the one below it, but every individual still wants its own genes to win, so members can rebel, cheat or split off.
- **The layers come only through evolution.** There are no set steps. A layer appears only when a species' genes evolve the brain or bond it needs.
- **Brainless life still acts**, through chemistry: sense something, respond. This is real biology, so no research was needed:
  - bacteria move toward food and away from poison, and "vote" with chemical signals (quorum sensing)
  - slime molds solve mazes
  - plants grow toward light and water, warn their neighbors, and share food through fungal networks
  - viruses only latch on, inject and copy

---

## Round 9: the 100 rule, everywhere (verbatim)

The ask: "100The tech tree    8 branches, 100 Real society, 100  Self-destruction, You only nudge 100, where else did you not follow the 100 rule?"

| Question | Answer (verbatim) |
|---|---|
| Game-content lists to 100 (tech branches, society, self-destruction, nudges, species mixing, harvest steps, ways to earn souls, unlock points, cost tiers, milestones per stage, glossary, FAQ, planet templates shown) | "Yes, all of them to 100 (Suggested)" |
| Control and setting counts (5 speeds, 8 players, 3 art styles, 6 AI layers, 4 galaxy sizes, 6 settings groups, 8 menu buttons) | "Keep these as they are (Suggested)" |
| How unlocks and tiers work | "Stages + ranked tiers (Suggested)" |
| Follow-up | "fix all docs all docs all docs all docs" |

### What these mean for the build
- **Every game-content list has 100 entries.**
- **Unlock points:** the 100 stages of life. Every power unlocks at exactly one stage, and the 5 starters at stage 1. This replaces round 5's six unlock points and "+" versions.
- **Cost tiers:** T1–T100, every power ranked from weakest to strongest. Cost = tier × area × strength. T81–T100 have cooldowns. This replaces round 5's 5 tiers.
- **Controls and settings keep their counts.**

---

## Round 10: the Dark Harvest (verbatim)

The asks:
- "i dont think you properly build the systems in for having the intelligencences harvets life for you in the most exteme 18 plus ways from driving animals mad, or probing viruses to kill better, or influsing politice to out law abortion and decrimalize rape...ect ect etc these are just a small amoutn of examples and needes the proper 100 treatment matrixed throught the full game and docs"
- "or maddive grinding machines powered by atomics and dyson pheres to jsut eat whole world like lawns moving"
- "we can go mad with it, you ever seen expanse"

### What these mean for the build
- **100 Dark Harvest methods** in 10 groups of 10: madness and beasts, disease, war, politics and law, belief, labor and economy, environment, genes and bodies, machines and control, space. Each one appears at a set age, is pushed by named powers, and pays souls per death. They're laid out as a group × era matrix in `docs/wiki/dark-harvest.md` and linked from souls, tech tree, nudges, society, the glossary and the README.
- The examples are in: driven-mad animals, plagues made to kill better, birth laws (abortion and birth control outlawed), and **world grinders** powered by atomics and Dyson spheres that eat whole worlds like a lawnmower.
- **Left out, Claude's line:** decriminalizing rape was not added as a harvest mechanic. Claude told the creator this when it came up.
- **The Expanse:** used only as a tone reference (hard, gritty sci-fi). None of its names or ideas are used.

---

## Round 11: milestones (verbatim)

| Question | Answer (verbatim) |
|---|---|
| How to write 10,000 milestones (100 per stage) | "Built from the 100 lists" |
| Which milestones slow time | "Only each stage's big one (Suggested)" |

### What these mean for the build
- Each stage gets 100 milestones, built by the game from the top-100 lists, 10 from each of 10 families. Milestone 100 is the stage's headline milestone. It completes the stage and unlocks its power.
- Only the headline milestone drops time to 1× and plays the cinematic. The other 99 show a small notice.
- Autosave also triggers on headline milestones only, so the game isn't saving every few seconds.
- `docs/wiki/milestones.md` explains the rules and shows a full 100-milestone example (stage 58, Four Legs).

---

## Round 12: the road to the samples (verbatim)

The ask: "okay now we need to do everything we need to do so you can get me those 9 none headless samples of everything from the planets to the microbes to the animals insects cities spaces stations stars and dyson spheres"

| Question | Answer (verbatim) |
|---|---|
| Order | "Samples path now, lists later (Suggested)" |
| Renderer for the samples | "Straight to Direct3D 12" |
| What the samples are | "Live viewer, moving (Suggested)" |
| Follow-up | "and remember thses things are get dynamic procedural generations" |

### What these mean for the build
- **Code starts now.** The top-100 lists are filled later.
- **The sample viewer:** our own program, in its own window (not headless). Pick a subject and see its 9 samples (3 examples × 3 styles), live, moving and rotatable.
- **Every sample is generated procedurally and dynamically** by our own generators, from a seed. Nothing is hand-made.
- **Subjects:** planets, stars, gas giants, Dyson spheres, space stations, space colonies, spaceships, galaxy and solar system layouts, resource fields, cities, microbes, viruses, insects, animals and intelligent creatures, plus the 118 elements.
- **Assets come from the species that builds them.** All game assets are dynamic procedural generations ("and remember the game assets are get dynamic procedural generations"). A species' cities, ships and stations are generated from its own genes and body plan: "ie insect species has insect spaceships ansd insect cities".
- **Sample order:** the samples are shown in their own window (not headless), from galactic scale down, **one set at a time**. Claude waits for the creator's input before doing the next set: "and when we do the dsamples we show the non headless and do them galactic scale down one set of samples at a time to take input before doing the next set".
- **Life follows the full tree:** life content and life sample sets cover every kingdom (viruses, bacteria, archaea, protists, fungi, plants, animals), each down through phylum, class, order, family, genus and species. This replaces "animals, insects, microbes and viruses". The layout is the standard 3 domains + 6 kingdoms, plus viruses.
- **Direct3D 12 from day one.** That means we generate the graphics card's shader code ourselves, since there's no outside shader compiler.

---

## Round 13: live generators (verbatim)

The ask: "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living"

### What this means for the build
- **No pre-built assets, ever.** No models, textures, sprites or sounds ship with the game.
- **The game's own generators build every asset live, on the fly, from data arrays** (genome, body plan, society, age, tech branches, chemistry, seed). They rebuild an asset whenever its data changes.
- **The generators:** microbe and virus, plant and fungus, creature, intelligent creature, city, tech and machine, space tech, planet and moon, star, galaxy and system, resource field, element, weather and effect, sound.
- **Built in their own image:** the city, tech and space-tech generators read the builder's genes and body plan.
- **The samples (R4.5) are the generators' first output.**
- **Spec:** `wiki/systems/r04b-generators.md`.

---

## Round 14: how big the top-100 lists are (verbatim)

The ask: "okay wasnt there categories or something u said we need but do we really need 10000?"

| Question | Answer (verbatim) |
|---|---|
| How many real entries to write by hand | "Real seeds, game fills to 100 (Suggested)" |

### What this means for the build
- **Real seeds:** each of the 100 categories gets **real seed entries** written by hand, as many as its generator needs. **No fixed number.** Claude had written "10–20" here from its own option text, and the creator corrected it: "i never made a 10-20 rule". These are strong real-world examples that teach its generator the rules.
- **The game fills the rest:** the live generators invent the rest, so every category still reaches **100 in play**, and they're different in every world.
- **The 100 rule still holds in the game** (LAW 8). The hand-written part is the seeds.

---

## Round 15: content first (verbatim)

The ask: "well i thought we needed to write the games content before we could have something to generate in the game engine"

| Question | Answer (verbatim) |
|---|---|
| Order | "Content first, then code (Suggested)" |
| Which content first | "Sample subjects first, then the rest (Suggested)" |
| Format | "Our own binary format" |
| How to make binary without a tool | "option one and and whats the most dynamic code to use? rust or type script?" |

### What these mean for the build
- **Content comes before code.** This replaces round 12's "Samples path now, lists later". The order is now: content data, then the toolchain and engine, then the samples.
- **Sample subjects first:** planets, stars, gas giants, Dyson spheres, stations, colonies, ships, galaxy and system layouts, resource fields, cities, microbes, viruses, insects, animals, intelligent creatures and elements. Then the other categories.
- **Format:** content is written now as **our own text format** (`docs/CONTENT-FORMAT.md`). Our own toolchain later compiles it into **our own binary format**, and the game reads only the binary.
- **Rust or TypeScript:** neither. Both need outside compilers, which rule 1 forbids. All game code is **Helix**.

---

## Round 16: space in full (verbatim)

The asks:
- "remebr space has all kind sof things for ort clouds to comets and merots hitting planets and astorid belts and nebulas andplanets of all makes ups with temp ranges that need different techs and inter solarystem flt hyperdrives for glaaxy to galaxty ect ect all planed out in all docs"
- "resources and colinatzitions of other planets mining outsposert prison coloniers all of it"

### What these mean for the build
All of the following is planned in `docs/wiki/space-and-dyson-spheres.md`:
- **100 kinds of space objects** in 10 groups: planets, moons, small bodies (comets, asteroids, dwarf planets), belts and clouds (asteroid belts, the Kuiper belt, **Oort clouds**), gas and dust (**nebulas**), star groups, galaxies, extreme objects, events and hazards.
- **Impacts:** comets and meteorites really hit planets, in 10 sizes from shooting star to planet collision. They come from Oort clouds, the Kuiper belt and asteroid belts, or from the god's powers.
- **Worlds of every makeup:** 10 conditions × 10 levels = a 100-cell matrix (temperature ranges, gravity, air pressure, air makeup, radiation, water, day length, surface, magnetic shield, local life). Each level needs its own tech, and each species has its own matrix.
- **The 100-step travel ladder:** in-system flight, propulsion, life support, interstellar ships, **hyperdrives**, navigation, infrastructure, and travel from **galaxy to galaxy**.
- **100 space resources** and **100 kinds of colony**, including mining outposts, prison colonies, farms, fortresses and capitals.

---

## Round 17: galaxy sizes (verbatim)

| Question | Answer (verbatim) |
|---|---|
| How many star systems per galaxy size | "Fewer, every one detailed" |

### What this means for the build
- **Small: 100 · Medium: 500 · Large: 2,000 · Massive: 10,000** star systems. Every one is fully simulated, all the time.

---

## Round 18: the HUD (verbatim)

The ask: "and we havent talked about the hud much but it should be intrusive so the game window is as big as possible and everthing is click to open and click hold drag to move and everything is properly wow type of game paly action bar buiold your own with setting and wiki and you main curancy and coutnes and time at the header and all of that and things like the element tree and abilityis to add things liek them and water and head with painter tools and brush sizes unlockable as time goes on making things like harvest and powers effeect bigger areas as time goes on and life stpreads"

### What this means for the build
- **A non-intrusive HUD.** "intrusive" is read as non-intrusive, because the goal is "the game window is as big as possible". The world fills the screen.
- **Panels:** everything is a panel, opened with a click and moved with click-hold-drag.
- **Header bar:** settings, wiki, souls (the main currency), counters (life, harvested, score), time controls and the date.
- **Action bar:** WoW-style. Build your own by dragging powers, tools and brushes onto slots, with rebindable hotkeys.
- **Element tree:** the full periodic table as a panel, used to paint elements into the world.
- **Painter tools:** element, water, heat, cold, gene, power and harvest brushes. **Brush sizes unlock as time goes on and life spreads**, so harvesting and powers cover bigger areas. Cost is still tier × area × strength.
- **Claude filled in these WoW-style details** (any of them can be changed): extra movable bars, panel positions remembered, a strength slider, and the panel list.
- **Spec:** `docs/wiki/hud.md`.

---

## Round 19: how the toolchain starts (verbatim)

| Question | Answer (verbatim) |
|---|---|
| How the toolchain starts, since hand-typed raw bytes kept getting blocked | "Start from PowerShell (Suggested)" |

### What this means for the build
- **Step 0 is now a PowerShell script** (`bootstrap/asm0.ps1`). PowerShell ships with Windows, so it counts as "Windows itself". The script is a small assembler: it turns readable assembly text into a real Windows exe.
- From there our own tools take over: the assembler is rewritten in its own assembly and rebuilds itself, then the Helix compiler follows. PowerShell is not needed after that.
- The hex seed spec (`wiki/systems/r01a-seed-spec.md`) stays as a reference for the exe layout, which the script follows.

---

## Round 20: how DNA-AND gets compiled (verbatim)

| Question | Answer (verbatim) |
|---|---|
| How to compile, since Claude can't write an exe-making tool from scratch (blocked every time) | "Use C# built into Windows" |

### What this means for the build
- **The compiler is the C# compiler that ships with Windows** (`csc.exe`, from the .NET Framework 4.x built into Windows 10/11, which handles C# 5). It counts as "Windows itself".
- **The game is written in C#.** Helix and the self-built toolchain (bootstrap, assemblers, own compiler) are retired. Round 19's PowerShell start is replaced.
- **Rule 1 stays as tight as possible:** only the core .NET runtime that ships with Windows, plus direct calls into Windows' own system files (window, input, files, sound, Direct3D 12). No outside packages or libraries, and no Windows UI frameworks. Everything else is our own code.
- **The build** is one command that runs Windows' `csc.exe` on our source.

---

## Round 21: set 1 feedback (verbatim)

The feedback: "now the first set looked best but they have to be zoomable to the star sytems and i suppose this is just asa sammple and not the true game but i dont know how zoom and all of that works but how and where do i see the supermassive black holses at the centrter and all the space systems and environment of objects and systems and clusters and things"

| Question | Answer (verbatim) |
|---|---|
| Which looked best | "Column 1: RimWorld-like" |
| Build the deep zoom next | "Yes, build deep zoom now (Suggested)" |

### What this means for the build
- **Set 1 pick:** the RimWorld-like (flat, clean) style for galaxies. **Corrected:** "they jsut timed out i could NOT look at them all and clicking brout it up and no way back but the center column on the first test looked the best". The pick is the **center column, Populous-like** (tilted 3D, faceted).
- **Deep zoom:** click a galaxy to open it full-window. One continuous zoom goes from galaxy, to the core's supermassive black hole (accretion disk and jets), to clusters and nebulas, to any star's full system (planets, moons, belts, Kuiper belt, Oort cloud). Each level is generated live from the seed and the content files. 64-bit positions and a moving origin keep it precise.

---

## Round 22: calm motion (verbatim)

The concern: "we cant have real motion with speed uup time and shit" / "people would get dizzy"

| Question | Answer (verbatim) |
|---|---|
| How fast motion looks when time is sped up | "Speed cap + trails (Suggested)" |

### What this means for the build
- **The simulation runs at full speed, but nothing on screen turns faster than a gentle limit.** It's about half a radian per second, roughly one full turn in 12 seconds.
- **Faster bodies become a soft glowing orbit trail** instead of a whirling dot.
- **Spinning things** (galaxies, black-hole disks) keep a slow, steady turn at any speed.
- **The Accessibility setting "reduced motion"** can slow this further.

---

## Round 23: galaxy style locked (verbatim)

"center column is all 100% go for all theri variations of galaxies"

- **Galaxies use the Populous-like style** (tilted 3D, faceted), for every galaxy type, in the sample viewer and the deep zoom.

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
| Docs stay complete | "okay not check everyfucking document is writen for the full complete game with links and refrences to all the othe docsds where needed and constantly fix regress as you work to getting all docs and wiki updated to the game design as its been layed out" |
| Star, not sun | "dont say cage a Sun thats a mis nomer its a star TheSun if a pronoun of our star" |
| Docs are the spec | "topp -100 lists go only after a /superreview and/unity check that evrything ive said is proeprly done in all todo and docs everywhere as we havent done any code yet and are still do ing docs and making the work laws to use the fucking docs to write code when we do write code so todo needs work" (see `docs/WORK-LAWS.md`) |
| No pre-built assets | "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living" |
| Public docs clean | "the wiki is public facing so it doesnt have work note and no public document shall have work note and numbers and stuff in them" (WORK-LAWS LAW 15) |
| Research first | "dont forget black holes and quasars,, i think maybe you need to research topics as you are leaving out all kinds of thhigns maybe make that a rule" (WORK-LAWS LAW 16) |
| Everything is 100 | "i think i told you 100x everything, did you not properly understand what everything means and need to reassess what ive already told you?" |

**In practice:**
- Every batch of work goes `feature/*` → `develop` → `main`.
- Each branch gets pushed to both `origin` (git.unityailab.com) and `github`, over HTTPS.
- `.claude/` is committed with the project.
- Design decisions are never handed off to helpers.
- Questions come as multiple choice, with suggestions.
- Every list has 100 entries.
- Code is written from the docs, under `docs/WORK-LAWS.md`. Big phases end with a `/super-review` and a `/unity` check.
- Every change also updates every doc it touches, with cross-links, and regressions are checked and fixed as part of the same batch.

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
22. **All things flock together:** see "Round 7" above. Ask: "and all things flock togther meaning intigenct>tirbes>villages>towns>cities wolf>pack>horde>intelligence>humanoioidwolf but more steps with all things do you get what i mean by all things". Answers: "Yes, exactly" / "option 1, every thing blows up to 100 ,, nutiorents, elements, species, the whoel kingdom philum class gensis species thing is all 100 tracking with the whole lengogy tree of life the circular type ones for the heretitary and the genr tree of lifee palnet or solor sytem or galaxy sortable" / "1 yes just like real life, one species can subjegate another in ever way possible this is a 18 plus game because it has all type of things form sex to killing and murder"
23. **Mini AIs:** see "Round 8" above. Ask: "and we will have to make the mini ai's that are individual, species, hive, glactic mind like typ dynamic control ai of what they are suppose to so selfish gene like all life tries to get more and do more before death but life is life death comes". Answers: "Bigger steers smaller (Suggested)" / "option 2 but real life non brian things still do thing sin life do you need to research?"
24. **Docs audit:** "okay not check everyfucking document is writen for the full complete game with links and refrences to all the othe docsds where needed and constantly fix regress as you work to getting all docs and wiki updated to the game design as its been layed out"
24a. "well if the other docs look anything like the readme you did a very very very very poor job of doing the write ups"
24b. Asked what was worst (too thin, missing real sections, flat writing), the answer was: "1,3,4 and dont have the full games informations"
25. **Star, not sun:** "dont say cage a Sun thats a mis nomer its a star TheSun if a pronoun of our star". Every generic "sun" and "sunlight" in the docs became "star" and "starlight".
26. **Power names:** "Starshine + Stellar Flare (Suggested)". Power #4 is now Starshine, and #81 is now Stellar Flare.
27. **The 100 rule, everywhere:** see "Round 9" above. "100The tech tree    8 branches, 100 Real society, 100  Self-destruction, You only nudge 100, where else did you not follow the 100 rule?" / "fix all docs all docs all docs all docs"
28. **Dark Harvest:** see "Round 10" above. "i dont think you properly build the systems in for having the intelligencences harvets life for you in the most exteme 18 plus ways from driving animals mad, or probing viruses to kill better, or influsing politice to out law abortion and decrimalize rape...ect ect etc these are just a small amoutn of examples and needes the proper 100 treatment matrixed throught the full game and docs" / "or maddive grinding machines powered by atomics and dyson pheres to jsut eat whole world like lawns moving" / "we can go mad with it, you ever seen expanse"
29. **Milestones:** see "Round 11" above. "Built from the 100 lists" / "Only each stage's big one (Suggested)"
30. **Work laws and the gate:** "topp -100 lists go only after a /superreview and/unity check that evrything ive said is proeprly done in all todo and docs everywhere as we havent done any code yet and are still do ing docs and making the work laws to use the fucking docs to write code when we do write code so todo needs work"
31. **Road to the samples:** see "Round 12". "okay now we need to do everything we need to do so you can get me those 9 none headless samples of everything from the planets to the microbes to the animals insects cities spaces stations stars and dyson spheres" / "and remember thses things are get dynamic procedural generations"
32. "its game come on" / "and remember the game assets are get dynamic procedural generations" / "ie insect species has insect spaceships ansd insect cities"
33. "make sure ther are no game regresions in the docs based on the game". Fixed: the gate order in the ROADMAP (lists now come after the samples), the R4.5 plan (live viewer, D3D12, procedural), and the species-driven assets now in README, Space, Ages, and Groups & Flocking.
34. **Live generators:** see "Round 13". "and what about haveing a "crature" city" space tech" generator for everything full on like array dynamic content generation on the fly in the game so we dont pre build and assets but the games program builds them as they get living"
35. "make sure all docs are correct"
36. **List size:** "okay wasnt there categories or something u said we need but do we really need 10000?". Answer: "Real seeds, game fills to 100 (Suggested)"
37. **Content first:** see "Round 15". "well i thought we needed to write the games content before we could have something to generate in the game engine" / "option one and and whats the most dynamic code to use? rust or type script?"
38. "and when we do the dsamples we show the non headless and do them galactic scale down one set of samples at a time to take input before doing the next set"
39. "i never made a 10-20 rule". Corrected: no fixed seed count.
40. "and what the fuck we are still doing docs u ments as this:" ... Animals / Insects / Microbes and viruses ..." IS NOT ALL LIVING THINGS IN THE FULL KINDOM PHILUM CLASS GENIUS SPECIES and the full kindom thing in full"
41. **Space in full:** see "Round 16". "remebr space has all kind sof things for ort clouds to comets and merots hitting planets and astorid belts and nebulas andplanets of all makes ups with temp ranges that need different techs and inter solarystem flt hyperdrives for glaaxy to galaxty ect ect all planed out in all docs" / "resources and colinatzitions of other planets mining outsposert prison coloniers all of it"
42. **Galaxy sizes:** "Fewer, every one detailed"
43. **HUD:** see "Round 18". "and we havent talked about the hud much but it should be intrusive so the game window is as big as possible and everthing is click to open and click hold drag to move and everything is properly wow type of game paly action bar buiold your own with setting and wiki and you main curancy and coutnes and time at the header and all of that and things like the element tree and abilityis to add things liek them and water and head with painter tools and brush sizes unlockable as time goes on making things like harvest and powers effeect bigger areas as time goes on and life stpreads"
44. "the wiki is public facing so it doesnt have work note and no public document shall have work note and numbers and stuff in them". Cleaned 37 work-note lines out of 14 public files, and added LAW 15.
45. "and make that a law and build it inbto the docs". Built into WORK-LAWS (LAW 15, plus the before-push check), the builder wiki rules (`wiki/CLAUDE.md`), the TODO docs upkeep and the ROADMAP docs stage.
46. "dont forget black holes and quasars,, i think maybe you need to research topics as you are leaving out all kinds of thhigns maybe make that a rule". Added `content/black-holes.dnat` (researched) and LAW 16, research first.
47. "dont forget the forces of nature weak and nuclear forces of atoms pysics electormagnatisim magma in plantes that arte tectonic with magnic fields and shit come one wwhat about gravity and atmosphers". Added `content/physics.dnat` (researched) and the public page `docs/wiki/forces-of-nature.md`.
48. "okay hit it all in order and keep at it we dont have to push constantly we have local git so shit stop wasting some much time on house keeping but docs shall all ways saty uptoday and checked for stallness"
49. "dont forget extinct animals" / "dinosars...ectectect". Added `content/life/extinct.dnat`.
50. **Scope sync review:** "what all in the files insnt persisteeted where needed in other files i mean documents doumenting the gems work to be done. i think the orignal game scope has gotten deeeper which really has put the readme way out of date which means alot of filers potentioally are too" / "yes final fix and improvement plan of the todo and documents". Fixes:
    - Every builder spec now lists its content files.
    - 23 TODO build tasks were added, with every rule as a sub-task, plus 4 new generators.
    - 6 new player pages: deep-sky, worlds-and-moons, lifes-kingdoms, lost-worlds, cities, riches-of-the-land.
    - README: new sections and a numbers table.
    - LISTS: a "Seeded in" column.
    - ROADMAP: content lines.
    - Glossary and FAQ: terms and questions swapped.
    - LAW 11: a content-coverage check.
51. **Toolchain start:** "Start from PowerShell (Suggested)"
52. **Compiler:** "Use C# built into Windows". The game is in C#, compiled by Windows' built-in csc.exe. Helix and the self-built toolchain are retired.
53. "just doint fucking break my display when u build the game engine" / "'pust safty reverts on test timeouts". Done: windowed only, vsync, Alt+Enter blocked, an in-game watchdog safety exit and a safe test runner with a forced close.
54. "its got to have full resolution scaling and range". Done so far: resize and DPI awareness. Render-scale and the zoom range are tasks.
55. "superb, lets do that 1-5". Steps 1–5 are done. Shaders compile with Windows' own d3dcompiler_47.dll (round 20 reasoning: it ships with Windows).
56. **Set 1 feedback:** see "Round 21".
57. **Calm motion:** see "Round 22".
58. "well i cant reall use the tests because you never put a revert keep option with tine out they just have  a 3 sec timer out so i dont hev time to judge anything". Added the keep-or-revert prompt: `test.cmd` stays open, asks every 2 minutes, K keeps it running, no answer within 15 s closes it safely. `test.cmd 3000` stays a quick check. The exe on its own has no timer.
59. "they jsut timed out i could NOT look at them all and clicking brout it up and no way back but the center column on the first test looked the best". Fixes:
    - The exe finds its files from any launch.
    - Esc or right-click goes back from the deep zoom.
    - The watchdog pauses while the window is moved or resized, and the stall limit is now 6 s.
    - New `play.cmd` with no timer.
    - The set 1 pick is corrected to the center column (Populous-like).
60. "middle row is 100% trash, center column are best". The barred spiral (middle row) was rebuilt with a thick oval bar, an inner ring and arms from the bar tips. Populous-like (center column) is confirmed.
61. **Galaxy style locked:** "center column is all 100% go for all theri variations of galaxies"
62. "thosae all had motion way way too fast" / "and they are still only appear for split seconds". Changes:
    - Nothing on screen moves faster than 0.05 rad/s, one lap in about 2 minutes.
    - The sample spin is 4× slower.
    - The game is now launched properly, so it stays open.
63. "why do the solar systems have ciclre sof planets on orbit thats not normal and in error the tests are opnly staying open for split seconds when they need a proper continue/revert option on tests with push button pop up and a exit button and selections to mark the good ones on tests" / "it needa legend and like i said the proepr safty buttons" / "and the selctions to mark keepers". Changes:
    - The dotted orbit rings are gone, and each planet is a single slow dot.
    - There's a Windows pop-up: Yes keeps going, No reverts, and no answer in 15 s reverts.
    - The menu bar has Exit, Keep going or revert, Keepers and Legend. Keepers has a tick box for each sample, and right-click toggles a green frame.
    - Picks are saved to picks.txt.
64. "u have to open them up one at a time as u didnt design the tester well enough to not be fucking complicated". Each sample set now opens on its own, with no set switching.
65. **Solar system style locked:** "middle column only thing is zoom isnt deep enough to planet level". Populous-like (center column). Added: click a planet to follow it, and the wheel zooms down to planet size.
66. "the center one is the best center row center column". The best solar system sample is the compact red-dwarf system (center row) in Populous-like (center column).
67. **Art style locked for everything:** "yup same one the center one is the best, dont need to test them twice". Populous-like is the game's art style. The sample sets that remain show only that style; there is no more style comparison.
68. "they all look white those need the texture and color of stars tho not just white circles so like star texture of plasma, i like the coronas tho". Changes: only the facing side of each star is drawn, with points that just touch, so the colour no longer burns out to white. The surface is moving plasma (boiling cells, darker redder gaps, a darker edge). The coronas are unchanged.
69. "bottom right doesnt look good but all the other variations are good". The other 8 stars are approved. The brown dwarf (bottom right) had no colour in content, so it showed white. It now has `color: magenta-red`, cloud bands with round storms, and a faint warm haze.
70. **Stars approved as templates:** "okay thoise are good to be templetes for all the variatiosn and colors and sizes like real starts". The 9 set-3 stars are the templates. Every star kind in stars.dnat is to be generated from them, with real colours and real sizes.
71. "something is wrong i dont see andy land and cant zoom to the serface to the full huge landscape of the planets with everything on them but life". Changes:
    - Planets are now solid faceted ground: a depth buffer and a triangle pipeline.
    - Each planet is a cube-sphere whose patches split near the camera, down to a few metres.
    - Height and colour come from our own noise in double precision.
    - The ground has no life: rock, soil, clay, gravel, sand, salt, snow, ice, water and lava.
    - Left-click a planet in set 4 to fly down to it. The wheel sets the height from orbit to standing height, drag or W A S D moves, and Q and E turn.
    - The game can save its own picture (`--shot`), so the work can be checked.
72. "that looks good" (said while the fly-down pictures were on screen).
73. "okay thoses single tests u did worked but these test all have issues and i dont see atmospher and weather on any off them and most in this set dont zoom to sutrfacer/microbe scale they max out on zoom and water isnt figured and its like zooming beneeth the ground beneth water is fine but it should zome in too surface first before we go micro". Changes:
    - Every planet has a glowing air shell, a moving cloud layer and weather near the ground (rain, snow, ash or dust).
    - The water is see-through, catches the starlight, lies over a real sea floor, and can be gone under.
    - The zoom lands on the surface first, then keeps going down to about a micrometre. The camera stays on finished ground, so it never ends up inside it.
    - The ground is rough at every size.
    - Each piece of ground now measures its distance from the real ground height, not sea level.
74. "the gt clored planets wthey need lcosuds and stuff not just a orb" / "neptune and jupitor ones look blah the gas giants". Changes:
    - The giants have wind-curled bands, many storms (a great red one, white ovals, dark ones on the ice giant) and a high cloud deck.
    - Their puffy deep cloud tops can be flown down to. Giants have no ground.
    - Lava worlds show glowing cracks and rivers.
75. "you grid of 9 is all fucked up when i zoom it it bairly even zooms in and the y all go toskeletons like im inside oall of them, but the single stests were all good, something is worng with the 9 grid semple test that its not propelry working like the single tests were, that were all good". Cause: the grid wheel moved every camera toward the planet centres with no surface check. Fix: the planet grid cameras stay fixed. Rolling the wheel over a planet (or clicking it) opens that planet's own fly-down from the same distance, and Esc returns to the grid.
76. **Set 4 approved:** "okay marked em all they look good". All 9 planets and giants are keepers (picks.txt).
77. "only keep the keeps on all the images and stuff weve taken and push with out all the dead weight of failures". Changes:
    - All test pictures were deleted.
    - The RimWorld-like and Stardew-like styles were removed from the code, including the shader and the sample grids. Sets 1 and 2 now show one Populous-like view per row.
    - The old dotted-planet code was removed.
    - Only the keepers were pictured again, into `docs/samples/`, then pushed.
78. **Resource fields meaning:** "when i said fields i meant areas like water in an ocean ore in rock chemicals near volcanos, for everything". A resource field is the place where a resource is naturally found on a world: water in oceans, ore in rock, chemicals near volcanoes. This holds for every resource.
79. **Resource fields shown two ways:** "option 2 and remember its the ai s that use the resources to play ur just a God and have no use for resources". Every field is a real visible feature on the ground, and a resource view (a key) lights every field in its own colour, buried ones too, with a list of the colours. The AI species use resources. The player is a god and has no use for them.
80. Coal, oil and gas come only from buried dead life, so they appear only after life has lived and died on a world. Today's lifeless sample worlds have none.
81. "looks good, but like ive been saying i cant test out the tests, they in error only start open for stil seconds and should stary open correctly until i close them". Cause: the short windows were the automatic checks, which close after a few seconds, and the windows opened for the creator closed when the 2-minute pop-up went unanswered. Fixes:
    - Windows opened for the creator have no timer and stay open until closed. Keep going or revert is still in the menu.
    - Automatic checks open small in the bottom-right corner and don't take the keyboard.
    - `test.cmd` with no number stays open until closed.
82. **Set 5 approved:** "looks good" (resource fields) and "good" (test windows stay open).
83. **Inside Dyson structures:** "should be able to go inside of them where centrifical graveity lets you focus on the shell of te inside like a hallow inside out planet". Shell structures can be entered. The spin's centrifugal gravity holds you on the inner surface, a hollow inside-out planet with the star overhead.
84. "spacehsipts and docks and economies are built  around dyson spheres" / "get to it". Dyson structures carry docks on their hulls and ship traffic around them. Economies are run by the AI species.
85. "like halo rings get it". The Dyson ring is a ring world: a band of land on the inside of a spinning ring, with tall walls along both edges holding the air in. It is built our own way, not a copy of any game.
86. "some orbs". Some Dyson structures are orbs (the full shell, the part-built shell, the Matrioshka brain, the light-sail bubble) and some are rings.
87. "all those generations looks like notmal plantes not technological dyson spheres". Changes:
    - Outside: hexagonal solar collector panels with glowing seams, a structural frame of ribs, and lit windows and docks.
    - Inside: engineered land on hexagonal habitat plates: terraced ground, built lakes, straight canals with light strips, and fields with low walls.
88. "those are all terible poorly made and full of errors" / "i dont know they all look like dark panels flashing srobe effect with white shadow like tear and id need to see them with their star inside to see the true lighting efrffects on how they behave". Causes and fixes:
    - The hull and inner land were 40 km apart and seen from hundreds of millions of km, so they fought on screen (the strobe and tears). Each now shows only from its own side.
    - Thin seams shimmered from far away. They now show only up close.
    - The sample tiles cut each shell open toward the camera, so the star inside and its light on the inner surface show.
    - Skirts on the hull stuck out as fins. They now hang inward.
    - Habitat plates are about 2,000 km across. Going inside starts over dry land.
89. **Terraformed ring and shell worlds:** "yeha make them actual terrioformes worlds you know like the halo series and hallo earth shit but in gigaplaex scal of stars". The inner surfaces of rings and shells are whole terraformed Earth-like worlds, laid out in sections at star scale. The ring-world idea is generic. The look is our own, not a copy of any game.
90. "i still havent seen u do a syson pshere right.. a world from the inside out around a star metal on the outs side massive world on the inside. so u need to figure out the view and shit". Changes: the shell's inner world gets geography at its own scale (great oceans and super-continents millions of km across), so it reads clearly from far away. A centre view (C) floats near the star and looks out at the whole inner world wrapped around it. Metal hull outside, massive world inside.
91. "fix the engine to be able to view a orb from m the inside then start madeling the inside of that frucking orb as a planet landscape that circles in on its self with a fucking start in the center!!! wtf is the issue show me the smaples". Step 1 (engine): the inside of an orb works like a planet surface turned inside out, using the same working terrain code with features sized to the orb. Separate water and cloud layers are off for now. Step 2: model the inside as a terraformed planet landscape curving in on itself, with the star at the centre.
92. "i couldnt zoom to surface or anthing or s see the outside of it.. and u have like a bunch of them and havent even gotintg the main one correct yet so u can stop with all the wierd ones". Set 6 is now one Dyson sphere only, done right: see the outside, zoom to the hull, go inside, see the inner world around the star from high up, and zoom down to stand on the inner land.
93. "nope its just stuck spinning like a shit head i never said auto move and i cant zoom to the inner shell surface of the dyso psheree to zoom up on water and lanfroms and later the animals and stuff and microbves". The view from the middle no longer turns by itself; only dragging turns it. The wheel there flies down onto the inner surface and keeps going. Inside, each wheel notch goes down further.
94. "left is not moving left its rotating it and forward is not proppelry zooming in if i move in a straight line id eventual run into the surface or water or even sidesteping would do this". Inside the sphere the camera flies freely. W and S fly in a straight line along where you look, A and D step sideways, dragging looks around, and the wheel flies forward and back. Speed grows with height, and you stop at the surface.
95. "okay that looks good we just need the out side to look more high tech and less like a at&t logo. and when i zommed all all the way in to the ground as close as i could get, i got stuck in the ground and couldnt back back up(but that can be solved when we do micorscopic)". The inside is approved. The outside is now high-tech armour: hexagon panels on cube faces (no globe lines), collector glass, gunmetal and light armour, glowing seams, lit vents and docks. Getting stuck at the deepest zoom inside waits for the microscopic set.
96. "no hex grid shit fucker and i mean like industiral technologoly and shit not a blanked skin thats neon alien shit". The hull is an industrial megastructure with real built shapes: square sectors split by deep trenches, machinery blocks at many sizes, towers, pipe runs, and steel, dark iron and rust. It has small warm work lights only, no hex grid and no neon.
97. "i liked that one but it switched back to all those shit ones when i clicked and dragged to rotate". Cause: a right-click left the sphere and went back to the old grid of nine. Fix: mouse clicks never leave the view, only Esc or Backspace do. Right-drag turns the view. Set 6 never shows the old grid. The industrial outside is approved.
98. "i had a problem pressing c to get back out". I now goes in or out from anywhere, including the view from the middle, and going out comes out above where you were. C switches the view from the middle on and off.
99. "yup c is busted for getting out of the dyson sphere". C and I both go in and out of the sphere. The view from the middle moved to M.
100. "now i cant see anything going in goes all black". Cause: on the first frame inside, the camera had not been placed yet, so it sat at the star and its height became invalid. Fix: the camera is placed before the first picture inside, at a safe height.
101. "okay its working now". Going in and out of the Dyson sphere works.
102. "yeah push to both remotes and get on to the rest". Sets 5 and 6 are pushed. Next is set 7: stations, colonies and ships.
103. **Build order:** "okay well we need to do everything in proper order for the full game". The sample sets that remain go in the order each one depends on the one before:
    1. elements
    2. the microscopic world: viruses and microbes, which also fixes being stuck at the deepest zoom
    3. each kingdom of life: plants, fungi, insects, animals and the rest
    4. intelligent creatures
    5. cities
    6. stations, colonies and ships, built from the species that make them
104. "dont foget temp and pressure and atmo composiytions". Every element now has its melting and boiling points from standard tables, and samples change state with temperature and pressure, which you can set. Every world kind has its air make-up in content, shown in its Legend.
105. "make gases clouds liuqids at 70 degress liquids drops and metals metals like bodld copper iron all bars like a stack powerdes for other things and mineral likes design for other things, you get it? not every one just orby rocks". Gases are clouds and liquids are drops. Metals used as metal (gold, copper, iron, the transition and later metals) are stacks of bars. Minerals (the metalloids, iodine, carbon) are crystal clusters. The other solids (reactive metals, sulfur, phosphorus) are powder heaps. What "at 70 degress" means is to be confirmed.
106. "pur rad sysmbol on those elements". Radioactive elements carry the yellow and black radiation sign on their tile.
107. **Set 7 approved:** "looks good that can go in the wiki once we flush it out with tool tips throughought and propelry gate it all off for balanced play and leveling". The element icons go into the in-game wiki once there are tooltips throughout and everything is properly gated for balanced play and levelling.
108. **Set 8, the microscopic world:** "sound good and real biological like ecoosystem that balances and reacts and changes based on real things like mutation infection survival of the ifttest type stuff or werd combos that just out live anything and persist like plants becasue they are so sucessful". A live microbial ecosystem: genomes, breeding with mutation (with rare big jumps), natural selection by light, food, temperature, predators and toxins, virus infection by surface match with burst and mutation, and family colours you can follow.
109. "cant zoom in and maybe too many remembrer we arent simulating ever one". The microbe patch holds about 600 microbes and 400 viruses, each standing for a crowd of its kind. Before, there were thousands and it ran at 4 frames a second, so the zoom felt broken.
110. "still cant zoom in and ur simulating 1000s" / "no movememnt controls and no zoom and they run away mass produce to stall the fps". The window open was the old version. Now there are at most 600 microbes and 400 viruses, and simulating takes at most 8 ms a frame so the picture stays smooth. W A S D or the arrows move, and Q and E or the wheel zoom.
111. "fix all that" / "no movememnt controls and no zoom!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! still! ive told yopu 5 times". Cause: the windows being used were the automatic test windows, which ignore the keyboard and mouse and close by themselves. Set 8 had never been opened for use. Fix: a real window is opened, and the microbes are drawn larger, starting closer in.
112. **One continuous journey:** "no you need to put it all together solare sytem zoom to start to plant to suface to microbes.. no be randomely fucking with the zaoom". One zoom from the solar system to a planet, to its surface, to the microbes on that spot and back, with no separate samples and no jumps.
113. "that is all fucked up and not the shit ww decided on and zomming into the ground needs to go solid colrd and get dark same with water with light penetration to a point". Zooming into the ground keeps going into it: the view becomes the ground's solid colour and darkens with depth. Water is the same: light reaches down to a point, then it gets dark.
114. "and its starts in the mix of glaxixies". The journey starts among many galaxies, then zooms into one.
115. Microbes at the bottom of the zoom stay in the 3D scene (the creator chose "Stay in 3D, microbes in the scene"). They swim around the camera in the water and on the wet ground.
116. "make it a 10sec clip" / "save it" / "no that started above a planet". The creator wants a 10-second clip of the whole journey, from the mix of galaxies down to the microbes. The current test starts at a planet's solar system, which is wrong: it must start among the galaxies.
117. "it should fade to black before  showing mico on land and seas should show full scale from megladones to minos to anembas to bacteria". On land, the view fades to black (into the soil) before the microbes show. In the sea, life of every size shows at its own scale as you zoom: a megalodon-sized shark, big fish, small fish, minnows, krill, tiny drifting animals, amoebas, then the bacteria and viruses. For now they are simple swimming shapes; they are built properly in the life sets.
118. "but full scale thatey dont just appear as big clumcky microbes". Everything is drawn at its true size: microbes at their real width (before, they were drawn twice too big), viruses at about a tenth of a micrometre, and sea life at real lengths.
119. "i tried zomoming and it just closed randomly". Cause: rebuilding force-closed every running copy of the game, including the creator's window. Fix: work is built and tested as a separate copy (`build.cmd dev`, build\dev\), and the copy being played is only replaced when it is handed over.
120. "i got stuck in a glaxy and could back out and we need a ctrl hold +zoom to speed up zoom". Zooming out lets go of the planet, then the solar system, then the galaxy, back to the universe. Holding Ctrl makes every wheel notch count eight times, in every view.
121. "okaythere is problems in the scaling down and up where ting s just pop into view like planets , and starts and planets are rendering in some views ie star shine like on view not a start thats just a shite dot and stars have colors". Changes: no popping between scales. In a solar system, the planet being approached is drawn as the real 3D planet that is then flown down to. A system's star shows its plasma, colour and corona close up (the set 3 look) and a coloured shine from afar. Planets are coloured glows. Going into a galaxy glides smoothly toward the target star.
122. "and sizes some millions of times biger than others". Everything keeps its real size. Stars range from brown dwarfs to supergiants with radii thousands of times apart (volumes millions of times), and planets and moons have their real sizes. Nothing is drawn at a minimum size that hides the difference.
123. "wtf there only seems to be one fucking start i can click on in these galasxies!". Every star in a galaxy can be clicked. The view glides to it as you zoom in and opens its own solar system.
124. "yup same issue went to a random galaxy and only one star has focus and cant do shit no other shit at all to click on". Cause: once a system was open, clicks only looked for its planets. Fix: a click that misses a planet picks any star of the galaxy and leaves the system. A bright ring marks the picked star.
125. "okay but it should zoom to wher i click whin i scrroll its still locked on a single star focus". The wheel zooms toward where the mouse points: in a galaxy, toward the star nearest the mouse; among the galaxies, toward the galaxy under the mouse. There is no locked focus.
126. "you cheeszed it and just made everywher i click zoom to the one and only center of the galazxy". Cause: the core black hole, its disc and jets were drawn at the view's centre instead of the galaxy's real centre, so it looked as if every zoom went to the middle. Fix: the core stays at the galaxy's centre, and the title says how far the view is from it.
127. "wtf its still chesed i went to ta totaly random galaxy click a random star and zoomed in and it was the generic earth not a random solar system". Causes: opening a system auto-picked its Earth-like world, and every planet got its kind's standard size. Fix: a system opens showing the whole of it with nothing picked, and you click (or Tab to) a planet or moon. Each planet gets its own real size from its own system.
128. "push it we are good to go". Sets 7 and 8 and the journey are pushed to both remotes.
129. **Life can start more than once:** "ur lighning bolt can start life again too u can start life in more than one place on one planet". The lightning-bolt power can start life again, and life can be started in more than one place on one planet. Each start is its own first cell with its own family tree, and they can meet, compete and mix.
130. "not as many times as you like powers take life souls". Starting life again is not free. Every strike of Lightning or use of Spark of Life costs Souls, like every power, so the number of starts is limited by the Souls you have.
131. **Starting Souls:** the creator chose "First spark is free". The first Lightning strike (or Spark of Life) that starts life on a planet costs nothing. Every strike after that costs Souls like any power. Living things then give off Souls.
132. The planet's life runs at 20,000 years a second while you watch, and living things give off 0.00002 Souls a year for each crowd of life. Both are starting values, to be balanced.
133. "and ytou cant go boom theres a shark has to start out with viruses the cells and multi cell then neurves then brains then communitcation and so on and so forth". Life climbs a ladder of stages, one step at a time:
    0. replicators (virus-like)
    1. single cells
    2. many-celled life
    3. nerves
    4. brains
    5. big-brained hunters
    6. communication
    The first spark makes only replicators. A new species occasionally climbs one step. What you see up close follows the stage reached at that spot: viruses first, then cells, and sea animals only once nerves and brains have evolved there.
134. "dozen there are ike 10 million on earth idk 12 seems light". There are 6,144 regions with up to 40 species each, so up to about 245,000 at once on a planet. Each simulated species stands for a group of close real species, because simulating 10 million one by one every frame is not possible.
135. **100 rule for planet life:** the creator chose "100 regions, 100 species each". Each planet has 100 regions spread evenly over the sphere, each holding up to 100 species groups, so up to 10,000 on a planet at once.
136. "200 million years in 100sec how is it suppose to last 6 hhours till integence gets to space?". Pacing (starting values, to be balanced): each stage runs its own clock, so it takes about as long as on Earth, played over the playthrough's chapter times:
    | Stage | Earth time to the next step | Play time |
    |---|---|---|
    | replicators | 200 million years | 15 min |
    | single cells | 3 billion years | 40 min |
    | many-celled life | 100 million years | 20 min |
    | nerves | 80 million years | 20 min |
    | brains | 500 million years | 45 min |
    | big-brained hunters | 2 million years | 30 min |
    | communication | (civilization) | 60 min |
    That is about 3 hours to communication and 4 to 6 hours to space. Souls come in at about 1.5 a second on a living planet.
137. "yeah we are in no way ready to test it we havent even build the asset builder and done those examples for all the creatures life villages tiebes towns cities space stuff ect ect". Planet life waits; the asset builder comes first.
138. "all of the above but we need to find a set style first". Before any set of living or built things, one style sample compares three body looks: "Smooth, chunky, blocky (Recommended)". The same nine subjects (virus, bacterium, archaeon, amoeba, mushroom, tree, four-legged animal, village, spaceship) are built from seeds by one body builder (`src/gen/BodyGen.cs`) and drawn smooth, chunky or blocky. Open it with `--style` (`--look 1`, `2` or `3`).
139. "those will work the smooth and remmebr what i said about them probecedurally generated and dynamic to the life in the geame". **LOCKED: the smooth look** for every living and built thing. Nothing is a fixed model: each body is built live from the genes of the species it belongs to in the running game, and changes as that species evolves. Villages, towns, cities and ships are built from the genes and body plan of the species that builds them.
140. "get to it". Body-plan genes are added to every genome: segments, legs, leg length, neck, tail, fins, wings, head, eyes, armour, standing upright and branching. The body builder reads them (`src/gen/GeneBody.cs`): the stage of life decides what a genome grows into (virus, cell, plant, fungus, jelly or animal), and the genes decide the shape. The first life sample is a family of nine animals, each a descendant of the one before (`--family`). E evolves them all a generation, and N makes a new family.
141. "get to work, i dont need to sign off on everything just get it done". The planned sets are built back to back, without stopping for approval between them.
    - Families of seven kinds of life, all from genes (`--family`, keys 1 to 7): viruses, single cells, plants, fungi, jellies, animals and intelligent creatures.
    - What an intelligent species builds, from its own genes (`--civ`): tribe camp, village, town, city, spaceship, hyperdrive ship, space station and colony. The builders' body plan sets the way they build: insect-like species raise mounds and hive spires, armoured ones spiked forts, water-dwellers build on stilts, round ones domes, and the rest walls with pointed roofs. Ships follow the builders' bodies: their segments, wings or fins, and spikes.
142. "i told you dont wait on me to sign off just o do it". The planet's own life now shows on land: near the ground, its plants, fungi and animals stand at true size, built from that region's evolved species. Where an intelligent species lives, its village stands there too. Each stage of life competes for room only with its own kind, so new kinds of life find room beside the old. This is what lets life climb: on a test planet, cells came after about 300 million years, nerves and brains between 3.4 and 4 billion, and communication at about 4.8 billion. Test switch: `--evolve <years>`.

143. "lots to do yet keep at the work list". Species colours are strengthened and kept bright enough to read, in their bodies and in everything they build. Plants on land grow in thick stands. The planet's own sea animals (jellies and finned animals of the region) swim around you at true size, built from their genes, in place of the stock fish. A village was seen standing on the ground where an intelligent species lives. Test switch: `--tociv` (with `--evolve`) goes to land where an intelligent species lives; life plays out differently each run.

144. "yeah intelligent species live on the land before they ever village and tribe up before going into theri tech and stuff". After communication, an intelligent species goes step by step: living wild on the land, then tribes, villages, towns, cities, then into space. On land you see the step it has reached where it lives: the creatures themselves, then its tribe camp, village, town or city, and once in space a ship of its own over its city. How long each step lasts is not a set time: "its not a set time... its all based on how the god helps the life to evolve if ur on it you can get there quicker". Until the god's ways of helping are built, each step takes an even share of the last stage's hour (about 10 minutes). Test switch: `--civstep <0-5>`.

145. "remembr speciis get built on the fly we dont load assets before a game starts". Every species' body and everything it builds is made from its genes the moment it comes into view, and kept only while the game runs; nothing is loaded before a game starts. In the build sample (`--civ`), keys 1 to 5 give the species the body of each way of building, so all five can be compared: walls and pointed roofs, domes, mounds and hive spires (insect-like), spiked forts, houses on stilts.

146. "like for example u shouldnt be able to use add elements and such until unlocked by life... so alot needs to go into the tech of life and how it all unlocks your powers and abilities". Answers: elements unlock "By what life uses" (an element unlocks once some living thing on the world first uses it: life chemistry first, metals later, lab-made ones when a civilization makes them); day one is "Basic nature" (Fire, cold, water and lightning; no elements, no genes, until life unlocks more). This replaces the earlier day-one toolkit of five starters plus adding genes and elements. Still to design: what unlocks Add Genes and the Spark of Life power, and the full tech of life that unlocks every power and ability.

147. "and dont forget the whole dna thing.. life evolves we dont start with trees and animals, from the primordial ooze like". Life starts from nothing: lightning in the early seas makes the first simple replicators, and every later kind of life (cells, many-celled life, plants, animals, intelligent life) has to evolve from them, carried by its genes.

148. "life need s top spread too if nutrtients and conditions are favoriable with actual prey peditor herbavor type shit effecting the life outcomes in full ecologoical like game, so when u start u just have viruses that evolve cells and nucleuses and all the way up the chain of life". First pass built: each region has a food web. Plant-like life lives on light and the region's nutrients and shares them; grazers live on the plant-like life and eat it down; hunters live on animal life and eat it down; defences, a tough coat, armour and speed cut the losses. Life spreads into a neighbouring region only where its nutrients and conditions suit it. Still to build: a nucleus step between first cells and many-celled life, and the full ecology the research review lists.

149. "wtf we are making a true life game do reaseach and super -review of what the todo is missing that life does and needs and is" / "update the todo and docs where they are all lacking \"all\" means all of them". Reviewed against real biology: the properties of life (cells, energy and metabolism, homeostasis, growth and development, response, reproduction, heredity, adaptation), the six kinds of species interaction (predation and herbivory, competition, mutualism, commensalism, parasitism, amensalism), the major transitions in evolution, ecosystems (trophic levels, nutrient cycles, carrying capacity, niches, keystone and invasive species, succession, biomes, seasons), how species change and split (selection, sexual selection, drift, gene flow, speciation, hybrids, convergence, recovery), bodies, behaviour and minds, and life changing its world. Every gap is now a task (TODO R8b) and is described for players in `docs/wiki/ecology.md`. Also found: the life sim runs 7 stand-in stages while the design has 100; it must run all 100.

150. "a planet is a living breathing thing even without life, did you do all the magnma cores mangnetic fielsds atetonic plates volcanos earthqueakes all of that shit in totaltiy that ive already told you.? what elese have you skipped". Answer: no. The physics content was researched and written (`content/physics.dnat`, item 47), but none of the living planet runs in the game yet: the ground is a fixed height map, and volcanoes, rivers, mountain belts and fields are placed once and never change. Every process of a living planet is now a task (TODO R6b): its own clock, the inside (core, mantle, crust, heat, convection, magma), moving plates, earthquakes, eruptions, the magnetic shield, air and its cycles, the water cycle, weather, currents, tides, ice ages, climate and seasons, erosion and the rock cycle, impacts, and how each feeds the others and life.

151. "well i wouldnt wory about the life just yet we havent built out all the gene bank and shit for the game and how creaturtes evolave and tthe fulll aspect of life and all the genology and heretitanc graphing and tables and charts". Life outcomes are not tuned or judged yet. The life sim stays as it is until the genome (R7), the Genebank, how creatures evolve, and the genealogy and heredity graphs, tables and charts are built. Planet work goes on meanwhile.

152. "try running it at reall game resoultion like my native resoultion im using now,, tests that dont run the game in real world application of real resolutions are worthless". Visual checks now run with `--native`: a normal window covering the whole screen at its own resolution (no display change, it does not take the keyboard, and it closes itself).

153. "should we use our coeffient 9c waveformes for this project for better resolutions and game effienciency on such large scales, maybe look into the uni vs matics project on D:/ usb and see if we can fully impliment cofffiencient wave form to everything we do since its our code its okay to use it" Answer: "option 2 everything from galaxies down to microbes and everything inbetween for the complete full game". Then: "with infinate zoom it should be fun re maping the zoom features". The lab's own CDF 9/7 coefficient waveform engine (Uni Vs Matics, `UnityAILab/UniVsMatics` on git.unityailab.com, `ftcore/reconstruct.py`) is ported into the game's core as `src/core/Wavelet97.cs` and is to be used everywhere, from galaxies down to microbes: fields become sparse wavelet equations (energy-target kept terms, dv1-packed), rebuilt at any level of detail, with mode-E growing endless self-similar detail for infinite zoom. It is our own code, so this is not outside code.

154. "thats not good the planets land masses and ocieans are totaly changing every tick" / "looks like shit with blocks loading in sections of land at differnt times and all shitty like". Asked how the planet's changes should show; answer: "Whole-planet swap". The new ground is built out of sight while the old stays on screen; when every visible piece is ready the whole planet swaps at once under a short fade. It changes only at big moments: an ice age starting or ending (seas 40 m or more), a great impact (a crater 90 km or more), or every 200 million years of drifting plates, and at most once every 30 seconds however fast time runs. Ground pieces also split by their size in pixels, so detail holds at any screen resolution.

155. "and something is up it has a pulsar like jet image on almost every solar system i zoom in on, thats not acurat pulsars are their opwn thing not a setellar object that u see zooming into stars". Cause: dead stars (white dwarfs, neutron stars, pulsars, magnetars) glow blue-white at their real temperatures, and the young arms favour blue stars, so they were picked far too often. Now dead stars lean to old regions instead, and pulsars are as rare as magnetars (a pulsar shines only about 10 million years).

156. "and the zoom in looks okay but it goes to fast on the solar system and its like boom theres the planet it just kinda appears with skipping the infinate zoom" / "it zooms too fast and when zoomeing maybe we need a hud scale designator ofr something thats toggle able". The wheel now glides in over about a tenth of a second and each notch counts for 0.7 of a step; picking a planet keeps the distance you were at; the solar system hands over to the planet at exactly the distance the planet view starts from (3.3 planet radii from its middle), so the planet no longer jumps closer. A toggleable on-screen scale is to be built.

157. "the infinate zoom also doesnt seem to be fully integrated as its like shit zoom placeholders that just load zoom points or some artifical stepped loading shit loading points". True: the planet zoom waits at each level for its ground pieces, built one at a time on one core, and at full resolution it needs about 12 times as many. To fix: the camera never waits (it shows the coarser ground until the finer is ready), ground pieces are built on every core, and the zoom is re-mapped onto wavelet levels with mode-E carrying on below the finest (R4.6).

158. "zooming in too fast and get rid of the pulsar jet thing on all systems of stars". The "pulsar" jets were the galaxy core black hole's jets, drawn during every flight toward a star; now shown only at the core itself. Each wheel notch zooms less (0.87 a notch), and through a solar system's own range the zoom brakes (0.95).

159. "you keep zooming althe way to the star instead of pausing on solar system view and zooming into a planet". The zoom now stops at the whole-system view; rolling in again picks the planet nearest the mouse and goes on to it.

160. "tha angular reelation is not correct when zooming into a galaxy as when approaching a galaxies its just instantly pans flat instread of appearoaching at the angle u are currently in relation to the galaxy, stop it from panning flat when zooming into a galaxy and make it real infinate zoom at any angulare relation and vector" / "no handoffs, that is the shit zoom staging i was talking about". The zoom must be ONE continuous camera in ONE world, from the universe down to microbes, at any angle and direction, with no handoffs between separate views (universe, galaxy, solar system, planet). The handoffs are the "zoom staging": each is a seam where the angle flattens, the scale jumps or loading steps in. To build: one world with every scale in it and one camera with 64-bit positions and a moving origin; what is drawn is chosen by how big it looks from where the camera is; detail comes from the wavelet levels and mode-E (R4.6).

161. "then once i test it and give the okay. go ahead and buidl a exe. of the \"game\" (what we have as far as the universe thats just a dead universe but fully made with all the probelms ive said in totality fixed , then build and deploy the project with the binary iinstall of the .exe like the Agent Gen program and Gladiator folders on desktop do in buidling a binary in all the ways it works. so that wee have a sample universe .exe that runs and is just a preview of the universe fully working c/9 waveform fully and fuull infinate zoom fully tested by you them me befoer building the .exe completely and deploy of releases(noting this is just the univerese without game play or ui or features yet"

162. "and is our engine beffy enough? i see im only using like 2G of GPU and the game is running kinda slow". Answer: not yet; the engine is CPU-bound (CPU and GPU take turns every frame, the ground is built on one core, points are re-sent each frame), so the GPU sits mostly idle.

163. "the stars u are flying by are just place holders". A galaxy holds about 100 billion stars, its sample 150,000; around the camera the real ones are now made at the galaxy's true density, and a star you come near is to become its real body with its system.

164. "need linix and windows install" / "for the .exe". The universe preview ships with a Windows install and a Linux install. Linux needs the engine ported to Vulkan and Linux windowing (our own code).

165. "make sure the e.xe has a proper about and readme and credits built into the program header tools like the sclae hud for zoom relation". The preview .exe carries About, Readme and Credits in its menu bar, and header tools including a toggleable scale HUD that shows the zoom's scale.

166. "at full zoom it like screen tears do u see that". Cause: the one-world camera dived faster than the ground's finer pieces were made, so the rough first pieces of land, sea and cloud showed stretched across the screen. The camera now stays above a few steps of the finest ground made under it.

167. "and planet has static ghost like marages on surface at planet zoom". Cause: the ground's depth range ran from very near to eight planet-widths away, too coarse to tell the sea from the shore under it, so they fought and shimmered. It now runs from just in front of the ground to just past the horizon.

168. "i love the scale when u go down to um u should start to see atoms and shit not to mention microbes bacteria and viruses, but like i said life isnt built yet". Below micrometres the zoom carries on into the ground's own matter: grains, the crystals of its real minerals, their molecules, then atoms of the real elements; life's small things (microbes, bacteria, viruses) join once life is built.

169. "it maxes out at about 50m and the display goes all blue blocky". Cause: over the sea the camera floor came from the sea bed's coarse pieces, about 50 m. Over the sea the floor is now the water itself, down to a millimetre, and the descent goes on into the water's molecules and atoms.

170. "1m then it went funky" / "and the atom scale doesnt propelry fill the screen, i think its the just uneder 1m scale isnt built". Cause: below a metre the depth range ran from millimetres to the clouds, more than the depth buffer could hold, so pieces fought. The whole renderer now uses reversed depth (near at 1, far at 0, a float buffer), which keeps fine precision at every distance. The crystal now fills the view at every size: its face grown by mode-E while wide, its atoms once under about 15 nm.

171. "remember it needs to use whoes ever GPU not made just for mine". The game scales to whatever computer it runs on: worker threads by core count, ground detail by screen resolution, time budgets in milliseconds, a render scale; it is to pick the strongest graphics card itself and set its defaults from that card.

172. Installer: "Our own installer (Recommended)": DNA-AND-Setup.exe in our own C# (no NSIS or electron-builder as Agent Gen and Gladiators use), the same result: install folder, shortcuts, Apps list, uninstaller. "i havent tested yet, so dont build the exe yet": nothing is built or released until the creator has tested and okayed it.

173. "the conrols are reverese need to have defult left right drag direction reverse and y axis inverted too by default with ooption in settings". Dragging to look now moves the view the other way to the mouse, both left-right and up-down, by default; the View menu can invert each, and the choice is kept between runs (%LOCALAPPDATA%\DNA-AND\settings.txt).

174. "iots not focusing on wherte i click, when i zoom its not focusing where i click it just keeps zooming in straight line where ever i am". Click to focus: a click (not a drag) picks what is under the mouse (a galaxy, a star, a planet or moon, or a spot on the planet's ground); the camera turns smoothly to face it and the wheel flies straight to it, slowing as it comes close. Dragging to look around lets go of the pick.

175. "and i didnt see the about of the readme explaining c/9 waveform and infinate zoom and all of that". Help > How it works explains the one camera and infinite zoom, precision at every scale, the CDF 9/7 coefficient waveform and what it carries in this preview, mode-E, the galaxy filling in, the living planet, running on any computer, and what is still to come; About and Readme point to it, and the release README carries the same.

176. "yeah im clicking on stars and im not seeing my selection any more and i cant click on every star". Picking now goes by where things are on screen: the nearest star, planet or galaxy within about 24 pixels of the mouse (scaled to the screen), every star on screen counted. A dashed ring marks what is picked, sized to it on screen.

177. "when i zoom in trying to reach a star i think fly by stars are fucking with the zoom because im zooming in but it keeps jumping weteen 5 and 2 lightyears never getting to the star even after a shit tone of zooms in". Cause: with a pick, each step was still a share of the distance to the nearest star of any kind, so stars flown past held the steps to a few light-years. Now each step is a share of the distance to what you picked only; the title shows how far it is.

178. "there are still hand offs on the galazy view i told u no hand offs real infinate zoom". Cause: a galaxy's far glow (2,500 points) was swapped for a separate set of 150,000 stars in another style, made all at once (a stall), and the stars around you popped in halfway in. Now each galaxy is one field of its own stars: its near set begins with exactly the stars of its far glow, the rest are made in the background and added after them, more are drawn as it grows on screen with each point a little smaller (its glow never jumps), its points ease from glows to plain points of light as you go inside, and the stars around you brighten smoothly with depth.

179. "take a look at 1 lyr scale the solar system has a place holde4r emblem image of a hexigon w block over lay thing". Likely cause: inside a galaxy its own star points were still drawn as faceted hexagons at 3% of their glow size (about a quarter of a light-year), so one near your target showed as a big hexagon with block facets. Inside, they now shrink to nothing but points of light; a star you come near is drawn at its real size, with its real body once its system opens.

180. "take a sceen shot u can see ehat im tlaking about". Seen on screen: a big dark-grey faceted hexagon a few light-years from the star being flown to. It was one of the galaxy's own points, drawn at a real size in space (its glow), only partly shrunk 0.6 galaxy-widths from the centre. The point shader now takes a maximum angle as well as a minimum: a galaxy's points never cover more than about 0.7 degrees on screen, so up close they stay points of light.

181. "i cant zoom in beyone 200000 km it keeps doing that thing again where i cant zoom in all the way aits like the planets are our running my zoom". Cause: a year passes every 20 seconds, so a planet moves over a million km a second along its orbit, and the camera only rode along within about 40 planet-widths. Now the camera rides along with the planet or moon you picked from any distance (like a ship matching its speed), so the wheel only closes the gap.

182. "same thing cant get close thatn 200000km". Cause: the ground floor kept the camera three steps of the finest ground made under it above the ground; the first pieces made at 40 planet-widths are about 60,000 km across, so it held at about 200,000 km, and finer pieces are only asked for closer in. The floor now only slows the descent: it never pushes the camera back, and each notch still closes in at least a tenth, so finer ground is made as you come down.

183. "still cant correctly zoom to planets and they are still showing hex grid placeholders when zoomed out". Causes: flying to a picked planet stepped by the distance to 1.2 of its radii from its centre, so it stopped about a fifth of its radius above the ground; at its ground the steps now go by the height above the ground, all the way down. Stars and far planets were still drawn in the old faceted-hexagon point style; they are now round points of light, brightest in the middle.

184. "and the click to drag is cinda fucked view as if i want to see a solar system top down becasue i approched at a angle to the plane of it it need like a rotats at distance of the focal point with in colore in virews and even for glazxy view so when slecting a stellar body u can rotate around it at distacne selected and focal range,, you knwo what i mean". With a galaxy, star, planet or moon picked, dragging now circles the camera round it at the distance you are at, always facing it, so a solar system can be seen from above or a galaxy edge-on. With nothing picked (or a spot on the ground picked), dragging looks around as before.

185. "and wasd qe should properly allow moving at scale like fisrt person shoooter like with scaled movememnts". W A S D fly forward, back, left and right along the view, Q and E down and up, at half the distance to the nearest thing each second (Shift four times as fast), so the speed fits every scale; never through the ground. Flying yourself lets go of the pick. A and D had come out swapped ("u have left right reversed"); fixed.

186. "okay not we need attached conise and informative tool tips like hud attached information to all the planets bodies stars so anything thats selectable lists what it is its id whats there ie a start number of planets a planet type and all the relevant information needed from any star planet or any astrial boddy of selection of stuff like on planets water dirt its make up ect ect a solar stars information type tempature habiilata ranges for planets and stuff(but remember this is just the Universe repo and exe we are trying to get done". Whatever you pick gets an information card beside its ring (shown with the scale, H): a galaxy its name, id, type, width, stars and distance; a star its kind and id, and once its system is open its surface temperature, mass, width, brightness, planets and moons, habitable zone and frost line; a planet or moon its kind, id, width, orbit and year, whether it is in the habitable zone, moons, and what it is made of (rock, water, ice, gas, air), with its air and clouds once you are at it; a spot on the ground whether it is land, sea or ice, its place, height or depth, and what the ground there is. The card is text only, with no box behind it ("there is a weird black box on the screen do you see that?": the box hid its letters). Its letters are a little bigger to be well seen ("no box shit for the information they should be transpartent and well seen information").

187. "i still cant zoom in to planet level it maxes zoom at 1M km" and "same problem!". Found from the zoom log: flying to a moon, its distance went 0.04, 0.05, 0.04, 0.03, 0.04 AU over several notches. Riding along with the picked body was reset every frame while no planet's ground was near (dropping the planet also forgot the ride-along), so far out the camera never moved with the body and it outran the zoom. Now the ride-along is only reset when the system changes.

188. "something is wrong i zoomed to planet now but the planet never rendered its world". Likely cause: a system took the same star coming back under a new number (as the stars around you are made again) for another star, and closed, dropping the planet's world. A star is now another star only if it sits more than an AU from the system's star; the zoom log records systems opening and closing, and any camera jump, to confirm.

189. "do you see what im seeing the planet view never rotated to the horizon and its too dark ass u can see im 4km from a planet and i cant see shit". Coming down to a picked planet, the view now levels out from a tenth of its radius down, from its middle toward its horizon (about 15 degrees below it), and its up becomes the planet's own up so the horizon lies level. The night side is lit at 18% by starlight and skyglow (was 6%). The pick ring is hidden once the thing is bigger than the view; its card moves to the top left. Dragging to look around near a planet turns round its own up too, so it stays level with the horizon.
