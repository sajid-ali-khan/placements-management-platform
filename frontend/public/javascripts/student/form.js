
function AddStudentViewModel() {
    var self = this;

    self.id = ko.observable("");
    self.name = ko.observable("");
    self.phone = ko.observable("");
    self.email = ko.observable("");
    self.dob = ko.observable("");
    self.password = ko.observable("");

    self.successMessage = ko.observable("");
    self.errorMessage = ko.observable("");

    self.addStudent = async function () {
        self.successMessage("");
        self.errorMessage("");

        const newStudent = {
            id: self.id(),
            name: self.name(),
            phone: self.phone(),
            email: self.email(),
            dob: new Date(self.dob()).toISOString(),
            password: self.password()
        };

        try {
            const response = await fetch("https://localhost:7209/api/Student", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newStudent)
            });

            const responseData = await response.text();

            if (response.status === 201) {
                self.successMessage("Student added successfully!");
                self.clearForm();
            } else if (response.status === 400) {
                self.errorMessage("Validation failed: " + responseData);
            } else if (response.status === 402) {
                self.errorMessage("Payment required. Please check with the administrator.");
            } else if (response.status === 500) {
                self.errorMessage("Something went wrong while trying to add the student.");
            } else {
                self.errorMessage("Unexpected error: " + responseData);
            }
        } catch (error) {
            console.error("Error adding student:", error);
            self.errorMessage("Network error. Please try again.");
        }
    };

    self.clearForm = function () {
        self.id("");
        self.name("");
        self.phone("");
        self.email("");
        self.dob("");
        self.password("");
    };
}

ko.applyBindings(new AddStudentViewModel());