﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Events.Data;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    [Migration("20221117145124_initMigrate")]
    partial class initMigrate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("ThAmCo.Events.Data.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasFirstAider")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Guest", b =>
                {
                    b.Property<int>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ForeName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("GuestId");

                    b.ToTable("Guests");

                    b.HasData(
                        new
                        {
                            GuestId = 1,
                            ForeName = "Thomas",
                            Surname = "Wintersgill"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.Property<int>("EventID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GuestID")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventID", "GuestID");

                    b.HasIndex("GuestID");

                    b.ToTable("GuestBookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Forename")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("StaffId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StaffId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("staffing");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany("Guests")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Data.Guest", "Guest")
                        .WithMany("Events")
                        .HasForeignKey("GuestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany("Staff")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Data.Staff", "Staff")
                        .WithMany("Events")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Event", b =>
                {
                    b.Navigation("Guests");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Guest", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staff", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
