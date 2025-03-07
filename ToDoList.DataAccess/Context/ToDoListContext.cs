using Microsoft.EntityFrameworkCore;
using ToDoList.DataAccess.Configurations;
using Task = ToDoList.Model.Entities.Task;

namespace ToDoList.DataAccess.Context;

public class ToDoListContext : DbContext
{
    public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
    {

    }

    public DbSet<Task> Tasks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }
}

