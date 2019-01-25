using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Infrastructure.Data
{
    public sealed class EntityFrameworkDbContext : DbContext
    {
        public EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.MapUsers(modelBuilder);
            this.MapMovies(modelBuilder);

            modelBuilder.Entity<Rating>()
                .HasKey(r => new { r.UserId, r.MovieId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        private void MapUsers(ModelBuilder modelBuilder)
        {
            var userEntity = modelBuilder.Entity<User>();

            userEntity
                .HasIndex(u => u.Username)
                .IsUnique();

            userEntity.HasMany(u => u.Ratings);
        }

        private void MapMovies(ModelBuilder modelBuilder)
        {
            var movieEntity = modelBuilder.Entity<Movie>();

            const string GenreSeparator = ", ";
            var genresConverter = new ValueConverter<ICollection<Genre>, string>(
                gns => string.Join(GenreSeparator, gns.Select(g => g.ToString())),
                gns => gns.Split(GenreSeparator, StringSplitOptions.RemoveEmptyEntries).Select(g => Enum.Parse<Genre>(g)).ToList()
            );

            movieEntity.Property(m => m.Title)
                .IsRequired();

            movieEntity.Property(m => m.Genres)
                .HasConversion(genresConverter);

            movieEntity.Property(m => m.ReleaseDate)
                .HasConversion(new DateTimeToStringConverter());

            movieEntity.HasMany(m => m.Ratings);

            movieEntity.Ignore(m => m.YearOfRelease);
        }
    }
}
