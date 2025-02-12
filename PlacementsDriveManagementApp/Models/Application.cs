using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementsDriveManagementApp.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int? OpeningId { get; set; }
        public int ResumeId { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime? InterviewSlot { get; set; }
        public bool? StudentAppeared { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Package { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? PlaceOfWork { get; set; }


        public Student Student { get; set; }
        public Opening Opening { get; set; }
        public Resume Resume { get; set; }


    }

    public enum ApplicationStatus
    {
        Pending = 1,
        InterviewScheduled = 2,
        Selected = 3,
        Rejected = 4
    }
}
