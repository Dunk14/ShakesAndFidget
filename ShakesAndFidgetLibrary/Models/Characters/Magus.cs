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
            Level = 1;
            Type = "M";
            Life = 80;
            Mana = 50;
            Energy = 5;
            Strength = 5;
            Agility = 20;
            Spirit = 25;
            Luck = 1;
            CriticalDamage = 20;
            MagicDamage = 10;
            PhysicalDamage = 5;
            CriticalProba = 5;
            PhysicalArmor = 5;
            MagicalArmor = 15;
            Money = 150;
        }
    }
}
