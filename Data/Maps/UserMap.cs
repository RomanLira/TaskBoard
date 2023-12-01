using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps;

public class UserMap
{
    public UserMap(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
    }
}