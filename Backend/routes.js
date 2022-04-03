const express = require("express")
const Highscore = require("./highscore") // new
const router = express.Router()

// Get all Highscores
router.get("/highscores", async (req, res) => {
    const highscores = await Highscore.find().sort({ highscore: -1 }).limit(20)
    res.send(highscores)
})


router.post("/highscores", async (req, res) => {
    const highscore = new Highscore({
        name: req.body.name,
        highscore: req.body.highscore,
    })
    await highscore.save()
    const highscores = await Highscore.find().sort({ highscore: -1 }).limit(20)
    const resMes = [];
    highscores.forEach(score => {
        resMes.push({ name: score.name, highscore: score.highscore })
    })
    res.send({ scores: resMes })
})

module.exports = router