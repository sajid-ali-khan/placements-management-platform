const express = require("express");
const path = require("path");
const cookieParser = require("cookie-parser");
const cors = require("cors");
const adminRoutes = require("./routes/admin");
const studentRoutes = require("./routes/student");
const hrRoutes = require("./routes/hr");
const { authenticate, authorizeRole } = require("./middlewares/authMiddleware");

const PORT = 3000;
const SERVER_NAME = "localhost";

const app = express();

app.use(
    cors({
        origin: "http://localhost:3000", // Adjust based on frontend URL
        credentials: true, // Allow cookies in requests
    })
);
app.use(express.static(path.join(__dirname, "public")));
app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.use(cookieParser());
app.set("view engine", "ejs");

app.use((req, res, next) => {
    res.setHeader('Cache-Control', 'no-store, no-cache, must-revalidate, private');
    res.setHeader('Pragma', 'no-cache');
    res.setHeader('Expires', '0');
    next();
});


app.post("/store-token", (req, res) => {
    const { token } = req.body;

    if (!token) {
        return res.status(400).json({ message: "Token is required" });
    }

    res.cookie("token", token, {
        httpOnly: true,
        secure: process.env.NODE_ENV === "production", // Secure only in production
        sameSite: "Strict",
        maxAge: 60 * 60 * 1000,
    });

    res.json({ message: "Token stored in cookie" });
});

app.get("/", (req, res) => res.render("home"));
app.get("/login", (req, res) => res.render("login"));

app.use("/admin", adminRoutes, authenticate, authorizeRole("STUDENT"));
app.use("/student", studentRoutes, authenticate, authorizeRole("STUDENT"));
app.use("/hr", hrRoutes);

app.get('/logout', (req, res) => {
    res.setHeader('Clear-Site-Data', '"cache", "cookies", "storage"');
    res.clearCookie('token');
    res.redirect('/login');
});


// 404 Handler
app.use((req, res) => {
    res.status(404).json({ message: "Route not found" });
});

app.listen(PORT, () => {
    console.log(`The server is listening at http://${SERVER_NAME}:${PORT}...`);
});
