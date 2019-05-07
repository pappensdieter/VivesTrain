using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VivesTrein.Domain.Entities
{
    public partial class vivestrainContext : DbContext
    {
        public vivestrainContext()
        {
        }

        public vivestrainContext(DbContextOptions<vivestrainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Boeking> Boeking { get; set; }
        public virtual DbSet<Reis> Reis { get; set; }
        public virtual DbSet<Stad> Stad { get; set; }
        public virtual DbSet<Treinrit> Treinrit { get; set; }
        public virtual DbSet<TreinritReis> TreinritReis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQL_VIVES; Database=vivestrain;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Boeking>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ReisId).HasColumnName("reis_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Reis)
                    .WithMany(p => p.Boeking)
                    .HasForeignKey(d => d.ReisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Boeking_Reis");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Boeking)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Boeking_AspNetUsers");
            });

            modelBuilder.Entity<Reis>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BestemmingsstadId).HasColumnName("bestemmingsstad_id");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasColumnName("naam")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Prijs).HasColumnName("prijs");

                entity.Property(e => e.VertrekstadId).HasColumnName("vertrekstad_id");

                entity.HasOne(d => d.Bestemmingsstad)
                    .WithMany(p => p.ReisBestemmingsstad)
                    .HasForeignKey(d => d.BestemmingsstadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reis__bestemming__412EB0B6");

                entity.HasOne(d => d.Vertrekstad)
                    .WithMany(p => p.ReisVertrekstad)
                    .HasForeignKey(d => d.VertrekstadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reis__vertreksta__4222D4EF");
            });

            modelBuilder.Entity<Stad>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasColumnName("naam")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Treinrit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aankomst)
                    .HasColumnName("aankomst")
                    .HasColumnType("datetime");

                entity.Property(e => e.AtlZitplaatsen).HasColumnName("atlZitplaatsen");

                entity.Property(e => e.BestemmingsstadId).HasColumnName("bestemmingsstad_id");

                entity.Property(e => e.Prijs).HasColumnName("prijs");

                entity.Property(e => e.Vertrek)
                    .HasColumnName("vertrek")
                    .HasColumnType("datetime");

                entity.Property(e => e.VertrekstadId).HasColumnName("vertrekstad_id");

                entity.HasOne(d => d.Bestemmingsstad)
                    .WithMany(p => p.TreinritBestemmingsstad)
                    .HasForeignKey(d => d.BestemmingsstadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Treinrit__bestem__403A8C7D");

                entity.HasOne(d => d.Vertrekstad)
                    .WithMany(p => p.TreinritVertrekstad)
                    .HasForeignKey(d => d.VertrekstadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Treinrit__vertre__3F466844");
            });

            modelBuilder.Entity<TreinritReis>(entity =>
            {
                entity.ToTable("Treinrit_Reis");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Klasse).HasColumnName("klasse");

                entity.Property(e => e.Plaats).HasColumnName("plaats");

                entity.Property(e => e.ReisId).HasColumnName("reis_id");

                entity.Property(e => e.TreinritId).HasColumnName("treinrit_id");

                entity.HasOne(d => d.Reis)
                    .WithMany(p => p.TreinritReis)
                    .HasForeignKey(d => d.ReisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Treinrit_Reis_Reis");

                entity.HasOne(d => d.Treinrit)
                    .WithMany(p => p.TreinritReis)
                    .HasForeignKey(d => d.TreinritId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Treinrit___trein__4316F928");
            });
        }
    }
}
