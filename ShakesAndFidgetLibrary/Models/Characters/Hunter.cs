using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models.Characters
{
    public class Hunter : Character
    {
        public const string IMAGE_SOURCE_M = "pack://application:,,,/Resources/Hunter.png";
        public const string IMAGE_SOURCE_F = "pack://application:,,,/Resources/Female Hunter.png";

        public Hunter()
        {

        }

        public Hunter(String sexe, Boolean initStats = false)
        {
            if (sexe == "M")
                this.Sexe = "M";
            else
                this.Sexe = "F";

            if (initStats)
                InitStats();
        }

        override
        public string LoadImage()
        {
            if (this.Sexe == "M")
                return IMAGE_SOURCE_M;
            else
                return IMAGE_SOURCE_F;
        }

        private void InitStats()
        {
            this.Level = 1;
            this.Type = "H";
            this.Life = 110;
            this.Mana = 10;
            this.Energy = 35;
            this.Strength = 15;
            this.Agility = 40;
            this.Spirit = 10;
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
