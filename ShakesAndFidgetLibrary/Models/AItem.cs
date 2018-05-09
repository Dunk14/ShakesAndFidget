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
        public int Price { get; set; }

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

        public ICommand Equip
        {
            get { return new DelegateCommand(() => OnEquiping(new EventArgs())); }
        }

        public ICommand Buy
        {
            get { return new DelegateCommand(() => OnBuying(new EventArgs())); }
        }

        public ICommand Sell
        {
            get { return new DelegateCommand(() => OnSelling(new EventArgs())); }
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
                if (equiping != value)
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

        private EventHandler selling;
        public event EventHandler Selling
        {
            add
            {
                if (selling != value)
                    selling += value;
            }
            remove
            {
                selling -= value;
            }
        }

        protected virtual void OnSelling(EventArgs e)
        {
            selling?.Invoke(this, e);
        }

        private EventHandler buying;
        public event EventHandler Buying
        {
            add
            {
                if (buying != value)
                    buying += value;
            }
            remove
            {
                buying -= value;
            }
        }

        protected virtual void OnBuying(EventArgs e)
        {
            buying?.Invoke(this, e);
        }

        private EventHandler unequiping;
        public event EventHandler Unequiping
        {
            add
            {
                if (unequiping != value)
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
