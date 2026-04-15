using Microsoft.EntityFrameworkCore;

namespace <<Your Namespace>>.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 🔹 DbSets

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Apply configurations per entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                // Column: Id
                                entity.Property(e => e.Id)
                                    .HasColumnName("Id")
                                    ;
                                
                                // Column: Name
                                entity.Property(e => e.Name)
                                    .HasColumnName("Name")
                                    .HasMaxLength(100)
                                        .IsRequired()
                    ;
                                
                                // Column: Price
                                entity.Property(e => e.Price)
                                    .HasColumnName("Price")
                                    ;
                                
                                // 🔹 Primary Key
                entity.HasKey(e => e.Id);
                // 🔹 Identity column
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                });

        }
    }
}