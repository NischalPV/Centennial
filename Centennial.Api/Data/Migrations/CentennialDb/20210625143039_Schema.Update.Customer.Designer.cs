﻿// <auto-generated />
using System;
using Centennial.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Centennial.Api.Data.Migrations.CentennialDb
{
    [DbContext(typeof(CentennialDbContext))]
    [Migration("20210625143039_Schema.Update.Customer")]
    partial class SchemaUpdateCustomer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Centennial.Api.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Centennial.Api.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "Created"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "Modified"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            Name = "Communicated"
                        },
                        new
                        {
                            Id = 5,
                            IsActive = true,
                            Name = "Closed"
                        },
                        new
                        {
                            Id = 6,
                            IsActive = true,
                            Name = "Deleted"
                        },
                        new
                        {
                            Id = 7,
                            IsActive = true,
                            Name = "Partial"
                        },
                        new
                        {
                            Id = 8,
                            IsActive = true,
                            Name = "PendingApproval"
                        },
                        new
                        {
                            Id = 9,
                            IsActive = true,
                            Name = "Rejected"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
