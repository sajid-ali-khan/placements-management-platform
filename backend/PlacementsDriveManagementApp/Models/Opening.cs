﻿using System.ComponentModel.DataAnnotations;

namespace PlacementsDriveManagementApp.Models
{
    public class Opening
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        [Required]
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastDate { get; set; }
        public bool IsActive { get; set; }

        public Company  Company { get; set; }
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
