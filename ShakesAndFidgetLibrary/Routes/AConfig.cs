using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Routes
{
    public abstract class AConfig
    {
        public const string BASE_URL = "http://localhost:3000";
        public const string CHARACTER_URL = "/characters";
        public const string GEAR_URL = "/gears";
        public const string GEAR_BASE_URL = "/gearBases";
    }
}
