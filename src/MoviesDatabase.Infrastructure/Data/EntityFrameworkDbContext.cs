﻿using System;
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

            userEntity.HasMany(u => u.RatedMovies);
        }

        private void MapMovies(ModelBuilder modelBuilder)
        {
            var movieEntity = modelBuilder.Entity<Movie>();

            const string genresSeparator = ", ";
            var genresConverter = new ValueConverter<ICollection<Genre>, string>(
                gns => string.Join(genresSeparator, gns.Select(g => g.ToString())),
                gns => gns.Split(genresSeparator, StringSplitOptions.RemoveEmptyEntries).Select(g => Enum.Parse<Genre>(g)).ToList()
            );

            movieEntity.Property(m => m.Genres)
                .HasConversion(genresConverter);

            movieEntity.Property(m => m.ReleaseDate)
                .HasConversion(new DateTimeToStringConverter());
        }
    }
}
