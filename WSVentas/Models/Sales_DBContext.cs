using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WSVentas.Models
{
    public partial class Sales_DBContext : DbContext
    {
        public Sales_DBContext()
        {
        }

        public Sales_DBContext(DbContextOptions<Sales_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Concept> Concepts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Sales_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ci");

                entity.Property(e => e.City)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("number");
            });

            modelBuilder.Entity<Concept>(entity =>
            {
                entity.ToTable("concept");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.IdSale).HasColumnName("id_sale");

                entity.Property(e => e.Imports)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("imports");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_concept_product");

                entity.HasOne(d => d.IdSaleNavigation)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.IdSale)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_concept_sale");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("unit_price");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("sale");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sale_client");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
