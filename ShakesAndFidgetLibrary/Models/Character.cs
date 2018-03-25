using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    [Table("character")]
    public class Character : Stats
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        private String name;
        private String sexe;
        private int level;
        private User user;
        private int user_id;

        //public String imageSource;
        #endregion

        #region Attributs
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [MaxLength(1)]
        public String Sexe
        {
            get { return sexe; }
            set { sexe = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [ForeignKey("User_id")]
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        #endregion

        #region Constructors
        public Character(String name, String sexe, int level, User user): base()
        {
            this.name = name;
            this.sexe = sexe;
            this.level = level;
            this.user = user;
        }

        public Character() : base()
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
