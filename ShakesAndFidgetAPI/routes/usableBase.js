var express = require('express');
var router = express.Router();

/* GET usable bases listing. */
router.get('/', function(req, res) {
  var models = req.app.get('models');
  models.UsableBase.findAll()
  .then((usableBases) => {
    res.send(usableBases);
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

module.exports = router;
