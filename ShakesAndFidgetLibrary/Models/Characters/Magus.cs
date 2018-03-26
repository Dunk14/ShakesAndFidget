using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models.Characters
{
    public class Magus : Character
    {
        public const string IMAGE_SOURCE_M = "pack://application:,,,/Resources/Magus.png";
        public const string IMAGE_SOURCE_F = "pack://application:,,,/Resources/Female Magus.png";

        public Magus(String sexe, Boolean initStats = false)
        {
            if (sexe == "M")
                this.sexe = "M";
            else
                this.sexe = "F";

            if (initStats)
                InitStats();
        }

        override
        public string LoadImage()
        {
            if (this.sexe == "M")
                return IMAGE_SOURCE_M;
            else
                return IMAGE_SOURCE_F;
        }

        private void InitStats()
        {
            this.Life = 80;
            this.Mana = 50;
            this.Energy = 5;
            this.Strength = 5;
            this.Agility = 20;
            this.Spirit = 25;
            this.Luck = 1;
            this.CriticalDamage = 20;
            this.MagicDamage = 10;
            this.PhysicalDamage = 5;
            this.CriticalProba = 5;
            this.PhysicalArmor = 5;
            this.MagicalArmor = 15;
        }
    }
}
