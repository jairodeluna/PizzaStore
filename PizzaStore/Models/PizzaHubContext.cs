using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models;

public partial class PizzaHubContext : DbContext
{
    public PizzaHubContext()
    {
    }

    public PizzaHubContext(DbContextOptions<PizzaHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaType> PizzaTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-43ASOFV\\SQLEXPRESS;Database=PizzaHub;User Id=sa;Password=12345678;TrustServerCertificate=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Time)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("time");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId);

            entity.Property(e => e.OrderDetailsId).ValueGeneratedNever();
            entity.Property(e => e.PizzaId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.PizzaId).HasName("PK__Pizza__0B6012DD71A661DD");

            entity.ToTable("Pizza");

            entity.Property(e => e.PizzaId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PizzaType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PizzaType>(entity =>
        {
            entity.HasKey(e => e.PizzaType1).HasName("PK_Table_1");

            entity.Property(e => e.PizzaType1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PizzaType");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ingredients).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
