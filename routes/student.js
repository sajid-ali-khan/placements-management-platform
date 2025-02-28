const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student")
});
router.get("/openings", (req, res) => {
    res.render("opening")
});
router.get("/profile", (req, res) => {
    res.render("profile");
});

module.exports = router;
