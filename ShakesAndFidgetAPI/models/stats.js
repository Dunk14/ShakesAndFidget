var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Stats = sequelize.define('stats', {
    Id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    Life: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    Mana: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    Energy: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    Strength: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    Agility: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    Spirit: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    Luck: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    CriticalDamage: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    MagicDamage: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    PhysicalDamage: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    CriticalProba: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    PhysicalArmor: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    },
    MagicalArmor: {
      type: Sequelize.INTEGER,
      defaultValue: 0
    }
  });

  return Stats;
};