﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOATApiReact.Data;

namespace SOATApiReact.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210128222745_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11");

            modelBuilder.Entity("SOATApiReact.Model.SOAT", b =>
                {
                    b.Property<int>("OwnerDocument")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VehiclePlate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Year")
                        .HasColumnType("Date");

                    b.HasKey("OwnerDocument", "VehiclePlate", "Year");

                    b.HasIndex("VehiclePlate");

                    b.ToTable("SOATs");
                });

            modelBuilder.Entity("SOATApiReact.Model.User", b =>
                {
                    b.Property<int>("Document")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Document");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SOATApiReact.Model.Vehicle", b =>
                {
                    b.Property<string>("Plate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Axles")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Engine")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Plate");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("SOATApiReact.Model.SOAT", b =>
                {
                    b.HasOne("SOATApiReact.Model.User", "Owner")
                        .WithMany("SOATs")
                        .HasForeignKey("OwnerDocument")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOATApiReact.Model.Vehicle", "Vehicle")
                        .WithMany("SOATs")
                        .HasForeignKey("VehiclePlate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
