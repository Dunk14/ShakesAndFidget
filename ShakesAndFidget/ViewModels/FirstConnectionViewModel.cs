using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using ShakesAndFidgetLibrary.Models.Characters;
using ShakesAndFidgetLibrary.Routes;

namespace ShakesAndFidget.ViewModels
{
    class FirstConnectionViewModel
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        #endregion

        #region Properties
        public FirstConnectionPage FirstConnectionPage { get; set; }
        public int CurrentIndex { get; set; }
        public Boolean IsFemale { get; set; }
        ICharacter[] CharactersListM { get; set; }
        ICharacter[] CharactersListF { get; set; }
        List<GearBase> GearBases { get; set; }
        Gear[] Gears { get; set; }
        Stats[] Stats { get; set; }
        #endregion

        #region Constructors
        public FirstConnectionViewModel(FirstConnectionPage firstConnectionPage)
        {
            FirstConnectionPage = firstConnectionPage;
            Events();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private async void InitializeViewModel()
        {
            FirstConnectionPage.FirstConnectionUC.User_name.Content = MainWindow.Instance.CurrentUser.Name;

            GearBases = await AGearBaseRoutes.GetAllGearBases();
            Gears = new Gear[3];
            Gears[0] = GearBases.Find(x => x.Name == "Big Sword").ToGear();
            Gears[1] = GearBases.Find(x => x.Name == "Bow").ToGear();
            Gears[2] = GearBases.Find(x => x.Name == "Staff").ToGear();

            Stats = new Stats[3];
            Stats[0] = GearBases.Find(x => x.Name == "Big Sword").ToStats();
            Stats[1] = GearBases.Find(x => x.Name == "Bow").ToStats();
            Stats[2] = GearBases.Find(x => x.Name == "Staff").ToStats();

            CharactersListM = new ICharacter[3];
            CharactersListM[0] = new Warrior("M", true);
            CharactersListM[1] = new Hunter("M", true);
            CharactersListM[2] = new Magus("M", true);

            CharactersListF = new ICharacter[3];
            CharactersListF[0] = new Warrior("F", true);
            CharactersListF[1] = new Hunter("F", true);
            CharactersListF[2] = new Magus("F", true);

            CurrentIndex = 0;
            IsFemale = false;
            Render();
        }

        private void Events()
        {
            FirstConnectionPage.FirstConnectionUC.Validate.Click += Validate_Click;
            FirstConnectionPage.FirstConnectionUC.Validate_Yes.Click += Validate_Yes_Click;
            FirstConnectionPage.FirstConnectionUC.Validate_No.Click += Validate_No_Click;
            FirstConnectionPage.FirstConnectionUC.Loaded += FirstConnectionUC_Loaded;
            FirstConnectionPage.FirstConnectionUC.IsFemaleButton.Checked += IsFemaleButton_Checked;
            FirstConnectionPage.FirstConnectionUC.IsFemaleButton.Unchecked += IsFemaleButton_Unchecked;
            FirstConnectionPage.FirstConnectionUC.PreviousCharacter.Click += PreviousCharacter_Click;
            FirstConnectionPage.FirstConnectionUC.NextCharacter.Click += NextCharacter_Click;
        }

        private void NextCharacter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentIndex++;
            if (CurrentIndex > 2)
                CurrentIndex = 0;
            Render();
        }

        private void PreviousCharacter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentIndex--;
            if (CurrentIndex < 0)
                CurrentIndex = 2;
            Render();
        }

        private void Render()
        {
            RenderCharacterImage();
            RenderCharacterStats();
        }

        private void RenderCharacterImage()
        {
            if (IsFemale)
                FirstConnectionPage.FirstConnectionUC.ImageCharacter.Source = new BitmapImage(new Uri(CharactersListF[CurrentIndex].LoadCharacterImage()));
            else
                FirstConnectionPage.FirstConnectionUC.ImageCharacter.Source = new BitmapImage(new Uri(CharactersListM[CurrentIndex].LoadCharacterImage()));
        }

        private void RenderCharacterStats()
        {
            if (IsFemale)
                FirstConnectionPage.FirstConnectionUC.CharacterStatsUC.RenderCharacterStats(CharactersListF[CurrentIndex]);
            else
                FirstConnectionPage.FirstConnectionUC.CharacterStatsUC.RenderCharacterStats(CharactersListF[CurrentIndex]);
        }

        private void IsFemaleButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as ToggleButton).Content = "MALE";
            IsFemale = false;
            RenderCharacterImage();
        }

        private void IsFemaleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as ToggleButton).Content = "FEMALE";
            IsFemale = true;
            RenderCharacterImage();
        }

        private void FirstConnectionUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitializeViewModel();
        }

        private void Validate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!FirstConnectionPage.FirstConnectionUC.isPlaceholder)
            {
                FirstConnectionPage.FirstConnectionUC.Validate.Visibility = System.Windows.Visibility.Collapsed;
                FirstConnectionPage.FirstConnectionUC.Validate_Label.Visibility = System.Windows.Visibility.Visible;
                FirstConnectionPage.FirstConnectionUC.Validate_Yes.Visibility = System.Windows.Visibility.Visible;
                FirstConnectionPage.FirstConnectionUC.Validate_No.Visibility = System.Windows.Visibility.Visible;
            }
            else
                MainWindow.Logger.Warning("Enter a name for your character please");
        }

        private void Validate_No_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FirstConnectionPage.FirstConnectionUC.Validate.Visibility = System.Windows.Visibility.Visible;
            FirstConnectionPage.FirstConnectionUC.Validate_Label.Visibility = System.Windows.Visibility.Collapsed;
            FirstConnectionPage.FirstConnectionUC.Validate_Yes.Visibility = System.Windows.Visibility.Collapsed;
            FirstConnectionPage.FirstConnectionUC.Validate_No.Visibility = System.Windows.Visibility.Collapsed;
        }

        private async void Validate_Yes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // First step is to create Gear with Stats relation
            int resultGear = await AGearRoutes.CreateGear(Stats[CurrentIndex], GearBases[CurrentIndex].Id);
            if (resultGear >= 0)
            {
                // Link generated Id on Gear
                Gears[CurrentIndex].Id = resultGear;

                int resultCharacter = 0;
                // Attribute name to the character and send Character creation with Stats relation too
                if (IsFemale)
                {
                    CharactersListF[CurrentIndex].Name = FirstConnectionPage.FirstConnectionUC.CharacterName.Text;
                    CharactersListF[CurrentIndex].Attack = Gears[CurrentIndex];
                    CharactersListF[CurrentIndex].AttackId = resultGear;
                    resultCharacter = await ACharacterRoutes.CreateCharacter(CharactersListF[CurrentIndex], MainWindow.Instance.CurrentUser.Id);
                }
                else
                {
                    CharactersListM[CurrentIndex].Name = FirstConnectionPage.FirstConnectionUC.CharacterName.Text;
                    CharactersListM[CurrentIndex].Attack = Gears[CurrentIndex];
                    CharactersListM[CurrentIndex].AttackId = resultGear;
                    resultCharacter = await ACharacterRoutes.CreateCharacter(CharactersListM[CurrentIndex], MainWindow.Instance.CurrentUser.Id);
                }

                if (resultCharacter >= 0)
                {
                    MainWindow.Instance.CurrentCharacter = IsFemale ? CharactersListF[CurrentIndex] : CharactersListM[CurrentIndex];
                    MainWindow.Instance.CurrentPage = new HomePage();
                }
                else if (resultCharacter == -1)
                    MainWindow.Logger.Error("Stats and Character tables not created");
                else if (resultCharacter == -2)
                    MainWindow.Logger.Error("Stats table created but Character has not");
            }
            else if (resultGear == 1)
                MainWindow.Logger.Error("Stat and Gear not created");
            else if (resultGear == -2)
                MainWindow.Logger.Error("Stat created but Gear has not");
            
        }
        #endregion

        #region Events
        #endregion
    }
}
