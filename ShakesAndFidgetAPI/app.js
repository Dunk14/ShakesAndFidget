var createError = require('http-errors');
var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var userModel = require('./models/user');
var characterModel = require('./models/character');
var statsModel = require('./models/stats');
var gearModel = require('./models/gear');

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
var models = {User, Character, Stats, Gear};
app.set('models', models);

User.hasMany(Character, {as: 'characters', hooks: true, onDelete: 'CASCADE', inverse: true});
Character.belongsTo(User, {as: 'user'});
Stats.hasOne(Character, {as: 'statCharacter', foreignKey: 'statId'});
Gear.hasOne(Character, {as: 'headGear', foreignKey: 'headGearId'});
Gear.hasOne(Character, {as: 'earring1', foreignKey: 'earring1Id'});
Gear.hasOne(Character, {as: 'earring2', foreignKey: 'earring2Id'});
Gear.hasOne(Character, {as: 'chest', foreignKey: 'chestId'});
Gear.hasOne(Character, {as: 'legs', foreignKey: 'legsId'});
Gear.hasOne(Character, {as: 'ring1', foreignKey: 'ring1Id'});
Gear.hasOne(Character, {as: 'ring2', foreignKey: 'ring2Id'});
Gear.hasOne(Character, {as: 'feet', foreignKey: 'feetId'});
Stats.hasOne(Gear, {as: 'statGear', foreignKey: 'statId'});

/*sequelize.sync({force: true})
  .then(() => {
    User.create({
      name: 'Dunk14',
      mail: 'fosseykilyan@gmail.com',
      password: '5337AFF4D7C42F4124010FC66BCEC881'
    })
    .then((user) => {
      Stats.create({
        life: 1
      })
      .then((stat1) => {
        Stats.create({
          mana: 1
        })
        .then((stat2) => {
          Gear.create({
            name: 'Baby magus Hat',
            type: 'head',
            levelMin: 1,
            statId: stat2.get('id')
          })
          .then((gear) => {
            Character.create({
              name: 'Fistalheure',
              sexe: 'M',
              level: 1,
              userId: user.get('id'),
              statId: stat1.get('id'),
              headGearId: gear.get('id')
            })
          });
        })
      })
    });
  });*/

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
