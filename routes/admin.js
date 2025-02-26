const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin")
});

module.exports = router;
