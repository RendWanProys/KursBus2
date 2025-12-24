using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KursBus2.Models;

public partial class KursProjectContext : DbContext
{
    public KursProjectContext()
    {
        Database.EnsureCreated();
    }

    public KursProjectContext(DbContextOptions<KursProjectContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<UserData> UserDates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TeacherPC;Initial Catalog=KursProject;User ID=user4;Password=user4;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.RaceId).HasName("RaceFly_PK");

            entity.ToTable("Race");

            entity.Property(e => e.RaceId).ValueGeneratedOnAdd();
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("Schedule_PK");

            entity.ToTable("Schedule");

            entity.Property(e => e.TripId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TimeArrival).HasPrecision(0);
            entity.Property(e => e.TimeDepar).HasPrecision(0);
        });

        modelBuilder.Entity<UserData>(entity =>
        {
            entity.ToTable("UserData");
            entity.HasKey(e => e.UserId).HasName("UserData_PK");

            entity.HasIndex(e => e.Email, "3_Email_IDX");

            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PassWord).HasColumnType("varchar(max)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
