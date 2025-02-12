using PlacementsDriveManagementApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementsDriveManagementApp.Dto
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int? OpeningId { get; set; }
        public int ResumeId { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime? InterviewSlot { get; set; }
        public bool? StudentAppeared { get; set; }
        public decimal? Package { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string PlaceOfWork { get; set; }
    }
}
