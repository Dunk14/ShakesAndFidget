using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models.Characters
{
    public class Magus : ICharacter
    {
        public const string IMAGE_SOURCE_M = "pack://application:,,,/Resources/Magus.png";
        public const string IMAGE_SOURCE_F = "pack://application:,,,/Resources/Female Magus.png";
        public const string IMAGE_SOURCE_SPECIAL = "pack://application:,,,/Resources/Inventory Usable.png";
        public const string IMAGE_SOURCE_ATTACK = "pack://application:,,,/Resources/Inventory Staff.png";

        public Magus()
        {

        }

        public Magus(String sexe, Boolean initStats = false)
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

        override
        public string LoadSpecial()
        {
            return IMAGE_SOURCE_SPECIAL;
        }

        override
        public string LoadAttack()
        {
            return IMAGE_SOURCE_ATTACK;
        }

        private void InitStats()
        {
            this.Level = 1;
            this.Type = "M";
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
