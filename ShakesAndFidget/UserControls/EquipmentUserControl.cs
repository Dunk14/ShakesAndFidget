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
    /// Logique d'interaction pour HomeUserControl.xaml
    /// </summary>
    public partial class EquipmentUserControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICharacter character;
        public ICharacter Character
        {
            get { return character; }
            set
            {
                character = value;
                OnPropertyChanged("Character");
            }
        }

        public EquipmentUserControl()
        {
            InitializeComponent();
            DataContext = this;
            Events();
        }

        private void Events()
        {
        }

        private void Character_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RenderItems();
        }

        public async void RenderItems()
        {
            ImageCharacter.Source = new BitmapImage(new Uri(Character.LoadCharacterImage()));
            Head.Source = new BitmapImage(new Uri(await Character.LoadHeadImage()));
            Ring1.Source = new BitmapImage(new Uri(await Character.LoadRing1Image()));
            Ring2.Source = new BitmapImage(new Uri(await Character.LoadRing2Image()));
            Attack.Source = new BitmapImage(new Uri(await Character.LoadAttackImage()));
            Armor.Source = new BitmapImage(new Uri(await Character.LoadArmorImage()));
            Legs.Source = new BitmapImage(new Uri(await Character.LoadLegsImage()));
            Special.Source = new BitmapImage(new Uri(await Character.LoadSpecialImage()));
            Usable.Source = new BitmapImage(new Uri(await Character.LoadUsableImage()));
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
