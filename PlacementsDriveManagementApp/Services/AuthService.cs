using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Helper;
using PlacementsDriveManagementApp.Interfaces;

namespace PlacementsDriveManagementApp.Services
{
    public class AuthService
    {
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;
        private readonly IStudentRepo _studentRepo;
        private readonly ICompanyRepo _companyRepo;
        private readonly IPlacementOfficerRepo _placementOfficerRepo;
        public AuthService(PasswordService passwordService, JwtService jwtService, IStudentRepo studentRepo, ICompanyRepo companyRepo, IPlacementOfficerRepo placementOfficerRepo)
        {
            _passwordService = passwordService;
            _jwtService = jwtService;
            _studentRepo = studentRepo;
            _companyRepo = companyRepo;
            _placementOfficerRepo = placementOfficerRepo;
        }

        public UserDto? AuthenticateUser(UserDto user)
        {
            if (user is null)
                return null;

            string hashedPassword = "";
            bool userExists = false;

            switch (user.Role)
            {
                case UserRole.STUDENT:
                    userExists = _studentRepo.StudentExistsByEmail(user.Email);
                    hashedPassword = userExists ? _studentRepo.GetHashedPassword(user.Email) : null;
                    break;
                case UserRole.ADMIN:
                    userExists = _placementOfficerRepo.PlacementOfficerExistsByEmail(user.Email);
                    hashedPassword = userExists ? _placementOfficerRepo.GetHashedPassword(user.Email) : null;
                    break;
                case UserRole.HR:
                    userExists = _companyRepo.CompanyExistsByEmail(user.Email);
                    hashedPassword = userExists ? _companyRepo.GetHashedPassword(user.Email) : null;
                    break;
                default:
                    return null;
            }

            if (!userExists || !_passwordService.VerifyPassword(user.Password, hashedPassword))
            {
                return null;
            }

            return user;
        }
    }
}
