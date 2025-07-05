document.addEventListener("DOMContentLoaded", function () {
    function ViewModel() {
        var self = this;
        self.applicationId = window.location.pathname.split("/")[3];
        self.studentName = ko.observable();
        self.companyName = ko.observable();
        self.jobTitle = ko.observable();
        self.resumeName = ko.observable();
        self.resumePath = ko.observable();
        self.applicationStatus = ko.observable();
        self.appliedDate = ko.observable();
        self.interviewSlot = ko.observable();
        self.studentAppeared = ko.observable(false);
        self.package = ko.observable();
        self.joiningDate = ko.observable();
        self.placeOfWork = ko.observable();
        self.availableStatuses = ko.observableArray([]);
        self.newStatus = ko.observable();
        
        self.showInterviewSlot = ko.computed(() => self.applicationStatus() === "InterviewScheduled");
        self.showStudentAppeared = ko.computed(() => self.applicationStatus() === "InterviewScheduled");
        self.showSelectionDetails = ko.computed(() => self.applicationStatus() === "Selected");
        
        self.showInterviewSlotInput = ko.computed(() => self.newStatus() === "InterviewScheduled");
        self.showStudentAppearedInput = ko.computed(() => self.applicationStatus() === "InterviewScheduled");
        self.showSelectionInputs = ko.computed(() => self.newStatus() === "Selected" || self.applicationStatus() === 'Selected');

        self.onStatusChange = function () {
            let current = self.applicationStatus();
            if (current === "Pending") {
                self.availableStatuses(["InterviewScheduled", "Rejected"]);
            } else if (current === "InterviewScheduled") {
                self.availableStatuses(["Selected", "Rejected"]);
            }
        };

        const statusList = {
            Pending: 1,
            InterviewScheduled: 2,
            Selected: 3,
            Rejected: 4
        };

        self.fetchApplicationDetails = function () {
            fetch(`https://localhost:7209/api/Application/${self.applicationId}`)
                .then(response => response.json())
                .then(data => {
                    self.studentName(data.studentName);
                    self.companyName(data.companyName);
                    self.jobTitle(data.jobTitle);
                    self.resumeName(data.resumeName);
                    self.resumePath(data.resumePath);
                    self.applicationStatus(data.applicationStatus);
                    self.appliedDate(data.appliedDate);
                    self.interviewSlot(data.interviewSlot);
                    self.studentAppeared(data.studentAppeared);
                    self.package(data.package);
                    self.joiningDate(data.joiningDate);
                    self.placeOfWork(data.placeOfWork);

                    self.onStatusChange();
                })
                .catch(error => console.error("Error fetching data:", error));
        };

        self.updateApplication = function () {
            let updateData = {
                Status: statusList[self.newStatus()],
                InterviewSlot: self.showInterviewSlotInput() ? new Date(self.interviewSlot()).toISOString() : null,
                StudentAppeared: self.showStudentAppearedInput() ? self.studentAppeared() : null,
                Package: self.showSelectionInputs() ? self.package() : null,
                JoiningDate: self.showSelectionInputs() ? new Date(self.joiningDate()).toISOString() : null,
                PlaceOfWork: self.showSelectionInputs() ? self.placeOfWork() : null
            };

            console.log(updateData);
            
            fetch(`https://localhost:7209/api/Application/${self.applicationId}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(updateData)
            })
            .then(response => {
                if (response.ok) {
                    alert("Application updated successfully");
                    self.fetchApplicationDetails();
                    window.location.href = "/hr/applications"
                } else {
                    alert("Failed to update application");
                }
            })
            .catch(error => console.error("Error updating application:", error));
        };

        self.fetchApplicationDetails();
    }
    
    ko.applyBindings(new ViewModel());
});
