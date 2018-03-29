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
        public const string ITEM_IMAGE = "pack://application:,,,/Resources/Inventory Item.png";
        public const string HEAD_IMAGE = "pack://application:,,,/Resources/Inventory Helmet.png";
        public const string USABLE_IMAGE = "pack://application:,,,/Resources/Inventory Usable.png";
        public const string LEGS_IMAGE = "pack://application:,,,/Resources/Inventory Legs.png";

        public ICharacter Character { get; set; }

        public EquipmentUserControl()
        {
            InitializeComponent();
            Events();
        }

        private void Events()
        {
            
        }

        public void RenderItems()
        {
            this.ImageCharacter.Source = new BitmapImage(new Uri(this.Character.LoadImage()));

            if (Character.HeadId == null)
                this.Head.Source = new BitmapImage(new Uri(HEAD_IMAGE));

            if (Character.Ring1Id == null)
                this.Ring1.Source = new BitmapImage(new Uri(ITEM_IMAGE));

            if (Character.Ring2Id == null)
                this.Ring2.Source = new BitmapImage(new Uri(ITEM_IMAGE));

            if (Character.AttackId == null)
                this.Attack.Source = new BitmapImage(new Uri(this.Character.LoadAttack()));

            if (Character.Usable1Id == null)
                this.Usable1.Source = new BitmapImage(new Uri(USABLE_IMAGE));

            if (Character.Usable2Id == null)
                this.Usable2.Source = new BitmapImage(new Uri(USABLE_IMAGE));

            if (Character.LegsId == null)
                this.Legs.Source = new BitmapImage(new Uri(LEGS_IMAGE));

            if (Character.SpecialId == null)
                this.Special.Source = new BitmapImage(new Uri(this.Character.LoadSpecial()));
            
            
        }
    }
}
