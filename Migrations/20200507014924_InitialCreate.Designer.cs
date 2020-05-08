﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace apiDotnetCoreEmail.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200507014924_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EmailData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("adress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("from")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("msg")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("options")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("to")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("Emails");
                });
#pragma warning restore 612, 618
        }
    }
}
