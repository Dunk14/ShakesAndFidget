var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Gear = sequelize.define('gear', {
    id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    name: {
      type: Sequelize.STRING
    },
    type: {
      type: Sequelize.STRING
    },
    levelMin: {
      type: Sequelize.INTEGER
    }
  });

  return Gear;
};