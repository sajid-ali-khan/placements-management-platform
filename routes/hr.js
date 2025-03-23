const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", { body: "hr-profile" });
});

router.get("/applications", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", {body: "hr-update"});
});

router.get("/profile", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", {body: "hr-profile"});
});

router.get("/openings", authenticate, authorizeRole("HR"), (req, res) =>{
    res.render("dashboard-hr", {body: "hr-showopenings"});
});

router.get("/openings/new", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("dashboard-hr", {body: "hr-openings"});
});
module.exports = router;
