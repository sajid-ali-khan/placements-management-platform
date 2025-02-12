using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Models
{
    public class Opening
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }

        [Required]
        public string JobTitle { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime LastDate { get; set; }
        public bool IsActive { get; set; }

        public Company  Company { get; set; }
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
