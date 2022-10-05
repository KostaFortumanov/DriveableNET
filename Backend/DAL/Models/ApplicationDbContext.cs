using DAL.Models.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ReportPost> ReportPosts { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<PostCarType> PostCarTypes { get; set; }
        public DbSet<Images> images { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RepositoryExercise;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Car>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<AppUser>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<CarType>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ReportPost>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Images>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Reason>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<RefreshToken>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.Entity<PostCarType>()
                .HasKey(z => new { z.CarTypeId, z.PostId });
            builder.Entity<CarType>().HasData(
                new CarType { Type="CAR", Id=Guid.NewGuid()},
                new CarType { Type = "SEDAN", Id = Guid.NewGuid()},
                new CarType { Type = "SPORTS CAR", Id = Guid.NewGuid() },
                new CarType { Type = "HATCHBACK", Id = Guid.NewGuid() },
                new CarType { Type = "CONVERTIBLE", Id = Guid.NewGuid() },
                new CarType { Type = "SPORT-UTILITY VEHICLE (SUV)", Id = Guid.NewGuid() },
                new CarType { Type = "MINIVAN", Id = Guid.NewGuid() },
                new CarType { Type = "PICKUP TRUCK", Id = Guid.NewGuid() },
                new CarType { Type = "TRUCK", Id = Guid.NewGuid() },
                new CarType { Type = "MOTORCYCLE", Id = Guid.NewGuid() },
                new CarType { Type = "STANDARD MOTORCYCLE", Id = Guid.NewGuid() },
                new CarType { Type = "CRUISER MOTORCYCLE", Id = Guid.NewGuid() },
                new CarType { Type = "SPORT MOTORCYCLE", Id = Guid.NewGuid() }
                );
        }
    }
}