using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cinema_project.Models;
using System.Reflection.Emit;

namespace cinema_project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Session> Sessions { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Place> Places { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .HasKey(t => t.Id);

            builder.Entity<Ticket>()
                .Property(t => t.UserName)
                .IsRequired();

            builder.Entity<Movie>()
                .HasMany<Session>(movie => movie.Sessions)
                .WithOne(session => session.Movie)
                .HasForeignKey(session => session.MovieId);

            builder.Entity<Genre>()
                .HasMany<Movie>(genre => genre.Movies)
                .WithOne(movie => movie.Genre)
                .HasForeignKey(movie => movie.GenreId);

            builder.Entity<Ticket>()
                .HasOne(t => t.Session)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Hall>()
                .HasMany<Place>(hall => hall.Places)
                .WithOne(place => place.Hall)
                .HasForeignKey(place => place.HallId);

            builder.Entity<Ticket>()
                .HasOne(t => t.Place)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Session>()
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Session)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Place>()
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Place)
                .HasForeignKey(t => t.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}