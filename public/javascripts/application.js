document.addEventListener("DOMContentLoaded", async () => {
    function ApplicationViewModel() {
        const self = this;
        self.applications = ko.observableArray([]);
        self.searchQuery = ko.observable("");
        
        self.filteredApplications = ko.computed(() => {
            const query = self.searchQuery().toLowerCase();
            if (!query) return self.applications();
            return self.applications().filter(app =>
                app.companyName.toLowerCase().includes(query) ||
                app.jobTitle.toLowerCase().includes(query) ||
                app.applicationStatus.toLowerCase().includes(query)
            );
        });

        self.loadApplications = async () => {
            const studentEmail = localStorage.getItem("userEmail");
            if (!studentEmail) {
                alert("No student logged in. Redirecting to login.");
                window.location.href = "/login";
                return;
            }

            const apiUrl = `https://localhost:7209/api/Student/${encodeURIComponent(studentEmail)}/applications`;
            try {
                const response = await fetch(apiUrl);
                if (!response.ok) throw new Error("Failed to fetch applications");
                const applications = await response.json();
                self.applications(applications);
            } catch (error) {
                console.error("Error fetching applications:", error);
                self.applications([]);
            }
        };
    }

    const viewModel = new ApplicationViewModel();
    ko.applyBindings(viewModel);
    viewModel.loadApplications();
});