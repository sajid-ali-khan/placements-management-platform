document.addEventListener("DOMContentLoaded", async function () {
    function HRViewModel() {
        const self = this;

        self.openings = ko.observableArray([]); // Store job openings
        self.companyName = ko.observable(""); // Store company name
        self.errorMessage = ko.observable("");
        self.showCreateForm = ko.observable(false); // Toggle form visibility
        self.searchQuery = ko.observable(""); // Search query for job title

        const companyEmail = "Iota@gmail.com"; // Replace this dynamically if needed
        const companyApiUrl = `https://localhost:7209/api/Company/email/${encodeURIComponent(companyEmail)}`;
        const openingsApiUrl = "https://localhost:7209/api/Opening";

        // Fetch company name using email
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

        // Fetch company job openings
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

        // Toggle Create Opening Form
        self.toggleCreateForm = function () {
            self.showCreateForm(!self.showCreateForm());
        };

        // Computed observable for filtering job openings by search query
        self.filteredOpenings = ko.computed(() => {
            const query = self.searchQuery().toLowerCase();
            return ko.utils.arrayFilter(self.openings(), function (opening) {
                return opening.jobTitle.toLowerCase().includes(query);
            });
        });

        fetchCompanyOpenings();
        self.toggleCreateForm = function () {
            window.location.href = "/hr/openings/new"; // Redirect to hr-opening.ejs
        }; 
    }   

    ko.applyBindings(new HRViewModel());
});
