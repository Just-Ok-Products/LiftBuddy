﻿// <auto-generated />
using Lift.Buddy.Core.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230809224642_AddedSecurityQuestions")]
    partial class AddedSecurityQuestions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("Lift.Buddy.Core.DB.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "username");

                    b.Property<string>("Answers")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "answers");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasAnnotation("Relational:JsonPropertyName", "isAdmin");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.Property<string>("Questions")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "questions");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "surname");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
