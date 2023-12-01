using Data.Maps;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Task = Data.Models.Task;

namespace Repository.Classes;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserMap(modelBuilder.Entity<User>());
        new ProjectMap(modelBuilder.Entity<Project>());
        new SprintMap(modelBuilder.Entity<Sprint>());
        new TaskMap(modelBuilder.Entity<Task>());
        new SprintUserMap(modelBuilder.Entity<SprintUser>());
        
        modelBuilder.ApplyConfiguration(new UserData());
        modelBuilder.ApplyConfiguration(new ProjectData());
        modelBuilder.ApplyConfiguration(new SprintData());
        modelBuilder.ApplyConfiguration(new TaskData());
        modelBuilder.ApplyConfiguration(new SprintUserData());
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<SprintUser> SprintUsers { get; set; }
}