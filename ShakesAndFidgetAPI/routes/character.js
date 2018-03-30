var express = require('express');
var router = express.Router();

/* GET characters listing. */
router.get('/', function(req, res) {
  var models = req.app.get('models');
  models.Character.findAll()
  .then((characters) => {
    res.send(characters);
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

/* GET characters listing */
router.get('/:characterId', function(req, res) {
  var models = req.app.get('models');
  models.Character.findById(req.params.characterId)
    .then((character) => {
      res.send(character);
    })
    .catch((error) => {
      console.error(error);
      res.send(error);
    })
});

/* GET characters listing by user Id */
router.get('/byUserId/:userId', function(req, res) {
  var models = req.app.get('models');
  models.Character.find({where: {UserId: req.params.userId}})
    .then((character) => {
      if (!character)
        res.sendStatus(404);
      else
        res.send(character);
    })
    .catch((error) => {
      console.error(error);
      res.send(error);
    })
});

/* GET characters counting based on userId listing */
router.get('/countByUserId/:userId', function(req, res) {
  var models = req.app.get('models');
  models.Character.count({where: {UserId: req.params.userId}})
    .then((count) => {
      res.json({count: count});
    })
    .catch((error) => {
      console.error(error);
      res.send(error);
    })
});

/* POST character */
router.post('/:userId', function(req, res) {
  var models = req.app.get('models');
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
    models.Character.create({
      Type: req.body.Type,
      Name: req.body.Name,
      Sexe: req.body.Sexe,
      Level: req.body.Level,
      UserId: req.params.userId,
      StatId: stat.get('Id')
    })
    .then((character) => {
      res.send({characterId: character.get('Id')})
    })
    .catch((error) => {
      let msg = 'Stats table created but Character has not';
      console.error(error, msg);
      res.send({error: -2});
    })
  })
  .catch((error) => {
    let msg = 'Stats and Character tables not created';
    console.error(error, msg);
    res.send({error: -1});
  })
});

module.exports = router;
