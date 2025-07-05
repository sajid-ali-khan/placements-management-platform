using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Dto
{
    public class CompanyCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
