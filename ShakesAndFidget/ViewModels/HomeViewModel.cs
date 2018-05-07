using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using ShakesAndFidgetLibrary.Routes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ShakesAndFidget.ViewModels
{
    public class HomeViewModel
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        private HomePage homePage;

        public HomePage HomePage { get => homePage; set => homePage = value; }
        #endregion

        #region Attributs
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public HomeViewModel(HomePage homePage)
        {
            this.homePage = homePage;
            Events(); 
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void Events()
        {
            HomePage.EquipmentUC.Loaded += EquipmentUC_Loaded;
            HomePage.InventoryUC.Loaded += InventoryUC_Loaded;
            HomePage.DealerUC.Loaded += DealerUC_Loaded;

        }

        // Bind every gears to the equip event
        public void BinderGear()
        {
            foreach (var gear in MainWindow.Instance.CurrentCharacter.InventoryGears)
            {
                gear.Equiping -= Gear_Equiping;
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
                // Give the updated list
                HomePage.InventoryUC.RenderGears(
                    MainWindow.Instance.CurrentCharacter.InventoryGears, 
                    true
                );
            }
            else
                MainWindow.Logger.Error("You can't equip that gear (class or level)");
        }

        private void InventoryUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Filled lists for tests
            MainWindow.Instance.CurrentCharacter.InventoryGears = new ObservableCollection<Gear>()
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
            MainWindow.Instance.CurrentCharacter.InventoryUsables = new ObservableCollection<Usable>()
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
            MainWindow.Instance.CurrentCharacter.InventoryGears.CollectionChanged += InventoryGears_CollectionChanged;
            MainWindow.Instance.CurrentCharacter.InventoryUsables.CollectionChanged += InventoryUsables_CollectionChanged;
            HomePage.InventoryUC.RenderGears(
                MainWindow.Instance.CurrentCharacter.InventoryGears,
                true
            );
            BinderGear();
            HomePage.InventoryUC.RenderUsables(
                MainWindow.Instance.CurrentCharacter.InventoryUsables,
                true
            );
            HomePage.InventoryUC.SizeChanged += InventoryUC_SizeChanged;
        }

        private void InventoryUC_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            HomePage.InventoryUC.RenderGears(
                MainWindow.Instance.CurrentCharacter.InventoryGears
            );
            HomePage.InventoryUC.RenderUsables(
                MainWindow.Instance.CurrentCharacter.InventoryUsables
            );
        }

        private void InventoryGears_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HomePage.InventoryUC.RenderGears(
                MainWindow.Instance.CurrentCharacter.InventoryGears,
                true
            );
            BinderGear();
        }

        private void InventoryUsables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HomePage.InventoryUC.RenderUsables(
               MainWindow.Instance.CurrentCharacter.InventoryUsables,
               true
            );
            //HomePage.InventoryUC.BinderUsable();
        }

        private void EquipmentUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            HomePage.EquipmentUC.Character = MainWindow.Instance.CurrentCharacter;
            HomePage.EquipmentUC.RenderItems();

        }

        public async void DealerUC_Loaded(object sender, EventArgs e)
        {
            HomePage.DealerUC.FillItems(
                 await AGearBaseRoutes.GetAllGearBases(),
                 MainWindow.Instance.CurrentCharacter
             );
        }  //control K + control C / control K + control U


        private void BinderEquipment()
        {
            MainWindow.Instance.CurrentCharacter.Ring1.Unequiping -= Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Ring1.Unequiping += Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Ring2.Unequiping -= Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Ring2.Unequiping += Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Head.Unequiping -= Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Head.Unequiping += Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Armor.Unequiping -= Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Armor.Unequiping += Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Special.Unequiping -= Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Special.Unequiping += Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Attack.Unequiping -= Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Attack.Unequiping += Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Legs.Unequiping -= Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Legs.Unequiping += Gear_Unequiping;
            MainWindow.Instance.CurrentCharacter.Usable.Unequiping -= Usable_Unequiping;
            MainWindow.Instance.CurrentCharacter.Usable.Unequiping += Usable_Unequiping;
        }

        private void Gear_Unequiping(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            MainWindow.Instance.CurrentCharacter.Unequip(gear);
            // Give the updated list
            HomePage.InventoryUC.RenderGears(
                MainWindow.Instance.CurrentCharacter.InventoryGears,
                true
            );
        }

        private void Usable_Unequiping(object sender, EventArgs e)
        {
            Usable usable = (sender as Usable);
            MainWindow.Instance.CurrentCharacter.Unequip(usable);
            // Give the updated list
            HomePage.InventoryUC.RenderUsables(
                MainWindow.Instance.CurrentCharacter.InventoryUsables,
                true
            );
        }
        #endregion

        #region Events
        #endregion
    }
}
