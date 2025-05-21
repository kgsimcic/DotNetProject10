using System;
using System.Collections.Generic;
using PatientMicroservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace PatientMicroservice;

public partial class PatientDbContext : DbContext
{
    public PatientDbContext()
    {
    }

    public PatientDbContext(DbContextOptions<PatientDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("PatientConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.FamilyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GivenName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
