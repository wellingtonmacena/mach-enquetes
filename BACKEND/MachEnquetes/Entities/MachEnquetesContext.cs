using System;
using System.Collections.Generic;
using MachEnquetes.Models;
using Microsoft.EntityFrameworkCore;

namespace MachEnquetes.Entities;

public partial class MachEnquetesContext : DbContext
{
    public DbSet<User> User { get; set; }

    public MachEnquetesContext(DbContextOptions<MachEnquetesContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
