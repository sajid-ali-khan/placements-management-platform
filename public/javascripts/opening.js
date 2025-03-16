function Job(data) {
    this.openingId = ko.observable(data.id);
    this.companyName = ko.observable(data.companyName);
    this.jobTitle = ko.observable(data.jobTitle);
    this.description = ko.observable(data.description);
    this.createdDate = ko.observable(data.createdDate);
    this.lastDate = ko.observable(data.lastDate);
    this.isActive = ko.observable(data.isActive);

    // Capitalize first letter of company name
    this.formattedCompanyName = ko.computed(() => {
        return this.companyName().charAt(0).toUpperCase() + this.companyName().slice(1);
    });

    // Apply function
    this.apply = function () {
        if (this.isActive()) {
            const openingId = parseInt(this.openingId(), 10);
            console.log("Opening ID:", openingId);
            window.location.href = `/student/apply?openingId=${openingId}`;
        }
    };
}

function JobViewModel() {
    var self = this;
    self.jobs = ko.observableArray([]);
    self.isLoading = ko.observable(true);
    self.hasError = ko.observable(false);
    self.searchQuery = ko.observable('');

    // Fetch jobs
    self.fetchJobs = function () {
        fetch('https://localhost:7209/api/Opening')
            .then(response => response.json())
            .then(data => {
                self.jobs(data.map(job => new Job(job)));
                self.isLoading(false);
            })
            .catch(error => {
                console.error("Error fetching jobs:", error);
                self.hasError(true);
                self.isLoading(false);
            });
    };

    // Filter jobs based on search query
    self.filteredJobs = ko.computed(() => {
        let query = self.searchQuery().toLowerCase();
        return self.jobs().filter(job =>
            job.companyName().toLowerCase().includes(query) ||
            job.jobTitle().toLowerCase().includes(query)
        );
    });

    self.fetchJobs();
}

document.addEventListener("DOMContentLoaded", () => ko.applyBindings(new JobViewModel()));
