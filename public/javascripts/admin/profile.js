function AdminProfileViewModel(){
    var self = this;

    self.id = ko.observable("");
    self.name = ko.observable("");
    self.email = ko.observable("");

    self.fetchAdminProfile = async function () {
        const userEmail = localStorage.getItem("userEmail");

        if (!userEmail){
            alert("User email not found");
            return;
        }

        try {
            const apiUrl = `https://localhost:7209/api/PlacementOfficer/email/${userEmail}`;
            const response = await fetch(apiUrl);

            if (!response.ok){
                throw new Error("Error fetching admin data")
            }

            const admin = await response.json();
            self.id(admin.id);
            self.name(admin.userName)
            self.email(admin.email);
        }catch (error){
            console.error("Error fetching admin data", error)
        }
    };

    self.fetchAdminProfile();
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

console.log("hello")
document.addEventListener("DOMContentLoaded", ()=>{
    ko.applyBindings(new AdminProfileViewModel());
})