var express = require('express');
var router = express.Router();

/* GET gear bases listing. */
router.get('/', function(req, res) {
  var models = req.app.get('models');
  models.GearBase.findAll()
  .then((gearBases) => {
    res.send(gearBases);
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

/* POST gear */
router.post('/', function(req, res) {
  var models = req.app.get('models');
  models.Gear.create({
    Type: req.body.Type,
    LevelMin: req.body.LevelMin
  })
  .then((gear) => {
    models.Stats.create({
      Life: req.body.Life,
      Mana: req.body.Mana,
      Energy: req.body.Energy,
      Strength: req.body.Strength,
      Agility: req.body.Agility,
      Spirit: req.body.Spirit,
      Luck: req.body.Luck,
      CriticalDamage: req.body.CriticalDamage,
      MagicDamage: req.body.MagicDamage,
      PhysicalDamage: req.body.PhysicalDamage,
      CriticalProba: req.body.CriticalProba,
      PhysicalArmor: req.body.PhysicalArmor,
      MagicalArmor: req.body.MagicalArmor
    })
    .then((stat) => {
      res.send({
        gearId: gear.get('Id'),
        statId: stat.get('Id')
      });
    })
    .catch((error) => {
      let msg = 'Stats not created';
      console.error(error, msg);
      res.send({error: -2});
    })
  })
  .catch((error) => {
    let msg = 'Gear and Stats not created';
    console.error(error, msg);
    res.send({error: -1});
  })
});

module.exports = router;
