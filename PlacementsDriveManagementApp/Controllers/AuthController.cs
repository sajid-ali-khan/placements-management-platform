using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Helper;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Services;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            var user = _authService.AuthenticateUser(userDto);

            if (user is null)
            {
                return Unauthorized(new { message = "Login unsuccessful. Invalid email or password" });
            }

            var token = _jwtService.GenerateToken(user.Email, user.Role.ToString());

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(60)
            });

            return Ok(new { 
                message = "Successfully logged in.",
                token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return Ok(new { message = "Logout successful" });
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedRoute()
        {
            return Ok(new { message = "This is a protected API route!" });
        }

    }
}
