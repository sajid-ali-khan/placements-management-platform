const express = require("express");
const router = express.Router();
const { authenticate, authorizeRole } = require("../middlewares/authMiddleware");

router.get("/", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("company/dashboard", { body: "profile" });
});

router.get("/applications", authenticate, authorizeRole("HR"), (req, res) => {
    const jobTitle = req.query.jobTitle || "";
    res.render("company/dashboard", { body: "../application/for_opening", jobTitle });
});


router.get("/applications/:id/details", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("company/dashboard", {body: "../application/details", applicationId: req.params.id})
})

router.get("/profile", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("company/dashboard", {body: "profile"});
});

router.get("/openings", authenticate, authorizeRole("HR"), (req, res) =>{
    res.render("company/dashboard", {body: "../opening/view_and_create"});
});

router.get("/openings/new", authenticate, authorizeRole("HR"), (req, res) => {
    res.render("company/dashboard", {body: "../opening/form"});
});
module.exports = router;
