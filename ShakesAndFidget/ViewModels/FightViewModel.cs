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
        private int currentPlayer;
        private List<ICharacter> players = new List<ICharacter>();
        private List<ICharacter> allCharacters;
        
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

        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        public List<ICharacter> Players
        {
            get { return players; }
            set { players = value; }
        }
        ICharacter[] CharactersList { get; set; }
        #endregion

        #region Constructors
        public FightViewModel(FightPage page1)
        {
            userManager = new UserManager();
            this.page1 = page1;
            this.I = 3;
            this.CurrentPlayer = new Random().Next(0, 2);
            this.CharactersList = new ICharacter[6];
            CharactersList[0] = new Warrior("M", true);
            CharactersList[1] = new Hunter("M", true);
            CharactersList[2] = new Magus("M", true);
            CharactersList[3] = new Warrior("F", true);
            CharactersList[4] = new Hunter("F", true);
            CharactersList[5] = new Magus("F", true);
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
            page1.MenuUC.Fight.IsEnabled = false;
        }

        private async void EquipmentEnnemyUC_Loaded(object sender, RoutedEventArgs e)
        {
            page1.EquipmentEnnemyUC.Character = CharactersList[new Random().Next(0,5)];
            page1.EquipmentEnnemyUC.Character.Name = Faker.Name.First();
            List<GearBase> GearBasesList = await AGearBaseRoutes.GetAllGearBasesByCharacterType(page1.EquipmentEnnemyUC.Character.Type);
            page1.EquipmentEnnemyUC.Character.Attack = GearBasesList.Find(x => x.GearType == "Attack").ToGear();
            page1.EquipmentEnnemyUC.Character.Armor = GearBasesList.Find(x => x.GearType == "Armor").ToGear();
            page1.EquipmentEnnemyUC.RenderItems();
            page1.EnnemyStatsUC.RenderCharacterStats(page1.EquipmentEnnemyUC.Character);
            page1.EnnemyLife.Maximum = page1.EquipmentEnnemyUC.Character.Life;
            page1.EnnemyLife.Value = page1.EquipmentEnnemyUC.Character.Life;
            Players.Add(page1.EquipmentEnnemyUC.Character);
        }

        private void EquipmentUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            page1.EquipmentUC.Character = MainWindow.Instance.CurrentCharacter;
            page1.EquipmentUC.RenderItems();
            page1.CharacterStatsUC.RenderCharacterStats(page1.EquipmentUC.Character);
            page1.CharacterLife.Maximum = page1.EquipmentUC.Character.Life;
            page1.CharacterLife.Value = page1.EquipmentUC.Character.Life;
            Players.Add(page1.EquipmentUC.Character);
        }

        private void DisplayButtonStart()
        {
            page1.StartButton.Visibility = Visibility.Visible;
        }

        private void PlayFight()
        {
            page1.FightText.FontSize = 30;
            Attack(CurrentPlayer % 2);
            CurrentPlayer++;
        }

        private void Attack(int Attaker)
        {
            if (page1.EnnemyLife.Value <= 0)
            {
                dispatcherTimer.Stop();
                page1.FightText.Text = MainWindow.Instance.CurrentCharacter.Name + " Win!";
                page1.MenuUC.Fight.IsEnabled = true;
                return;
            }
            if (page1.CharacterLife.Value <= 0)
            {
                dispatcherTimer.Stop();
                page1.MenuUC.Fight.IsEnabled = true;
                page1.FightText.Text = page1.EquipmentEnnemyUC.Character.Name + " Win!";
                return;
            }
            int Other = (Attaker + 1) % 2;
            page1.FightText.Text = Players[Attaker].Name + " slap " + Players[Other].Name;
            if (Attaker == 0)
            {
                page1.EnnemyLife.Value -= Players[Attaker].PhysicalDamage;
            }
            else
            {
                page1.CharacterLife.Value -= Players[Attaker].PhysicalDamage;
            }
        }
        #endregion

        #region Events
        #endregion
    }
}
