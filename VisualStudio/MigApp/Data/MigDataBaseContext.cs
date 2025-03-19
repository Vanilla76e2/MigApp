using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MigApp.Data;

public partial class MigDataBaseContext : DbContext
{
    public MigDataBaseContext()
    {
    }

    public MigDataBaseContext(DbContextOptions<MigDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cctv> Cctvs { get; set; }

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<ComputersComponent> ComputersComponents { get; set; }

    public virtual DbSet<ComputersDevice> ComputersDevices { get; set; }

    public virtual DbSet<ComputersServiceHistory> ComputersServiceHistories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Favourite> Favourites { get; set; }

    public virtual DbSet<FavouriteView> FavouriteViews { get; set; }

    public virtual DbSet<GenericDevice> GenericDevices { get; set; }

    public virtual DbSet<IpAddressInfo> IpAddressInfos { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<LogEntry> Logs { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<Orgtechnic> Orgtechnics { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesView> RolesViews { get; set; }

    public virtual DbSet<Router> Routers { get; set; }

    public virtual DbSet<Switch> Switches { get; set; }

    public virtual DbSet<Tablet> Tablets { get; set; }

    public virtual DbSet<UserProfileView> UserProfileViews { get; set; }

    public virtual DbSet<UsersProfile> UsersProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Test;Username=Test;Password=Test123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cctv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cctv_pkey");
        });

        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computers_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Computers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_computers_employees");
        });

        modelBuilder.Entity<ComputersComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computers_components_pkey");

            entity.HasOne(d => d.Computer).WithMany(p => p.ComputersComponents).HasConstraintName("FK_computers_components_computers");
        });

        modelBuilder.Entity<ComputersDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computers_devices_pkey");

            entity.HasOne(d => d.Computer).WithMany(p => p.ComputersDevices).HasConstraintName("FK_computers_devices_computers");
        });

        modelBuilder.Entity<ComputersServiceHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computers_service_history_pkey");

            entity.HasOne(d => d.Computer).WithMany(p => p.ComputersServiceHistories).HasConstraintName("FK_computers_service_history_computers");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departments_pkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_employees_departments");
        });

        modelBuilder.Entity<Favourite>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Favourites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_favourite_users");
        });

        modelBuilder.Entity<FavouriteView>(entity =>
        {
            entity.ToView("favourite_view", "Misc");
        });

        modelBuilder.Entity<GenericDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("generic_devices_pkey");
        });

        modelBuilder.Entity<IpAddressInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ip_address_info_pkey");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("laptops_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Laptops).HasConstraintName("laptops_employee_id_fkey");
        });

        modelBuilder.Entity<LogEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logs_pkey");
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("monitors_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.DpPort).HasDefaultValue((short)0);
            entity.Property(e => e.DviPort).HasDefaultValue((short)0);
            entity.Property(e => e.HdmiPort).HasDefaultValue((short)0);
            entity.Property(e => e.VgaPort).HasDefaultValue((short)0);
        });

        modelBuilder.Entity<Orgtechnic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orgtechnic_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.Property(e => e.EmployeesAccesslevel).HasDefaultValue(0);
            entity.Property(e => e.FurnitureAccesslevel).HasDefaultValue(0);
            entity.Property(e => e.IsAdministrator).HasDefaultValue(false);
            entity.Property(e => e.TechnicsAccesslevel).HasDefaultValue(0);
        });

        modelBuilder.Entity<RolesView>(entity =>
        {
            entity
                .ToView("roles_view", "Misc")
                .HasAnnotation("Npgsql:StorageParameter:check_option", "cascaded");
        });

        modelBuilder.Entity<Router>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("routers_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Switch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("switches_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Tablet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tablets_pkey");

            entity.Property(e => e.Deleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<UserProfileView>(entity =>
        {
            entity.ToView("user_profile_view", "Misc");
        });

        modelBuilder.Entity<UsersProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_profiles_pkey");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.UsersProfiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_profiles_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
