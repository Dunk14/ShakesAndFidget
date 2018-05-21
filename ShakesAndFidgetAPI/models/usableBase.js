var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const UsableBase = sequelize.define('usableBase', {
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
    },
    Life: {
      type: Sequelize.INTEGER
    },
    Mana: {
      type: Sequelize.INTEGER
    },
    Energy: {
      type: Sequelize.INTEGER
    },
    Strength: {
      type: Sequelize.INTEGER
    },
    Agility: {
      type: Sequelize.INTEGER
    },
    Spirit: {
      type: Sequelize.INTEGER
    },
    Luck: {
      type: Sequelize.INTEGER
    },
    CriticalDamage: {
      type: Sequelize.INTEGER
    },
    MagicDamage: {
      type: Sequelize.INTEGER
    },
    PhysicalDamage: {
      type: Sequelize.INTEGER
    },
    CriticalProba: {
      type: Sequelize.INTEGER
    },
    PhysicalArmor: {
      type: Sequelize.INTEGER
    },
    MagicalArmor: {
      type: Sequelize.INTEGER
    },
    Price: {
      type: Sequelize.INTEGER
    }
  });

  return UsableBase;
};
