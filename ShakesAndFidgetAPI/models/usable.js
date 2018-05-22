var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Usable = sequelize.define('usable', {
    Id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    }
  });

  return Usable;
};
