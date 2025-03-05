document.addEventListener("DOMContentLoaded", async () => {
    const studentEmail = localStorage.getItem("userEmail");

    if (!studentEmail) {
        alert("No student logged in. Redirecting to login.");
        window.location.href = "/login";
        return;
    }

    const apiUrl = `https://localhost:7209/api/Student/${encodeURIComponent(studentEmail)}/applications`;

    try {
        const response = await fetch(apiUrl);
        if (!response.ok) throw new Error("Failed to fetch applications");

        const applications = await response.json();
        displayApplications(applications);
    } catch (error) {
        console.error("Error fetching applications:", error);
        document.getElementById("applicationsTable").innerHTML =
            `<tr><td colspan="10" class="text-center text-red-500 p-4">Failed to load applications.</td></tr>`;
    }
});

function displayApplications(applications) {
    const tableBody = document.getElementById("applicationsTable");
    tableBody.innerHTML = "";

    if (applications.length === 0) {
        tableBody.innerHTML = `<tr><td colspan="10" class="text-center text-gray-500 p-4">No applications found.</td></tr>`;
        return;
    }

    applications.forEach(app => {
        const row = document.createElement("tr");
        row.classList.add("border", "border-gray-300");

        row.innerHTML = `
            <td class="border border-gray-300 px-4 py-2">${app.applicationId}</td>
            <td class="border border-gray-300 px-4 py-2">${app.companyName}</td>
            <td class="border border-gray-300 px-4 py-2">${app.jobTitle}</td>
            <td class="border border-gray-300 px-4 py-2">${formatDate(app.appliedDate)}</td>
            <td class="border border-gray-300 px-4 py-2">${app.applicationStatus}</td>
            <td class="border border-gray-300 px-4 py-2">${app.interviewSlot ? formatDateTime(app.interviewSlot) : "N/A"}</td>
            <td class="border border-gray-300 px-4 py-2">${app.studentAppeared !== null ? (app.studentAppeared ? "Yes" : "No") : "N/A"}</td>
            <td class="border border-gray-300 px-4 py-2">${app.package !== null ? app.package + " LPA" : "N/A"}</td>
            <td class="border border-gray-300 px-4 py-2">${app.joiningDate ? formatDate(app.joiningDate) : "N/A"}</td>
            <td class="border border-gray-300 px-4 py-2">${app.placeOfWork || "N/A"}</td>
        `;
        tableBody.appendChild(row);
    });
}

function formatDate(dateString) {
    return dateString ? new Date(dateString).toLocaleDateString() : "N/A";
}

function formatDateTime(dateTimeString) {
    return dateTimeString ? new Date(dateTimeString).toLocaleString() : "N/A";
}