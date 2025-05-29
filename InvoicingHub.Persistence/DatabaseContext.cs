using Microsoft.EntityFrameworkCore;
using InvoicingHub.Domain.Entities;

namespace InvoicingHub.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #region DbSet Properties
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        #endregion

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        #region Entities configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.ToTable("CatTipoCliente");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Type).HasColumnName("TipoCliente");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("TblClientes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.BusinessName).HasColumnName("RazonSocial");
                entity.Property(e => e.CustomerTypeId).HasColumnName("IdTipoCliente");
                entity.Property(e => e.CreatedDate).HasColumnName("FechaCreacion");
                entity.Property(e => e.RFC).HasColumnName("RFC");

                entity.HasOne(e => e.CustomerType)
                      .WithMany(c => c.Customers)
                      .HasForeignKey(e => e.CustomerTypeId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("CatProductos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name).HasColumnName("NombreProducto");
                entity.Property(e => e.Image).HasColumnName("ImagenProducto");
                entity.Property(e => e.UnitPrice).HasColumnName("PrecioUnitario");
                entity.Property(e => e.Extension).HasColumnName("ext");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("TblFacturas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.IssuedDate).HasColumnName("FechaEmisionFactura");
                entity.Property(e => e.CustomerId).HasColumnName("IdCliente");
                entity.Property(e => e.InvoiceNumber).HasColumnName("NumeroFactura");
                entity.Property(e => e.TotalItems).HasColumnName("NumeroTotalArticulos");
                entity.Property(e => e.Subtotal).HasColumnName("SubTotalFacturas");
                entity.Property(e => e.Taxes).HasColumnName("TotalImpuestos");
                entity.Property(e => e.Total).HasColumnName("TotalFactura");

                entity.HasOne(e => e.Customer)
                      .WithMany(c => c.Invoices)
                      .HasForeignKey(e => e.CustomerId);
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("TblDetallesFactura");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.InvoiceId).HasColumnName("IdFactura");
                entity.Property(e => e.ProductId).HasColumnName("IdProducto");
                entity.Property(e => e.Quantity).HasColumnName("CantidadDeProducto");
                entity.Property(e => e.UnitPrice).HasColumnName("PrecioUnitarioProducto");
                entity.Property(e => e.Subtotal).HasColumnName("SubtotalProducto");
                entity.Property(e => e.Notes).HasColumnName("Notas");

                entity.HasOne(e => e.Invoice)
                      .WithMany(i => i.Details)
                      .HasForeignKey(e => e.InvoiceId);

                entity.HasOne(e => e.Product)
                      .WithMany(p => p.InvoiceDetails)
                      .HasForeignKey(e => e.ProductId);
            });
        }
        #endregion
    }
}