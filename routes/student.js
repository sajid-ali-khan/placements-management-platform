const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.use(express.json());
router.use(express.urlencoded({ extended: true }));

router.get("/", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student", { body: "profile" });
});

router.get("/openings", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student", { body: "opening" });
});

router.get("/profile", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student", { body: "profile" });
});

module.exports = router;
