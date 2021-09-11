﻿// <auto-generated />
using System;
using Centennial.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Centennial.Api.Data.Migrations.CentennialDb
{
    [DbContext(typeof(CentennialDbContext))]
    partial class CentennialDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Centennial.Api.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Centennial.Api.Entities.Inventory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.HasIndex("ProductId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("Centennial.Api.Entities.Material", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = "410b785b-5484-48f5-8555-7974fe5b63dd",
                            CreatedDate = new DateTime(2021, 7, 13, 18, 48, 12, 823, DateTimeKind.Utc).AddTicks(7840),
                            IsActive = true,
                            Name = "SS"
                        },
                        new
                        {
                            Id = "b0217cf5-af55-4472-bf79-27167df5ee52",
                            CreatedDate = new DateTime(2021, 7, 13, 18, 48, 12, 823, DateTimeKind.Utc).AddTicks(9080),
                            IsActive = true,
                            Name = "Titanium"
                        });
                });

            modelBuilder.Entity("Centennial.Api.Entities.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMandatory")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOutBound")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemovable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("Centennial.Api.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dimensions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MaterialId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Centennial.Api.Entities.ProductPriceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Change")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPriceRecords");
                });

            modelBuilder.Entity("Centennial.Api.Entities.RawMaterial", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MaterialId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("RawMaterials");
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

            modelBuilder.Entity("Centennial.Api.Entities.StockRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StatusId");

                    b.ToTable("StockRecords");
                });

            modelBuilder.Entity("Centennial.Api.Entities.StockTransaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InventoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("TransactionDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("StockTransactions");
                });

            modelBuilder.Entity("Centennial.Api.Entities.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sent to"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Received from"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Added by Stock record"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Sent to customer"
                        });
                });

            modelBuilder.Entity("Centennial.Api.Entities.Inventory", b =>
                {
                    b.HasOne("Centennial.Api.Entities.Process", "Process")
                        .WithMany()
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Centennial.Api.Entities.Product", "Product")
                        .WithMany("Inventories")
                        .HasForeignKey("ProductId");

                    b.Navigation("Process");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Centennial.Api.Entities.Product", b =>
                {
                    b.HasOne("Centennial.Api.Entities.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Centennial.Api.Entities.ProductPriceRecord", b =>
                {
                    b.HasOne("Centennial.Api.Entities.Product", "Product")
                        .WithMany("ProductPriceRecords")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Centennial.Api.Entities.RawMaterial", b =>
                {
                    b.HasOne("Centennial.Api.Entities.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Centennial.Api.Entities.StockRecord", b =>
                {
                    b.HasOne("Centennial.Api.Entities.Process", "Process")
                        .WithMany()
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Centennial.Api.Entities.Product", "Product")
                        .WithMany("StockRecords")
                        .HasForeignKey("ProductId");

                    b.HasOne("Centennial.Api.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Process");

                    b.Navigation("Product");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Centennial.Api.Entities.StockTransaction", b =>
                {
                    b.HasOne("Centennial.Api.Entities.Inventory", "Inventory")
                        .WithMany("Transactions")
                        .HasForeignKey("InventoryId");

                    b.HasOne("Centennial.Api.Entities.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Centennial.Api.Entities.Inventory", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Centennial.Api.Entities.Product", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("ProductPriceRecords");

                    b.Navigation("StockRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
