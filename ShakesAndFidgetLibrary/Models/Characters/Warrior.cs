using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class Warrior : Character
    {
        public const string IMAGE_SOURCE_M = "pack://application:,,,/Resources/Knight.png";
        public const string IMAGE_SOURCE_F = "pack://application:,,,/Resources/Female Knight.png";

        public Warrior(String sexe, Boolean initStats = false)
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
            this.Life = 150;
            this.Mana = 5;
            this.Energy = 50;
            this.Strength = 20;
            this.Agility = 10;
            this.Spirit = 7;
            this.Luck = 1;
            this.CriticalDamage = 20;
            this.MagicDamage = 5;
            this.PhysicalDamage = 10;
            this.CriticalProba = 5;
            this.PhysicalArmor = 15;
            this.MagicalArmor = 5;
        }
    }
}
