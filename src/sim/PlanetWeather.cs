// DNA-AND · simulation · a planet's water and weather, region by region, on the planet's own clock.
// Each of the 100 regions gets its own temperature (from the world's air, its latitude and its height), its own rain and
// its own ice. Rain follows the real belts of the air's circulation: wet at the equator, dry deserts near 30 degrees, wet
// again near 60, dry at the poles; warmer air carries more water. Ice ages come and go with the real wobbles of a planet's
// orbit (about 100,000, 41,000 and 23,000 years); ice caps grow toward the warm middle and the seas fall as water is locked
// in ice (on Earth, by up to about 120 m). Hurricanes form over seas warmer than 26.5 °C, 5 to 20 degrees from the equator
// (about 80 a year on Earth). Seasons come from the planet's tilt, and show at slow game speeds.
using System;

namespace DnaAnd.Sim
{
    internal sealed class PlanetWeather
    {
        public readonly double[] TempC, Rain, Ice;   // per region: temperature, rain (1 = average), ice cover (0 to 1)
        public double IceAgeK;                       // how much the orbit wobbles cool (minus) or warm (plus) the world right now
        public double IceShare;                      // share of the planet under ice
        public double SeaLevelM;                     // sea level against today's (minus: water locked in ice)
        public double HurricanesPerYear;
        public double TiltDeg = 23.4;
        public string Events = "";
        private bool _wasGlacial;

        public PlanetWeather(int regions) { TempC = new double[regions]; Rain = new double[regions]; Ice = new double[regions]; }

        /// <summary>Works out every region's weather at the planet's age. <paramref name="seasonPhase"/>: 0 to 1 through the year,
        /// or a negative number when time runs too fast for seasons to show.</summary>
        public void Update(PlanetLife life, PlanetAir air, double age, bool hasWater, double seasonPhase)
        {
            double tw = 2 * Math.PI;
            IceAgeK = 2.5 * Math.Sin(tw * age / 100000) + 1.2 * Math.Sin(tw * age / 41000) + 0.8 * Math.Sin(tw * age / 23000);
            double tg = air.TempK - 273.15 + IceAgeK;
            double wetAir = hasWater && !air.Snowball && !air.Runaway ? Math.Max(0.1, Math.Exp((tg - 15) / 15)) : 0;   // warmer air holds more water
            int n = TempC.Length, iced = 0, hurricaneRegions = 0;
            for (int i = 0; i < n; i++)
            {
                double y = life.RegionY(i), lat = Math.Asin(Math.Min(1, Math.Abs(y)));
                double season = seasonPhase >= 0 ? TiltDeg / 90 * 25 * y * Math.Cos(tw * seasonPhase) : 0;   // summer in one half while it is winter in the other
                TempC[i] = tg + 40 * (life.RegionWarmth(i) - 0.62) + season;
                Rain[i] = Math.Max(0.05, Math.Min(1.6, (0.6 + 0.9 * Math.Cos(lat * 6)) * wetAir));
                Ice[i] = air.Snowball ? 1 : Math.Max(0, Math.Min(1, (-TempC[i] - 2) / 10));
                if (Ice[i] > 0.5) iced++;
                if (life.RegionSea(i) > 0.6 && TempC[i] > 26.5 && lat > 0.087 && lat < 0.35) hurricaneRegions++;
            }
            IceShare = iced / (double)n;
            SeaLevelM = hasWater ? -130 * Math.Max(0, Math.Min(1, (IceShare - 0.06) / 0.3)) : 0;
            HurricanesPerYear = hurricaneRegions * 8;
            bool glacial = IceShare > 0.16;
            Events = glacial && !_wasGlacial ? "an ice age: the ice caps are spreading and the seas are falling" : !glacial && _wasGlacial ? "the ice age is ending: the ice is melting back and the seas are rising" : "";
            _wasGlacial = glacial;
        }

        public string Describe()
        {
            return "ice covers " + (IceShare * 100).ToString("0") + "%" + (SeaLevelM < -1 ? ", seas " + (-SeaLevelM).ToString("0") + " m lower" : "") +
                   (HurricanesPerYear > 0 ? ", about " + HurricanesPerYear.ToString("0") + " hurricanes a year" : "") +
                   (IceAgeK < -2 ? " (orbit cooling)" : IceAgeK > 2 ? " (orbit warming)" : "");
        }
    }
}
