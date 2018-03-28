using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class Gear : GearBase
    {
        public Gear()
        {
        }

        public String Type { get; set; }
        public int LevelMin{ get; set; }
    }
}
