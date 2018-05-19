using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class GearBase : ModelBase
    {
        public String Name { get; set; }
        public String ImageSource { get; set; }
        public String CharacterType { get; set; }
        public String GearType { get; set; }
        public int LevelMin { get; set; }
        public int Life { get; set; }
        public int Mana { get; set; }
        public int Energy { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Spirit { get; set; }
        public int Luck { get; set; }
        public int CriticalDamage { get; set; }
        public int MagicDamage { get; set; }
        public int PhysicalDamage { get; set; }
        public int CriticalProba { get; set; }
        public int PhysicalArmor { get; set; }
        public int MagicalArmor { get; set; }
        public int Price { get; set; }

        public Gear ToGear()
        {
            Gear gear = new Gear
            {
                Name = Name,
                ImageSource = ImageSource,
                CharacterType = CharacterType,
                GearType = GearType,
                LevelMin = LevelMin,
                Life = Life,
                Mana = Mana,
                Energy = Energy,
                Strength = Strength,
                Agility = Agility,
                Spirit = Spirit,
                Luck = Luck,
                CriticalDamage = CriticalDamage,
                MagicDamage = MagicDamage,
                PhysicalDamage = PhysicalDamage,
                CriticalProba = CriticalProba,
                PhysicalArmor = PhysicalArmor,
                MagicalArmor = MagicalArmor,
                Price = Price
            };
            return gear;
        }

        public Stats ToStats()
        {
            Stats stats = new Stats
            {
                Life = Life,
                Mana = Mana,
                Energy = Energy,
                Strength = Strength,
                Agility = Agility,
                Spirit = Spirit,
                Luck = Luck,
                CriticalDamage = CriticalDamage,
                MagicDamage = MagicDamage,
                PhysicalDamage = PhysicalDamage,
                CriticalProba = CriticalProba,
                PhysicalArmor = PhysicalArmor,
                MagicalArmor = MagicalArmor
            };
            return stats;
        }
    }
}
