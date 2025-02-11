using Microsoft.EntityFrameworkCore;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Opening> Openings { get; set; }
        public DbSet<PlacementOfficer> PlacementOfficers { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlacementOfficer>()
                .HasKey(po => po.Id);

            builder.Entity<Resume>()
                .HasKey(r => r.Id);


            //student rules
            builder.Entity<Student>()
                .HasKey(s => s.Id);

            builder.Entity<Student>()
                .Property(s => s.Id)
                .ValueGeneratedNever(); //to prevent from auto generating the id

            builder.Entity<Student>()
                .HasMany(student => student.Applications)
                .WithOne(application => application.Student)
                .HasForeignKey(application => application.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            //company rules
            builder.Entity<Company>()
                .HasKey(c => c.Id);

            builder.Entity<Company>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Entity<Company>()
                .HasMany(company => company.Openings)
                .WithOne(opening => opening.Company)
                .HasForeignKey(opening => opening.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);


            //opening rules
            builder.Entity<Opening>()
                .HasKey(op => op.Id);

            builder.Entity<Opening>()
                .HasMany(opening => opening.Applications)
                .WithOne(application => application.Opening)
                .HasForeignKey(application => application.OpeningId)
                .OnDelete(DeleteBehavior.SetNull);


            //application rules
            builder.Entity<Application>()
                .HasKey(ap => ap.Id);

            builder.Entity<Application>()
                .Property(ap => ap.Status)
                .HasConversion<int>();

        }
    }
}
