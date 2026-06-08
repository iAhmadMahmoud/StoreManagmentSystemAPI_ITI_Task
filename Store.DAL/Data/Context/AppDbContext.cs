using Microsoft.EntityFrameworkCore;

namespace Store.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public override int SaveChanges()
        {
            AuditLog();
            return base.SaveChanges();
        }

        private void AuditLog()
        {
            var dateTimeNow = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = dateTimeNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = dateTimeNow;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createdDate = new DateTime(2026, 5, 31);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            List<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Computer Peripherals",CreatedAt=createdDate },
                new Category { Id = 2, Name = "Office Furniture & Setup",CreatedAt=createdDate },
                new Category { Id = 3, Name = "Smart Home & Lifestyle",CreatedAt=createdDate }
            };

            List<Product> products = new List<Product>
            {
                // Category 1: Computer Peripherals
                new Product { Id = 1, CategoryId = 1, Title = "Mechanical Keyboard", Description = "Wireless RGB with tactile switches.", Price = 85.00m, Count = 12,CreatedAt=createdDate },
                new Product { Id = 4, CategoryId = 1, Title = "Portable SSD 1TB", Description = "High-speed external storage for developers.", Price = 115.00m, Count = 8,CreatedAt=createdDate },
                new Product { Id = 6, CategoryId = 1, Title = "USB-C Hub Adapter", Description = "7-in-1 connectivity for modern laptops.", Price = 35.00m, Count = 40,CreatedAt=createdDate },
                new Product { Id = 7, CategoryId = 1, Title = "Vertical Mouse", Description = "Reduces wrist strain during long coding sessions.", Price = 29.99m, Count = 25,CreatedAt=createdDate },
                new Product { Id = 8, CategoryId = 1, Title = "Noise Cancelling Headphones", Description = "Active noise cancellation for focused work.", Price = 199.00m, Count = 10,CreatedAt=createdDate },
                new Product { Id = 10, CategoryId = 1, Title = "Webcam 4K Ultra HD", Description = "Wide-angle lens with built-in dual microphones.", Price = 89.90m, Count = 6,CreatedAt=createdDate },

                // Category 2: Office Furniture & Setup
                new Product { Id = 2, CategoryId = 2, Title = "Ergonomic Office Chair", Description = "High-back mesh chair with lumbar support.", Price = 210.50m, Count = 5,CreatedAt=createdDate },
                new Product { Id = 5, CategoryId = 2, Title = "Dual Monitor Stand", Description = "Heavy-duty aluminum arm for two 27-inch screens.", Price = 65.25m, Count = 15,CreatedAt=createdDate },
                new Product { Id = 9, CategoryId = 2, Title = "Desk Pad Protector", Description = "Large felt mat for mouse and keyboard stability.", Price = 18.50m, Count = 50,CreatedAt=createdDate },

                // Category 3: Smart Home & Lifestyle
                new Product { Id = 3, CategoryId = 3, Title = "Smart Indoor Herb Garden", Description = "Self-watering kit with LED grow lights for mint and basil.", Price = 45.99m, Count = 20,CreatedAt=createdDate }
            };
            
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Category>().HasData(categories);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

    }
}
