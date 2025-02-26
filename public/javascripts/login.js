$(document).ready(function () {
    $("#login-btn").on("click", function () {
        const email = $("#email").val();
        const password = $("#password").val();
        const role = parseInt($("#role").val()); // Convert role to integer

        $.ajax({
            url: "https://localhost:7209/api/Auth/login", // Ensure backend API is correct
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({ email, password, role }),
            success: function (data) {
                if (data.token) {
                    // Send token to Express to store in cookie
                    $.ajax({
                        url: "/store-token", // Ensure Express backend handles this
                        method: "POST",
                        contentType: "application/json",
                        data: JSON.stringify({ token: data.token }),
                        xhrFields: { withCredentials: true }, // Ensure cookies are stored
                        success: function () {
                            // Redirect based on role
                            if (role === 1) window.location.href = "/student";
                            else if (role === 2) window.location.href = "/admin";
                            else window.location.href = "/hr";
                        },
                        error: function (err) {
                            console.error("Error storing token:", err);
                        }
                    });
                } else {
                    alert("Login failed: " + data.message);
                }
            },
            error: function (err) {
                console.error("Login API error:", err);
            }
        });
    });
});
