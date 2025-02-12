using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Opening> Openings { get; set; } = new List<Opening>();
    }
}
