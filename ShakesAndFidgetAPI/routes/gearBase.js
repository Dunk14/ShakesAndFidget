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

module.exports = router;
