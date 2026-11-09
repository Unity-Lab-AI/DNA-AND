# Now: where DNA-AND and Universe stand (2026-11-08)

> "go ahead and wrap up finish doc checks write Now .md showing whre we are , just minor bugs to find and pin point but its working rather well"

Working rather well. What follows is the state of things, what is checked, and the small bugs left to find and pin point. Details of every change are in [DESIGN.md](DESIGN.md) (entries 706 to 737); the open items are in [TODO.md](TODO.md).

## The clip (.cdf97w.webm)

Every filmed clip is one file, `name.cdf97w.webm`:

| Inside it | What it is | Who reads it |
|---|---|---|
| The front man | Our own VP9 video of the frames as drawn | Every player: Media Player, Discord, browsers |
| `universe.cdf97w` (attached) | The universe's own equations: the seed, the camera's track in the universe's coordinates, the scene's calls (about 1 MB for ten minutes) | Our program, which flies the universe again from it |

- **Size:** held under 850 MB for ten minutes (under Discord's 1 GB) by one account for the whole clip; still over at the coarsest quality, a frame repeats the last.
- **Encoded on the graphics card** inside each frame (DESIGN 733): the planes, the motion from the camera's equations, block and transform sizes, the format's own whole-number inverse, and the loop filter. Only the decisions and coefficients come back; the processor only writes them into the stream. If the card path cannot start, the processor encodes the clip as before.
- **Checked:** Windows' own decoder gives back exactly the card's picture (every luma sample the same) on filmed frames, the busiest at the coarsest quality included.
- **In a real run (16:55 tour):** 17,326 frames (9.6 minutes) filmed in about 8 minutes of play, 807 MB, 118 frames repeated to hold the cap. Before the card it filmed at about 3 frames a second.

## The encoder, as built

| Part | State |
|---|---|
| Motion | From the camera's equations, refined over nearby whole pairs of pixels (DESIGN 726, 732) |
| Transforms | 4 x 4 to 32 x 32, chosen block by block (DESIGN 730) |
| Loop filter | As the decoder runs it, level from the quantiser (DESIGN 731) |
| Card path | Bit-exact with Windows' decoder (DESIGN 733) |

Still to come for sharper clips at the same size: motion in eighths of a pixel (with the format's own filters), the coding odds adapted each frame, the directional intra modes in whole frames, and choices made by their true cost. The card's motion is simpler than the processor's: on the busiest frames a change frame is about a quarter larger at the same quality.

## The README videos

Five 30-second cuts of the 16:55 take, each under 10 MB, cut where the clip's equations mark a landmark and starting and ending in motion (DESIGN 735, 736): across the universe, into the black hole, planetfall, into the atom, out across the universe. They are GitHub's own uploads (players with full screen); the originals are in `docs/release/universe/media` (Universe: `docs/media`). They were coded again from the filmed clip, a copy of a copy, at the Half size it was filmed at; a fresh take at Full, or the cuts filmed straight from the equations, would be sharper.

## Minor bugs to find and pin point

| What | Where it stands |
|---|---|
| The README links matched to their cuts by name order | Check each caption against its video on GitHub; swap any that are crossed |
| The GitHub uploads seen by strangers | Check the README in a private window; if blank, post the issue they were dropped into |
| Clip size is set to Half in the settings (`clip-down = 2`) | Set Full (start page or View > Clip size) to film at 4K |
| A GPU hang after about 12 minutes of filming at 4K (DESIGN 722) | Not seen since the card path; watch for it on a long take at Full |
| The back-out from the quark (DESIGN 729) | Built: the held atom's height no longer adds the ground's; waits on a look at the pull-out through the atom, crystal and grain |
| The tour caption in the lower bar (DESIGN 727) | Built; waits on a look |
| A clip finished on its own thread when the tour ends (DESIGN 728) | Worked in the 16:55 run (saved whole, log clean) |
| A whole frame (one in sixty) takes 130 to 150 ms on the card | A small hitch every two seconds while filming; worth a look if it shows |

## Ship routine

Build, commit on `feature/planet-life`, merge to `develop` then `main`, push `origin` and `github`, `universe-sync.cmd`, commit and push Universe, `release.cmd` for both, upload the releases to GitHub and to Forgejo (`build/work/fj.py`). Work files live in `build/work` (ignored by git), never on the Desktop.
