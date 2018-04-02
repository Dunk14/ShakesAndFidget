using Newtonsoft.Json;
using ShakesAndFidgetLibrary.Routes;
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
        public const string IMAGE_SOURCE_ITEM = "pack://application:,,,/Resources/Inventory Item.png";
        public const string IMAGE_SOURCE_HEAD = "pack://application:,,,/Resources/Inventory Helmet.png";
        public const string IMAGE_SOURCE_ARMOR = "pack://application:,,,/Resources/Inventory Armor.png";
        public const string IMAGE_SOURCE_USABLE = "pack://application:,,,/Resources/Inventory Usable.png";
        public const string IMAGE_SOURCE_LEGS = "pack://application:,,,/Resources/Inventory Legs.png";
        #endregion

        #region Variables
        #endregion

        #region Attributs
        #endregion

        #region Properties
        public virtual string ImageSourceM => "";
        public virtual string ImageSourceF => "";
        public virtual string ImageSourceSpecial => "";
        public virtual string ImageSourceAttack => "";

        public string Name { get; set; }
        public string Type { get; set; }
        public string Sexe { get; set; }
        public int Level { get; set; }
        public int UserId { get; set; }
        public int StatId { get; set; }
        [JsonIgnore]
        public Gear Head { get; set; }
        public int? HeadId { get; set; }
        [JsonIgnore]
        public Gear Armor { get; set; }
        public int? ArmorId { get; set; }
        [JsonIgnore]
        public Gear Legs { get; set; }
        public int? LegsId { get; set; }
        [JsonIgnore]
        public Gear Ring1 { get; set; }
        public int? Ring1Id { get; set; }
        [JsonIgnore]
        public Gear Ring2 { get; set; }
        public int? Ring2Id { get; set; }
        [JsonIgnore]
        public Gear Usable { get; set; }
        public int? UsableId { get; set; }
        [JsonIgnore]
        public Gear Special { get; set; }
        public int? SpecialId { get; set; }
        [JsonIgnore]
        public Gear Attack { get; set; }
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
        public string LoadCharacterImage()
        {
            if (Sexe == "M")
                return ImageSourceM;
            else
                return ImageSourceF;
        }
        
        public async Task<string> LoadHeadImage()
        {
            if (Head != null)
                return Head.ImageSource;
            else if (HeadId.HasValue)
            {
                Head = await AGearRoutes.GetOne(HeadId.Value);
                return Head.ImageSource;
            }
            return IMAGE_SOURCE_HEAD;
        }
        
        public async Task<string> LoadArmorImage()
        {
            if (Armor != null)
                return Armor.ImageSource;
            else if (ArmorId.HasValue)
            {
                Armor = await AGearRoutes.GetOne(ArmorId.Value);
                return Armor.ImageSource;
            }
            return IMAGE_SOURCE_ARMOR;
        }
        
        public async Task<string> LoadLegsImage()
        {
            if (Legs != null)
                return Legs.ImageSource;
            else if (LegsId.HasValue)
            {
                Legs = await AGearRoutes.GetOne(LegsId.Value);
                return Legs.ImageSource;
            }
            return IMAGE_SOURCE_LEGS;
        }
        
        public async Task<string> LoadRing1Image()
        {
            if (Ring1 != null)
                return Ring2.ImageSource;
            else if (Ring1Id.HasValue)
            {
                Ring1 = await AGearRoutes.GetOne(Ring1Id.Value);
                return Ring1.ImageSource;
            }
            return IMAGE_SOURCE_ITEM;
        }
        
        public async Task<string> LoadRing2Image()
        {
            if (Ring2 != null)
                return Ring2.ImageSource;
            else if (Ring2Id.HasValue)
            {
                Ring2 = await AGearRoutes.GetOne(Ring2Id.Value);
                return Ring2.ImageSource;
            }
            return IMAGE_SOURCE_ITEM;
        }
        
        public async Task<string> LoadUsableImage()
        {
            if (Usable != null)
                return Usable.ImageSource;
            else if (UsableId.HasValue)
            {
                Usable = await AGearRoutes.GetOne(UsableId.Value);
                return Usable.ImageSource;
            }
            return IMAGE_SOURCE_USABLE;
        }
        
        public async Task<string> LoadSpecialImage()
        {
            if (Special != null)
                return Attack.ImageSource;
            else if (SpecialId.HasValue)
            {
                Special = await AGearRoutes.GetOne(SpecialId.Value);
                return Special.ImageSource;
            }
            return ImageSourceSpecial;
        }

        public async Task<string> LoadAttackImage()
        {
            if (Attack != null)
                return Attack.ImageSource;
            else if (AttackId.HasValue)
            {
                Attack = await AGearRoutes.GetOne(AttackId.Value);
                return Attack.ImageSource;
            }
            return ImageSourceAttack;
        }
        #endregion

        #region Events
        #endregion
    }
}
