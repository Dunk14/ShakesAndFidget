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
    /// Logique d'interaction pour Dealer.xaml
    /// </summary>
    public partial class DealerUserControl : UserControl, INotifyPropertyChanged
    {
            public event PropertyChangedEventHandler PropertyChanged;

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

            public DealerUserControl()
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
            }

            public void RenderGears(ICollection<Gear> gearsList, Boolean force = false)
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
                    double notCeiledListsNumber = (gearsList.Count + .0) / (horizontalMaxItems + .0);
                    int listsNumber = Convert.ToInt32(Math.Ceiling(notCeiledListsNumber));
                    if (listsNumber < 1)
                        listsNumber = 1;

                    // First big list
                    GearsRows = new ObservableCollection<GearsRow>();

                    // Inject items by cutting them in parent lists that contain some children lists
                    int maxIterations = gearsList.Count;
                    for (int i = 0; i < listsNumber; i++)
                    {
                        // Creates every rows
                        GearsRow gearsRow = new GearsRow() { Gears = new ObservableCollection<Gear>() };
                        GearsRows.Add(gearsRow);
                        for (int y = 0; y < horizontalMaxItems && maxIterations != 0; y++)
                        {
                            // And every sub items
                            gearsRow.Gears.Add(gearsList.ToArray<Gear>()[maxIterations - 1]);
                            maxIterations--;
                        }
                    }
                }
            }

            public void RenderUsables(ICollection<Usable> usablesList, Boolean force = false)
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
                    double notCeiledListsNumber = (usablesList.Count + .0) / (horizontalMaxItems + .0);
                    int listsNumber = Convert.ToInt32(Math.Ceiling(notCeiledListsNumber));
                    if (listsNumber < 1)
                        listsNumber = 1;

                    // First big list
                    UsablesRows = new ObservableCollection<UsableRow>();

                    // Inject items by cutting them in parent lists that contain some children lists
                    int maxIterations = usablesList.Count;
                    for (int i = 0; i < listsNumber; i++)
                    {
                        // Creates every rows
                        UsableRow usableRow = new UsableRow() { Usables = new ObservableCollection<Usable>() };
                        UsablesRows.Add(usableRow);
                        for (int y = 0; y < horizontalMaxItems && maxIterations != 0; y++)
                        {
                            // And every sub items
                            usableRow.Usables.Add(usablesList.ToArray<Usable>()[maxIterations - 1]);
                            maxIterations--;
                        }
                    }
                }
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