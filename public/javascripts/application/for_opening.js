document.addEventListener("DOMContentLoaded", function () {
    function ApplicationViewModel() {
        const self = this;

        // Observable properties
        self.applications = ko.observableArray([]);
        self.searchQuery = ko.observable("");
        self.selectedStatus = ko.observable("");
        self.selectedJobTitle = ko.observable("");
        self.statusOptions = ko.observableArray(["Pending", "Selected", "InterviewScheduled"]);
        self.jobTitleOptions = ko.observableArray([]);

        // Computed filtered applications
        self.filteredApplications = ko.computed(function () {
            return ko.utils.arrayFilter(self.applications(), function (app) {
                const matchesSearch = !self.searchQuery() || app.studentName.toLowerCase().includes(self.searchQuery().toLowerCase());
                const matchesStatus = !self.selectedStatus() || app.applicationStatus === self.selectedStatus();
                const matchesJobTitle = !self.selectedJobTitle() || app.jobTitle === self.selectedJobTitle();
                return matchesSearch && matchesStatus && matchesJobTitle;

            });
        });

        // Fetch applications from API
        self.fetchApplications = function () {
            const companyEmail = localStorage.getItem("userEmail");
            if (!companyEmail) {
                console.error("❌ Company email not found in localStorage");
                return;
            }

            const url = `https://localhost:7209/api/Company/email/${encodeURIComponent(companyEmail)}/applications`;

            fetch(url)
                .then(response => {
                    if (!response.ok) throw new Error(`HTTP Error! Status: ${response.status}`);
                    return response.json();
                })
                .then(data => {
                    console.log("✅ Fetched applications:", data);

                    self.applications(data);

                    // Populate job title options dynamically
                    const jobTitles = [...new Set(data.map(app => app.jobTitle))];
                    self.jobTitleOptions(jobTitles);

                    // Apply job title filter from URL if present
                    const jobTitleFilter = getJobTitleFromUrl();
                    if (jobTitleFilter && self.jobTitleOptions().includes(jobTitleFilter)) {
                        console.log("Applying job title filter from URL:", jobTitleFilter);
                        self.selectedJobTitle(jobTitleFilter);
                    } else {
                        alert("No filters for the job title: " + jobTitleFilter);
                    }
                    
                })
                .catch(error => console.error("❌ Error fetching applications:", error));
        };

        // Navigate to application details page
        self.viewDetails = function (application) {
            if (application.applicationId) {
                window.location.href = `applications/${application.applicationId}/details`;
            } else {
                console.error("❌ Application ID missing!");
            }
        };

        // Get job title from URL
        function getJobTitleFromUrl() {
            const urlParams = new URLSearchParams(window.location.search);
            const fetchedJobTItle = urlParams.get("jobTitle") || "";
            console.log(fetchedJobTItle);
            
            return fetchedJobTItle;
        }

        // Fetch applications on load
        self.fetchApplications();
    }

    // Apply Knockout bindings
    ko.applyBindings(new ApplicationViewModel());
});
