var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Character = sequelize.define('character', {
    id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    name: {
      type: Sequelize.STRING
    },
    sexe: {
      type: Sequelize.STRING(1)
    },
    level: {
      type: Sequelize.INTEGER
    }
  });

  return Character;
};