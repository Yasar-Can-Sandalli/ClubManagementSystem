using Microsoft.EntityFrameworkCore;
using ClubManagementSystem.Models;

namespace ClubManagementSystem.DataAccess
{
    public class ClubDbContext : DbContext
    {
        public DbSet<ClubMember> Members { get; set; }
        public DbSet<ClubEvent> Events { get; set; }
        public DbSet<ClubProject> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=YASARCAN;Database=RotomDxClub;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}