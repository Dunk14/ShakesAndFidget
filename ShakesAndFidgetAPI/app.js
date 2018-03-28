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
var models = {User, Character, Stats, Gear};
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
      Name: 'Big Sword',
      ImageSource: 'pack://application:,,,/Resources/Big Sword.png'
    });
    GearBase.create({
      Name: 'Staff',
      ImageSource: 'pack://application:,,,/Resources/Staff.png'
    });
    GearBase.create({
      Name: 'Bow',
      ImageSource: 'pack://application:,,,/Resources/Bow.png'
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