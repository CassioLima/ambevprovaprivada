using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //=== SEED USERS ===
        //modelBuilder.Entity<User>().HasData(
        //    new User
        //    {
        //        Id = Guid.NewGuid(),
        //        Username = "admin",
        //        Email = "admin@ambev.com",
        //        Phone = "(11) 99999-9999",
        //        Password = "Admin@123",
        //        Role = UserRole.Admin,
        //        Status = UserStatus.Active,
        //        CreatedAt = DateTime.UtcNow
        //    },
        //    new User
        //    {
        //        Id = Guid.NewGuid(),
        //        Username = "user1",
        //        Email = "user1@ambev.com",
        //        Phone = "(11) 98888-8888",
        //        Password = "User@123",
        //        Role = UserRole.Manager,
        //        Status = UserStatus.Active,
        //        CreatedAt = DateTime.UtcNow
        //    }
        //);

        base.OnModelCreating(modelBuilder);
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.WebApi")
        );

        return new DefaultContext(builder.Options);
    }
}