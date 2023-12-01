using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps;

public class ProjectMap
{
    public ProjectMap(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(project => project.Id);
    }
}