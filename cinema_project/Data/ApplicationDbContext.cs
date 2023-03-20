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

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Place> Places { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Genre>()
                .HasMany<Movie>(genre => genre.Movies)
                .WithOne(movie => movie.Genre)
                .HasForeignKey(movie => movie.GenreId);

            base.OnModelCreating(builder);
            builder.Entity<Movie>()
                .HasMany<Session>(movie => movie.Sessions)
                .WithOne(session => session.Movie)
                .HasForeignKey(session => session.MovieId);

            base.OnModelCreating(builder);
            builder.Entity<Session>()
                .HasMany<Ticket>(session => session.Tickets)
                .WithOne(ticket => ticket.Session)
                .HasForeignKey(ticket => ticket.SessionId);

            base.OnModelCreating(builder);
            builder.Entity<Hall>()
                .HasMany<Place>(hall => hall.Places)
                .WithOne(place => place.Hall)
                .HasForeignKey(place => place.HallId);

            base.OnModelCreating(builder);
            builder.Entity<Hall>()
                .HasMany<Session>(hall => hall.Sessions)
                .WithOne(session => session.Hall)
                .HasForeignKey(session => session.HallId);

            base.OnModelCreating(builder);
            builder.Entity<Place>()
                .HasMany<Ticket>(place => place.Tickets)
                .WithOne(ticket => ticket.Place)
                .HasForeignKey(ticket => ticket.PlaceId)
                ;

        }
    }

}