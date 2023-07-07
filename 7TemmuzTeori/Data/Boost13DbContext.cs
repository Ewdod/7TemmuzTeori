using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _7TemmuzTeori.Data
{
    public partial class Boost13DbContext : DbContext
    {
        public Boost13DbContext()
        {
        }

        public Boost13DbContext(DbContextOptions<Boost13DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bolge> Bolgeler { get; set; } = null!;
        public virtual DbSet<IletisimBilgi> IletisimBilgileri { get; set; } = null!;
        public virtual DbSet<Ogrenci> Ogrenciler { get; set; } = null!;
        public virtual DbSet<Sehir> Sehirler { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\EWDOD;Initial Catalog=Boost13Db;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bolge>(entity =>
            {
                entity.ToTable("Bolgeler");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BolgeAd).HasMaxLength(50);
            });

            modelBuilder.Entity<IletisimBilgi>(entity =>
            {
                entity.ToTable("IletisimBilgileri");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Adres).HasMaxLength(400);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Telefon).HasMaxLength(15);

                entity.HasOne(d => d.Ogrenci)
                    .WithOne(p => p.IletisimBilgileri)
                    .HasForeignKey<IletisimBilgi>(d => d.Id)
                    .HasConstraintName("FK__IletisimBilg__Id__403A8C7D");
            });

            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.ToTable("Ogrenciler");

                entity.Property(e => e.Ad).HasMaxLength(50);

                entity.Property(e => e.Cinsiyet).HasMaxLength(1);

                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                entity.Property(e => e.KanGrubu).HasMaxLength(3);

                entity.Property(e => e.MezuniyetNotu).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.Soyad).HasMaxLength(50);

                entity.HasOne(d => d.DogumYeri)
                    .WithMany(p => p.Doganlar)
                    .HasForeignKey(d => d.DogumYeriId)
                    .HasConstraintName("FK__Ogrencile__Dogum__3C69FB99");

                entity.HasOne(d => d.TakimLideri)
                    .WithMany(p => p.TakimUyeleri)
                    .HasForeignKey(d => d.TakimLideriId)
                    .HasConstraintName("FK__Ogrencile__Takim__3D5E1FD2");

                entity.HasMany(d => d.FavoriSehirler)
                    .WithMany(p => p.Sevenler)
                    .UsingEntity<Dictionary<string, object>>(
                        "FavoriSehirler",
                        l => l.HasOne<Sehir>().WithMany().HasForeignKey("SehirId").HasConstraintName("FK__FavoriSeh__Sehir__440B1D61"),
                        r => r.HasOne<Ogrenci>().WithMany().HasForeignKey("OgrenciId").HasConstraintName("FK__FavoriSeh__Ogren__4316F928"),
                        j =>
                        {
                            j.HasKey("OgrenciId", "SehirId").HasName("PK__FavoriSe__498961FAC4AD6F0D");

                            j.ToTable("FavoriSehirler");
                        });
            });

            modelBuilder.Entity<Sehir>(entity =>
            {
                entity.ToTable("Sehirler");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SehirAd).HasMaxLength(50);

                entity.HasOne(d => d.Bolge)
                    .WithMany(p => p.Sehirler)
                    .HasForeignKey(d => d.BolgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sehirler__BolgeI__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
