const express = require("express");
const multer = require("multer");
const path = require("path");
const fs = require("fs");
const axios = require("axios");

const router = express.Router();

// Ensure upload directory exists
const uploadDir = path.join(__dirname, "../public/uploads/resumes");
if (!fs.existsSync(uploadDir)) {
    fs.mkdirSync(uploadDir, { recursive: true });
}

// Configure multer storage
const storage = multer.diskStorage({
    destination: (req, file, cb) => {
        cb(null, uploadDir);
    },
    filename: (req, file, cb) => {
        const timestamp = Date.now();
        const ext = path.extname(file.originalname);
        const filename = path.basename(file.originalname, ext) + "_" + timestamp + ext;
        cb(null, filename);
    },
});

const upload = multer({ 
    storage,
    limits: { fileSize: 10 * 1024 * 1024 },
    fileFilter: (req, file, cb) => {
        if (["application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"].includes(file.mimetype)) {
            cb(null, true);
        } else {
            cb(new Error("Only PDF and DOCX files are allowed"), false);
        }
    }
});

router.post("/upload", upload.single("resume"), async (req, res) => {
    try {
        if (!req.file) return res.status(400).json({ message: "No file uploaded" });

        const filePath = `/uploads/resumes/${req.file.filename}`;
        res.status(200).json({ message: "Resume uploaded successfully", filePath });
    } catch (error) {
        console.error("Upload error:", error);
        res.status(500).json({ message: "Internal Server Error" });
    }
});


module.exports = router;
