using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps;

public class SprintUserMap
{
    public SprintUserMap(EntityTypeBuilder<SprintUser> builder)
    {
        builder.HasKey(sprintUser => new { sprintUser.SprintId, sprintUser.UserId });

        builder
            .HasOne<Sprint>(sprintUser => sprintUser.Sprint)
            .WithMany(sprint => sprint.SprintUsers)
            .HasForeignKey(sprintUser => sprintUser.SprintId);
        
        builder
            .HasOne<User>(sprintUser => sprintUser.User)
            .WithMany(user => user.SprintUsers)
            .HasForeignKey(sprintUser => sprintUser.UserId);
    }
}