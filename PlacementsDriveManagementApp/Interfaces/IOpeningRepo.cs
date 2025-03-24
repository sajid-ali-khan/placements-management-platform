using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IOpeningRepo
    {
        ICollection<Opening> GetOpenings();
        Opening GetOpeningById(int openingId);
        Company GetCompanyByOpening(int openingId);
        ICollection<Application> GetApplicationsByOpening(int openingId);
        bool OpeningExists(int openingId);
        bool CreateOpening(Opening opening);
        bool UpdateOpening(Opening opening);
        bool Save();

        void Dispose();
    }
}
