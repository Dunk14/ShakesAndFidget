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
      else {
        models.Stats.findById(character.get('StatId'))
        .then((stat) => {
          models.Gear.findAll({
            InventoryCharacterId: character.get('Id')
          })
          .then((gears) => {
            models.Usable.findAll({
              InventoryCharacterId: character.get('Id')
            })
            .then((usables) => {
              res.send({
                Id: character.get('Id'),
                Type: character.get('Type'),
                Name: character.get('Name'),
                Sexe: character.get('Sexe'),
                Level: character.get('Level'),
                Money: character.get('Money'),
                UserId: character.get('UserId'),
                StatId: character.get('StatId'),
                HeadId: character.get('HeadId'),
                Ring1Id: character.get('Ring1Id'),
                Ring2Id: character.get('Ring2Id'),
                Usable1Id: character.get('Usable1Id'),
                Usable2Id: character.get('Usable2Id'),
                ArmorId: character.get('ArmorId'),
                LegsId: character.get('LegsId'),
                AttackId: character.get('AttackId'),
                SpecialId: character.get('SpecialId'),
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
                InventoryGears: gears,
                InventoryUsables: usables
              });
            })
            .catch((error) => {
              console.error(error);
              res.send(error);
            });
          })
          .catch((error) => {
            console.error(error);
            res.send(error);
          });
        })
      }
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
  console.log(req.body);
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
      Money: req.body.Money,
      UserId: req.params.userId,
      StatId: stat.get('Id'),
      AttackId: req.body.AttackId || null
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

/* PUT character */
router.put('/:characterId', function(req, res) {
  var models = req.app.get('models');
  console.log(req.body);
  models.Character.update({
    Type: req.body.Type,
    Name: req.body.Name,
    Sexe: req.body.Sexe,
    Level: req.body.Level,
    Money: req.body.Money,
    UserId: req.params.userId,
    StatId: stat.get('Id'),
    HeadId: req.body.HeadId || null,
    ArmorId: req.body.ArmorId || null,
    LegsId: req.body.LegsId || null,
    Ring1Id: req.body.Ring1Id || null,
    Ring2Id: req.body.Ring2Id || null,
    SpecialId: req.body.SpecialId || null,
    AttackId: req.body.AttackId || null,
    UsableId: req.body.UsableId || null
  }, {
    returning: true,
    where: {
      id: req.params.characterId
    }
  })
  .then(([rowsUpdated, [updatedCharacter]]) => {
    models.Stats.update({
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
    },{
      where: {
        id: updatedCharacter.get('Id')
      }
    })
    .then((rowsUpdated) => {
      res.send(updatedCharacter);
    })
    .catch((error) => {
      let msg = 'Character table updated but Stats has not';
      console.error(error, msg);
      res.send(req.body);
    });
  })
  .catch((error) => {
    let msg = 'Stats and Character tables not updated';
    console.error(error, msg);
    res.send(req.body);
  });
});


module.exports = router;
