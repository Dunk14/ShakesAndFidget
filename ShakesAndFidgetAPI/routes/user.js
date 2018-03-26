var express = require('express');
var router = express.Router();

/* GET users listing. */
router.get('/', function(req, res) {
  var models = req.app.get('models');
  models.User.findAll({
    include: [
      {
        model: models.Character,
        as: 'characters'
      }
    ]
  })
  .then((users) => {
    res.send(users);
  })
  .catch((error) => {
    console.error(error);
    res.send(error);
  })
});

module.exports = router;
