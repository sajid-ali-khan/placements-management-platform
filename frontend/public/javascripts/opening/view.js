
function OpeningsViewModel(companyId) {
    console.log(companyId);
    
    var self = this;
    self.jobs = ko.observableArray([]);
    self.isLoading = ko.observable(true);
    self.fetchOpenings = async function () {
        try {
            const apiUrl = `https://localhost:7209/api/Company/${companyId}/openings`;
            const response = await fetch(apiUrl);
            if (!response.ok) throw new Error("Failed to fetch job openings");
            const data = await response.json();
            self.jobs(data);
        } catch (error) {
            console.error("Error fetching job openings:", error);
        } finally {
            self.isLoading(false);
        }
    };

    self.fetchOpenings();
}

const companyId = parseInt(document.getElementById("companyId").textContent)


ko.applyBindings(new OpeningsViewModel(companyId));