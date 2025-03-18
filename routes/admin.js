const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "admin-profile"})
});

router.get("/profile", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "admin-profile"})
})

router.get("/companies", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "managing_companies"})
})

router.get("/companies/new", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "company_form"})
})

router.get("/companies/:id/openings", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "company_openings", companyId: req.params.id})
})

router.get("/students", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "managing_students"})
})

router.get("/students/new", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "student_form"})
})

router.get("/placement_records", authenticate, authorizeRole("ADMIN"), (req, res) => {
    res.render("dashboard-admin", {body: "placement_records"})
})
module.exports = router;
