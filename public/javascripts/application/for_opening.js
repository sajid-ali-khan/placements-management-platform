function ApplicationViewModel() {
    const self = this;
    
    // Observable properties
    self.applications = ko.observableArray([]);
    self.searchQuery = ko.observable("");
    self.selectedStatus = ko.observable("");
    self.selectedJobTitle = ko.observable("");
    self.statusOptions = ko.observableArray(["Pending", "Selected", "InterviewScheduled"]);
    self.jobTitleOptions = ko.observableArray([]);
    
    self.filteredApplications = ko.computed(function () {
        return self.applications().filter(app => {
            const matchesSearch = self.searchQuery().length === 0 || app.studentName.toLowerCase().includes(self.searchQuery().toLowerCase());
            const matchesStatus = !self.selectedStatus() || app.applicationStatus === self.selectedStatus();
            const matchesJobTitle = !self.selectedJobTitle() || app.jobTitle === self.selectedJobTitle();
            return matchesSearch && matchesStatus && matchesJobTitle;
        });
    });

    self.fetchApplications = function () {
        const companyEmail = localStorage.getItem("userEmail");
        if (!companyEmail) {
            console.error("Company email not found in localStorage");
            return;
        }
        
        const url = `https://localhost:7209/api/Company/email/${encodeURIComponent(companyEmail)}/applications`;
        
        fetch(url)
            .then(response => response.json())
            .then(data => {
                self.applications(data);
                
                const jobTitles = [...new Set(data.map(app => app.jobTitle))];
                self.jobTitleOptions(jobTitles);
            })
            .catch(error => console.error("Error fetching applications:", error));
    };
    
    self.viewDetails = function (application) {
        alert(`Viewing details for ${application.studentName}`);
    };
    
    self.fetchApplications();
}

ko.applyBindings(new ApplicationViewModel());
