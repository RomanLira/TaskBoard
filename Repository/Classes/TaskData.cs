using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Data.Models.Task;

namespace Repository.Classes;

public class TaskData : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasData(
            new Task
            {
                Id = "38c96dee-3d08-4d89-b4d7-df3f70b3c922",
                Name = "First Task",
                Description = "Description of First Task",
                Status = 0,
                Comment = "First Comment",
                UserId = "6c67f22b-6731-4927-a57c-6297d9b1a892",
                SprintId = "27b42fa3-ab04-4772-a2a8-d881c7185524"
            },
            new Task
            {
                Id = "b46492d2-5b1a-4f4d-9c29-771e3cbd1e0c",
                Name = "Second Task",
                Description = "Description of Second Task",
                Status = 0,
                Comment = "Second Comment",
                UserId = "ba3fedc2-c08e-4222-b51c-c3f2e911df8f",
                SprintId = "27b42fa3-ab04-4772-a2a8-d881c7185524"
            },
            new Task
            {
                Id = "a7dcfe99-e1f2-4900-a759-b740a9554e50",
                Name = "Third Task",
                Description = "Description of Third Task",
                Status = 0,
                Comment = "Third Comment",
                UserId = "6c67f22b-6731-4927-a57c-6297d9b1a892",
                SprintId = "15888b6d-df67-4814-a464-950f86f75fde"
            },
            new Task
            {
                Id = "9e5dfd49-4dbf-47d7-9e19-6d7f67017e83",
                Name = "Fourth Task",
                Description = "Description of Fourth Task",
                Status = 0,
                Comment = "Fourth Comment",
                UserId = "ba3fedc2-c08e-4222-b51c-c3f2e911df8f",
                SprintId = "15888b6d-df67-4814-a464-950f86f75fde"
            });
    }
}