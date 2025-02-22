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

        public bool CreateStudent(Student student)
        {
            _context.Students.Add(student);
            return Save();
        }

        public ICollection<Application> GetApplicationsByStudent(string studentId)
        {
            return _context.Students.Where(s => s.Id.ToUpper() == studentId.ToUpper()).Select(s => s.Applications).FirstOrDefault();
        }

        public string GetHashedPassword(string email)
        {
            return _context.Students
                .Where(s => s.Email.ToLower() == email.ToLower())
                .Select(s => s.PassWord)
                .FirstOrDefault();
        }

        public Student GetStudentByEmail(string email)
        {
            return _context.Students.Where(s => s.Email.ToUpper() == email.ToUpper()).FirstOrDefault();
        }

        public Student GetStudentById(string studentId)
        {
           return _context.Students.Where(s => s.Id == studentId).FirstOrDefault();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool StudentExists(string studentId)
        {
            return _context.Students.Any(s => s.Id == studentId);
        }

        public bool StudentExistsByEmail(string email)
        {
            return _context.Students.Any(s => s.Email.ToLower() == email.ToLower());
        }

        public bool UpdateStudent(Student student)
        {
            _context.Update(student);
            return Save();
        }
    }
}
