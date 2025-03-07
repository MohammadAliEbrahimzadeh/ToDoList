using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = ToDoList.Model.Entities.Task;

namespace ToDoList.DataAccess.Configurations;

class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder
            .HasKey(x=>x.Id);

        builder
            .Property(x => x.Title)
            .HasMaxLength(256);

        builder
            .Property(x => x.Description)
            .HasMaxLength(256);
    }
}
