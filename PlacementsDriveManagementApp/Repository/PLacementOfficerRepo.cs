
using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class PlacementOfficerRepo : IPlacementOfficerRepo
    {
        private readonly DataContext _context;

        private bool _disposed = false;
        public PlacementOfficerRepo(DataContext dataContext)
        {
            _context = dataContext;
        }

        public ICollection<PlacementOfficer> GetPlacementOfficers()
        {
            return _context.PlacementOfficers.ToList();
        }

        public bool CreatePlacementOfficer(PlacementOfficer placementOfficer)
        {
            _context.PlacementOfficers.Add(placementOfficer);
            return Save();
        }

        public PlacementOfficer GetPlacementOfficerById(int placementOfficerId)
        {
            return _context.PlacementOfficers.Where(po => po.Id == placementOfficerId).FirstOrDefault();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool PlacementOfficerExists(int placementOfficerId)
        {
            return _context.PlacementOfficers.Any(po => po.Id == placementOfficerId);
        }

        public PlacementOfficer GetPlacementOfficerByEmail(string email)
        {
            return _context.PlacementOfficers
                .Where(po => po.Email.ToUpper() == email.ToUpper())
                .FirstOrDefault();
        }

        public bool PlacementOfficerExistsByEmail(string email)
        {
            return _context.PlacementOfficers.Any(po => po.Email.ToLower() == email.ToLower());
        }

        public string GetHashedPassword(string email)
        {
            var hashedPassword = _context.PlacementOfficers
                .Where(po => po.Email.ToLower() == email.ToLower())
                .Select(po => po.PasswordHash)
                .FirstOrDefault();

            Console.WriteLine($"Retrieved Hashed Password: {hashedPassword}");
            return hashedPassword;
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
