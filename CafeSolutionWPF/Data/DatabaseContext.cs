using CafeSolutionWPF.ConfigClasses;
using CafeSolutionWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeSolutionWPF.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Table> TableCookingStatuses { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<DishesInOrder> DishesInOrders { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeesAtShift> EmployeesAtShifts { get; set; }
    public DbSet<EmployeesAtTable> EmployeesAtTables { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<TablePaymentStatus> TablePaymentStatuses { get; set; }
    public DbSet<TablePaymentType> TablePaymentTypes { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
    public DbSet<Table> Tables { get; set; }

    public DatabaseContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(
                "server=" + ConfigReader.ParseSecrets().Secrets.DbServer + ";" +
                "user=" + ConfigReader.ParseSecrets().Secrets.DbUser + ";" +
                "password=" + ConfigReader.ParseSecrets().Secrets.DbPassword + ";" +
                "database=" + ConfigReader.ParseSecrets().Secrets.DbDatabase + ";",
                new MySqlServerVersion(new Version(8, 2, 0))).LogTo(Console.WriteLine);
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Document>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");
    //         entity.Property(e => e.Id).ValueGeneratedNever();
    //         entity.Property(e => e.ContractScan).HasMaxLength(2083);
    //         entity.Property(e => e.Photo).HasMaxLength(2083);
    //         entity.HasOne(d => d.DocumentId).WithOne(p => p.Document)
    //             .HasForeignKey<Document>(d => d.Id)
    //             .OnDelete(DeleteBehavior.ClientSetNull)
    //             .HasConstraintName("Documents_ibfk_1");
    //     });
    // }
}