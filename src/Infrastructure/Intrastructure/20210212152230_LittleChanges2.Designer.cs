﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Intrastructure
{
    [DbContext(typeof(EasyEatsDbContext))]
    [Migration("20210212152230_LittleChanges2")]
    partial class LittleChanges2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Aggregates.CatalogAggregate.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId1");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Aggregates.CatalogAggregate.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OutOfStock")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Phone")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.OrderItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Aggregates.ScheduleAggregate.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValue("New");

                    b.Property<int>("StartHour")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Domain.Aggregates.ScheduleAggregate.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MonthScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("MonthScheduleId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Domain.ValueObjects.Month", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MonthNumber")
                        .HasColumnName("Month")
                        .HasColumnType("int");

                    b.Property<int>("MountDays")
                        .HasColumnName("NumberOfDays")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnName("Year")
                        .HasColumnType("int");

                    b.HasKey("ScheduleId");

                    b.ToTable("Month");
                });

            modelBuilder.Entity("Domain.Aggregates.CatalogAggregate.Image", b =>
                {
                    b.HasOne("Domain.Aggregates.CatalogAggregate.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId1");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.Customer", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<string>("CustomerId")
                                .HasColumnType("nvarchar(256)");

                            b1.Property<string>("AddressLine")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("PostalCode")
                                .HasColumnType("int");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.HasOne("Domain.Aggregates.OrderAggregate.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.OrderItems", b =>
                {
                    b.HasOne("Domain.Aggregates.OrderAggregate.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Domain.Aggregates.ScheduleAggregate.Schedule", b =>
                {
                    b.HasOne("Domain.ValueObjects.Month", "Month")
                        .WithMany()
                        .HasForeignKey("MonthScheduleId");
                });
#pragma warning restore 612, 618
        }
    }
}
