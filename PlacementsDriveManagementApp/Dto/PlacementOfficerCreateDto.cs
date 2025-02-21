using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Dto
{
    public class PlacementOfficerCreateDto
    {
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
