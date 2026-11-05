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

## OPEN questions (waiting on the creator)

These are tracked in `docs/TODO.md` under "Questions for the creator".

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
