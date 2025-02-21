using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Dto
{
    public class OpeningCreateDto
    {
        public int CompanyId { get; set; }

        [Required]
        public string JobTitle { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime LastDate { get; set; }

        public bool IsActive { get; set; }
    }
}
