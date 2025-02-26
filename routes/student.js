const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student")
});

module.exports = router;
