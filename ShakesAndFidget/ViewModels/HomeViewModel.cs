using ShakesAndFidget.Views;
using System;
using System.Collections.Generic;
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
        }

        private void EquipmentUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            HomePage.EquipmentUC.Character = MainWindow.Instance.CurrentCharacter;
            HomePage.EquipmentUC.RenderItems();
        }

        #endregion

        #region Events
        #endregion
    }
}
