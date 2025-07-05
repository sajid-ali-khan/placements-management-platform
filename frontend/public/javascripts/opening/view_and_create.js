document.addEventListener("DOMContentLoaded", async function () {
    function HRViewModel() {
        const self = this;

        self.openings = ko.observableArray([]);
        self.companyName = ko.observable("");
        self.errorMessage = ko.observable("");
        self.showCreateForm = ko.observable(false);
        self.searchQuery = ko.observable(""); 

        const companyEmail = localStorage.getItem("userEmail");
        const companyApiUrl = `https://localhost:7209/api/Company/email/${encodeURIComponent(companyEmail)}`;
        const openingsApiUrl = "https://localhost:7209/api/Opening";

        async function fetchCompanyName() {
            try {
                const response = await fetch(companyApiUrl);
                if (!response.ok) throw new Error("Failed to fetch company details");

                const companyData = await response.json();
                if (!companyData || !companyData.name) throw new Error("Company name not found");

                self.companyName(companyData.name);
                return companyData.name;
            } catch (error) {
                console.error("Error fetching company name:", error);
                self.errorMessage("Failed to fetch company details.");
                return null;
            }
        }

        self.viewApplications = function (jobTitle) {
            const encodedJobTitle = encodeURIComponent(jobTitle);
            window.location.href = `/hr/applications?jobTitle=${encodedJobTitle}`;
        };
        
        

        async function fetchCompanyOpenings() {
            try {
                const companyName = await fetchCompanyName();
                if (!companyName) return;

                const response = await fetch(openingsApiUrl);
                if (!response.ok) throw new Error("Failed to fetch job openings");

                const allOpenings = await response.json();
                const companyOpenings = allOpenings.filter(opening => opening.companyName === companyName);

                self.openings(companyOpenings);
            } catch (error) {
                console.error("Error fetching openings:", error);
                self.errorMessage("Failed to load job openings.");
            }
        }

        self.toggleCreateForm = function () {
            self.showCreateForm(!self.showCreateForm());
        };

        self.filteredOpenings = ko.computed(() => {
            const query = self.searchQuery().toLowerCase();
            return ko.utils.arrayFilter(self.openings(), function (opening) {
                return opening.jobTitle.toLowerCase().includes(query);
            });
        });

        fetchCompanyOpenings();
        
        self.toggleCreateForm = function () {
            window.location.href = "/hr/openings/new";
        };
    }   

    ko.applyBindings(new HRViewModel());
});
