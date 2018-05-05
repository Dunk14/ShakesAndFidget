using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class Stats : ModelBase
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private int life;
        private int mana;
        private int energy;
        private int strength;
        private int agility;
        private int spirit;
        private int luck;
        private int criticalDamage;
        private int magicDamage;
        private int physicalDamage;
        private int criticalProba;
        private int physicalArmor;
        private int magicalArmor;
        #endregion

        #region Properties

        public int Life
        {
            get { return life; }
            set
            {
                life = value;
                OnPropertyChanged("Life");
            }
        }

        public int Mana
        {
            get { return mana; }
            set
            {
                mana = value;
                OnPropertyChanged("Mana");
            }
        }

        public int Energy
        {
            get { return energy; }
            set {
                energy = value;
                OnPropertyChanged("Energy");
            }
        }

        public int Strength
        {
            get { return strength; }
            set {
                strength = value;
                OnPropertyChanged("Strength");
            }
        }

        public int Agility
        {
            get { return agility; }
            set
            {
                agility = value;
                OnPropertyChanged("Agility");
            }
        }

        public int Spirit
        {
            get { return spirit; }
            set
            {
                spirit = value;
                OnPropertyChanged("Spirit");
            }
        }

        public int Luck
        {
            get { return luck; }
            set
            {
                luck = value;
                OnPropertyChanged("Luck");
            }
        }

        public int CriticalDamage
        {
            get { return criticalDamage; }
            set
            {
                criticalDamage = value;
                OnPropertyChanged("CriticalDamage");
            }
        }
        public int MagicDamage
        {
            get { return magicDamage; }
            set {
                magicDamage = value;
                OnPropertyChanged("MagicalDamage");
            }
        }

        public int PhysicalDamage
        {
            get { return physicalDamage; }
            set
            {
                physicalDamage = value;
                OnPropertyChanged("PhysicalDamage");
            }
        }

        public int CriticalProba
        {
            get { return criticalProba; }
            set
            {
                criticalProba = value;
                OnPropertyChanged("CriticalProba");
            }
        }

        public int PhysicalArmor
        {
            get { return physicalArmor; }
            set
            {
                physicalArmor = value;
                OnPropertyChanged("PhysicalArmor");
            }
        }

        public int MagicalArmor
        {
            get { return magicalArmor; }
            set
            {
                magicalArmor = value;
                OnPropertyChanged("MagicalArmor");
            }
        }
        #endregion

        #region Constructors
        public Stats(int life, int mana, int energy, int strength, int agility,
            int spirit, int luck, int criticalDamage, int magicDamage, int physicalDamage,
            int criticalProba, int physicalArmor, int magicalArmor) : base()
        {
            this.life = life;
            this.mana = mana;
            this.energy = energy;
            this.strength = strength;
            this.Agility = agility;
            this.spirit = spirit;
            this.luck = luck;
            this.criticalDamage = criticalDamage;
            this.magicDamage = magicDamage;
            this.physicalDamage = physicalDamage;
            this.criticalProba = criticalProba;
            this.physicalArmor = physicalArmor;
            this.magicalArmor = magicalArmor;
        }

        public Stats(ICharacter character) : base()
        {
            this.life = character.Life;
            this.mana = character.Mana;
            this.energy = character.Energy;
            this.strength = character.Strength;
            this.Agility = character.Agility;
            this.spirit = character.Spirit;
            this.luck = character.Luck;
            this.criticalDamage = character.CriticalDamage;
            this.magicDamage = character.MagicDamage;
            this.physicalDamage = character.PhysicalDamage;
            this.criticalProba = character.CriticalProba;
            this.physicalArmor = character.PhysicalArmor;
            this.magicalArmor = character.MagicalArmor;
        }

        public Stats() : base()
        {
            this.life = 0;
            this.mana = 0;
            this.energy = 0;
            this.strength = 0;
            this.Agility = 0;
            this.spirit = 0;
            this.luck = 0;
            this.criticalDamage = 0;
            this.magicDamage = 0;
            this.physicalDamage = 0;
            this.criticalProba = 0;
            this.physicalArmor = 0;
            this.magicalArmor = 0;
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public Stats AddStats(Stats stats)
        {
            Stats newStats = new Stats
            {
                Life = Life + stats.Life,
                Mana = Mana + stats.Mana,
                Energy = Energy + stats.Energy,
                Strength = Strength + stats.Strength,
                Agility = Agility + stats.Agility,
                Spirit = Spirit + stats.Spirit,
                Luck = Luck + stats.Luck,
                CriticalDamage = CriticalDamage + stats.CriticalDamage,
                MagicDamage = MagicDamage + stats.MagicDamage,
                PhysicalDamage = PhysicalDamage + stats.PhysicalDamage,
                CriticalProba = CriticalProba + stats.CriticalProba,
                PhysicalArmor = PhysicalArmor + stats.PhysicalArmor,
                MagicalArmor = MagicalArmor + stats.MagicalArmor
            };
            
            return newStats;
        }
        #endregion

        #region Events
        #endregion
    }
}
