// DNA-AND · shell · the words of the universe preview's Help menu: About, Readme and Credits.
namespace DnaAnd.Shell
{
    internal static class PreviewText
    {
        public const string Version = "0.1 (universe preview)";

        public const string About =
            "DNA-AND " + Version + "\n\n" +
            "A preview of the universe of DNA-AND, a 3D god game of life: one continuous zoom from a sky full of galaxies, into a galaxy and its stars, into a star's solar system, down to a planet's ground and on into its matter, to atoms.\n\n" +
            "This preview is the universe only: a dead universe, fully made, with no life, gameplay or interface yet.\n\n" +
            "Everything is made live from a seed by our own code: no outside code, no stored pictures. It is one camera with true infinite zoom, no separate views and no loading screens; detail comes from our own coefficient waveform engine (the CDF 9/7 wavelet) and endless self-similar detail (mode-E).\n\n" +
            "Help > How it works explains the waveform, the infinite zoom, how precision holds from galaxies to atoms, and the living planet.\n\n" +
            "Made by the Unity AI Lab team.\nhttps://unityailab.com";

        public const string Readme =
            "How to use the preview\n\n" +
            "Click a galaxy, star, planet or a spot on the ground: the camera turns to it and the wheel flies straight to it.\n" +
            "Mouse wheel: fly in or out toward what the mouse points at. A star or planet near the pointer is flown at straight.\n" +
            "Hold Ctrl with the wheel: eight times as fast.\n" +
            "Drag with the left button: look around (the View menu can invert left-right and up-down).\n" +
            "H: the scale on screen, on or off.\n" +
            "View menu: the scale, and the render scale (lower is faster, higher is sharper).\n" +
            "Esc or Exit: close.\n\n" +
            "What you will see\n\n" +
            "The galaxies, each of its own kind. Close to one, its own stars fade in, about a hundred billion of them, made around you as you go.\n" +
            "Near a star, its solar system: its planets, moons, belts and the star itself.\n" +
            "Near a planet, its real ground, its seas, clouds and air. The planet is alive even with no life on it: plates, volcanoes, quakes, weather, ice ages and impacts run on its own clock.\n" +
            "Below the finest ground, its matter: grains, the crystal of its mineral, then its atoms.\n\n" +
            "How it is done (the waveform, the infinite zoom and the rest): Help > How it works.";

        public const string HowItWorks =
            "How it works\n\n" +
            "ONE CAMERA, INFINITE ZOOM\n" +
            "There are no separate views and no loading screens between scales. One camera flies through one world that holds every scale: the universe of galaxies, each galaxy's stars, each star's system, each planet's ground, and the matter it is made of. Each roll of the wheel moves the camera a share of the way to the nearest thing, so the zoom slows by itself as things come close and never runs out.\n\n" +
            "PRECISION AT EVERY SCALE\n" +
            "A single number cannot hold both the width of the universe and a millimetre. The camera keeps its place relative to an anchor (a galaxy's centre, then the star of the system you are in, then the planet you are at), so everything near you is worked out exactly, whether you are light-years or atoms away. The screen's depth is kept in reversed floating point, so near and far never fight, from a micrometre in front of you to across a galaxy.\n\n" +
            "THE COEFFICIENT WAVEFORM (CDF 9/7)\n" +
            "The lab's own wavelet engine, from Uni Vs Matics, the same wavelet JPEG 2000 uses. A field of values (heights, colours, densities) becomes a short list of wavelet terms: the strongest are kept until almost all of the field's energy is captured, and packed small. Coarse terms are the big shapes and fine terms the detail, so a field can be rebuilt at any level of detail, edited by changing a few terms, and saved small. In this preview it carries the planet's changes: its mountains wearing down and rising, and its craters, over its six cube faces.\n\n" +
            "MODE-E: DETAIL THAT NEVER RUNS OUT\n" +
            "Where stored detail ends, mode-E grows new detail of the same character, forever: Newton's method on z cubed minus K, carried on into a 3D cubic surface, textured by cellular noise whose cells divide by ten-fold steps as you zoom. In this preview it grows the grains of the ground and the face of its crystals, below the finest ground.\n\n" +
            "A GALAXY THAT FILLS IN AROUND YOU\n" +
            "A galaxy holds about a hundred billion stars; it keeps a sample of 150,000. Around the camera, the real ones are made at the galaxy's true density, in cubes about 25 light-years wide, each made from its own position, so they are the same stars whenever you come back.\n\n" +
            "A LIVING PLANET, EVEN DEAD\n" +
            "Every rocky planet runs on its own clock: its heat inside runs down, plates move, volcanoes erupt and quakes shake at real rates, the magnetic field flips, the air and climate follow real chemistry, ice ages come with the orbit's wobbles, mountains wear down, and rocks from space strike. Its ground changes all at once at big moments, never piece by piece in front of you.\n\n" +
            "YOUR OWN COMPUTER\n" +
            "The game picks the strongest graphics card in the computer, builds the ground in the background on every processor core, sizes its detail to your screen's resolution, and starts softer on cards with little video memory. The render scale in the View menu trades sharpness for speed.\n\n" +
            "STILL TO COME\n" +
            "The waveform carrying every field from galaxies to microbes (gas and star fields, the ground itself, clouds, stars' surfaces); life; gameplay; and the interface.";

        public const string Credits =
            "Made by the Unity AI Lab team\nhttps://unityailab.com\n\n" +
            "GFourteen (Gee): co-founder, engineer, financial advisor\n" +
            "SpongeBong (Sponge / hackall360): co-founder, engineer, ethical hacker, sys admin\n" +
            "Alfreddo: engineer, agentic systems, researcher\n" +
            "Red: engineer, security, sys admin\n\n" +
            "The coefficient waveform engine and mode-E come from the lab's own Uni Vs Matics engine.\n\n" +
            "Unity AI Lab Git: https://git.unityailab.com\nUnity AI Lab on GitHub: https://github.com/Unity-Lab-AI";
    }
}
