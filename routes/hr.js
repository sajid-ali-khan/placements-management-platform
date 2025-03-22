const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", { body: "hr-profile",email: req.user.email });
});

router.get("/applications", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", {body: "hr-update",email: req.user.email});
});

router.get("/profile", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", { body: "hr-profile", email: req.user.email });
});

router.get("/openings", authenticate, authorizeRole("HR"), (req, res) =>{
    res.render("dashboard-hr", {body: "hr-showopenings",email: req.user.email});
});

router.get("/openings/new", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", {body: "hr-openings",email: req.user.email});
});
module.exports = router;
