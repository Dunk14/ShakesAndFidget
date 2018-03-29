using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public abstract class ICharacter : Stats
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
        public int? HeadId { get; set; }
        public int? ArmorId { get; set; }
        public int? LegsId { get; set; }
        public int? Ring1Id { get; set; }
        public int? Ring2Id { get; set; }
        public int? Usable1Id { get; set; }
        public int? Usable2Id { get; set; }
        public int? SpecialId { get; set; }
        public int? AttackId { get; set; }
        #endregion

        #region Constructors
        public ICharacter() : base()
        {

        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public abstract string LoadImage();
        public abstract string LoadSpecial();
        public abstract string LoadAttack();
        #endregion

        #region Events
        #endregion
    }
}
