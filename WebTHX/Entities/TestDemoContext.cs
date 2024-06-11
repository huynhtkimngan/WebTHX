using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebTHX.Entities;

public partial class TestDemoContext : DbContext
{
    public TestDemoContext()
    {
    }

    public TestDemoContext(DbContextOptions<TestDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Huyen> Huyens { get; set; }

    public virtual DbSet<Tinh> Tinhs { get; set; }

    public virtual DbSet<Xa> Xas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-8HR2JIR2\\SQLEXPRESS;Database=TestDemo;User Id=sa; Password=12345; Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Huyen>(entity =>
        {
            entity.HasKey(e => e.MaH).HasName("PK__Huyen__C7977BB0A8A178B1");

            entity.ToTable("Huyen");

            entity.Property(e => e.MaH).ValueGeneratedNever();
            entity.Property(e => e.Cap).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(255);

            entity.HasOne(d => d.MaTNavigation).WithMany(p => p.Huyens)
                .HasForeignKey(d => d.MaT)
                .HasConstraintName("FK__Huyen__MaT__267ABA7A");
        });

        modelBuilder.Entity<Tinh>(entity =>
        {
            entity.HasKey(e => e.MaT).HasName("PK__Tinh__C7977BA4C3F80EC4");

            entity.ToTable("Tinh");

            entity.Property(e => e.MaT).ValueGeneratedNever();
            entity.Property(e => e.Cap).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(255);
        });

        modelBuilder.Entity<Xa>(entity =>
        {
            entity.HasKey(e => e.MaX).HasName("PK__Xa__C7977BA039994338");

            entity.ToTable("Xa");

            entity.Property(e => e.MaX).ValueGeneratedNever();
            entity.Property(e => e.Cap).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(255);

            entity.HasOne(d => d.MaHNavigation).WithMany(p => p.Xas)
                .HasForeignKey(d => d.MaH)
                .HasConstraintName("FK__Xa__MaH__29572725");

            entity.HasOne(d => d.MaTNavigation).WithMany(p => p.Xas)
                .HasForeignKey(d => d.MaT)
                .HasConstraintName("FK__Xa__MaT__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
