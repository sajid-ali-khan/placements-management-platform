const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "profile"})
});

router.get("/profile", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "profile"})
})

router.get("/companies", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "../company/view_and_add"})
})

router.get("/companies/new", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "../company/form"})
})

router.get("/companies/:id/openings", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "../opening/view", companyId: req.params.id})
})

router.get("/students", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "../student/view_and_add"})
})

router.get("/students/new", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "../student/form"})
})

router.get("/placement_records", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("admin/dashboard", {body: "../application/placement_records"})
})
module.exports = router;
