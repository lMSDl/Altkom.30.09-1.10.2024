﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241001132757_AddAddressToPerson")]
    partial class AddAddressToPerson
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .IsConcurrencyToken()
                        .HasPrecision(6)
                        .HasColumnType("datetime2(6)");

                    b.Property<Point>("DeliveryPoint")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("[Name] + ' alamakota'", true);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parameters")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timer")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(6)
                        .HasColumnType("datetime2(6)")
                        .HasComputedColumnSql("getdate()");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Models.OrderSummary", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasPrecision(6)
                        .HasColumnType("datetime2(6)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("View_OrderSummary", (string)null);
                });

            modelBuilder.Entity("Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    SqlServerPropertyBuilderExtensions.IsSparse(b.Property<string>("Description"));

                    b.Property<DateTime>("From")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(6)
                        .HasColumnType("datetime2(6)")
                        .HasColumnName("From");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("To")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(6)
                        .HasColumnType("datetime2(6)")
                        .HasColumnName("To");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                            {
                                ttb.UseHistoryTable("PersonHistory");
                                ttb
                                    .HasPeriodStart("To")
                                    .HasColumnName("To");
                                ttb
                                    .HasPeriodEnd("From")
                                    .HasColumnName("From");
                            }));
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProducts", (string)null);
                });

            modelBuilder.Entity("Models.ProductDetails", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.Property<byte[]>("_TableSharingConcurrencyTokenConvention_Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("OrderProducts", (string)null);
                });

            modelBuilder.Entity("Models.Person", b =>
                {
                    b.OwnsOne("Models.Address", "Address", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PersonId");

                            b1.ToTable("People");

                            b1.ToJson("Address");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");

                            b1.OwnsOne("Models.Coordinates", "Coordinates", b2 =>
                                {
                                    b2.Property<int>("AddressPersonId")
                                        .HasColumnType("int");

                                    b2.Property<float>("Latitude")
                                        .HasColumnType("real");

                                    b2.Property<float>("Longitude")
                                        .HasColumnType("real");

                                    b2.HasKey("AddressPersonId");

                                    b2.ToTable("People");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressPersonId");
                                });

                            b1.Navigation("Coordinates")
                                .IsRequired();
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.HasOne("Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Models.ProductDetails", b =>
                {
                    b.HasOne("Models.Product", null)
                        .WithOne("Details")
                        .HasForeignKey("Models.ProductDetails", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
