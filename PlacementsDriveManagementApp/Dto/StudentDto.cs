using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Dto
{
    public class StudentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public DateTime Dob { get; set; }
        public string PassWord { get; set; }
    }
}
