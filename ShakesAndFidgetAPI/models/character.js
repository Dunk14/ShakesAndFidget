var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Character = sequelize.define('character', {
    Id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    Type: {
      type: Sequelize.STRING(1)
    },
    Name: {
      type: Sequelize.STRING
    },
    Sexe: {
      type: Sequelize.STRING(1)
    },
    Level: {
      type: Sequelize.INTEGER
    },
    Money: {
      type: Sequelize.INTEGER
    }
  });

  return Character;
};
