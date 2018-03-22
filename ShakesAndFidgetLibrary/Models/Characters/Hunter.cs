using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models.Characters
{
    public class Hunter : Character
    {
        public Hunter(String name, String sexe, int level)
        {
            this.Name = name;
            this.Sexe = sexe;
            this.Level = level;
        }
    }
}
