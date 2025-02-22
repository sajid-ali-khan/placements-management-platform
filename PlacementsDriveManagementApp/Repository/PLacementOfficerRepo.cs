
using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class PlacementOfficerRepo : IPlacementOfficerRepo
    {
        private readonly DataContext _context;
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

        public PlacementOfficer GetPlacementOfficerByUserName(string userName)
        {
            return _context.PlacementOfficers.Where(po => po.UserName == userName).FirstOrDefault();
        }

        public bool PlacementOfficerExistsByUserName(string userName)
        {
            return _context.PlacementOfficers.Any(po => po.UserName.ToLower() == userName.ToLower());
        }
    }
}
