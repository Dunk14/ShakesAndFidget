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
      LevelMin: 1,
      Life: 0,
      Mana: 0,
      Energy: 0,
      Strength: 0,
      Agility: 0,
      Spirit: 0,
      Luck: 0,
      CriticalDamage: 5,
      MagicDamage: 0,
      PhysicalDamage: 2,
      CriticalProba: 0,
      PhysicalArmor: 0,
      MagicalArmor: 0
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
      MagicalArmor: 0
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
      MagicalArmor: 0
    });
    Stats.create({
      PhysicalDamage: 5,
      CriticalDamage: 3
    })
    .then((stat) => {
      Gear.create({
        GearBaseId: 1,
        StatId: stat.get('Id')
      })
    })
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
