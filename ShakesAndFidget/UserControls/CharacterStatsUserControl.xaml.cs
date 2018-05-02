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
    public partial class CharacterStatsUserControl : UserControl
    {
        public Stats Stats { get; set; }

        public CharacterStatsUserControl()
        {
            InitializeComponent();
            Stats = new Stats();
        }

        public void Events()
        {
            Loaded += CharacterStatsUserControl_Loaded;
        }

        private void CurrentCharacter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RenderCharacterStats();
        }

        private void CharacterStatsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.CurrentCharacter.PropertyChanged += CurrentCharacter_PropertyChanged;
        }

        public void RenderCharacterStats()
        {
            if (MainWindow.Instance.CurrentCharacter != null)
                Stats = MainWindow.Instance.CurrentCharacter.ComputeStats();

            Life.Content = Stats.Life;
            Mana.Content = Stats.Mana;
            Energy.Content = Stats.Energy;
            Strength.Content = Stats.Strength;
            Agility.Content = Stats.Agility;
            Spirit.Content = Stats.Spirit;
            Luck.Content = Stats.Luck;
            CriticalDamage.Content = Stats.CriticalDamage;
            MagicDamage.Content = Stats.MagicDamage;
            PhysicalDamage.Content = Stats.PhysicalDamage;
            CriticalProba.Content = Stats.CriticalProba;
            PhysicalArmor.Content = Stats.PhysicalArmor;
            MagicalArmor.Content = Stats.MagicalArmor;
        }
    }
}
