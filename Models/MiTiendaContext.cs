using Microsoft.EntityFrameworkCore;

namespace TiendaWebAPI.Models;

public partial class MiTiendaContext : DbContext
{
    public MiTiendaContext()
    {
    }

    public MiTiendaContext(DbContextOptions<MiTiendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Dispositivo> Dispositivos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=PC_CELIA_DAVID;Initial Catalog=MiTienda;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Categoria>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07BB9EC503");

            _ = entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        _ = modelBuilder.Entity<Dispositivo>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PK__Disposit__3214EC078C665226");

            _ = entity.Property(e => e.Nombre).HasMaxLength(150);
            _ = entity.Property(e => e.Precio).HasColumnType("decimal(9, 2)");

            _ = entity.HasOne(d => d.Marca).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.MarcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Marcas_Dispositivos");
        });

        _ = modelBuilder.Entity<Marca>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PK__Marcas__3214EC07048F16E4");

            _ = entity.Property(e => e.Nombre).HasMaxLength(100);

            _ = entity.HasOne(d => d.Categoria).WithMany(p => p.Marcas)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categorias_Marcas");
        });

        _ = modelBuilder.Entity<Usuario>(entity =>
        {
            _ = entity.HasKey(e => e.Email).HasName("PK__tmp_ms_x__A9D105354BBE2114");

            _ = entity.Property(e => e.Email).HasMaxLength(100);
            _ = entity.Property(e => e.Password).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
