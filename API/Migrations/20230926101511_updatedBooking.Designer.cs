﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20230926101511_updatedBooking")]
    partial class updatedBooking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Admin", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long?>("Hotelid")
                        .HasColumnType("bigint");

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

            modelBuilder.Entity("Models.Booking", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("customerid")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("roomId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("customerid");

                    b.HasIndex("roomId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("Models.Customer", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long?>("Hotelid")
                        .HasColumnType("bigint");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("zipcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Hotelid");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Models.Hotel", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

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

            modelBuilder.Entity("Models.Room", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long?>("Hotelid")
                        .HasColumnType("bigint");

                    b.Property<int>("roomNum")
                        .HasColumnType("int");

                    b.Property<long>("typeId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("Hotelid");

                    b.HasIndex("typeId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("Models.RoomType", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("bedAmount")
                        .HasColumnType("bigint");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("price")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("RoomType");
                });

            modelBuilder.Entity("Models.Admin", b =>
                {
                    b.HasOne("Models.Hotel", null)
                        .WithMany("admins")
                        .HasForeignKey("Hotelid");
                });

            modelBuilder.Entity("Models.Booking", b =>
                {
                    b.HasOne("Models.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Room", "room")
                        .WithMany()
                        .HasForeignKey("roomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("room");
                });

            modelBuilder.Entity("Models.Customer", b =>
                {
                    b.HasOne("Models.Hotel", null)
                        .WithMany("customers")
                        .HasForeignKey("Hotelid");
                });

            modelBuilder.Entity("Models.Room", b =>
                {
                    b.HasOne("Models.Hotel", null)
                        .WithMany("rooms")
                        .HasForeignKey("Hotelid");

                    b.HasOne("Models.RoomType", "type")
                        .WithMany()
                        .HasForeignKey("typeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("type");
                });

            modelBuilder.Entity("Models.Hotel", b =>
                {
                    b.Navigation("admins");

                    b.Navigation("customers");

                    b.Navigation("rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
