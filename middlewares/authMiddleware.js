const jwt = require("jsonwebtoken");

const SECRET_KEY = "MySuperSecretKeyIsThatThisKeyIsGoingToBeSoLongButStillContainingNothingMuchMeaningfull"; // Move this to an environment variable in production

// Middleware to authenticate the user
const authenticate = (req, res, next) => {
    const token = req.cookies.token; // Extract token from cookies

    if (!token) {
        return res.status(401).json({ message: "Unauthorized: No token provided" });
    }

    try {
        const decoded = jwt.verify(token, SECRET_KEY); // Verify JWT
        req.user = decoded; // Attach user info to request
        next(); // Proceed to the next middleware
    } catch (error) {
        return res.status(403).json({ message: "Forbidden: Invalid token" });
    }
};

// Middleware to authorize specific roles
const authorizeRole = (requiredRole) => {
    return (req, res, next) => {
        if (!req.user || req.user.role !== requiredRole) {
            return res.status(403).json({ message: "Forbidden: Insufficient privileges" });
        }
        next(); // Proceed if role matches
    };
};

module.exports = { authenticate, authorizeRole };
