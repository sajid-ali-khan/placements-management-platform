function PlacementRecordsViewModel() {
    var self = this;

    self.applications = ko.observableArray([]);
    self.searchQuery = ko.observable("");

    
    self.filteredApplications = ko.computed(function () {
        var search = self.searchQuery().toLowerCase();
        if (!search) return self.applications();
        return ko.utils.arrayFilter(self.applications(), function (app) {
            return app.studentId.toLowerCase().includes(search) ||
                app.studentName.toLowerCase().includes(search) ||
                app.companyName.toLowerCase().includes(search);
        });
    });

    self.fetchApplications = async function () {
        try {
            const apiUrl = "https://localhost:7209/api/Application";
            const response = await fetch(apiUrl);
            if (!response.ok) throw new Error("Failed to fetch applications");
            const data = await response.json();

            const applicationsWithComputedStatus = data.map(app => ({
                ...app,
                statusClass: ko.observable(self.getStatusClass(app.applicationStatus))
            }));

            self.applications(applicationsWithComputedStatus);
        } catch (error) {
            console.error("Error fetching applications:", error);
        }
    };

    self.getStatusClass = function (status) {
        if (!status) return "text-gray-700";
        switch (status.toLowerCase()) {
            case "pending": return "text-yellow-500";
            case "selected": return "text-green-500";
            case "rejected": return "text-red-500";
            case "interviewscheduled": return "text-blue-500";
            default: return "text-gray-700";
        }
    };

    self.fetchApplications();
}

ko.applyBindings(new PlacementRecordsViewModel());