using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShakesAndFidget.Views;

namespace ShakesAndFidget.ViewModels
{
    class ConnectionViewModel
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        private ConnectionPage page1;

        public ConnectionPage Page1 { get => page1; set => page1 = value; }
        #endregion

        #region Attributs
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ConnectionViewModel(ConnectionPage page1)
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
            this.page1.ConnectionUC.LogIn.Click += BtnNavigate_Click;
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
