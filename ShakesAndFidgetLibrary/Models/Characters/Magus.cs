using ShakesAndFidgetLibrary.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models.Characters
{
    public class Magus : ICharacter
    {
        public override string ImageSourceM => "pack://application:,,,/Resources/Magus.png";
        public override string ImageSourceF => "pack://application:,,,/Resources/Female Magus.png";
        public override string ImageSourceSpecial => "pack://application:,,,/Resources/Inventory Shield.png";
        public override string ImageSourceAttack => "pack://application:,,,/Resources/Inventory Staff.png";

        public Magus()
        {

        }

        public Magus(String sexe, Boolean initStats = false)
        {
            if (sexe == "M")
                Sexe = "M";
            else
                Sexe = "F";

            if (initStats)
                InitStats();
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
