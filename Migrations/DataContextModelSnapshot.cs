﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace apiDotnetCoreEmail.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
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