function StudentViewModel() {
    var self = this;

    self.students = ko.observableArray([]);
    self.searchQuery = ko.observable("");

    self.filteredStudents = ko.computed(function () {
        var search = self.searchQuery().toLowerCase();
        if (!search) {
            return self.students();
        } else {
            return ko.utils.arrayFilter(self.students(), function (student) {
                return student.id.toLowerCase().includes(search) || student.name.toLowerCase().includes(search);
            });
        }
    });

    self.fetchStudents = async function () {
        try {
            const apiUrl = "https://localhost:7209/api/Student";
            const response = await fetch(apiUrl);
            if (!response.ok) throw new Error("Failed to fetch students");
            const data = await response.json();

            const studentsWithFormattedDob = data.map(student => ({
                id: student.id,
                name: student.name,
                phone: student.phone,
                email: student.email,
                formattedDob: student.formattedDob
            }));

            self.students(studentsWithFormattedDob);
        } catch (error) {
            console.error("Error fetching students:", error);
        }
    };

    self.fetchStudents();
}

ko.applyBindings(new StudentViewModel());