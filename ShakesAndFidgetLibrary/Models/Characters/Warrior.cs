using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class Warrior : Character
    {
        public Warrior(String name, String sexe, int level)
        {
            this.Name = name;
            this.Sexe = sexe;
            this.Level = level;

            this.Life = 150;
            this.Mana = 5;
            this.Energy = 50;
            this.Strength = 20;
            this.Agility = 10;
            this.Spirit = 7;
            this.Luck = 1;
            this.CriticalDamage = 20;
            this.MagicDamage = 5;
        }
    }
}
