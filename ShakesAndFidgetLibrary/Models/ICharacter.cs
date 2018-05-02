﻿using Newtonsoft.Json;
using ShakesAndFidgetLibrary.Routes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string name;
        private string type;
        private string sexe;
        private int level;
        private Gear head;
        private Gear armor;
        private Gear legs;
        private Gear ring1;
        private Gear ring2;
        private Gear attack;
        private Gear special;
        private Usable usable;
        #endregion

        #region Properties
        public virtual string ImageSourceM => "";
        public virtual string ImageSourceF => "";
        public virtual string ImageSourceSpecial => "";
        public virtual string ImageSourceAttack => "";

        public string Name
        {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        
        public string Type
        {
            get { return type; }
            set {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public string Sexe
        {
            get { return sexe; }
            set {
                sexe = value;
                OnPropertyChanged("Sexe");
            }
        }

        public int Level
        {
            get { return level; }
            set {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        public int UserId { get; set; }
        public int StatId { get; set; }

        [JsonIgnore]
        public Gear Head
        {
            get { return head; }
            set {
                head = value;
                OnPropertyChanged("Head");
            }
        }
        public int? HeadId { get; set; }

        [JsonIgnore]
        public Gear Armor
        {
            get { return armor; }
            set {
                armor = value;
                OnPropertyChanged("Armor");
            }
        }
        public int? ArmorId { get; set; }

        [JsonIgnore]
        public Gear Legs
        {
            get { return legs; }
            set {
                legs = value;
                OnPropertyChanged("Legs");
            }
        }
        public int? LegsId { get; set; }

        [JsonIgnore]
        public Gear Ring1
        {
            get { return ring1; }
            set {
                ring1 = value;
                OnPropertyChanged("Ring1");
            }
        }
        public int? Ring1Id { get; set; }

        [JsonIgnore]
        public Gear Ring2
        {
            get { return ring2; }
            set {
                ring2 = value;
                OnPropertyChanged("Ring2");
            }
        }
        public int? Ring2Id { get; set; }

        [JsonIgnore]
        public Gear Special
        {
            get { return special; }
            set {
                special = value;
                OnPropertyChanged("Special");
            }
        }
        public int? SpecialId { get; set; }

        [JsonIgnore]
        public Gear Attack
        {
            get { return attack; }
            set {
                attack = value;
                OnPropertyChanged("Attack");
            }
        }
        public int? AttackId { get; set; }

        [JsonIgnore]
        public Usable Usable
        {
            get { return usable; }
            set
            {
                usable = value;
                OnPropertyChanged("Usable");
            }
        }
        public int? UsableId { get; set; }

        private ObservableCollection<Gear> inventoryGears;

        public ObservableCollection<Gear> InventoryGears
        {
            get { return inventoryGears; }
            set {
                inventoryGears = value;
                OnPropertyChanged("InventoryGears");
            }
        }

        private ObservableCollection<Usable> inventoryUsables;

        public ObservableCollection<Usable> InventoryUsables
        {
            get { return inventoryUsables; }
            set {
                inventoryUsables = value;
                OnPropertyChanged("InventoryUsables");
            }
        }
        #endregion

        #region Constructors
        public ICharacter() : base()
        {

        }
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        public void Equip(Gear gear)
        {
            if (gear.GearType == "Head")
            {
                if (Head != null)
                    InventoryGears.Add(Head);
                Head = gear;
                HeadId = Head.Id;
            }
            else if (gear.GearType == "Armor")
            {
                if (Armor != null)
                    InventoryGears.Add(Armor);
                Armor = gear;
                ArmorId = Armor.Id;
            }
            else if (gear.GearType == "Legs")
            {
                if (Legs != null)
                    InventoryGears.Add(Legs);
                Legs = gear;
                LegsId = Legs.Id;
            }
            else if (gear.GearType == "Ring")
            {
                if (Ring1 == null)
                {
                    Ring1 = gear;
                    Ring1Id = Ring1.Id;
                }
                else
                {
                    if (Ring2 != null)
                        InventoryGears.Add(Ring2);
                    Ring2 = gear;
                    Ring1Id = Ring2.Id;
                }
            }
            else if (gear.GearType == "Special")
            {
                if (Special != null)
                    InventoryGears.Add(Special);
                Special = gear;
                SpecialId = Special.Id;
            }
            else if (gear.GearType == "Attack")
            {
                if (Attack != null)
                    InventoryGears.Add(Attack);
                Attack = gear;
                AttackId = Attack.Id;
            }
        }

        public void Unequip(Gear gear)
        {
            if (gear == Head)
            {
                InventoryGears.Add(gear);
                Head = null;
                HeadId = null;
            }
            else if (gear == Armor)
            {
                InventoryGears.Add(gear);
                Armor = null;
                ArmorId = null;
            }
            else if (gear == Legs)
            {
                InventoryGears.Add(gear);
                Legs = null;
                LegsId = null;
            }
            else if (gear == Ring1)
            {
                InventoryGears.Add(gear);
                Ring1 = null;
                Ring1Id = null;
            }
            else if (gear == Ring2)
            {
                InventoryGears.Add(gear);
                Ring2 = null;
                Ring2Id = null;
            }
            else if (gear == Special)
            {
                InventoryGears.Add(gear);
                Special = null;
                SpecialId = null;
            }
            else if (gear == Attack)
            {
                InventoryGears.Add(gear);
                Attack = null;
                AttackId = null;
            }
        }

        public void Equip(Usable usable)
        {
            if (Usable != null)
                InventoryUsables.Add(Usable);
            Usable = usable;
            UsableId = Usable.Id;
        }

        public void Unequip(Usable usable)
        {
            if (usable == Usable)
            {
                InventoryUsables.Add(usable);
                Usable = null;
                UsableId = null;
            }
        }

        public Stats ComputeStats()
        {
            Stats stats = new Stats
            {
                Life = Life,
                Mana = Mana,
                Energy = Energy,
                Strength = Strength,
                Agility = Agility,
                Spirit = Spirit,
                Luck = Luck,
                CriticalDamage = CriticalDamage,
                MagicDamage = MagicDamage,
                PhysicalDamage = PhysicalDamage,
                CriticalProba = CriticalProba,
                PhysicalArmor = PhysicalArmor,
                MagicalArmor = MagicalArmor
            };

            if (Head != null)
                stats.AddStats(Head);
            if (Armor != null)
                stats.AddStats(Armor);
            if (Legs != null)
                stats.AddStats(Legs);
            if (Ring1 != null)
                stats.AddStats(Ring1);
            if (Ring2 != null)
                stats.AddStats(Ring2);
            if (Special != null)
                stats.AddStats(Special);
            if (Attack != null)
                stats.AddStats(Attack);
            if (Usable != null)
                stats.AddStats(Usable);

            return stats;
        }

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
