using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IStudentRepo
    {
        ICollection<Student> GetStudents();
        Student GetStudentById(string studentId);
        Student GetStudentByEmail(string email);
        ICollection<Application> GetApplicationsByStudent(string studentId);
        bool StudentExists(string studentId);
        bool StudentExistsByEmail(string Email);
        bool CreateStudent(Student student);

        bool UpdateStudent(Student student);
        bool Save();
        string GetHashedPassword(string Email);
    }
}
