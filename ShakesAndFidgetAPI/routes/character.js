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

/* GET characters counting based on userId listing */
router.get('/countByUserId/:userId', function(req, res) {
  var models = req.app.get('models');
  models.Character.count({where: {userId: req.params.userId}})
    .then((count) => {
      console.log(count);
      res.json({count: count});
    })
    .catch((error) => {
      console.error(error);
      res.send(error);
    })
});

/* POST warrior */
router.post('/warrior', function(req, res) {
  var models = req.app.get('models');
  models.Character.count()
    .then((count) => {
      console.log(req.body);
      res.status(403);
    })
    .catch((error) => {
      console.error(error);
      res.status(403).send(error);
    })
});

module.exports = router;
