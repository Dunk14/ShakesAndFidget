using ShakesAndFidget.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidget.ViewModels
{
    public class HomeViewModel
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        private HomePage page1;

        public HomePage Page1 { get => page1; set => page1 = value; }
        #endregion

        #region Attributs
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public HomeViewModel(HomePage page1)
        {
            this.page1 = page1;
            Events();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void Events()
        {
            this.page1.HomeUC.btn1.Click += BtnNavigate_Click;
        }

        private void BtnNavigate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.Instance.CurrentPage = new FirstConnectionPage();
        }
        #endregion

        #region Events
        #endregion
    }
}
