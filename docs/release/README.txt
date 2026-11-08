DNA-AND 0.1.0 (universe preview)
Made by the Unity AI Lab team - https://unityailab.com

A preview of the universe of DNA-AND, a 3D god game of life: one continuous zoom from a sky full of galaxies, into a galaxy
and its stars, into a star's solar system, down to a planet's ground and on into its matter, to the atoms and the quarks inside them.

This preview is the universe only: a dead universe, fully made, with no life, gameplay or interface yet.
It opens on a landing page: type a seed (or leave it blank for a new one), Begin or Take the tour (T in the game).

Controls
  Click                   pick a galaxy, star, planet or a spot on the ground: the camera turns to it and the wheel flies to it
  Mouse wheel             fly in or out toward what the mouse points at (a star or planet near the pointer is flown at straight)
  Ctrl + mouse wheel      eight times as fast
  W A S D                 fly forward, back, left, right (speed scales to the nearest thing)
  Q / E                   fly down / up
  Shift                   with W A S D Q E: four times as fast
  Right click             let go of what you picked
  Space                   pause time, or carry on
  , and .                 time slower and faster (real time to ten years a second; Time menu)
  Left-drag               circle round what you picked, at your distance;
                          with nothing picked, look around (View menu: drag speed, show FPS, invert left-right or up-down)
  H                       the scale on screen, on or off
  T                       the tour on or off; carries a paused tour on
  R                       starts a paused tour again
  F12 / F9                a screenshot (PNG) / start or stop a clip (.cdf97w), in the captures folder
  View menu               drag speed, FPS with the GPU and CPU stats, render scale (lower is faster, higher is sharper), GPU use,
                          the clip's size (auto, full, half, quarter), the periodic table, the tour, screenshot and clip
  Help menu               Documents (what's here), Readme, How it works, Against the field, Credits, About
  Esc or Exit             close

Needs: Windows 10 or 11, 64-bit, and a graphics card that runs Direct3D 12.
The game runs in a window and never changes your display settings.

Uninstall: from Windows' Apps list, or "Uninstall DNA-AND.exe" in the install folder.

How it works
  One camera, infinite zoom: no separate views and no loading screens. Each roll of the wheel moves the camera a share
  of the way to the nearest thing, so the zoom slows by itself as things come close and never runs out.
  Precision at every scale: the camera keeps its place relative to an anchor (a galaxy, then a star, then a planet), and
  the screen's depth is kept in reversed floating point, so near and far never fight at any distance.
  The coefficient waveform (CDF 9/7, the lab's own Uni Vs Matics engine): a field becomes a short list of wavelet terms,
  rebuilt at any level of detail and saved small. In this preview it carries the planet's changing mountains and craters.
  Mode-E: where stored detail ends, new detail of the same character grows forever. Here it grows the ground's grains
  and the face of its crystals.
  A galaxy fills in with its real stars, at its true density, as you fly through it.
  A living planet, even dead: plates, volcanoes, quakes, weather, ice ages and impacts on the planet's own clock.
  Still to come: the waveform carrying every field from the cosmic web to the microbes, life, gameplay and the interface.
