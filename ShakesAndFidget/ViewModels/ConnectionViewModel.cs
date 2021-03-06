﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.MySQL;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using System.Windows;
using System;
using ShakesAndFidgetLibrary.Routes;

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
            MainWindow.Instance.CurrentPage = new SubscribePage();
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
                        GoToNextPage(usersList[0].Id);
                    }
                    else
                        Application.Current.Dispatcher.Invoke(() => MainWindow.Logger.Error("Fail to login, please check your name and password"));
                });
            });
            
        }

        private async void GoToNextPage(int userId)
        {
            if (MainWindow.Instance.CurrentUser != null && await ACharacterRoutes.CountByUserId(userId) == 0)
                MainWindow.Instance.CurrentPage = new FirstConnectionPage();
            else if (MainWindow.Instance.CurrentUser != null && await ACharacterRoutes.CountByUserId(userId) == 1)
            {
                MainWindow.Instance.CurrentCharacter = await ACharacterRoutes.GetCharacter(userId);
                MainWindow.Instance.CurrentPage = new HomePage();
            }
        }
        #endregion

        #region Events
        #endregion
    }
}
