using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Data.Models.Task;

namespace Data.Maps;

public class TaskMap
{
    public TaskMap(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(task => task.Id);
        
        builder
            .HasOne(task => task.Sprint)
            .WithMany(sprint => sprint.Tasks)
            .HasForeignKey(task => task.SprintId);

        builder
            .HasOne(task => task.User)
            .WithMany(user => user.Tasks)
            .HasForeignKey(task => task.UserId);
    }
}