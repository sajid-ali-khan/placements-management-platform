using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IStudentRepo
    {
        ICollection<Student> GetStudents();
    }
}
