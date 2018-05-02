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
/*
namespace ShakesAndFidget.UserControls
{
    /// <summary>
    /// Logique d'interaction pour Dealer.xaml
    /// </summary>
    public partial class DealerUserControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Gear> GearsList { get; set; }
        public List<Usable> UsablesList { get; set; }

        public int HorizontalMaxItems { get; set; }

        private ObservableCollection<GearsRow> dealerGearsRows;
        public ObservableCollection<GearsRow> DealerGearsRows
        {
            get
            {
                return dealerGearsRows;
            }
            set
            {
                dealerGearsRows = value;
                OnPropertyChanged("GearsRows");
            }
        }

        private ObservableCollection<UsableRow> dealerUsablesRows;
        public ObservableCollection<UsableRow> DealerUsablesRows
        {
            get
            {
                return dealerUsablesRows;
            }
            set
            {
                dealerUsablesRows = value;
                OnPropertyChanged("UsablesRows");
            }
        }

        public DealerUserControl()
        {
            InitializeComponent();
            MainWindow.Instance.CurrentCharacter.InventoryGears = new List<Gear>()
            {
                new Gear()
                {
                    Name = "Big Helmet",
                    ImageSource = "pack://application:,,,/Resources/Big Helmet.png",
                    GearType = "Head",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Dragon Bow",
                    ImageSource = "pack://application:,,,/Resources/Dragon Bow.png",
                    GearType = "Attack",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Big Shield",
                    ImageSource = "pack://application:,,,/Resources/Big Shield.png",
                    GearType = "Special",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Electric Armor",
                    ImageSource = "pack://application:,,,/Resources/Electric Armor.png",
                    GearType = "Armor",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Scythe",
                    ImageSource = "pack://application:,,,/Resources/Scythe.png",
                    GearType = "Attack",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Wizard Hat",
                    ImageSource = "pack://application:,,,/Resources/Wizard Hat.png",
                    GearType = "Head",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Dark Katana",
                    ImageSource = "pack://application:,,,/Resources/Dark Katana.png",
                    GearType = "Attack",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Crooked Sword",
                    ImageSource = "pack://application:,,,/Resources/Crooked Sword.png",
                    GearType = "Attack",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Saber",
                    ImageSource = "pack://application:,,,/Resources/Saber.png",
                    GearType = "Attack",
                    LevelMin = 1
                },
                new Gear()
                {
                    Name = "Wizard Hat",
                    ImageSource = "pack://application:,,,/Resources/Wizard Hat.png",
                    GearType = "Head",
                    LevelMin = 1
                }
            };
            MainWindow.Instance.CurrentCharacter.InventoryUsables = new List<Usable>()
            {
                new Usable()
                {
                    Name = "Antidote",
                    ImageSource = "pack://application:,,,/Resources/Antidote.png"
                },
                new Usable()
                {
                    Name = "Electric Arrow",
                    ImageSource = "pack://application:,,,/Resources/Electric Arrow.png"
                },
                new Usable()
                {
                    Name = "Mana Potion",
                    ImageSource = "pack://application:,,,/Resources/Mana Potion.png"
                },
                new Usable()
                {
                    Name = "Throwing Weapon",
                    ImageSource = "pack://application:,,,/Resources/Throwing Weapon.png"
                },
                new Usable()
                {
                    Name = "Wind Arrow",
                    ImageSource = "pack://application:,,,/Resources/Wind Arrow.png"
                }
            };
            GearsList = MainWindow.Instance.CurrentCharacter.InventoryGears;
            UsablesList = MainWindow.Instance.CurrentCharacter.InventoryUsables;
            DealerGearsRows = new ObservableCollection<GearsRow>();
            DealerUsablesRows = new ObservableCollection<UsableRow>();
            this.DataContext = this;
            HorizontalMaxItems = 0;
            Events();
        }

        public void Events()
        {
            Loaded += DealerUserControl_Loaded;

            BinderGear();
        }



        private void DealerUserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderGears();
            RenderUsables();
        }

        private void DealerUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RenderGears(true);
            RenderUsables(true);
            SizeChanged += DealerUserControl_SizeChanged;
        }

        // Bind every gears to the equip event
        private void BinderGear()
        {
            foreach (var gear in GearsList)
            {
                gear.Equiping += Gear_Equiping;
            }
        }

        private void Gear_Equiping(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            // Compatibility of character with this gear
            if (gear.IsCompatible(MainWindow.Instance.CurrentCharacter))
            {
                MainWindow.Instance.CurrentCharacter.InventoryGears.Remove(gear);
                MainWindow.Instance.CurrentCharacter.Equip(gear);
                RenderGears(true);
            }
            else
                MainWindow.Logger.Error("You can't equip that gear (class or level)");
        }

        public void RenderGears(Boolean force = false)
        {
            Task.Factory.StartNew(() =>
            {
                // Maximum of items for horizontal ViewList
                double widthMinusItemSize = (GearsListViewParent.ActualWidth - 64) / 64.0;
                int horizontalMaxItems = Convert.ToInt32(Math.Floor(widthMinusItemSize));
                if (horizontalMaxItems < 1)
                    horizontalMaxItems = 1;

                // Load new UI only if needed
                if (force || HorizontalMaxItems != horizontalMaxItems)
                {
                    HorizontalMaxItems = horizontalMaxItems;

                    // Number of lists needed to create enough space for all items
                    double notCeiledListsNumber = (GearsList.Count + .0) / (horizontalMaxItems + .0);
                    int listsNumber = Convert.ToInt32(Math.Ceiling(notCeiledListsNumber));
                    if (listsNumber < 1)
                        listsNumber = 1;

                    // First big list
                    ObservableCollection<GearsRow> ThreadGearsRows = new ObservableCollection<GearsRow>();

                    // Inject items by cutting them in parent lists that contain some children lists
                    int maxIterations = GearsList.Count;
                    for (int i = 0; i < listsNumber; i++)
                    {
                        // Creates every rows
                        GearsRow gearsRow = new GearsRow() { Gears = new ObservableCollection<Gear>() };
                        ThreadGearsRows.Add(gearsRow);
                        for (int y = 0; y < horizontalMaxItems && maxIterations != 0; y++)
                        {
                            // And every sub items
                            gearsRow.Gears.Add(GearsList[maxIterations - 1]);
                            maxIterations--;
                        }
                    }

                    Application.Current.Dispatcher.Invoke(() => DealerGearsRows = ThreadGearsRows);
                }
            });
        }

        public void RenderUsables(Boolean force = false)
        {
            Task.Factory.StartNew(() =>
            {
                // Maximum of items for horizontal ViewList
                double widthMinusItemSize = (UsableListViewParent.ActualWidth - 64) / 64.0;
                int horizontalMaxItems = Convert.ToInt32(Math.Floor(widthMinusItemSize));
                if (horizontalMaxItems < 1)
                    horizontalMaxItems = 1;

                // Load new UI only if needed
                if (force || HorizontalMaxItems != horizontalMaxItems)
                {
                    HorizontalMaxItems = horizontalMaxItems;

                    // Number of lists needed to create enough space for all items
                    double notCeiledListsNumber = (UsablesList.Count + .0) / (horizontalMaxItems + .0);
                    int listsNumber = Convert.ToInt32(Math.Ceiling(notCeiledListsNumber));
                    if (listsNumber < 1)
                        listsNumber = 1;

                    // First big list
                    ObservableCollection<UsableRow> ThreadUsablesRows = new ObservableCollection<UsableRow>();

                    // Inject items by cutting them in parent lists that contain some children lists
                    int maxIterations = UsablesList.Count;
                    for (int i = 0; i < listsNumber; i++)
                    {
                        // Creates every rows
                        UsableRow usableRow = new UsableRow() { Usables = new ObservableCollection<Usable>() };
                        ThreadUsablesRows.Add(usableRow);
                        for (int y = 0; y < horizontalMaxItems && maxIterations != 0; y++)
                        {
                            // And every sub items
                            usableRow.Usables.Add(UsablesList[maxIterations - 1]);
                            maxIterations--;
                        }
                    }

                    Application.Current.Dispatcher.Invoke(() => DealerUsablesRows = ThreadUsablesRows);
                }
            });
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }

    public class DealerGearsRow
    {
        public ObservableCollection<Gear> Gears { get; set; }
        public Gear SelectedGear { get; set; }

        public DealerGearsRow()
        {
        }
    }

    public class DealerUsableRow
    {
        public ObservableCollection<Usable> Usables { get; set; }
    }
}
*/