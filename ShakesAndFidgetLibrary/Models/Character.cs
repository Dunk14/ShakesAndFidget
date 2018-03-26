using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public abstract class Character : Stats
    {
        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        #endregion

        #region Properties
        public string name { get; set; }
        public string sexe { get; set; }
        public int level { get; set; }
        public int userId { get; set; }
        public int statId { get; set; }
        public int headGearId { get; set; }
        public int earring1Id { get; set; }
        public int earring2Id { get; set; }
        public int chestId { get; set; }
        public int legsId { get; set; }
        public int ring1Id { get; set; }
        public int ring2Id { get; set; }
        public int feetId { get; set; }
        #endregion

        #region Constructors
        public Character() : base()
        {

        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public abstract string LoadImage();
        #endregion

        #region Events
        #endregion
    }
}
