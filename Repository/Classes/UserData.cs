using System.ComponentModel;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Classes;

public class UserData : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = "ee0cd788-cb4a-44b0-bfc2-f693d37c5dcf",
                Login = "admin",
                Password = "1",
                Role = 0
            },

            new User
            {
                Id = "0ab8f3b4-3647-4f03-af59-9d95c65b9c71",
                Login = "manager",
                Password = "2",
                Role = 1
            },

            new User
            {
                Id = "6c67f22b-6731-4927-a57c-6297d9b1a892",
                Login = "user1",
                Password = "3",
                Role = 2
            },

            new User
            {
                Id = "ba3fedc2-c08e-4222-b51c-c3f2e911df8f",
                Login = "user2",
                Password = "3",
                Role = 2
            });
    }
}