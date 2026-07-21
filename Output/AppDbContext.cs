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
        public DbSet<Category> s { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Apply configurations per entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");


                // Column: Id
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                // Column: Code
                entity.Property(e => e.Code)
                    .HasColumnName("Code")
                    .HasMaxLength(50)
                    .IsRequired();

                // Column: Name
                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .IsRequired();

                // Column: Description
                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(500);

                // Column: IsActive
                entity.Property(e => e.IsActive)
                    .HasColumnName("IsActive")
                    .IsRequired();

                // Column: CreatedBy
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CreatedBy")
                    .HasMaxLength(100)
                    .IsRequired();

                // Column: CreatedDate
                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CreatedDate");

                // Column: UpdatedBy
                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("UpdatedBy")
                    .HasMaxLength(100)
                    .IsRequired();

                // Column: UpdatedDate
                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UpdatedDate");


                // 🔹 Primary Key




















                // 🔹 Identity column


                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();


















            });
        }
    }
}