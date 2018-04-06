using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Gear> GearsList { get; set; }
        private ObservableCollection<GearsRow> gearsRows;
        public ObservableCollection<GearsRow> GearsRows
        {
            get
            {
                return gearsRows;
            }
            set
            {
                gearsRows = value;
                OnPropertyChanged("GearsRows");
            }
        }

        public Inventory()
        {
            InitializeComponent();
            GearsRows = new ObservableCollection<GearsRow>();
            this.DataContext = this;
            Events();
        }

        public void Events()
        {
            Loaded += Inventory_Loaded;
        }

        private void Inventory_Loaded(object sender, RoutedEventArgs e)
        {
            TestInventory();
        }

        public void TestInventory()
        {
            GearsList = new List<Gear>()
            {
                new Gear()
                {
                    Name = "Antidote",
                    ImageSource = "pack://application:,,,/Resources/Antidote.png"
                },
                new Gear()
                {
                    Name = "Big Heal Potion",
                    ImageSource = "pack://application:,,,/Resources/Big Heal Potion.png"
                },
                new Gear()
                {
                    Name = "Big Helmet",
                    ImageSource = "pack://application:,,,/Resources/Big Helmet.png"
                },
                new Gear()
                {
                    Name = "Dragon Bow",
                    ImageSource = "pack://application:,,,/Resources/Dragon Bow.png"
                },
                new Gear()
                {
                    Name = "Big Shield",
                    ImageSource = "pack://application:,,,/Resources/Big Shield.png"
                },
                new Gear()
                {
                    Name = "Electric Armor",
                    ImageSource = "pack://application:,,,/Resources/Electric Armor.png"
                },
                new Gear()
                {
                    Name = "Scythe",
                    ImageSource = "pack://application:,,,/Resources/Scythe.png"
                },
                new Gear()
                {
                    Name = "Wizard Hat",
                    ImageSource = "pack://application:,,,/Resources/Wizard Hat.png"
                },
                new Gear()
                {
                    Name = "Big Shield",
                    ImageSource = "pack://application:,,,/Resources/Big Shield.png"
                },
                new Gear()
                {
                    Name = "Electric Armor",
                    ImageSource = "pack://application:,,,/Resources/Electric Armor.png"
                },
                new Gear()
                {
                    Name = "Scythe",
                    ImageSource = "pack://application:,,,/Resources/Scythe.png"
                },
                new Gear()
                {
                    Name = "Wizard Hat",
                    ImageSource = "pack://application:,,,/Resources/Wizard Hat.png"
                }
            };
            RenderGears();
        }

        public void RenderGears()
        {
            // Maximum of items for horizontal ViewList
            double widthMinusItemSize = GearsListViewParent.ActualWidth / 64;
            int horizontalMaxItems = Convert.ToInt32(Math.Floor(widthMinusItemSize));
            if (horizontalMaxItems < 1)
                horizontalMaxItems = 1;

            // Number of lists needed to create enough space for all items
            int listsNumber = GearsList.Count / horizontalMaxItems;
            if (listsNumber < 1)
                listsNumber = 1;

            // First big list
            GearsRows = new ObservableCollection<GearsRow>();

            // Inject items by cutting them in parent lists that contain some children lists
            int maxIterations = GearsList.Count;
            for (int i = 0; i < listsNumber; i++)
            {
                // Creates every rows
                GearsRow gearsRow = new GearsRow() { Gears = new ObservableCollection<Gear>() };
                GearsRows.Add(gearsRow);
                for (int y = 0; y < horizontalMaxItems && maxIterations != 0; y++)
                {
                    Console.WriteLine(maxIterations);
                    // And every sub items
                    gearsRow.Gears.Add(GearsList[maxIterations-1]);
                    maxIterations--;
                }
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class GearsRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Gear> Gears { get; set; }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
