using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Classes;

public class ProjectData : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasData(
            new Project
            {
                Id = "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d",
                Name = "First Project",
                Description = "Description of First Project. Have a nice day!"
            });
    }
}