document.addEventListener("DOMContentLoaded", async function () {
    function HRViewModel() {
        const self = this;

        // Form fields
        self.jobTitle = ko.observable("");
        self.description = ko.observable("");
        self.lastDate = ko.observable("");
        self.successMessage = ko.observable("");
        self.errorMessage = ko.observable("");

        const companyEmail = localStorage.getItem("userEmail"); 
        if (!companyEmail) {
            console.error("No company email found in local storage.");
            self.errorMessage("Company email not found.");
            return;
        }

        const companyApiUrl = `https://localhost:7209/api/Company/email/${encodeURIComponent(companyEmail)}`;
        const openingCreateUrl = `https://localhost:7209/api/Opening`;

        // Fetch Company ID
        async function fetchCompanyData() {
            try {
                console.log("Fetching company data from:", companyApiUrl);
                const response = await fetch(companyApiUrl);
                if (!response.ok) throw new Error("Failed to fetch company data");
                const data = await response.json();
                self.companyId = data.id;
                console.log("Company ID:", self.companyId);
            } catch (error) {
                console.error("Error fetching company data:", error);
                self.errorMessage("Failed to load company data.");
            }
        }

        // Function to create job opening
        self.createOpening = async function () {
            if (!self.jobTitle() || !self.description() || !self.lastDate()) {
                self.errorMessage("All fields are required.");
                return;
            }

            const jobOpening = {
                companyId: self.companyId, 
                jobTitle: self.jobTitle(),
                description: self.description(),
                lastDate: new Date(self.lastDate()).toISOString(),
                isActive: true
            };

            console.log("Submitting job opening:", jobOpening);

            try {
                const response = await fetch(openingCreateUrl, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(jobOpening)
                });

                if (!response.ok) throw new Error("Failed to create job opening");

                self.successMessage("Job opening created successfully!");
                self.errorMessage("");
                self.jobTitle("");
                self.description("");
                self.lastDate("");
            } catch (error) {
                console.error("Error creating job opening:", error);
                self.errorMessage("Failed to create job opening.");
            }
        };

        fetchCompanyData();
    }

    ko.applyBindings(new HRViewModel());
});
