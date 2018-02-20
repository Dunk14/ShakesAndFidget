using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public class Character
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        private string name;
        private char sexe;
        private int level;
        #endregion

        #region Attributs
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public char Sexe
        {
            get { return sexe; }
            set { sexe = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        #endregion

        #region Constructors
        public Character()
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
