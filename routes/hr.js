const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", { body: "hr-profile",email: req.user.email });
});

router.get("/create-opening", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", {body: "hr-openings",email: req.user.email});
});
router.get("/student-update", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", {body: "hr-update",email: req.user.email});
});

router.get("/profile", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", { body: "hr-profile", email: req.user.email });
});
module.exports = router;
