using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SensorAPI.Database.Models;

namespace SensorAPI.Database;

public partial class SensorDbContext : DbContext
{
    public SensorDbContext()
    {
    }

    public SensorDbContext(DbContextOptions<SensorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataTable> DataTables { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PUTU0Q3\\MSSQLSERVERDM;Database=SensorDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataTable>(entity =>
        {
            entity.HasKey(e => e.DataId).HasName("PK__DataTabl__9D05303D0BD8F0A0");

            entity.ToTable("DataTable");

            entity.Property(e => e.DateReported).HasColumnType("date");
            entity.Property(e => e.SensorValue).HasColumnType("decimal(5, 1)");

            entity.HasOne(d => d.Sensor).WithMany(p => p.DataTables)
                .HasForeignKey(d => d.SensorId)
                .HasConstraintName("FK__DataTable__Senso__36B12243");
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.SensorId).HasName("PK__Sensor__D8099BFA4A062FC5");

            entity.ToTable("Sensor");

            entity.Property(e => e.DateInstalled).HasColumnType("date");
            entity.Property(e => e.SensorName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Sensors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Sensor__UserId__267ABA7A");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserTabl__1788CC4CDF201AE1");

            entity.ToTable("UserTable");

            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
