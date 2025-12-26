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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlite("Data Source=KursProject.db"); }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=TeacherPC;Initial Catalog=KursProject;User ID=user4;Password=user4;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.RaceId).HasName("RaceFly_PK");
            entity.ToTable("Race");
            entity.Property(e => e.RaceId).ValueGeneratedOnAdd();

            entity.Property(e => e.RaceNumber)
                .HasMaxLength(20);

            entity.Property(e => e.Circulation)
                .HasMaxLength(30);


           // entity.HasOne(d => d.Schedule)           
                //.WithMany(p => p.Races)              
                //.HasForeignKey(d => d.TripId)        
                //.HasConstraintName("FK_Race_Schedule")  
                //.OnDelete(DeleteBehavior.SetNull);   
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("Schedule_PK");
            entity.ToTable("Schedule");
            entity.Property(e => e.TripId).ValueGeneratedOnAdd();

            entity.Property(e => e.PeakLoad)
                .HasMaxLength(10);

            entity.HasIndex(e => e.TripNum, "IX_Schedule_TripNum")
                .IsUnique();
        });

        modelBuilder.Entity<UserData>(entity =>
        {
            entity.ToTable("UserData");
            entity.HasKey(e => e.UserId).HasName("UserData_PK");

            entity.HasIndex(e => e.Email, "3_Email_IDX");

            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            entity.Property(e => e.Email)
                .HasMaxLength(50);

            //entity.Property(e => e.PassWord).HasColumnType("varchar(max)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
