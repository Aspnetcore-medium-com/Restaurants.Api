using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Persistance
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {

        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne(d => d.Restaurant)
                .HasForeignKey(d => d.RestaurantId);
            if (Database.IsRelational())
            {
                modelBuilder.Entity<Dish>()
                    .Property(d => d.Price)
                    .HasPrecision(18, 2);
            }

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.OwnedRestaurants)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId);

        }


    }
}
