﻿using System;
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
        private int strength;
        private int agility;
        private int spirit;
        private int luck;
        private int criticalDamage;
        private int magicDamage;
        private int physicalDamage;
        private int criticalProbability;
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

        public int CriticalProbability
        {
            get { return criticalProbability; }
            set
            {
                criticalProbability = value;
                OnPropertyChanged("CriticalProbability");
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

        public Stats() : base()
        {
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        #endregion

        #region Events
        #endregion
    }
}