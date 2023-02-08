using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cinema_project.Models;

namespace cinema_project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Session> Sessions { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}