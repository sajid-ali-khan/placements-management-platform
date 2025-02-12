using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Dto
{
    public class OpeningDto
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastDate { get; set; }
        public bool IsActive { get; set; }
    }
}
