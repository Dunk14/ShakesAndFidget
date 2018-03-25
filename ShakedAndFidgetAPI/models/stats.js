var Sequelize = require('sequelize');

module.exports = function (sequelize) {
  const Stats = sequelize.define('stats', {
    id: {
      type: Sequelize.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    life: {
      type: Sequelize.INTEGER
    },
    mana: {
      type: Sequelize.INTEGER
    },
    energy: {
      type: Sequelize.INTEGER
    },
    strength: {
      type: Sequelize.INTEGER
    },
    agility: {
      type: Sequelize.INTEGER
    },
    spirit: {
      type: Sequelize.INTEGER
    },
    luck: {
      type: Sequelize.INTEGER
    },
    criticalDamage: {
      type: Sequelize.INTEGER
    },
    magicDamage: {
      type: Sequelize.INTEGER
    },
    physicalDamage: {
      type: Sequelize.INTEGER
    },
    criticalProba: {
      type: Sequelize.INTEGER
    },
    physicalArmor: {
      type: Sequelize.INTEGER
    },
    magicalArmor: {
      type: Sequelize.INTEGER
    },
  });

  return Stats;
};