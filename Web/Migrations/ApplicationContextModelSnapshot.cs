﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.Classes;

#nullable disable

namespace TaskBoard.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d",
                            Description = "Description of First Project. Have a nice day!",
                            Name = "First Project"
                        });
                });

            modelBuilder.Entity("Data.Models.Sprint", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Files")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints");

                    b.HasData(
                        new
                        {
                            Id = "27b42fa3-ab04-4772-a2a8-d881c7185524",
                            Comment = "First Comment",
                            Description = "Description of First Sprint",
                            EndDate = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "First Sprint",
                            ProjectId = "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d",
                            StartDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "15888b6d-df67-4814-a464-950f86f75fde",
                            Comment = "Second Comment",
                            Description = "Description of Second Sprint",
                            EndDate = new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Second Sprint",
                            ProjectId = "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d",
                            StartDate = new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Data.Models.Task", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Files")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SprintId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = "38c96dee-3d08-4d89-b4d7-df3f70b3c922",
                            Comment = "First Comment",
                            Description = "Description of First Task",
                            Name = "First Task",
                            SprintId = "27b42fa3-ab04-4772-a2a8-d881c7185524",
                            Status = 0,
                            UserId = "6c67f22b-6731-4927-a57c-6297d9b1a892"
                        },
                        new
                        {
                            Id = "b46492d2-5b1a-4f4d-9c29-771e3cbd1e0c",
                            Comment = "Second Comment",
                            Description = "Description of Second Task",
                            Name = "Second Task",
                            SprintId = "27b42fa3-ab04-4772-a2a8-d881c7185524",
                            Status = 0,
                            UserId = "ba3fedc2-c08e-4222-b51c-c3f2e911df8f"
                        },
                        new
                        {
                            Id = "a7dcfe99-e1f2-4900-a759-b740a9554e50",
                            Comment = "Third Comment",
                            Description = "Description of Third Task",
                            Name = "Third Task",
                            SprintId = "15888b6d-df67-4814-a464-950f86f75fde",
                            Status = 0,
                            UserId = "6c67f22b-6731-4927-a57c-6297d9b1a892"
                        },
                        new
                        {
                            Id = "9e5dfd49-4dbf-47d7-9e19-6d7f67017e83",
                            Comment = "Fourth Comment",
                            Description = "Description of Fourth Task",
                            Name = "Fourth Task",
                            SprintId = "15888b6d-df67-4814-a464-950f86f75fde",
                            Status = 0,
                            UserId = "ba3fedc2-c08e-4222-b51c-c3f2e911df8f"
                        });
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "ee0cd788-cb4a-44b0-bfc2-f693d37c5dcf",
                            Login = "admin",
                            Password = "1",
                            Role = 0
                        },
                        new
                        {
                            Id = "0ab8f3b4-3647-4f03-af59-9d95c65b9c71",
                            Login = "manager",
                            Password = "2",
                            Role = 1
                        },
                        new
                        {
                            Id = "6c67f22b-6731-4927-a57c-6297d9b1a892",
                            Login = "user1",
                            Password = "3",
                            Role = 2
                        },
                        new
                        {
                            Id = "ba3fedc2-c08e-4222-b51c-c3f2e911df8f",
                            Login = "user2",
                            Password = "3",
                            Role = 2
                        });
                });

            modelBuilder.Entity("SprintUser", b =>
                {
                    b.Property<string>("SprintsId")
                        .HasColumnType("text");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("SprintsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("SprintUser");
                });

            modelBuilder.Entity("Data.Models.Sprint", b =>
                {
                    b.HasOne("Data.Models.Project", "Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Data.Models.Task", b =>
                {
                    b.HasOne("Data.Models.Sprint", "Sprint")
                        .WithMany("Tasks")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sprint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SprintUser", b =>
                {
                    b.HasOne("Data.Models.Sprint", null)
                        .WithMany()
                        .HasForeignKey("SprintsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.Project", b =>
                {
                    b.Navigation("Sprints");
                });

            modelBuilder.Entity("Data.Models.Sprint", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
