using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;

namespace DAL.Context
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<RestaurantTableBooking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(UserConstants.dbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantTableBooking>().Property(e => e.OccassionType).HasConversion<int>();

            modelBuilder.Entity<RestaurantTableBooking>().Property(e => e.Status).HasConversion<int>();

            modelBuilder.Entity<RestaurantTableBooking>().Property(e => e.BookingTime).HasConversion<int>();

            modelBuilder.Entity<RestaurantTableBooking>().Property(e => e.PaymentMode).HasConversion<int>();

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    Email = "user1@gmail.com",
                    Password = "password1",
                },
                new Users
                {
                    Id = 2,
                    Email = "user2@gmail.com",
                    Password = "password2",
                },
                new Users
                {
                    Id = 3,
                    Email = "user3@gmail.com",
                    Password = "password3",
                },
                new Users
                {
                    Id = 4,
                    Email = "user4@gmail.com",
                    Password = "password4",
                },
                new Users
                {
                    Id = 5,
                    Email = "user5@gmail.com",
                    Password = "password5",
                });
        }
    }
}
