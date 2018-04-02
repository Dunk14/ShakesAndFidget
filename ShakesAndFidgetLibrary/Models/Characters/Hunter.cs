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
