const express = require("express")
const mongoose = require("mongoose") // new
const routes = require("./routes")

// Connect to MongoDB database
mongoose
    .connect("mongodb://localhost:27017/ld48", { useNewUrlParser: true })
    .then(() => {
        const app = express()
        app.use(express.json())

        app.use("/api", routes) // new

        app.listen(4000, () => {
            console.log("Server has started!")
        })
    })

