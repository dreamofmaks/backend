using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace User.Data.Model
{
    public partial class UserContext : DbContext
    {
        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=PF2FKAR1\\SQLEXPRESS;Database=\"Users\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Building).HasColumnName("BUILDING");

                entity.Property(e => e.CityId).HasColumnName("CITY_ID");

                entity.Property(e => e.CountryId).HasColumnName("COUNTRY_ID");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STREET");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_ADDRESS_R_CITY");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_ADDRESS_R_COUNTRY");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("CITY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("COUNTRY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PERSON");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("DATE_OF_BIRTH");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_PERSONS_R_ADDRESS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
