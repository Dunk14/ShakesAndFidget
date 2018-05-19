using ShakesAndFidgetLibrary.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models.Characters
{
    public class Hunter : ICharacter
    {
        public override string ImageSourceM => "pack://application:,,,/Resources/Hunter.png";
        public override string ImageSourceF => "pack://application:,,,/Resources/Female Hunter.png";
        public override string ImageSourceSpecial => "pack://application:,,,/Resources/Inventory Arrow.png";
        public override string ImageSourceAttack => "pack://application:,,,/Resources/Inventory Bow.png";

        public Hunter()
        {

        }

        public Hunter(String sexe, Boolean initStats = false)
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
            Type = "H";
            Life = 110;
            Mana = 10;
            Energy = 35;
            Strength = 15;
            Agility = 40;
            Spirit = 10;
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
