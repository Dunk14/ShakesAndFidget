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
    /// Logique d'interaction pour HomeUserControl.xaml
    /// </summary>
    public partial class EquipmentUserControl : UserControl
    {
        public ICharacter Character { get; set; }

        public EquipmentUserControl()
        {
            InitializeComponent();
            Events();
        }

        private void Events()
        {
            
        }

        public async void RenderItems()
        {
            ImageCharacter.Source = new BitmapImage(new Uri(Character.LoadCharacterImage()));
            Head.Source = new BitmapImage(new Uri(await Character.LoadHeadImage()));
            Ring1.Source = new BitmapImage(new Uri(await Character.LoadRing1Image()));
            Ring2.Source = new BitmapImage(new Uri(await Character.LoadRing2Image()));
            Attack.Source = new BitmapImage(new Uri(await Character.LoadAttackImage()));
            Usable.Source = new BitmapImage(new Uri(await Character.LoadUsableImage()));
            Armor.Source = new BitmapImage(new Uri(await Character.LoadArmorImage()));
            Legs.Source = new BitmapImage(new Uri(await Character.LoadLegsImage()));
            Special.Source = new BitmapImage(new Uri(await Character.LoadSpecialImage()));
        }
    }
}
