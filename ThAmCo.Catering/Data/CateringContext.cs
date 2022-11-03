using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Catering.Data
{
    public class CateringContext: DbContext
    {
        
        public string DbPath { get; }

        public CateringContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            DbPath = Path.Join(path, "ThAmCo.Catering.db");
        }

        public DbSet<Menu>Menu => Set<Menu>();

        public DbSet<FoodItem> FoodItems => Set<FoodItem>();

        public DbSet<MenuFoodItem> MenuFoodItems => Set<MenuFoodItem>();

        public DbSet<FoodBooking> Foodbookings => Set<FoodBooking>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Data Source = {DbPath}");

        }

        //seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuFoodItem>()
                .HasKey(mf => new { mf.FoodItemId, mf.MenuId }); 
            
            modelBuilder.Entity<MenuFoodItem>()
                .HasOne(mf => mf.Menu)
                .WithMany(mf => mf.FoodItems)
                .HasForeignKey(mf => mf.MenuId);

            modelBuilder.Entity<MenuFoodItem>()
                .HasOne(mf => mf.FoodItem)
                .WithMany(mf => mf.Menus)
                .HasForeignKey(mf => mf.FoodItemId);


        }
    }
}
