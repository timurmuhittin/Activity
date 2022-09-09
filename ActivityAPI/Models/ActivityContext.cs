using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ActivityAPI.Models
{
    public partial class ActivityContext : DbContext
    {
        public ActivityContext()
        {
        }

        public ActivityContext(DbContextOptions<ActivityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ActivityDetail> ActivityDetails { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Organizer> Organizers { get; set; } = null!;
        public virtual DbSet<TickedSale> TickedSales { get; set; } = null!;
        public virtual DbSet<TickedType> TickedTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MONSTER\\SQLSERVER; Database=Activity; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Adress)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DateDeadline).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");

                entity.Property(e => e.TickedPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TickedSalesId).HasColumnName("TickedSalesID");

                entity.Property(e => e.TickedTypeId).HasColumnName("TickedTypeID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Category");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_City");

                entity.HasOne(d => d.Organizer)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.OrganizerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Organizer");

                entity.HasOne(d => d.TickedSales)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.TickedSalesId)
                    .HasConstraintName("FK_Activity_TickedSales");

                entity.HasOne(d => d.TickedType)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.TickedTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_TickedType");
            });

            modelBuilder.Entity<ActivityDetail>(entity =>
            {
                entity.ToTable("ActivityDetail");

                entity.Property(e => e.ActivityDetailId).HasColumnName("ActivityDetailID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityDetails)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityDetail_Activity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ActivityDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityDetail_ActivityDetail");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Category1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Category");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.City1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("City");
            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.ToTable("Organizer");

                entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TickedSale>(entity =>
            {
                entity.HasKey(e => e.TickedSalesId);

                entity.Property(e => e.TickedSalesId).HasColumnName("TickedSalesID");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TickedType>(entity =>
            {
                entity.ToTable("TickedType");

                entity.Property(e => e.TickedTypeId).HasColumnName("TickedTypeID");

                entity.Property(e => e.TickedType1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TickedType");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
