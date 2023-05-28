using Microsoft.EntityFrameworkCore;

namespace RestaurantsMinimalApi.Data
{
    using RestaurantsMinimalApi.Models;
    using System;
    using System.Globalization;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Restaurant>? Restaurants { get; set; }
        public DbSet<MenuItem>? MenuItems { get; set; }
        public DbSet<Order>? Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasKey(r => r.Id);
        }
    }
}