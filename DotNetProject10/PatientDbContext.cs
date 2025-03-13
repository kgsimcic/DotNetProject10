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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=PatientDb;trusted_connection=true;MultipleActiveResultSets=True");

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
