using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShakesAndFidget.Views;

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
        #endregion

        #region Constructors
        public FirstConnectionViewModel(FirstConnectionPage page1)
        {
            this.page1 = page1;
            this.page1.FirstConnectionUC.UserName.Text = MainWindow.Instance.CurrentUser.Name;
            Events();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void Events()
        {
            this.page1.FirstConnectionUC.Validate.Click += BtnNavigate_Click;
        }

        private void BtnNavigate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.Instance.CurrentPage = new HomePage();
        }
        #endregion

        #region Events
        #endregion
    }
}
