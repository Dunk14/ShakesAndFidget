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
        private FirstConnectionPage page1;

        public FirstConnectionPage Page1 { get => page1; set => page1 = value; }
        #endregion

        #region Attributs
        #endregion

        #region Properties
        public int CurrentIndex { get; set; }
        public Boolean IsFemale { get; set; }
        Character[] CharactersListM { get; set; }
        Character[] CharactersListF { get; set; }
        Gear[] Gears { get; set; }
        #endregion

        #region Constructors
        public FirstConnectionViewModel(FirstConnectionPage page1)
        {
            this.page1 = page1;
            Events();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void InitializeViewModel()
        {
            this.page1.FirstConnectionUC.User_name.Content = MainWindow.Instance.CurrentUser.Name;

            CharactersListM = new Character[3];
            CharactersListM[0] = new Warrior("M", true);
            CharactersListM[1] = new Hunter("M", true);
            CharactersListM[2] = new Magus("M", true);

            CharactersListF = new Character[3];
            CharactersListF[0] = new Warrior("F", true);
            CharactersListF[1] = new Hunter("F", true);
            CharactersListF[2] = new Magus("F", true);
            CurrentIndex = 0;
            IsFemale = false;
            Render();
        }

        private void Events()
        {
            this.page1.FirstConnectionUC.Validate.Click += Validate_Click;
            this.page1.FirstConnectionUC.Validate_Yes.Click += Validate_Yes_Click;
            this.page1.FirstConnectionUC.Validate_No.Click += Validate_No_Click;
            this.page1.FirstConnectionUC.Loaded += FirstConnectionUC_Loaded;
            this.page1.FirstConnectionUC.IsFemaleButton.Checked += IsFemaleButton_Checked;
            this.page1.FirstConnectionUC.IsFemaleButton.Unchecked += IsFemaleButton_Unchecked;
            this.page1.FirstConnectionUC.PreviousCharacter.Click += PreviousCharacter_Click;
            this.page1.FirstConnectionUC.NextCharacter.Click += NextCharacter_Click;
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
                this.page1.FirstConnectionUC.ImageCharacter.Source = new BitmapImage(new Uri(CharactersListF[CurrentIndex].LoadImage()));
            else
                this.page1.FirstConnectionUC.ImageCharacter.Source = new BitmapImage(new Uri(CharactersListM[CurrentIndex].LoadImage()));
        }

        private void RenderCharacterStats()
        {
            if (IsFemale)
                this.page1.FirstConnectionUC.CharacterStatsUC.RenderCharacterStats(CharactersListF[CurrentIndex]);
            else
                this.page1.FirstConnectionUC.CharacterStatsUC.RenderCharacterStats(CharactersListF[CurrentIndex]);
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
            if (this.page1.FirstConnectionUC.CharacterName.Text != "")
            {
                this.page1.FirstConnectionUC.Validate.Visibility = System.Windows.Visibility.Collapsed;
                this.page1.FirstConnectionUC.Validate_Label.Visibility = System.Windows.Visibility.Visible;
                this.page1.FirstConnectionUC.Validate_Yes.Visibility = System.Windows.Visibility.Visible;
                this.page1.FirstConnectionUC.Validate_No.Visibility = System.Windows.Visibility.Visible;
            }
            else
                MainWindow.Logger.Warning("Enter a name for your character please");
        }

        private void Validate_No_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.page1.FirstConnectionUC.Validate.Visibility = System.Windows.Visibility.Visible;
            this.page1.FirstConnectionUC.Validate_Label.Visibility = System.Windows.Visibility.Collapsed;
            this.page1.FirstConnectionUC.Validate_Yes.Visibility = System.Windows.Visibility.Collapsed;
            this.page1.FirstConnectionUC.Validate_No.Visibility = System.Windows.Visibility.Collapsed;
        }

        private async void Validate_Yes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int result = 0;
            if (IsFemale)
            {
                CharactersListF[CurrentIndex].Name = this.page1.FirstConnectionUC.CharacterName.Text;
                result = await CharacterRoutes.CreateCharacter(CharactersListF[CurrentIndex], MainWindow.Instance.CurrentUser.Id);
            }
            else
            {
                CharactersListM[CurrentIndex].Name = this.page1.FirstConnectionUC.CharacterName.Text;
                result = await CharacterRoutes.CreateCharacter(CharactersListM[CurrentIndex], MainWindow.Instance.CurrentUser.Id);
            }

            if (result >= 0)
            {
                MainWindow.Instance.CurrentCharacter = IsFemale ? CharactersListF[CurrentIndex] : CharactersListM[CurrentIndex];
                MainWindow.Instance.CurrentPage = new HomePage();
            }
            else if (result == -1)
                MainWindow.Logger.Error("Stats and Character tables not created");
            else if (result == -2)
                MainWindow.Logger.Error("Stats table created but Character has not");
        }
        #endregion

        #region Events
        #endregion
    }
}
