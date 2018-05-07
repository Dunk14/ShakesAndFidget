var express = require('express');
var router = express.Router();

/* GET gear listing. */
router.get('/', function(req, res) {
  var models = req.app.get('models');
  models.Gear.findAll()
  .then((gears) => {
    res.send(gears);
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

/* GET gear */
router.get('/:gearId', function(req, res) {
  let models = req.app.get('models');
  models.Gear.findById(req.params.gearId)
  .then((gear) => {
    models.GearBase.findById(gear.get('GearBaseId'))
    .then((gearBase) => {
      models.Stats.findById(gear.get('StatId'))
      .then((stat) => {
        res.send({
          Id: gear.get('Id'),
          CharacterType: gearBase.get('CharacterType'),
          GearType: gearBase.get('GearType'),
          Name: gearBase.get('Name'),
          ImageSource: gearBase.get('ImageSource'),
          Type: gearBase.get('Type'),
          LevelMin: gearBase.get('LevelMin'),
          Life: stat.get('Life'),
          Mana: stat.get('Mana'),
          Energy: stat.get('Energy'),
          Strength: stat.get('Strength'),
          Agility: stat.get('Agility'),
          Spirit: stat.get('Spirit'),
          Luck: stat.get('Luck'),
          CriticalDamage: stat.get('CriticalDamage'),
          MagicDamage: stat.get('MagicDamage'),
          PhysicalDamage: stat.get('PhysicalDamage'),
          CriticalProba: stat.get('CriticalProba'),
          PhysicalArmor: stat.get('PhysicalArmor'),
          MagicalArmor: stat.get('MagicalArmor'),
        });
      })
    })
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

/* POST gear */
router.post('/:gearBaseId', function(req, res) {
  let models = req.app.get('models');
  models.Stats.create(req.body)
    .then((stat) => {
      models.Gear.create({
        GearBaseId: req.params.gearBaseId,
        StatId: stat.get('Id')
      })
        .then((gear) => {
          res.send({
            gearId: gear.get('Id')
          });
        })
        .catch((error) => {
          let msg = 'Stat created but Gear has not';
          console.error(error, msg);
          res.send({error: -2});
        })
    })
    .catch((error) => {
      let msg = 'Stat and Gear not created';
      console.error(error, msg);
      res.send({error: -1});
    })
});

module.exports = router;
