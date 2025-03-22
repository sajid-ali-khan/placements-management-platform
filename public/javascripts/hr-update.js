document.addEventListener("DOMContentLoaded", async function () {
    const companyEmail = localStorage.getItem("companyEmail"); // Assuming email is stored
    const companyApiUrl = `https://localhost:7209/api/Company/email/${companyEmail}`;
    
    try {
        // Fetch company details
        const companyResponse = await fetch(companyApiUrl);
        if (!companyResponse.ok) throw new Error("Failed to fetch company data");
        
        const companyData = await companyResponse.json();
        const companyName = companyData.companyName; // Assuming API returns companyName

        // Fetch all student applications
        const applicationsResponse = await fetch("https://localhost:7209/api/Application");
        if (!applicationsResponse.ok) throw new Error("Failed to fetch applications");

        const allApplications = await applicationsResponse.json();

        // Filter applications for this company
        const companyApplications = allApplications.filter(app => app.companyName === companyName);

        // Display applications
        renderApplications(companyApplications);

    } catch (error) {
        console.error("Error fetching data:", error);
    }
});

// Function to render applications in the UI
function renderApplications(applications) {
    const container = document.getElementById("applicationsContainer");
    container.innerHTML = "";

    if (applications.length === 0) {
        container.innerHTML = `<p class="text-gray-500 text-center">No student applications for this company.</p>`;
        return;
    }

    applications.forEach(app => {
        const appCard = `
            <div class="flex items-center justify-between p-4 border rounded-lg shadow-md bg-white">
                <div>
                    <h2 class="text-xl mb-2 font-semibold text-gray-800">${app.studentName}</h2>
                    <p class="text-gray-600"><strong>Student ID:</strong> ${app.studentId}</p>
                    <p class="text-gray-600"><strong>Job Title:</strong> ${app.jobTitle}</p>
                    <p class="text-gray-600"><strong>Application Status:</strong> ${app.applicationStatus}</p>
                    <p class="text-gray-500"><strong>Applied Date:</strong> ${app.appliedDate}</p>
                </div>
                <button class="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-700 cursor-pointer" 
                    onclick="updateApplication(${app.applicationId})">
                    Update
                </button>
            </div>
        `;
        container.innerHTML += appCard;
    });
}

// Function to handle application update
/*

console.log("Fetching applications from:", openingCreateUrl);
        const applicationResponse = await fetch(openingCreateUrl);
        if (!applicationResponse.ok) throw new Error("Failed to fetch applications");
        const applications = await applicationResponse.json();

        // Filter applications for this company
        const filteredApplications = applications.filter(app => app.companyName === companyName);

        // Update UI
        const container = document.getElementById("applications-container");
        if (filteredApplications.length === 0) {
            container.innerHTML = "<p class='text-gray-500 text-center'>No students have applied to this company.</p>";
        } else {
            container.innerHTML = filteredApplications.map(app => `
                <div class="p-4 border rounded-lg shadow-md bg-white">
                    <h2 class="text-xl font-semibold">${app.studentName}</h2>
                    <p><strong>Job Title:</strong> ${app.jobTitle}</p>
                    <p><strong>Status:</strong> ${app.applicationStatus}</p>
                    <p><strong>Applied Date:</strong> ${app.appliedDate}</p>
                    <button class="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-700 update-btn"
                        data-application-id="${app.applicationId}">
                        Update
                    </button>
                </div>
            `).join("");
        }

        // Add event listeners for update buttons
        document.querySelectorAll(".update-btn").forEach(button => {
            button.addEventListener("click", function () {
                const applicationId = this.getAttribute("data-application-id");
                window.location.href = `/hr/update-application/${applicationId}`;
            });
        });

    } catch (error) {
        console.error("Error fetching data:", error);
        document.getElementById("applications-container").innerHTML = `<p class="text-red-500 text-center">Failed to load data.</p>`;
    }
*/ 
