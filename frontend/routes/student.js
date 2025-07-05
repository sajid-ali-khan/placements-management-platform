const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.use(express.json());
router.use(express.urlencoded({ extended: true }));

router.get("/", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("student/dashboard", { body: "profile" });
});

router.get("/openings", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("student/dashboard", { body: "../opening/view_and_apply" });
});

router.get("/profile", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("student/dashboard", { body: "profile" });
});

router.get("/apply", authenticate, authorizeRole("STUDENT"), function(req, res) {
    res.render("student/dashboard", { body: "../application/form", openingId: req.query.openingId });
});

router.get("/applications", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("student/dashboard", { body: "../application/view_applied" });
});


module.exports = router;
