var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Gear = sequelize.define('gear', {
    Id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    Price: {
      type: Sequelize.INTEGER
    }
  });

  return Gear;
};
