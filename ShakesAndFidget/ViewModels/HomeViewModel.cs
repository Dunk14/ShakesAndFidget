using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using ShakesAndFidgetLibrary.Routes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            MainWindow.Instance.CurrentCharacter.InventoryGears.CollectionChanged += InventoryGears_CollectionChanged;
            MainWindow.Instance.CurrentCharacter.InventoryUsables.CollectionChanged += InventoryUsables_CollectionChanged;
            HomePage.InventoryUC.RenderGears(
                MainWindow.Instance.CurrentCharacter.InventoryGears,
                true
            );
            ICharacter character = MainWindow.Instance.CurrentCharacter;
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
                Task.Factory.StartNew(async () =>
                {
                    // Delete id in inventory attribute
                    Gear updatedGear = await AGearRoutes.DeleteFromInventory(gear.Id);
                    // Save new state of character
                    ICharacter character = await ACharacterRoutes.PutCharacter(MainWindow.Instance.CurrentCharacter);
                    if (!updatedGear.CharacterInventoryId.HasValue)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            gear = updatedGear;
                            MainWindow.Instance.CurrentCharacter.InventoryGears.Remove(gear);
                            MainWindow.Instance.CurrentCharacter.Equip(gear);
                            RenderCharacter();
                            MainWindow.Logger.Log(gear.Name + " has been equipped.");
                        });
                    }
                    else
                    {
                        MainWindow.Logger.Warning(gear.Name + " couldn't been equipped.");
                    }
                });
            }
            else
                MainWindow.Logger.Warning("You can't equip that gear (class or level)");
        }

        private void Gear_Selling(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            // Delete it from character inventory
            MainWindow.Instance.CurrentCharacter.Money += gear.Price;
            MainWindow.Instance.CurrentCharacter.InventoryGears.Remove(gear);
            RenderCharacter();
            // Add it to dealer UC
            HomePage.DealerUC.ListGear.Add(gear);
            HomePage.DealerUC.RenderGears(true);
        }

        private void Gear_Buying(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            // Character has to have enough money
            if (MainWindow.Instance.CurrentCharacter.Money >= gear.Price)
            {
                Task.Factory.StartNew(async () =>
                {
                    int price = gear.Price;
                    // Divide price
                    gear.Price = Convert.ToInt32(Math.Ceiling(gear.Price / 1.5));
                    int result = await AGearRoutes.CreateGear(gear, gear.ItemBaseId);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        HomePage.DealerUC.ListGear.Remove(gear);
                        MainWindow.Instance.CurrentCharacter.InventoryGears.Add(gear);
                        MainWindow.Instance.CurrentCharacter.Money -= price;
                        RenderCharacter();
                        HomePage.DealerUC.RenderGears(true);
                        MainWindow.Logger.Log(gear.Name + " has been bought !");
                    });
                });
            }
        }

        private void Usable_Equiping(object sender, EventArgs e)
        {
            Usable usable = (sender as Usable);
            ICharacter character = MainWindow.Instance.CurrentCharacter;
            MainWindow.Instance.CurrentCharacter.InventoryUsables.Remove(usable);
            MainWindow.Instance.CurrentCharacter.Equip(usable);
            RenderCharacter(true);
        }

        private void Gear_Unequiping(object sender, EventArgs e)
        {
            Gear gear = (sender as Gear);
            MainWindow.Instance.CurrentCharacter.Unequip(gear);

            Task.Factory.StartNew(async () =>
            {
                // Bind id in inventory attribute
                Gear updatedGear = await AGearRoutes.PutInInventory(gear.Id, MainWindow.Instance.CurrentCharacter.Id);
                // Save new state of character
                ICharacter updatedCharacter = await ACharacterRoutes.PutCharacter(MainWindow.Instance.CurrentCharacter);
                if (updatedGear.CharacterInventoryId.HasValue)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        gear = updatedGear;
                        MainWindow.Instance.CurrentCharacter = updatedCharacter;
                        RenderCharacter();
                        MainWindow.Logger.Log(gear.Name + " has been unequipped.");
                    });
                }
                else
                {
                    MainWindow.Instance.CurrentCharacter.InventoryGears.Remove(gear);
                    MainWindow.Instance.CurrentCharacter.Equip(gear);
                    MainWindow.Logger.Warning(gear.Name + " couldn't been equipped.");
                }
            });
        }

        private void Usable_Unequiping(object sender, EventArgs e)
        {
            Usable usable = (sender as Usable);
            MainWindow.Instance.CurrentCharacter.Unequip(usable);
            RenderCharacter();
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
        #endregion

        #region Events
        #endregion
    }
}
