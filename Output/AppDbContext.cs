using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 🔹 DbSets
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Apply configurations per entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");


                // Column: Id
                entity.Property(e => e.Id)
                    .HasColumnName("Id");

                // Column: Code
                entity.Property(e => e.Code)
                    .HasColumnName("Code")
                    .HasMaxLength(100)
                    .IsRequired();

                // Column: Name
                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .IsRequired();

                // Column: Description
                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(100);

                // Column: IsActive
                entity.Property(e => e.IsActive)
                    .HasColumnName("IsActive");


                // 🔹 Primary Key












                // 🔹 Identity column











            });
        }
    }
}