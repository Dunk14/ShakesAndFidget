var express = require('express');
var router = express.Router();

/* GET usable listing. */
router.get('/', function(req, res) {
  var models = req.app.get('models');
  models.Usable.findAll()
  .then((usables) => {
    res.send(usables);
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

/* GET usable */
router.get('/:usableId', function(req, res) {
  let models = req.app.get('models');
  models.Usable.findById(req.params.usableId)
  .then((usable) => {
    models.UsableBase.findById(gear.get('UsableBaseId'))
    .then((usableBase) => {
      models.Stats.findById(gear.get('StatId'))
      .then((stat) => {
        res.send({
          Id: gear.get('Id'),
          Name: gearBase.get('Name'),
          ImageSource: gearBase.get('ImageSource'),
          Price: gearBase.get('Price'),
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

/* POST usable */
router.post('/:usableBaseId', function(req, res) {
  let models = req.app.get('models');
  models.Stats.create(req.body)
    .then((stat) => {
      models.Usable.create({
        CharacterInventoryId: req.body.CharacterId,
        UsableBaseId: req.params.usableBaseId,
        StatId: stat.get('Id')
      })
        .then((usable) => {
          res.send({
            usableId: usable.get('Id')
          });
        })
        .catch((error) => {
          let msg = 'Stat created but Usable has not';
          console.error(error, msg);
          res.send({error: -2});
        })
    })
    .catch((error) => {
      let msg = 'Stat and Usable not created';
      console.error(error, msg);
      res.send({error: -1});
    })
});

/* PUT usable in characters inventory */
router.put('/:usableId/character/:characterId', function(req, res) {
  let models = req.app.get('models');
  models.Usable.update({
    CharacterInventoryId: req.params.characterId
  }, {
    returning: true,
    where: {
      id: req.params.usableId
    }
  })
  .then(([rowsUpdated, [updatedUsable]]) => {
    res.send(updatedUsable);
  })
  .catch((error) => {
    let msg = 'Usable not updated';
    console.error(error, msg);
    res.send(req.body);
  });
});

// DELETE usable from characters inventory
router.delete('/inventory/:usableId', function(req, res) {
  let models = req.app.get('models');
  models.Usable.update({
    CharacterInventoryId: null
  }, {
    returning: true,
    where: {
      id: req.params.usableId
    }
  })
  .then(([rowsUpdated], [updatedUsable]) => {
    res.send(updatedUsable);
  })
  .catch((error) => {
    let msg = 'Usable not updated';
    console.error(error, msg);
    res.send(req.body);
  })
});

// DELETE usable
router.delete(':usableId', function(req, res) {
  let models = req.app.get('models');
  models.Usable.destroy({
    where: {
      id: req.params.usableId
    }
  })
  .then((success) => {
    res.send({status: 1});
  })
  .catch((error) => {
    let msg = 'Usable not deleted';
    console.error(error, msg);
    res.send({status: -1});
  });
});

module.exports = router;
