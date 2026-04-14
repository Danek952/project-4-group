using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShopProducts.Model;

public partial class HostStepanovContext : DbContext
{

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public HostStepanovContext()
    {
    }

    public HostStepanovContext(DbContextOptions<HostStepanovContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HostStepanov;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
