using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime Dob { get; set; }
        public string PassWord { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
