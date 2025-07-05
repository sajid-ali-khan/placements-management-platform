﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.DTOs;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private bool _disposed = false;

        public ApplicationRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool ApplicationExists(int applicationId)
        {
            return _context.Applications.Any(ap => ap.Id == applicationId);
        }

        public bool CreateApplication(Application application)
        {
            _context.Applications.Add(application);
            return Save();
        }

        public Application GetApplicationById(int applicationId)
        {
            return _context.Applications
                .Where(ap => ap.Id == applicationId)
                .Include(ap => ap.Student)
                .Include(a => a.Resume)
                .Include(ap => ap.Opening)
                .ThenInclude(o => o.Company)
                .FirstOrDefault();
        }

        public Opening GetApplicationOpening(int applicationId)
        {
            return _context.Applications.Where(ap => ap.Id == applicationId).Select(ap => ap.Opening).FirstOrDefault();
        }

        public Resume GetApplicationResume(int applicationId)
        {
            return _context.Applications.Where(ap => ap.Id == applicationId).Select(ap => ap.Resume).FirstOrDefault();
        }

        public ICollection<Application> GetApplications()
        {
            return _context.Applications
                .Include(a => a.Student)
                .Include(a => a.Resume)
                .Include(a => a.Opening)
                .ThenInclude(o => o.Company)
                .ToList();
        }


        public Student GetStudentByApplication(int applicationId)
        {
            return _context.Applications
                .Where(ap => ap.Id == applicationId)
                .Select(ap => ap.Student)
                .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateApplication(Application application)
        {
            _context.Update(application);
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
