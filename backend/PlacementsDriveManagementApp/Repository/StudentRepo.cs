﻿using Microsoft.EntityFrameworkCore;
using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly DataContext _context;

        private bool _disposed = false;

        public StudentRepo(DataContext context)
        {
            _context = context;
        }

        public bool CreateStudent(Student student)
        {
            _context.Students.Add(student);
            return Save();
        }

        public ICollection<Application> GetApplicationsByStudentEmail(string studentEmail)
        {
            return _context.Applications
                .Where(a => a.Student.Email.ToUpper() == studentEmail.ToUpper())
                .Include(a => a.Student)
                .Include(a => a.Opening)
                .ThenInclude(o => o.Company)
                .ToList();
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

        public string GetStudentIdByEmail(string email)
        {
            return _context.Students.Where(s => s.Email.ToUpper() == email.ToUpper()).Select(s => s.Id).FirstOrDefault();
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose(); // Dispose of the DB context
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevents finalizer from running
        }
    }
}
