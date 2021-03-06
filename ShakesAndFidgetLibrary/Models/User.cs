﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShakesAndFidgetLibrary.Models
{
    [Table("user")]
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
        //private List<Character> characters;
        #endregion

        #region Properties
        [Column("mail")]
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

        [Column("name")]
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

        [Column("password")]
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        
        //[InverseProperty("User")]
        //public List<Character> Characters
        //{
        //    get { return characters; }
        //    set {
        //        characters = value;
        //        OnPropertyChanged("Characters");
        //    }
        //}
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
