using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.MySQL;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;

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
        private User user1;
        #endregion

        #region Properties
        public User User1
        {
            get { return user1; }
            set { user1 = value; }
        }
        #endregion

        #region Constructors
        public ConnectionViewModel(ConnectionPage page1)
        {
            MySQLManager<User> UserManager = new MySQLManager<User>();

            this.user1 = UserManager.Get(1).Result;
            this.page1 = page1;
            this.page1.ConnectionUC.Mail.Text = this.user1.Mail;
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
