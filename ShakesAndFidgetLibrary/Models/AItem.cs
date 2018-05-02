using ShakesAndFidgetLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShakesAndFidgetLibrary.Models
{
    public abstract class AItem : Stats
    {
        public String Name { get; set; }
        public String ImageSource { get; set; }

        public Stats ToStats()
        {
            Stats stats = new Stats();

            stats.Life = Life;
            stats.Mana = Mana;
            stats.Energy = Energy;
            stats.Strength = Strength;
            stats.Agility = Agility;
            stats.Spirit = Spirit;
            stats.Luck = Luck;
            stats.CriticalDamage = CriticalDamage;
            stats.MagicDamage = MagicDamage;
            stats.PhysicalDamage = PhysicalDamage;
            stats.CriticalProba = CriticalProba;
            stats.PhysicalArmor = PhysicalArmor;
            stats.MagicalArmor = MagicalArmor;

            return stats;
        }

        public ICommand Equip
        {
            get { return new DelegateCommand(() => OnEquiping(new EventArgs())); }
        }

        public ICommand Unequip
        {
            get { return new DelegateCommand(() => OnUnequiping(new EventArgs())); }
        }

        private EventHandler equiping;
        public event EventHandler Equiping
        {
            add
            {
                equiping += value;
            }
            remove
            {
                equiping -= value;
            }
        }

        protected virtual void OnEquiping(EventArgs e)
        {
            equiping?.Invoke(this, e);
        }

        private EventHandler unequiping;
        public event EventHandler Unequiping
        {
            add
            {
                unequiping += value;
            }
            remove
            {
                unequiping -= value;
            }
        }

        protected virtual void OnUnequiping(EventArgs e)
        {
            unequiping?.Invoke(this, e);
        }
    }
}
