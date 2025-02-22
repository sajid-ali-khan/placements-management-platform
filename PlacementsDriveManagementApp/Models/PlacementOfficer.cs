using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Models
{
    public class PlacementOfficer
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
