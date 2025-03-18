$(document).ready(function () {
    $("#login-btn").on("click", function () {
        const email = $("#email").val().trim();
        const password = $("#password").val().trim();
        const role = parseInt($("#role").val());
        const errorContainer = $("#error-message");

        // Clear previous error message
        errorContainer.text("").addClass("hidden");

        if (!email || !password) {
            errorContainer.text("Email and password are required.").removeClass("hidden");
            return;
        }

        $.ajax({
            url: "https://localhost:7209/api/Auth/login",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({ email, password, role }),
            success: function (data) {
                if (data.token) {
                    $.ajax({
                        url: "/store-token",
                        method: "POST",
                        contentType: "application/json",
                        data: JSON.stringify({ token: data.token }),
                        xhrFields: { withCredentials: true },
                        success: function () {
                            localStorage.setItem("userEmail", email);
                            if (role === 1) window.location.href = "/student";
                            else if (role === 2) window.location.href = "/admin";
                            else if (role === 3) window.location.href = "/hr";
                            else {
                                errorContainer.text("Invalid role selection.").removeClass("hidden");
                            }
                        },
                        error: function (err) {
                            console.error("Error storing token:", err);
                        }
                    });
                } else {
                    errorContainer.text("Login failed: " + (data.message || "Invalid credentials")).removeClass("hidden");
                }
            },
            error: function (err) {
                errorContainer.text("Invalid credentials. Please try again.").removeClass("hidden");
            }
        });
    });
});
