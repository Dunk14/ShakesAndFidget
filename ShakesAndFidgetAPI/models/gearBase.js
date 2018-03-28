var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const GearBase = sequelize.define('gearBase', {
    Id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    Name: {
      type: Sequelize.STRING
    },
    ImageSource: {
      type: Sequelize.STRING
    }
  });

  return GearBase;
};