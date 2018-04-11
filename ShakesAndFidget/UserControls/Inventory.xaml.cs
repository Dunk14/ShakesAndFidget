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
        public List<Usable> UsablesList { get; set; }

        public int HorizontalMaxItems { get; set; }

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

        private ObservableCollection<UsableRow> usablesRows;
        public ObservableCollection<UsableRow> UsablesRows
        {
            get
            {
                return usablesRows;
            }
            set
            {
                usablesRows = value;
                OnPropertyChanged("UsablesRows");
            }
        }

        public Inventory()
        {
            InitializeComponent();
            GearsRows = new ObservableCollection<GearsRow>();
            UsablesRows = new ObservableCollection<UsableRow>();
            this.DataContext = this;
            HorizontalMaxItems = 0;
            Events();
        }

        public void Events()
        {
            Loaded += Inventory_Loaded;
        }

        private void Inventory_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderGears();
            RenderUsables();
        }

        private void Inventory_Loaded(object sender, RoutedEventArgs e)
        {
            TestInventory();
            SizeChanged += Inventory_SizeChanged;
        }

        public void TestInventory()
        {
            GearsList = new List<Gear>()
            {
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
                    Name = "Dark Katana",
                    ImageSource = "pack://application:,,,/Resources/Dark Katana.png"
                },
                new Gear()
                {
                    Name = "Crooked Sword",
                    ImageSource = "pack://application:,,,/Resources/Crooked Sword.png"
                },
                new Gear()
                {
                    Name = "Saber",
                    ImageSource = "pack://application:,,,/Resources/Saber.png"
                },
                new Gear()
                {
                    Name = "Wizard Hat",
                    ImageSource = "pack://application:,,,/Resources/Wizard Hat.png"
                }
            };
            UsablesList = new List<Usable>()
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
            RenderGears();
            RenderUsables();
        }

        public void RenderGears()
        {
            Task.Factory.StartNew(() =>
            {
                // Maximum of items for horizontal ViewList
                double widthMinusItemSize = (GearsListViewParent.ActualWidth - 64) / 64.0;
                int horizontalMaxItems = Convert.ToInt32(Math.Floor(widthMinusItemSize));
                if (horizontalMaxItems < 1)
                    horizontalMaxItems = 1;

                // Load new UI only if needed
                if (HorizontalMaxItems != horizontalMaxItems)
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

                    Application.Current.Dispatcher.Invoke(() => GearsRows = ThreadGearsRows);
                }
            });
        }

        public void RenderUsables()
        {
            Task.Factory.StartNew(() =>
            {
                // Maximum of items for horizontal ViewList
                double widthMinusItemSize = (UsableListViewParent.ActualWidth - 64) / 64.0;
                int horizontalMaxItems = Convert.ToInt32(Math.Floor(widthMinusItemSize));
                if (horizontalMaxItems < 1)
                    horizontalMaxItems = 1;

                // Load new UI only if needed
                if (HorizontalMaxItems != horizontalMaxItems)
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

                    Application.Current.Dispatcher.Invoke(() => UsablesRows = ThreadUsablesRows);
                }
            });
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void SelectGear(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(sender);
        }
    }

    public class GearsRow
    {
        public ObservableCollection<Gear> Gears { get; set; }
        public Gear SelectedGear { get; set; }

        public GearsRow()
        {
        }

        public void SelectGear(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(sender);
        }

        public void EquipGear()
        {
            Console.WriteLine("EquipGear");
        }

        public ICommand Equip
        {
            get { return new DelegateCommand(EquipGear); }
        }
    }

    public class UsableRow
    {
        public ObservableCollection<Usable> Usables { get; set; }
    }

    public class DelegateCommand : ICommand
    {
        public delegate void SimpleEventHandler();
        private SimpleEventHandler handler;
        private bool isEnabled = true;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(SimpleEventHandler handler)
        {
            this.handler = handler;
        }

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object arg)
        {
            return IsEnabled;
        }

        void ICommand.Execute(object arg)
        {
            this.handler();
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }

            set
            {
                this.isEnabled = value;
                OnCanExecuteChanged();
            }
        }
    }
}
