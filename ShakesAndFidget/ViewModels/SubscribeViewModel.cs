using Database.MySQL;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShakesAndFidget.ViewModels
{
    class SubscribeViewModel
    {
        #region Variables

        private SubscribePage page1;
        public SubscribePage Page1 { get => page1; set => page1 = value; }

        #endregion

        #region Attributs
        public UserManager userManager;
        #endregion

        #region Constructors
        public SubscribeViewModel(SubscribePage page1)
        {
            userManager = new UserManager();
            this.page1 = page1;
            this.page1.SubscribeUC.LogIn.Click += LogIn_Click;
            this.page1.SubscribeUC.Subscribe.Click += Subscribe_Click;
        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            if (this.page1.SubscribeUC.isFormValidForSubscription())
            {
                Subscribe(
                    this.page1.SubscribeUC.Name.Text,
                    this.page1.SubscribeUC.MailBox.Text,
                    this.page1.SubscribeUC.RealPassword.Password
                    );
            }
        }
        #endregion

        #region Functions
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.CurrentPage = new ConnectionPage();
        }

        private void Subscribe(string name, string mail, string password)
        {
            Task.Factory.StartNew(async () =>
            {
                Console.WriteLine(userManager.GetByName(name));
                if (userManager.GetByName(name) == null && userManager.GetByMail(mail) == null) // truc qui vérifie si les infos sont déja dans la base
                {
                    bool insertion = await userManager.InsertUser(mail, name, UserManager.CalculateMD5Hash(this.page1.SubscribeUC.RealPassword.Password));
                    if (insertion)
                    {
                        Application.Current.Dispatcher.Invoke(() => MainWindow.Logger.Success("C'est okeee"));
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(() => MainWindow.Logger.Success("C'est pas oke, déja utilisé !"));
                    }
                }
                else
                {
                    MainWindow.Logger.Warning("Name or mail is already used");
                }
            });
        }

        #endregion
    }
}
