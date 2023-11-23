using Hotel_Booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Customer_Room> Customer_Rooms { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<HotelBranch> HotelBranches { get; set; }
        public virtual DbSet<Customer_Payment> Customer_Payments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
 
            modelBuilder.Entity<Customer_Room>()
            .HasKey(cr => new { cr.CustomerID, cr.HotelBranchId, cr.RoomId });


            modelBuilder.Entity<Customer_Payment>()
            .HasKey(cp => new { cp.PaymentId, cp.CustomerID});



            modelBuilder.Entity<Customer_Payment>()
               .HasOne(c => c.AppUser)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Customer_Payment>()
                .HasOne(c => c.Payment)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Customer_Room>()
                .HasOne(c => c.AppUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Customer_Room>()
                .HasOne(c => c.HotelBranch)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Customer_Room>()
                .HasOne(c => c.Room)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
