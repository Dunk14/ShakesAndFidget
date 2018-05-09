var createError = require('http-errors');
var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var userModel = require('./models/user');
var characterModel = require('./models/character');
var statsModel = require('./models/stats');
var gearModel = require('./models/gear');
var gearBaseModel = require('./models/gearBase');

var usersRouter = require('./routes/user');
var charactersRouter = require('./routes/character');
var gearBaseRouter = require('./routes/gearBase');
var gearRouter = require('./routes/gear');

var app = express();

// CORS
app.use(function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
  res.header("Access-Control-Allow-Headers", "Content-Type, X-Requested-With, Accept, Origin, Access-Control-Request-Method, Access-Control-Request-Headers, Authorization");
  next();
})

const Sequelize = require('sequelize');
const sequelize = new Sequelize('game', 'ShakesAndFidgetAPI', 'WebServiceCSharp', {
  host: 'localhost',
  dialect: 'mysql',
  operatorsAliases: false,

  define: {
    timestamps: false,
    freezeTableName: true
  },

  pool: {
    max: 2,
    min: 0,
    acquire: 30000,
    idle: 10000
  }
});

app.set('sequelize', sequelize);

sequelize
  .authenticate()
  .then(() => {
    console.log('Connection has been established successfully.');
  })
  .catch(err => {
    console.error('Unable to connect to the database:', err);
  });

const User = userModel(sequelize);
const Character = characterModel(sequelize);
const Stats = statsModel(sequelize);
const Gear = gearModel(sequelize);
const GearBase = gearBaseModel(sequelize);
var models = {User, Character, Stats, Gear, GearBase};
app.set('models', models);

User.hasMany(Character, {as: 'Characters', hooks: true, onDelete: 'CASCADE', foreignKey: 'UserId', inverse: true});
Character.belongsTo(User, {as: 'User'});
GearBase.hasMany(Gear, {as: 'Gears', hooks: true, onDelete: 'CASCADE', foreignKey: 'GearBaseId', inverse: true});
Gear.belongsTo(GearBase, {as: 'GearBase'});
Stats.hasOne(Character, {as: 'StatCharacter', foreignKey: 'StatId'});
Gear.hasOne(Character, {as: 'Head', foreignKey: 'HeadId'});
Gear.hasOne(Character, {as: 'Ring1', foreignKey: 'Ring1Id'});
Gear.hasOne(Character, {as: 'Ring2', foreignKey: 'Ring2Id'});
Gear.hasOne(Character, {as: 'Usable1', foreignKey: 'Usable1Id'});
Gear.hasOne(Character, {as: 'Usable2', foreignKey: 'Usable2Id'});
Gear.hasOne(Character, {as: 'Armor', foreignKey: 'ArmorId'});
Gear.hasOne(Character, {as: 'Legs', foreignKey: 'LegsId'});
Gear.hasOne(Character, {as: 'Attack', foreignKey: 'AttackId'});
Gear.hasOne(Character, {as: 'Special', foreignKey: 'SpecialId'});
Stats.hasOne(Gear, {as: 'StatGear', foreignKey: 'StatId'});

sequelize.sync({force: true})
  .then(() => {
    // One test user ; MDP: poulet
    User.create({
      name: 'Dunk14',
      mail: 'fosseykilyan@gmail.com',
      password: '5337AFF4D7C42F4124010FC66BCEC881'
    });

    // Some gears
    GearBase.create({
      Id: 1,
      Name: 'Big Sword',
      ImageSource: 'pack://application:,,,/Resources/Big Sword.png',
      CharacterType: 'W',
      GearType: 'Attack',
      LevelMin: 5,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 5,
      MagicDamage: 0,
      PhysicalDamage: 7,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 80
    });
    GearBase.create({
      Id: 2,
      Name: 'Staff',
      ImageSource: 'pack://application:,,,/Resources/Staff.png',
      CharacterType: 'M',
      GearType: 'Attack',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 3,
      MagicDamage: 4,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 40
    });
    GearBase.create({
      Id: 3,
      Name: 'Bow',
      ImageSource: 'pack://application:,,,/Resources/Bow.png',
      CharacterType: 'H',
      GearType: 'Attack',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 4,
      MagicDamage: 0,
      PhysicalDamage: 4,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 50
    });
    GearBase.create({
      Id: 4,
      Name: 'Knife',
      ImageSource: 'pack://application:,,,/Resources/Knife.png',
      CharacterType: '',
      GearType: 'Attack',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 1,
      MagicDamage: 0,
      PhysicalDamage: 2,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 20
    });
    GearBase.create({
      Id: 5,
      Name: 'Saber',
      ImageSource: 'pack://application:,,,/Resources/Saber.png',
      CharacterType: 'WH',
      GearType: 'Attack',
      LevelMin: 3,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 4,
      MagicDamage: 0,
      PhysicalDamage: 4,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 60
    });
    GearBase.create({
      Id: 6,
      Name: 'Spear',
      ImageSource: 'pack://application:,,,/Resources/Spear.png',
      CharacterType: 'WH',
      GearType: 'Attack',
      LevelMin: 5,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 1,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 7,
      MagicDamage: 0,
      PhysicalDamage: 5,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 75
    });    
    GearBase.create({
      Id: 7,
      Name: 'Scythe',
      ImageSource: 'pack://application:,,,/Resources/Scythe.png',
      CharacterType: '',
      GearType: 'Attack',
      LevelMin: 8,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 1,
      Agility: 2,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 6,
      MagicDamage: 0,
      PhysicalDamage: 6,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 90
    });
    GearBase.create({
      Id: 8,
      Name: 'Halberd',
      ImageSource: 'pack://application:,,,/Resources/Halberd.png',
      CharacterType: 'W',
      GearType: 'Attack',
      LevelMin: 6,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 5,
      MagicDamage: 0,
      PhysicalDamage: 6,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 85
    });
    GearBase.create({
      Id: 9,
      Name: 'Crooked Sword',
      ImageSource: 'pack://application:,,,/Resources/Crooked Sword.png',
      CharacterType: 'WH',
      GearType: 'Attack',
      LevelMin: 10,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 1,
      Agility: 3,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 7,
      MagicDamage: 0,
      PhysicalDamage: 6,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 130
    });
    GearBase.create({
      Id: 10,
      Name: 'Dark Katana',
      ImageSource: 'pack://application:,,,/Resources/Dark Katana.png',
      CharacterType: '',
      GearType: 'Attack',
      LevelMin: 13,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 4,
      Spirit: 1,
      Luck: 0,
      CriticalDamage: 8,
      MagicDamage: 2,
      PhysicalDamage: 8,
      CriticalProba: 1,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 160
    });
    GearBase.create({
      Id: 11,
      Name: 'Twisted Sword',
      ImageSource: 'pack://application:,,,/Resources/Twisted Sword.png',
      CharacterType: '',
      GearType: 'Attack',
      LevelMin: 10,
      Life: -2,
      Mana: -2,
      Energy: 0,
      Strength: 2,
      Agility: 2,
      Spirit: 1,
      Luck: 2,
      CriticalDamage: 13,
      MagicDamage: 0,
      PhysicalDamage: 6,
      CriticalProba: 2,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 140
    });
    GearBase.create({
      Id: 12,
      Name: 'Katana',
      ImageSource: 'pack://application:,,,/Resources/Katana.png',
      CharacterType: '',
      GearType: 'Attack',
      LevelMin: 10,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 3,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 8,
      MagicDamage: 0,
      PhysicalDamage: 6,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 90
    });
    GearBase.create({
      Id: 13,
      Name: 'Dragon bow',
      ImageSource: 'pack://application:,,,/Resources/Dragon bow.png',
      CharacterType: 'H',
      GearType: 'Attack',
      LevelMin: 10,
      Life: 0,
      Mana: 1,
      Energy: 0,
      Strength: 2,
      Agility: 2,
      Spirit: 1,
      Luck: 0,
      CriticalDamage: 8,
      MagicDamage: 1,
      PhysicalDamage: 7,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 160
    });
    GearBase.create({
      Id: 14,
      Name: 'Zeus Spear',
      ImageSource: 'pack://application:,,,/Resources/Zeus Spear.png',
      CharacterType: 'WH',
      GearType: 'Attack',
      LevelMin: 15,
      Life: 2,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 3,
      Spirit: 2,
      Luck: 0,
      CriticalDamage: 10,
      MagicDamage: 2,
      PhysicalDamage: 9,
      CriticalProba: 1,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 180
    });
    GearBase.create({
      Id: 15,
      Name: 'Electric Staff',
      ImageSource: 'pack://application:,,,/Resources/Electric Staff.png',
      CharacterType: 'M',
      GearType: 'Attack',
      LevelMin: 8,
      Life: 0,
      Mana: 1,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 4,
      Luck: 0,
      CriticalDamage: 4,
      MagicDamage: 8,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 80
    });    
    GearBase.create({
      Id: 16,
      Name: 'Fire Staff',
      ImageSource: 'pack://application:,,,/Resources/Fire Staff.png',
      CharacterType: 'M',
      GearType: 'Attack',
      LevelMin: 8,
      Life: 0,
      Mana: 1,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 4,
      Luck: 0,
      CriticalDamage: 4,
      MagicDamage: 8,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 80
    });  
    GearBase.create({
      Id: 17,
      Name: 'Ice Staff',
      ImageSource: 'pack://application:,,,/Resources/Ice Staff.png',
      CharacterType: 'M',
      GearType: 'Attack',
      LevelMin: 8,
      Life: 0,
      Mana: 1,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 4,
      Luck: 0,
      CriticalDamage: 4,
      MagicDamage: 8,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 80
    });  
    GearBase.create({
      Id: 18,
      Name: 'Wind Staff',
      ImageSource: 'pack://application:,,,/Resources/Wind Staff.png',
      CharacterType: 'M',
      GearType: 'Attack',
      LevelMin: 8,
      Life: 0,
      Mana: 1,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 4,
      Luck: 0,
      CriticalDamage: 4,
      MagicDamage: 8,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 80
    });           
    GearBase.create({
      Id: 20,
      Name: 'Cap',
      ImageSource: 'pack://application:,,,/Resources/Cap.png',
      CharacterType: '',
      GearType: 'Head',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 4,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 1,
      MagicalArmor: 1,
      Price : 10
    });  
    GearBase.create({
      Id: 21,
      Name: 'Wood Helmet',
      ImageSource: 'pack://application:,,,/Resources/Wood Helmet.png',
      CharacterType: '',
      GearType: 'Head',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 3,
      MagicalArmor: 0,
      Price : 25
    });  
    GearBase.create({
      Id: 22,
      Name: 'Helmet',
      ImageSource: 'pack://application:,,,/Resources/Helmet.png',
      CharacterType: '',
      GearType: 'Head',
      LevelMin: 4,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 5,
      MagicalArmor: 1,
      Price : 35
    });  
    GearBase.create({
      Id: 23,
      Name: 'Big Helmet',
      ImageSource: 'pack://application:,,,/Resources/Big Helmet.png',
      CharacterType: 'WH',
      GearType: 'Head',
      LevelMin: 8,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 7,
      MagicalArmor: 3,
      Price : 50
    });  
    GearBase.create({
      Id: 24,
      Name: 'Dragon Helmet',
      ImageSource: 'pack://application:,,,/Resources/Dragon Helmet.png',
      CharacterType: 'WH',
      GearType: 'Head',
      LevelMin: 15,
      Life: 3,
      Mana: 1,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 1,
      Luck: 1,
      CriticalDamage: 1,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 10,
      MagicalArmor: 6,
      Price : 110
    });  
    GearBase.create({
      Id: 25,
      Name: 'Wizard Hat',
      ImageSource: 'pack://application:,,,/Resources/Wizard Hat.png',
      CharacterType: 'M',
      GearType: 'Head',
      LevelMin: 7,
      Life: 0,
      Mana: 2,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 4,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 3,
      MagicalArmor: 8,
      Price : 60
    });  
    GearBase.create({
      Id: 26,
      Name: 'Studded Jacket',
      ImageSource: 'pack://application:,,,/Resources/Studded Jacket.png',
      CharacterType: '',
      GearType: 'Armor',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 5,
      MagicalArmor: 2,
      Price : 20
    });  
    GearBase.create({
      Id: 27,
      Name: 'Medium Armor',
      ImageSource: 'pack://application:,,,/Resources/Medium Armor.png',
      CharacterType: 'WH',
      GearType: 'Armor',
      LevelMin: 5,
      Life: 2,
      Mana: 0,
      Energy: 0,
      Strength: 1,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 8,
      MagicalArmor: 5,
      Price : 45
    });  
    GearBase.create({
      Id: 28,
      Name: 'Electric Armor',
      ImageSource: 'pack://application:,,,/Resources/Electric Armor.png',
      CharacterType: 'WH',
      GearType: 'Armor',
      LevelMin: 8,
      Life: 3,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 10,
      MagicalArmor: 7,
      Price : 70
    });  
    GearBase.create({
      Id: 29,
      Name: 'Fire Armor',
      ImageSource: 'pack://application:,,,/Resources/Fire Armor.png',
      CharacterType: 'WH',
      GearType: 'Armor',
      LevelMin: 8,
      Life: 3,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 10,
      MagicalArmor: 7,
      Price : 70
    });  
    GearBase.create({
      Id: 30,
      Name: 'Ice Armor',
      ImageSource: 'pack://application:,,,/Resources/Ice Armor.png',
      CharacterType: 'WH',
      GearType: 'Armor',
      LevelMin: 8,
      Life: 3,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 10,
      MagicalArmor: 7,
      Price : 70
    });  
    GearBase.create({
      Id: 31,
      Name: 'Wind Armor',
      ImageSource: 'pack://application:,,,/Resources/Wind Armor.png',
      CharacterType: 'WH',
      GearType: 'Armor',
      LevelMin: 8,
      Life: 3,
      Mana: 0,
      Energy: 0,
      Strength: 2,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 10,
      MagicalArmor: 7,
      Price : 70
    });  
    GearBase.create({
      Id: 32,
      Name: 'Power Armor',
      ImageSource: 'pack://application:,,,/Resources/Power Armor.png',
      CharacterType: 'W',
      GearType: 'Armor',
      LevelMin: 17,
      Life: 5,
      Mana: 2,
      Energy: 2,
      Strength: 6,
      Agility: 1,
      Spirit: 1,
      Luck: 1,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 15,
      MagicalArmor: 12,
      Price : 170
    });  
    GearBase.create({
      Id: 33,
      Name: 'Dress',
      ImageSource: 'pack://application:,,,/Resources/Dress.png',
      CharacterType: 'M',
      GearType: 'Armor',
      LevelMin: 1,
      Life: 0,
      Mana: 2,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 2,
      MagicalArmor: 5,
      Price : 50
    });  
    GearBase.create({
      Id: 34,
      Name: 'Dark Dress',
      ImageSource: 'pack://application:,,,/Resources/Dark Dress.png',
      CharacterType: 'M',
      GearType: 'Armor',
      LevelMin: 10,
      Life: 0,
      Mana: 6,
      Energy: 0,
      Strength: 2,
      Agility: 1,
      Spirit: 7,
      Luck: 1,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 5,
      MagicalArmor: 15 ,
      Price : 140
    });  
    Stats.create({
      PhysicalDamage: 5,
      CriticalDamage: 3
    })
    GearBase.create({
      Id: 35,
      Name: 'Little Shield',
      ImageSource: 'pack://application:,,,/Resources/Little Shield.png',
      CharacterType: '',
      GearType: 'Special',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 3,
      MagicalArmor: 1,
      Price : 25
    });  
    GearBase.create({
      Id: 36,
      Name: 'Big Shield',
      ImageSource: 'pack://application:,,,/Resources/Big Shield.png',
      CharacterType: 'WH',
      GearType: 'Special',
      LevelMin: 6,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 7,
      MagicalArmor: 3,
      Price : 50
    });  
    GearBase.create({
      Id: 37,
      Name: 'Sacred Shield',
      ImageSource: 'pack://application:,,,/Resources/Sacred Shield.png',
      CharacterType: '',
      GearType: 'Special',
      LevelMin: 13,
      Life: 2,
      Mana: 2,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 1,
      Luck: 1,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 12,
      MagicalArmor: 10,
      Price : 100
    });  
    GearBase.create({
      Id: 38,
      Name: 'Simple Trousers',
      ImageSource: 'pack://application:,,,/Resources/Simple Trousers.png',
      CharacterType: '',
      GearType: 'Legs',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 3,
      MagicalArmor: 1,
      Price : 15
    });
    GearBase.create({
      Id: 39,
      Name: 'Studded Trousers',
      ImageSource: 'pack://application:,,,/Resources/Studded Trousers.png',
      CharacterType: '',
      GearType: 'Legs',
      LevelMin: 3,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 6,
      MagicalArmor: 3,
      Price : 30
    });  
    GearBase.create({
      Id: 40,
      Name: 'Heavy Trousers',
      ImageSource: 'pack://application:,,,/Resources/Heavy Trousers.png',
      CharacterType: 'WH',
      GearType: 'Legs',
      LevelMin: 9,
      Life: 0,
      Mana: 0,
      Energy: 1,
      Strength: 3,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 9,
      MagicalArmor: 5,
      Price : 45
    });  
    GearBase.create({
      Id: 41,
      Name: 'Wallace\'s Automatic Trousers',
      ImageSource: 'pack://application:,,,/Resources/Wallace\'s Automatic Trousers.png',
      CharacterType: '',
      GearType: 'Legs',
      LevelMin: 15,
      Life: 3,
      Mana: 3,
      Energy: 1,
      Strength: 3,
      Agility: 3,
      Spirit: 3,
      Luck: 2,
      CriticalDamage: 2,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 15,
      MagicalArmor: 12,
      Price : 120
    });  
    GearBase.create({
      Id: 42,
      Name: 'Little Power Ring',
      ImageSource: 'pack://application:,,,/Resources/Little Power Ring.png',
      CharacterType: '',
      GearType: 'Ring',
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 1,
      Strength: 2,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 15
    });  
    GearBase.create({
      Id: 43,
      Name: 'Nice Power Ring',
      ImageSource: 'pack://application:,,,/Resources/Nice Power Ring.png',
      CharacterType: '',
      GearType: 'Ring',
      LevelMin: 5,
      Life: 0,
      Mana: 0,
      Energy: 3,
      Strength: 4,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 35
    });  
    GearBase.create({
      Id: 44,
      Name: 'Powerful Power Ring',
      ImageSource: 'pack://application:,,,/Resources/Powerful Power Ring.png',
      CharacterType: '',
      GearType: 'Ring',
      LevelMin: 10,
      Life: 0,
      Mana: 0,
      Energy: 6,
      Strength: 8,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 80
    });  
    GearBase.create({
      Id: 45,
      Name: 'Luck Earrings',
      ImageSource: 'pack://application:,,,/Resources/Luck Earrings.png',
      CharacterType: '',
      GearType: 'Special',
      LevelMin: 3,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 10,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 150
    });  
    GearBase.create({
      Id: 46,
      Name: 'Heal Potion',
      ImageSource: 'pack://application:,,,/Resources/Heal Potion.png',
      CharacterType: '',
      GearType: 'Usable',
      LevelMin: 0,
      Life: 50,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 5
    });  
    GearBase.create({
      Id: 47,
      Name: 'Big Heal Potion',
      ImageSource: 'pack://application:,,,/Resources/Big Heal Potion.png',
      CharacterType: '',
      GearType: 'Usable',
      LevelMin: 0,
      Life: 100,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 10
    });  
    GearBase.create({
      Id: 48,
      Name: 'Mana Potion',
      ImageSource: 'pack://application:,,,/Resources/Mana Potion.png',
      CharacterType: '',
      GearType: 'Usable',
      LevelMin: 0,
      Life: 0,
      Mana: 50,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 5
    });  
    GearBase.create({
      Id: 49,
      Name: 'Big Mana Potion',
      ImageSource: 'pack://application:,,,/Resources/Big Mana Potion.png',
      CharacterType: '',
      GearType: 'Usable',
      LevelMin: 0,
      Life: 0,
      Mana: 100,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 0,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 10
    });  
    GearBase.create({
      Id: 50,
      Name: 'Throwing Weapon',
      ImageSource: 'pack://application:,,,/Resources/Throwing Weapon.png',
      CharacterType: '',
      GearType: 'Usable',
      LevelMin: 0,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 0,
      PhysicalDamage: 25,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 10
    });  
    GearBase.create({
      Id: 51,
      Name: 'Electric Arrow',
      ImageSource: 'pack://application:,,,/Resources/Electric Arrow.png',
      CharacterType: '',
      GearType: 'Special',
      LevelMin: 0,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 5,
      PhysicalDamage: 5,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 5
    });      
    GearBase.create({
      Id: 52,
      Name: 'Fire Arrow',
      ImageSource: 'pack://application:,,,/Resources/Fire Arrow.png',
      CharacterType: '',
      GearType: 'Special',
      LevelMin: 0,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 5,
      PhysicalDamage: 5,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 5
    });  
    GearBase.create({
      Id: 53,
      Name: 'Ice Arrow',
      ImageSource: 'pack://application:,,,/Resources/Ice Arrow.png',
      CharacterType: '',
      GearType: 'Special',
      LevelMin: 0,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 5,
      PhysicalDamage: 5,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 5
    });  
    GearBase.create({
      Id: 54,
      Name: 'Wind Arrow',
      ImageSource: 'pack://application:,,,/Resources/Wind Arrow.png',
      CharacterType: '',
      GearType: 'Special',
      LevelMin: 0,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 0,
      MagicDamage: 5,
      PhysicalDamage: 5,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 5
    });  
    GearBase.create({
      Id: 55,
      Name: 'Bloody Sword',
      ImageSource: 'pack://application:,,,/Resources/Bloody Sword.png',
      CharacterType: 'WH',
      GearType: 'Attack',
      LevelMin: 15,
      Life: 2,
      Mana: 0,
      Energy: 2,
      Strength: 4,
      Agility: 1,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 10,
      MagicDamage: 1,
      PhysicalDamage: 10,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0,
      Price : 166
    });  
  });

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/users', usersRouter);
app.use('/characters', charactersRouter);
app.use('/gearBases', gearBaseRouter);
app.use('/gears', gearRouter);

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  next(createError(404));
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;
