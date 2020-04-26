﻿// <auto-generated />
using System;
using ExampleGrid.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExampleGrid.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200426035940_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("ExampleGrid.Models.CustomerTB", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phoneno")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerID");

                    b.ToTable("CustomerTB");
                });

            modelBuilder.Entity("ExampleGrid.Models.Sample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("COGS")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiscountBand")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discounts")
                        .HasColumnType("TEXT");

                    b.Property<string>("EnteredBy")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("GrossSales")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ManufacturingPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Product")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Profit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Sales")
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitsSold")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Samples");
                });
#pragma warning restore 612, 618
        }
    }
}
