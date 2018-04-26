using LoggerUtil;
using ShakesAndFidgetLibrary.Models.Characters;
using ShakesAndFidgetLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShakesAndFidgetLibrary.Models
{
    public class Gear : AItem
    {
        public String CharacterType { get; set; }
        public String GearType { get; set; }
        public int LevelMin { get; set; }

        public Gear()
        {
        }

        public void EquipGear()
        {
            OnEquiping(new EventArgs());
        }

        public ICommand Equip
        {
            get { return new DelegateCommand(EquipGear); }
        }

        public event EventHandler Equiping;
        protected virtual void OnEquiping(EventArgs e)
        {
            EventHandler handler = Equiping;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public Boolean IsCompatible(ICharacter character)
        {
            // Compatibilité des classes
            if (character is Warrior && !String.IsNullOrWhiteSpace(this.CharacterType)
                && this.CharacterType != "W")
                return false;
            else if (character is Hunter && !String.IsNullOrWhiteSpace(this.CharacterType)
                && this.CharacterType != "H")
                return false;
            else if (character is Magus && !String.IsNullOrEmpty(this.CharacterType)
                && this.CharacterType != "M")
                return false;
            // Compatibilité du niveau
            else if (this.LevelMin > character.Level)
                return false;
            return true;
        }
    }
}
