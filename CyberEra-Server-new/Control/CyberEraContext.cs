using System;
using System.Collections.Generic;
using CyberEra_Server.Control;
using CyberEra_Server_new.Model.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CyberEra_Server_new.Control
{
    public partial class CyberEraContext : DbContext
    {
        public CyberEraContext()
        {
        }

        public CyberEraContext(DbContextOptions<CyberEraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BuyedFood> BuyedFoods { get; set; } = null!;
        public virtual DbSet<Computer> Computers { get; set; } = null!;
        public virtual DbSet<ComputerApp> ComputerApps { get; set; } = null!;
        public virtual DbSet<ComputersIp> ComputersIps { get; set; } = null!;
        public virtual DbSet<ComputersLastActivity> ComputersLastActivities { get; set; } = null!;
        public virtual DbSet<ComputersPassword> ComputersPasswords { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<ShopItem> ShopItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Zone> Zones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(SettingsController.GetInstance().GetSettings().DBConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyedFood>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BuyedFood");

                entity.Property(e => e.TimeCreation).HasColumnType("datetime");

                entity.Property(e => e.TimeForPlaying).HasColumnType("datetime");

                entity.HasOne(d => d.Computer)
                    .WithMany()
                    .HasForeignKey(d => d.ComputerId)
                    .HasConstraintName("FK_Таблица1_Computers");

                entity.HasOne(d => d.ShopItem)
                    .WithMany()
                    .HasForeignKey(d => d.ShopItemId)
                    .HasConstraintName("FK_Таблица1_ShopItems");
            });

            modelBuilder.Entity<Computer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK_Computers_Zones");
            });

            modelBuilder.Entity<ComputerApp>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AppName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Computer)
                    .WithMany()
                    .HasForeignKey(d => d.ComputerId)
                    .HasConstraintName("FK_ComputerApps_Computers");
            });

            modelBuilder.Entity<ComputersIp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ComputersIp");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Computer)
                    .WithMany()
                    .HasForeignKey(d => d.ComputerId)
                    .HasConstraintName("FK_ComputersIp_Computers");
            });

            modelBuilder.Entity<ComputersLastActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ComputersLastActivity");

                entity.Property(e => e.TimeActivity).HasColumnType("datetime");

                entity.HasOne(d => d.Computer)
                    .WithMany()
                    .HasForeignKey(d => d.ComputerId)
                    .HasConstraintName("FK_Table_1_Computers");
            });

            modelBuilder.Entity<ComputersPassword>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.TimeCreation).HasColumnType("datetime");

                entity.Property(e => e.TimeForPlaying).HasColumnType("datetime");

                entity.HasOne(d => d.Computer)
                    .WithMany()
                    .HasForeignKey(d => d.ComputerId)
                    .HasConstraintName("FK_ComputersPasswords_Computers");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ComputersPasswords_Users");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LogContext).HasColumnType("ntext");

                entity.Property(e => e.TimeCreation).HasColumnType("datetime");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Review");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ShopItem>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ImageSrc).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
