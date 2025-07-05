function ApplyViewModel() {
    var self = this;

    self.jobTitle = ko.observable("");
    self.companyName = ko.observable("");
    self.description = ko.observable("");
    self.lastDate = ko.observable("");
    self.resumeName = ko.observable("No file selected");
    self.successMessage = ko.observable("");
    self.errorMessage = ko.observable("");

    self.formattedCompanyName = ko.computed(function () {
        return self.companyName().charAt(0).toUpperCase() + self.companyName().slice(1);
    });

    self.formattedLastDate = ko.computed(function () {
        return new Date(self.lastDate()).toLocaleDateString("en-GB");
    });

    const urlParams = new URLSearchParams(window.location.search);
    const openingId = urlParams.get("openingId");

    fetch(`https://localhost:7209/api/Opening/${openingId}`)
        .then(response => response.json())
        .then(data => {
            self.jobTitle(data.jobTitle);
            self.companyName(data.companyName);
            self.description(data.description);
            self.lastDate(data.lastDate);
        })
        .catch(error => console.error("Error fetching job details:", error));

    self.onFileSelect = function (event) {
        const file = event.target.files[0];
        if (file) self.resumeName(file.name);
    };

    document.getElementById('file-upload').addEventListener('change', self.onFileSelect);

    const dropzone = document.getElementById('dropzone');
    dropzone.addEventListener('dragover', e => {
        e.preventDefault();
        dropzone.classList.add('border-indigo-600');
    });

    dropzone.addEventListener('dragleave', () => dropzone.classList.remove('border-indigo-600'));

    dropzone.addEventListener('drop', e => {
        e.preventDefault();
        dropzone.classList.remove('border-indigo-600');
        const file = e.dataTransfer.files[0];
        if (file) {
            self.resumeName(file.name);
            document.getElementById('file-upload').files = e.dataTransfer.files;
        }
    });

    self.uploadResume = async function () {
        self.successMessage("");
        self.errorMessage("");

        const fileInput = document.getElementById("file-upload");
        const file = fileInput.files[0];

        if (!file) {
            self.errorMessage("Please select a resume before submitting.");
            return;
        }

        const formData = new FormData();
        formData.append("resume", file);

        try {
            const uploadResponse = await fetch("http://localhost:3000/resume/upload", {
                method: "POST",
                body: formData,
            });

            const uploadData = await uploadResponse.json();

            if (!uploadResponse.ok) {
                throw new Error(uploadData.message || "Failed to upload resume.");
            }

            const filePath = uploadData.filePath;
            const fileName = file.name;

            const resumeData = {
                name: fileName,
                filePath: filePath,
                openingId: openingId
            };

            const backendResponse = await fetch("https://localhost:7209/api/Resume", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(resumeData),
            });

            if (!backendResponse.ok) {
                throw new Error("Failed to send resume details to .NET backend.");
            }

            const resumeResponseData = await backendResponse.json();
            const resumeId = resumeResponseData.resumeId;

            const studentEmail = localStorage.getItem("userEmail");

            if (!studentEmail) {
                throw new Error("Student email not found in local storage.");
            }

            const applicationData = {
                studentEmail: studentEmail,
                openingId: openingId,
                resumeId: resumeId
            };

            const applicationResponse = await fetch("https://localhost:7209/api/Application", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(applicationData),
            });

            if (!applicationResponse.ok) {
                throw new Error("Failed to create application in .NET backend.");
            }

            self.successMessage("Application submitted successfully!");
            self.resumeName("No file selected");
            fileInput.value = "";

        } catch (error) {
            console.error("Upload error:", error);
            self.errorMessage(error.message);
        }
    };

}

ko.applyBindings(new ApplyViewModel());