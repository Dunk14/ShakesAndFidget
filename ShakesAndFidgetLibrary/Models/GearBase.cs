using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class GearBase : ModelBase
    {
        public String Name { get; set; }
        public String ImageSource { get; set; }

        public Gear ToGear(String type, int levelMin)
        {
            Gear gear = new Gear
            {
                Name = Name,
                ImageSource = ImageSource,
                Type = type,
                LevelMin = levelMin
            };
            return gear;
        }
    }
}
