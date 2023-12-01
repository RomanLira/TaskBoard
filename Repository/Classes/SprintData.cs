using System.Collections.ObjectModel;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Classes;

public class SprintData : IEntityTypeConfiguration<Sprint>
{
    public void Configure(EntityTypeBuilder<Sprint> builder)
    {
        builder.HasData(
            new Sprint
            {
                Id = "27b42fa3-ab04-4772-a2a8-d881c7185524",
                Name = "First Sprint",
                Description = "Description of First Sprint",
                StartDate = new DateTime(2024, 01, 01),
                EndDate = new DateTime(2024, 01, 10),
                Comment = "First Comment",
                ProjectId = "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d"
            },
            new Sprint
            {
                Id = "15888b6d-df67-4814-a464-950f86f75fde",
                Name = "Second Sprint",
                Description = "Description of Second Sprint",
                StartDate = new DateTime(2024, 01, 11),
                EndDate = new DateTime(2024, 01, 21),
                Comment = "Second Comment",
                ProjectId = "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d"
            });
    }
}