using EntityUtils.Reflection;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CharacterStatsUserControl : UserControl, INotifyPropertyChanged
    {
        public Stats stats;
        public Stats Stats
        {
            get
            {
                return stats;
            }
            set
            {
                stats = value;
                OnPropertyChanged("Stats");
            }
        }

        public CharacterStatsUserControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void RenderCharacterStats(ICharacter character)
        {
            Stats newStats = new Stats();
            newStats = character.ComputeStats();
            Stats = newStats;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
