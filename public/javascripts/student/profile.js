function StudentProfileViewModel() {
    var self = this;

    self.id = ko.observable("");
    self.name = ko.observable("");
    self.email = ko.observable("");
    self.phone = ko.observable("");
    self.dob = ko.observable("");

    self.formattedDob = ko.computed(() => {
        return self.dob() ? new Date(self.dob()).toISOString().split('T')[0] : "";
    });

    self.fetchStudentProfile = async function () {
        const userEmail = localStorage.getItem("userEmail");

        if (!userEmail) {
            alert("No user logged in.");
            return;
        }

        try {
            const apiUrl = `https://localhost:7209/api/Student/email/${userEmail}`;
            const response = await fetch(apiUrl);

            if (!response.ok) {
                throw new Error("Failed to fetch student data.");
            }

            const student = await response.json();
            self.id(student.id);
            self.name(student.name);
            self.email(student.email);
            self.phone(student.phone);
            self.dob(student.dob);

        } catch (error) {
            console.error("Error fetching student data:", error);
        }
    };

    self.fetchStudentProfile();
}

self.logout = async function () {
    try {
        const response = await fetch("/logout", {
            method: "GET",
            credentials: "include",
        });

        if (!response.ok) {
            throw new Error("Logout failed.");
        }

        alert("You have been logged out.");
        window.location.href = "/login";

    } catch (error) {
        console.error("Logout error:", error);
    }
};


document.addEventListener("DOMContentLoaded", () => {
    ko.applyBindings(new StudentProfileViewModel());
});