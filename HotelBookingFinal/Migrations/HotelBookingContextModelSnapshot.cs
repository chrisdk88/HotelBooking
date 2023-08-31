﻿// <auto-generated />
using System;
using HotelBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelBookingFinal.Migrations
{
    [DbContext(typeof(HotelBookingContext))]
    partial class HotelBookingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UMLHotel.Admin", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Hotelid")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Hotelid");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("UMLHotel.Booking", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("customerid")
                        .HasColumnType("int");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("roomNum")
                        .HasColumnType("int");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("customerid");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("UMLHotel.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Hotelid")
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("phoneNumber")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Hotelid");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("UMLHotel.Hotel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("UMLHotel.Room", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Hotelid")
                        .HasColumnType("int");

                    b.Property<int>("bookingsid")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("vacancy")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("Hotelid");

                    b.HasIndex("bookingsid");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("UMLHotel.Admin", b =>
                {
                    b.HasOne("UMLHotel.Hotel", null)
                        .WithMany("admins")
                        .HasForeignKey("Hotelid");
                });

            modelBuilder.Entity("UMLHotel.Booking", b =>
                {
                    b.HasOne("UMLHotel.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("UMLHotel.Customer", b =>
                {
                    b.HasOne("UMLHotel.Hotel", null)
                        .WithMany("customers")
                        .HasForeignKey("Hotelid");
                });

            modelBuilder.Entity("UMLHotel.Room", b =>
                {
                    b.HasOne("UMLHotel.Hotel", null)
                        .WithMany("rooms")
                        .HasForeignKey("Hotelid");

                    b.HasOne("UMLHotel.Booking", "bookings")
                        .WithMany()
                        .HasForeignKey("bookingsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bookings");
                });

            modelBuilder.Entity("UMLHotel.Hotel", b =>
                {
                    b.Navigation("admins");

                    b.Navigation("customers");

                    b.Navigation("rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
