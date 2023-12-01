using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Classes;

public class SprintUserData : IEntityTypeConfiguration<SprintUser>
{
     public void Configure(EntityTypeBuilder<SprintUser> builder)
     {
          var sprintUser1 = new SprintUser
               { SprintId = "27b42fa3-ab04-4772-a2a8-d881c7185524", UserId = "0ab8f3b4-3647-4f03-af59-9d95c65b9c71" };
          var sprintUser2 = new SprintUser
               { SprintId = "27b42fa3-ab04-4772-a2a8-d881c7185524", UserId = "6c67f22b-6731-4927-a57c-6297d9b1a892" };
          var sprintUser3 = new SprintUser
               { SprintId = "15888b6d-df67-4814-a464-950f86f75fde", UserId = "0ab8f3b4-3647-4f03-af59-9d95c65b9c71" };
          var sprintUser4 = new SprintUser
               { SprintId = "15888b6d-df67-4814-a464-950f86f75fde", UserId = "6c67f22b-6731-4927-a57c-6297d9b1a892" };

          builder.HasData(new List<SprintUser> { sprintUser1, sprintUser2, sprintUser3, sprintUser4 });
     }
}