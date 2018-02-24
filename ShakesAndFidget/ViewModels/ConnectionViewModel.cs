using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Media;
using Database.MySQL;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using System.Threading;
using System.Windows;

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
            {
                User userDb = this.Login(this.page1.ConnectionUC.Name.Text, this.page1.ConnectionUC.RealPassword.Password);
                if (userDb != null)
                {
                    User = userDb;
                    MainWindow.Instance.CurrentUser = User;
                    MainWindow.Instance.CurrentPage = new FirstConnectionPage();
                }
            }
        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            if (page1.ConnectionUC.isFormValidForSubscription())
            {
                Subscribe(
                    page1.ConnectionUC.Name.Text,
                    page1.ConnectionUC.Mail.Text,
                    page1.ConnectionUC.RealPassword.Password
                );
            }
        }

        private User Login(string name, string password)
        {
            List<User> usersList = userManager.GetByNameAndPassword(
                this.page1.ConnectionUC.Name.Text, 
                UserManager.CalculateMD5Hash(this.page1.ConnectionUC.RealPassword.Password)
            );
            if (usersList.Count == 1)
            {
                return usersList[0];
            }
            ChangeMessageAsync("Fail to login, please check your name and password", 200, 0, 0, 3000);
            return null;
        }

        private void Subscribe(string name, string mail, string password)
        {
            if (userManager.GetByName(name) == null && userManager.GetByMail(mail) == null)
            {
                Task.Factory.StartNew(async () =>
                {
                    bool insertion = await userManager.InsertUser(mail, name, UserManager.CalculateMD5Hash(this.page1.ConnectionUC.RealPassword.Password));
                    Application.Current.Dispatcher.Invoke(new System.Action(() => { Subscribe_Callback(insertion); }));
                });
            }
            else
            {
                ChangeMessageAsync("Please change your name or your mail", 200, 0, 0, 3000);
            }
        }

        private void Subscribe_Callback(bool success)
        {
            if (success)
            {
                ChangeMessageAsync("You successfully subscribed !", 0, 200, 0, 3000);
            }
            else
            {
                ChangeMessageAsync("Subscription didn't end properly", 200, 0, 0, 3000);
            }
        }

        private void ChangeMessageAsync(String message, byte red, byte green, byte blue, int delay)
        {
            this.page1.ConnectionUC.Message.Content = message;
            this.page1.ConnectionUC.Message.Foreground = new SolidColorBrush(Color.FromRgb(red, green, blue));
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(delay);
                Application.Current.Dispatcher.Invoke(new System.Action(ResetMessage));
            });
        }

        private void ResetMessage()
        {
            page1.ConnectionUC.Message.Content = "";
        }
        #endregion

        #region Events
        #endregion
    }
}
