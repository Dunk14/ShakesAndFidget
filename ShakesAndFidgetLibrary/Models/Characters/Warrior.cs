using ShakesAndFidgetLibrary.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class Warrior : ICharacter
    {
        public override string ImageSourceM => "pack://application:,,,/Resources/Knight.png";
        public override string ImageSourceF => "pack://application:,,,/Resources/Female Knight.png";
        public override string ImageSourceSpecial => "pack://application:,,,/Resources/Inventory Shield.png";
        public override string ImageSourceAttack => "pack://application:,,,/Resources/Inventory Weapon.png";

        public Warrior()
        {

        }

        public Warrior(String sexe, Boolean initStats = false)
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
            Type = "W";
            Life = 150;
            Mana = 5;
            Energy = 50;
            Strength = 20;
            Agility = 10;
            Spirit = 7;
            Luck = 1;
            CriticalDamage = 20;
            MagicDamage = 5;
            PhysicalDamage = 10;
            CriticalProba = 5;
            PhysicalArmor = 15;
            MagicalArmor = 5;
            Money = 150;
        }
    }
}
