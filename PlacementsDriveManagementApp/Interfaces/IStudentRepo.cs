using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IStudentRepo
    {
        ICollection<Student> GetStudents();
        Student GetStudentById(string studentId);
        ICollection<Application> GetApplicationsByStudent(string studentId);
        bool StudentExists(string studentId);
    }
}
