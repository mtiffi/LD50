const mongoose = require("mongoose")

const schema = mongoose.Schema({
    name: String,
    highscore: Number,
})

module.exports = mongoose.model("Highscore", schema)