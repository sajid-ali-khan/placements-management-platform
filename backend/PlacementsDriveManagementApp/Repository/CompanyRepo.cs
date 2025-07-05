﻿using Microsoft.EntityFrameworkCore;
using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly DataContext _context;
        private readonly IOpeningRepo _openingRepo;

        private bool _disposed = false;

        public CompanyRepo(DataContext context, IOpeningRepo openingRepo)
        {
            _context = context;
            _openingRepo = openingRepo;
        }

        public bool CompanyExists(int companyId)
        {
            return _context.Companies.Any(c => c.Id == companyId);
        }

        public bool CompanyExistsByEmail(string companyEmail)
        {
            return _context.Companies.Any(c => c.Email.ToUpper() == companyEmail.ToUpper());
        }

        public bool CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            return Save();
        }

        public ICollection<Application> GetApplicationByCompanyEmail(string companyEmail)
        {
            var companyId = _context.Companies.Where(com => com.Email == companyEmail)
                .Select(com => com.Id)
                .FirstOrDefault();

            return GetApplicationsByCompany(companyId);
        }

        public ICollection<Application> GetApplicationsByCompany(int companyId)
        {
            var companyOpenings = GetCompanyOpenings(companyId);

            var companyApplications = new List<Application>();

            foreach (var opening in companyOpenings)
            {
                companyApplications.AddRange(_openingRepo.GetApplicationsByOpening(opening.Id));
            }

            return companyApplications;
        }

        public ICollection<Company> GetCompanies()
        {
            return _context.Companies.OrderBy(c => c.Name).OrderBy(c => c.Id).ToList();
        }

        public Company GetCompanyByEmail(string companyEmail)
        {
            return _context.Companies.Where(c => c.Email == companyEmail).FirstOrDefault();
        }

        public Company GetCompanyById(int companyId)
        {
            return _context.Companies.Where(c => c.Id == companyId).FirstOrDefault();
        }

        public ICollection<Opening> GetCompanyOpenings(int companyId)
        {
            return _context.Openings
                .Where(op => op.CompanyId == companyId)
                .Include(op => op.Company)
                .ToList();
            //return _context.Companies.Where(c => c.Id == companyId).Select(c => c.Openings).FirstOrDefault();
        }

        public string GetHashedPassword(string companyEmail)
        {
            return _context.Companies
                .Where(c => c.Email.ToUpper() == companyEmail.ToUpper())
                .Select(c => c.PasswordHash).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
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
