using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models.Characters
{
    public class Magus : Character
    {
        public Magus(String name, String sexe, int level)
        {
            this.Name = name;
            this.Sexe = sexe;
            this.Level = level;
        }
    }
}
