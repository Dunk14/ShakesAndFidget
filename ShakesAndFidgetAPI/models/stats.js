var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Stats = sequelize.define('stats', {
    Id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
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
  });

  return Stats;
};