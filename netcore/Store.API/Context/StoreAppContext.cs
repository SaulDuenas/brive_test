using Microsoft.EntityFrameworkCore;
using StoreApp.Negocio.Model;

namespace Store.API.Context
{
    public partial class StoreAppContext : DbContext
    {
        string connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StoreApp;Integrated Security=True;";

        public StoreAppContext()
        {

        }

        public StoreAppContext(DbContextOptions<StoreAppContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Existencia> Existencia { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<Tipotransaccion> Tipotransaccion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder

                    .UseSqlServer(connectionstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Existencia>(entity =>
            {
                entity.HasKey(e => new { e.IdSucursal, e.IdProducto })
                    .HasName("PK_INVENTARIO")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("EXISTENCIA");

                entity.Property(e => e.IdSucursal).HasColumnName("IDSUCURSAL");

                entity.Property(e => e.IdProducto).HasColumnName("IDPRODUCTO");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Existencia)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_EXISTENC_REFERENCE_PRODUCTO");

                entity.HasOne(d => d.IdsucursalNavigation)
                    .WithMany(p => p.Existencia)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK_EXISTENC_REFERENCE_SUCURSAL");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.IdSucursal, e.IdProducto })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("MOVIMIENTO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IdSucursal).HasColumnName("IDSUCURSAL");

                entity.Property(e => e.IdProducto).HasColumnName("IDPRODUCTO");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.FechaOperacion)
                    .HasColumnName("FECHAOPERACION")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdTipoTrans).HasColumnName("IDTIPOTRANS");

                entity.Property(e => e.PrecioOperacion)
                    .HasColumnName("PRECIOOPERACION")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Movimiento)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIEN_REFERENCE_PRODUCTO");

                entity.HasOne(d => d.IdsucursalNavigation)
                    .WithMany(p => p.Movimiento)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIEN_REFERENCE_SUCURSAL");

                entity.HasOne(d => d.IdtipotransNavigation)
                    .WithMany(p => p.Movimiento)
                    .HasForeignKey(d => d.IdTipoTrans)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIEN_REFERENCE_TIPOTRAN");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("PRODUCTO");

                entity.HasIndex(e => e.CodigoBarras)
                    .HasName("UQ__PRODUCTO__8FA901D7924B4042")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ__PRODUCTO__B21D0AB91B8B8E5A")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CodigoBarras)
                    .HasColumnName("CODIGOBARRAS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio)
                    .HasColumnName("PRECIO")
                    .HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("SUCURSAL");

                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ__SUCURSAL__B21D0AB9297E5C96")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tipotransaccion>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TIPOTRANSACCION");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }


        //   base.OnModelCreating(modelBuilder);
    }
}
