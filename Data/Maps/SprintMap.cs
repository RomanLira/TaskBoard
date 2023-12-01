using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps;

public class SprintMap
{
    public SprintMap(EntityTypeBuilder<Sprint> builder)
    {
        builder.HasKey(sprint => sprint.Id);
        
        builder
            .HasOne(sprint => sprint.Project)
            .WithMany(project => project.Sprints)
            .HasForeignKey(sprint => sprint.ProjectId);
    }
}