using EntityUtils.Reflection;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShakesAndFidget.UserControls
{
    /// <summary>
    /// Logique d'interaction pour CharacterStats.xaml
    /// </summary>
    public partial class CharacterStats : UserControl
    {
        public CharacterStats()
        {
            InitializeComponent();
        }

        public void RenderCharacterStats(Stats stats)
        {
            this.Life.Content = stats.Life;
            this.Mana.Content = stats.Mana;
            this.Energy.Content = stats.Energy;
            this.Strength.Content = stats.Strength;
            this.Agility.Content = stats.Agility;
            this.Spirit.Content = stats.Spirit;
            this.Luck.Content = stats.Luck;
            this.CriticalDamage.Content = stats.CriticalDamage;
            this.MagicDamage.Content = stats.MagicDamage;
            this.PhysicalDamage.Content = stats.PhysicalDamage;
            this.CriticalProba.Content = stats.CriticalProba;
            this.PhysicalArmor.Content = stats.PhysicalArmor;
            this.MagicalArmor.Content = stats.MagicalArmor;
        }
    }
}
