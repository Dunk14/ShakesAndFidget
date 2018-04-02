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
            this.Level = 1;
            this.Type = "W";
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
