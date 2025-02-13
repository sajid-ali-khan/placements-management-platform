using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly DataContext _context;

        public StudentRepo(DataContext context)
        {
            _context = context;
        }
        public ICollection<Application> GetApplicationsByStudent(string studentId)
        {
            return _context.Applications.Where(ap => ap.StudentId == studentId).ToList();
        }

        public Student GetStudentById(string studentId)
        {
           return _context.Students.Where(s => s.Id == studentId).FirstOrDefault();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.ToList();
        }
    }
}
