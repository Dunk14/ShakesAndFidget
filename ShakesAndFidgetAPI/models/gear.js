var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Gear = sequelize.define('gear', {
    Id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    Type: {
      type: Sequelize.STRING
    },
    LevelMin: {
      type: Sequelize.INTEGER
    }
  });

  return Gear;
};