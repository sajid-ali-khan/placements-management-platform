using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Helper;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Repository;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly PasswordService _passwordService;
        private readonly IStudentRepo _studentRepo;
        private readonly ICompanyRepo _companyRepo;
        private readonly IPlacementOfficerRepo _placementOfficerRepo;

        public AuthController(PasswordService passwordService, IStudentRepo studentRepo, ICompanyRepo companyRepo, IPlacementOfficerRepo placementOfficerRepo)
        {
            _passwordService = passwordService;
            _studentRepo = studentRepo;
            _companyRepo = companyRepo;
            _placementOfficerRepo = placementOfficerRepo;
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            string hashedPassword = "";

            switch (user.Role)
            {
                case UserRole.STUDENT:
                    if (!_studentRepo.StudentExistsByEmail(user.Email))
                    {
                        ModelState.AddModelError("", "User doesn't exists.");
                        return BadRequest(ModelState);
                    }
                    hashedPassword = _studentRepo.GetHashedPassword(user.Email);
                    break;
                case UserRole.ADMIN:
                    if (!_placementOfficerRepo.PlacementOfficerExistsByEmail(user.Email))
                    {
                        ModelState.AddModelError("", "User doesn't exists.");
                        return BadRequest(ModelState);
                    }
                    hashedPassword = _placementOfficerRepo.GetHashedPassword(user.Email);
                    break;
                case UserRole.HR:
                    if (!_companyRepo.CompanyExistsByEmail(user.Email))
                    {
                        ModelState.AddModelError("", "User doesn't exists.");
                        return BadRequest(ModelState);
                    }
                    hashedPassword = _companyRepo.GetHashedPassword(user.Email);
                    break;
                default:
                    ModelState.AddModelError("", "Invalid user role.");
                    return BadRequest(ModelState);
            }

            if (_passwordService.VerifyPassword(user.Password, hashedPassword))
            {
                return Ok("Login Successfull");
            }

            return Unauthorized(new { message = "Invalid Credentials" });
        }
    }
}
