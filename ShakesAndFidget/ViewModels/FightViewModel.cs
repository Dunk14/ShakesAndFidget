using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.MySQL;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using System.Windows;
using System;
using ShakesAndFidgetLibrary.Routes;
using ShakesAndFidgetLibrary.Models.Characters;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Windows.Threading;

namespace ShakesAndFidget.ViewModels
{
    class FightViewModel
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        private FightPage page1;

        public FightPage Page1 { get => page1; set => page1 = value; }
        #endregion

        #region Attributs
        public UserManager userManager;
        private User user;
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private int i;
        #endregion

        #region Properties
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public DispatcherTimer MydispatcherTimer
        {
            get { return dispatcherTimer; }
            set { dispatcherTimer = value; }
        }

        public int I
        {
            get { return i; }
            set { i = value; }
        }

        #endregion

        #region Constructors
        public FightViewModel(FightPage page1)
        {
            userManager = new UserManager();
            this.page1 = page1;
            this.I = 3;
            Events();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void Events()
        {
            page1.EquipmentUC.Loaded += EquipmentUC_Loaded;
            page1.EquipmentEnnemyUC.Loaded += EquipmentEnnemyUC_Loaded;
            page1.StartButton.Click += StartButton_Click;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (I >= 0)
                page1.FightText.Text = I.ToString();
            I--;
            if (I == -1)
            {
                page1.FightText.Text = "Fight !";
            }
            else if (I < -1)
            {
                PlayFight();
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            page1.StartButton.Visibility = Visibility.Hidden;
            page1.FightText.Visibility = Visibility.Visible;
            dispatcherTimer.Start();
        }

        private void EquipmentEnnemyUC_Loaded(object sender, RoutedEventArgs e)
        {
            page1.EquipmentEnnemyUC.Character = new Hunter("M", true);
            Gear gear = new Gear()
            {
                Name = "Dragon Bow",
                ImageSource = "pack://application:,,,/Resources/Dragon Bow.png",
                GearType = "Attack",
                LevelMin = 1
            };
            page1.EquipmentEnnemyUC.Character.Attack = gear;
            page1.EquipmentEnnemyUC.RenderItems();
            page1.EnnemyStatsUC.RenderCharacterStats(page1.EquipmentEnnemyUC.Character);
        }

        private void EquipmentUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            page1.EquipmentUC.Character = MainWindow.Instance.CurrentCharacter;
            page1.EquipmentUC.RenderItems();
            page1.CharacterStatsUC.RenderCharacterStats(page1.EquipmentUC.Character);
        }

        private void DisplayButtonStart()
        {
            page1.StartButton.Visibility = Visibility.Visible;
        }

        private void PlayFight()
        {
            System.Console.WriteLine("Combat en cours!");
        }
        #endregion

        #region Events
        #endregion
    }
}
