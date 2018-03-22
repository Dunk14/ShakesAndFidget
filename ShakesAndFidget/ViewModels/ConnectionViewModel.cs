using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.MySQL;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using System.Windows;
using System;

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
        public UserManager userManager;
        private User user;
        #endregion

        #region Properties
        public User User
        {
            get { return user; }
            set { user = value; }
        }
        #endregion

        #region Constructors
        public ConnectionViewModel(ConnectionPage page1)
        {
            userManager = new UserManager();
            this.page1 = page1;
            Events();
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        private void Events()
        {
            this.page1.ConnectionUC.LogIn.Click += LogIn_Click;
            this.page1.ConnectionUC.Subscribe.Click += Subscribe_Click;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (page1.ConnectionUC.isFormValidForLogIn())
                this.Login(this.page1.ConnectionUC.Name.Text, this.page1.ConnectionUC.RealPassword.Password);
            else
                MainWindow.Logger.Warning("Name or password is invalid");
        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            if (page1.ConnectionUC.isFormValidForSubscription())
                Subscribe(
                    page1.ConnectionUC.Name.Text,
                    page1.ConnectionUC.Mail.Text,
                    page1.ConnectionUC.RealPassword.Password
                );
        }

        private void Login(string name, string password)
        {
            Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(() => {
                    List<User> usersList = userManager.GetByNameAndPassword(
                        this.page1.ConnectionUC.Name.Text, 
                        UserManager.CalculateMD5Hash(this.page1.ConnectionUC.RealPassword.Password)
                    );
                    if (usersList.Count == 1)
                    {
                        MainWindow.Instance.CurrentUser = usersList[0];
                        GoToNextPage();
                    }
                    else
                        Application.Current.Dispatcher.Invoke(() => MainWindow.Logger.Error("Fail to login, please check your name and password"));
                });
            });
            
        }

        private void GoToNextPage()
        {
            if (MainWindow.Instance.CurrentUser != null && MainWindow.Instance.CurrentUser.Characters == null)
                MainWindow.Instance.CurrentPage = new FirstConnectionPage();
        }

        private void Subscribe(string name, string mail, string password)
        {
            Task.Factory.StartNew(async () =>
            {
                Console.WriteLine(userManager.GetByName(name));
                if (userManager.GetByName(name) == null && userManager.GetByMail(mail) == null)
                {
                    bool insertion = await userManager.InsertUser(mail, name, UserManager.CalculateMD5Hash(this.page1.ConnectionUC.RealPassword.Password));
                    if (insertion)
                    {
                        Application.Current.Dispatcher.Invoke(() => Subscribe_Callback(insertion));
                        return;
                    }
                }
                else
                {
                    MainWindow.Logger.Warning("Name or mail is already used");
                }
            });
        }

        private void Subscribe_Callback(bool success)
        {
            if (success)
            {
                MainWindow.Logger.Success("You successfully subscribed !");
            }
            else
            {
                MainWindow.Logger.Error("Subscription didn't end properly");
            }
        }
        #endregion

        #region Events
        #endregion
    }
}
