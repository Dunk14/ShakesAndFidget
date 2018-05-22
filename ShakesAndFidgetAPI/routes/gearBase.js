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

/* GET gear by character type. */
router.get('/getgearbycharactertype/:type', function(req, res) {
  var models = req.app.get('models');
  models.GearBase.findAll({where: {CharacterType: req.params.type}})
  .then((gearBases) => {
  	console.log(gearBases)
    res.send(gearBases);
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

module.exports = router;
