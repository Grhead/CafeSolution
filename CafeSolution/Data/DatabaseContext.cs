using System;
using CafeSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeSolution.Data;

public class DatabaseContext : DbContext
{
    public DbSet<CookingStatus> CookingStatuses { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<DishesInOrder> DishesInOrders { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeesAtShift> EmployeesAtShifts { get; set; }
    public DbSet<EmployeesAtTable> EmployeesAtTables { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaymentStatus> PaymentStatuses { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Table> Tables { get; set; }

    public DatabaseContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseMySql("server=;user=root;password=;database=ShareFood;", 
                new MySqlServerVersion(new Version(8, 2, 0)));
    }
}