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
            HomePage.CharacterStatsUC.Loaded += CharacterStatsUC_Loaded;

        }

        private void EquipmentUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            HomePage.EquipmentUC.Character = MainWindow.Instance.CurrentCharacter;
            HomePage.EquipmentUC.RenderItems();
            BinderEquipment();
            MainWindow.Instance.CurrentCharacter.PropertyChanged += CurrentCharacter_PropertyChanged;
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
                    LevelMin = 1,
                    Agility = 5,
                    CriticalDamage = 5,
                    PhysicalDamage = 10,
                    Strength = 5
                },
                new Gear()
                {
                    Name = "Big Shield",
                    ImageSource = "pack://application:,,,/Resources/Big Shield.png",
                    GearType = "Special",
                    LevelMin = 1,
                    PhysicalArmor = 15
                },
                new Gear()
                {
                    Name = "Electric Armor",
                    ImageSource = "pack://application:,,,/Resources/Electric Armor.png",
                    GearType = "Armor",
                    LevelMin = 1,
                    MagicalArmor = 20,
                    PhysicalArmor = 10
                },
                new Gear()
                {
                    Name = "Scythe",
                    ImageSource = "pack://application:,,,/Resources/Scythe.png",
                    GearType = "Attack",
                    LevelMin = 1,
                    CriticalProba = 8
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
                    LevelMin = 5
                },
                new Gear()
                {
                    Name = "Crooked Sword",
                    ImageSource = "pack://application:,,,/Resources/Crooked Sword.png",
                    GearType = "Attack",
                    LevelMin = 1,
                    CriticalDamage = 5
                },
                new Gear()
                {
                    Name = "Saber",
                    ImageSource = "pack://application:,,,/Resources/Saber.png",
                    GearType = "Attack",
                    LevelMin = 1,
                    PhysicalDamage = 10
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
            BinderGears();
            HomePage.InventoryUC.RenderUsables(
                MainWindow.Instance.CurrentCharacter.InventoryUsables,
                true
            );
            BinderUsables();
            HomePage.InventoryUC.SizeChanged += InventoryUC_SizeChanged;
        }

        private void CharacterStatsUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            HomePage.CharacterStatsUC.RenderCharacterStats(
                MainWindow.Instance.CurrentCharacter
            );
        }

        // Bind every gears to the equip event
        public void BinderGears()
        {
            foreach (var gear in MainWindow.Instance.CurrentCharacter.InventoryGears)
            {
                gear.Equiping += Gear_Equiping;
                gear.Selling += Gear_Selling;
            }
        }

        public void BinderBuyGears()
        {
            foreach (var gear in HomePage.DealerUC.ListGear)
            {
                gear.Buying += Gear_Buying;
            }
        }

        // Bind every usables to the equip event
        public void BinderUsables()
        {
            foreach (var usable in MainWindow.Instance.CurrentCharacter.InventoryUsables)
            {
                usable.Equiping += Usable_Equiping;
            }
        }

        private void RenderCharacter(Boolean forUsables = false)
        {
            // Give the updated character
            HomePage.EquipmentUC.Character = MainWindow.Instance.CurrentCharacter;
            HomePage.EquipmentUC.RenderItems();
            HomePage.CharacterStatsUC.RenderCharacterStats(
                MainWindow.Instance.CurrentCharacter
            );
            // Give the updated list
            if (!forUsables)
                HomePage.InventoryUC.RenderGears(
                    MainWindow.Instance.CurrentCharacter.InventoryGears,
                    true
                );
            else
                HomePage.InventoryUC.RenderUsables(
                    MainWindow.Instance.CurrentCharacter.InventoryUsables,
                    true
                );
        }

        private void Gear_Equiping(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            // Compatibility of character with this gear
            if (gear.IsCompatible(MainWindow.Instance.CurrentCharacter))
            {
                MainWindow.Instance.CurrentCharacter.InventoryGears.Remove(gear);
                MainWindow.Instance.CurrentCharacter.Equip(gear);
                RenderCharacter();
            }
            else
                MainWindow.Logger.Warning("You can't equip that gear (class or level)");
        }

        private void Gear_Buying(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            MainWindow.Logger.Log("Money: "+MainWindow.Instance.CurrentCharacter.Money.ToString());
            MainWindow.Logger.Log("Price: "+gear.Price.ToString());
            if (MainWindow.Instance.CurrentCharacter.Money >= gear.Price)
            {
                HomePage.DealerUC.ListGear.Remove(gear);
                MainWindow.Instance.CurrentCharacter.InventoryGears.Add(gear);
                MainWindow.Instance.CurrentCharacter.Money -= gear.Price;
                RenderCharacter();
                HomePage.DealerUC.RenderGears(true);
            }
        }

        private void Gear_Selling(object sender, EventArgs e)
        {

        }

        private void Usable_Equiping(object sender, EventArgs e)
        {
            Usable usable = (sender as Usable);
            MainWindow.Instance.CurrentCharacter.InventoryUsables.Remove(usable);
            MainWindow.Instance.CurrentCharacter.Equip(usable);
            RenderCharacter(true);
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
            BinderGears();
            BinderEquipment();
        }

        private void InventoryUsables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HomePage.InventoryUC.RenderUsables(
               MainWindow.Instance.CurrentCharacter.InventoryUsables,
               true
            );
            BinderUsables();
            BinderEquipment();
        }

        private void CurrentCharacter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            BinderEquipment();
        }

        public async void DealerUC_Loaded(object sender, EventArgs e)
        {
            HomePage.DealerUC.FillItems(
                 await AGearBaseRoutes.GetAllGearBases(),
                 MainWindow.Instance.CurrentCharacter);
            HomePage.DealerUC.RenderGears();
            BinderBuyGears();
        }  //control K + control C / control K + control U


        private void BinderEquipment()
        {
            ICharacter character = HomePage.EquipmentUC.Character;

            if (character.Ring1 != null)
                character.Ring1.Unequiping += Gear_Unequiping;
            if (character.Ring2 != null)
                character.Ring2.Unequiping += Gear_Unequiping;
            if (character.Head != null)
                character.Head.Unequiping += Gear_Unequiping;
            if (character.Armor != null)
                character.Armor.Unequiping += Gear_Unequiping;
            if (character.Special != null)
                character.Special.Unequiping += Gear_Unequiping;
            if (character.Attack != null)
                character.Attack.Unequiping += Gear_Unequiping;
            if (character.Legs != null)
                character.Legs.Unequiping += Gear_Unequiping;
            if (character.Usable != null)
                character.Usable.Unequiping += Usable_Unequiping;
        }

        private void Gear_Unequiping(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            MainWindow.Instance.CurrentCharacter.Unequip(gear);
            RenderCharacter();
        }

        private void Usable_Unequiping(object sender, EventArgs e)
        {
            Usable usable = (sender as Usable);
            MainWindow.Instance.CurrentCharacter.Unequip(usable);
            RenderCharacter();
        }
        #endregion

        #region Events
        #endregion
    }
}
