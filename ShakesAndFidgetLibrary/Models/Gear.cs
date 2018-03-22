using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class Gear : ModelBase
    {
        public Gear()
        {
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
