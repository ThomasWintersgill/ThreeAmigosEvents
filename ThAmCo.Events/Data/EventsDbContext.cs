#nullable disable

using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;

namespace ThAmCo.Events.Data
{
    public class EventsDbContext: DbContext
    {
        public string DbPath { get; }

        public EventsDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            DbPath = Path.Join(path, "ThAmCo.Events.db");
        }

        public DbSet<Event> Events => Set<Event>();

        public DbSet<Guest> Guests => Set<Guest>();

        public DbSet<GuestBooking> GuestBookings => Set<GuestBooking>();

        public DbSet<Staff> Staff => Set<Staff>();

        public DbSet<Staffing> staffing => Set<Staffing>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Data Source = {DbPath}");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuestBooking>()
                .HasKey(gb => new {gb.EventID, gb.GuestID});

            modelBuilder.Entity<GuestBooking>()
                .HasOne(e => e.Event)
                .WithMany(g => g.Guests)
                .HasForeignKey(e => e.EventID);

            modelBuilder.Entity<GuestBooking>()
                .HasOne(g => g.Guest)
                .WithMany(e => e.Events)
                .HasForeignKey(g => g.GuestID);

            modelBuilder.Entity<Staffing>()
                .HasKey(st => new { st.StaffId, st.EventId });

            modelBuilder.Entity<Staffing>()
                .HasOne(s => s.Staff)
                .WithMany(e => e.Events)
                .HasForeignKey(s => s.StaffId);

            modelBuilder.Entity<Staffing>()
                .HasOne(e => e.Event)
                .WithMany(s => s.Staff)
                .HasForeignKey(e => e.EventId);

        }
    }
}
