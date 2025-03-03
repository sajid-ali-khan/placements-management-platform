const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.use(express.json());
router.use(express.urlencoded({ extended: true }));

router.get("/", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student", { body: "student-profile" });
});

router.get("/openings", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student", { body: "opening" });
});

router.get("/profile", authenticate, authorizeRole("STUDENT"), (req, res) => {
    res.render("dashboard-student", { body: "student-profile" });
});

router.get("/apply", authenticate, authorizeRole("STUDENT"), function(req, res) {
    res.render("dashboard-student", { body: "apply", openingId: req.query.openingId });
});


module.exports = router;
