document.addEventListener("DOMContentLoaded", async function () {
    const companyEmail = localStorage.getItem("userEmail"); // Fetch from local storage
    if (!companyEmail) {
        console.error("No company email found in local storage.");
        return;
    }
    console.log(companyEmail)

    const companyApiUrl = `https://localhost:7209/api/Company/email/${encodeURIComponent(companyEmail)}`;
    const openingCreateUrl = `https://localhost:7209/api/Opening`;

    try {
        // Fetch Company Data
        console.log("Fetching company data from:", companyApiUrl);
        const companyResponse = await fetch(companyApiUrl);
        if (!companyResponse.ok) throw new Error("Failed to fetch company data");
        const companyData = await companyResponse.json();
        console.log(companyData);
        
        const companyId = companyData.id;
        console.log(companyId);

        
    } catch (error) {
        console.error("Error fetching data:", error);
        document.getElementById("applications-container").innerHTML = `<p class="text-red-500 text-center">Failed to load data.</p>`;
    }
});
