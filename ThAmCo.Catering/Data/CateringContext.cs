using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Controllers;

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

            //modelBuilder.Entity<FoodItem>()
            //    .Property(fc => fc.Category)
            //    .HasConversion<string>()
            //    .HasMaxLength(50);


           modelBuilder.Entity<FoodItem>().HasData(
                                                    new FoodItem (1, "chips",  "lovely chips", 250, true, new DateTime(2015, 12, 25))

                                                    ,new FoodItem(2, "sosig",  "just a sosig", 300, false, new DateTime(2015, 12, 25))
                                                    
                                                   );

            modelBuilder.Entity<Menu>().HasData(new Menu(1, "Breakfast Menu", new DateTime(2015, 12, 25)),
                                                new Menu(2, "Brunch Menu", new DateTime(2015, 12, 25))
                                                
                                                );

            modelBuilder.Entity<MenuFoodItem>().HasData(new MenuFoodItem(1, 2),
                                                        new MenuFoodItem(1, 1));
 

        }
    }
}
