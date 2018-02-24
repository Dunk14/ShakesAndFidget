﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShakesAndFidgetLibrary.Models
{
    public class User : ModelBase
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private String mail;
        private String name;
        private String password;
        #endregion

        #region Properties
        [Index(IsUnique = true)]
        public String Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }

        [Index(IsUnique = true)]
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        #region Constructors

        public User(string mail, string name, string password) : base()
        {
            this.mail = mail;
            this.name = name;
            this.password = password;
        }

        public User() : base()
        {
        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        #endregion

        #region Events
        #endregion
    }
}
