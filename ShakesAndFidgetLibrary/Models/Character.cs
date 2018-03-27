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
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sexe { get; set; }
        public int Level { get; set; }
        public int UserId { get; set; }
        public int StatId { get; set; }
        public int? HeadGearId { get; set; }
        public int? Earring1Id { get; set; }
        public int? Earring2Id { get; set; }
        public int? ChestId { get; set; }
        public int? LegsId { get; set; }
        public int? Ring1Id { get; set; }
        public int? Ring2Id { get; set; }
        public int? FeetId { get; set; }
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
