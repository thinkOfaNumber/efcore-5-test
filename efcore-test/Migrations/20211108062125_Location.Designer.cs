﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using efcore_test.Data;

namespace efcore_test.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20211108062125_Location")]
    partial class Location
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("efcore_test.Data.Attribute", b =>
                {
                    b.Property<int>("AttributeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttributeId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Attribute");
                });

            modelBuilder.Entity("efcore_test.Data.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("efcore_test.Data.History", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoryId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("LocationId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("efcore_test.Data.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("efcore_test.Data.Attribute", b =>
                {
                    b.HasOne("efcore_test.Data.Device", "Device")
                        .WithMany("Attributes")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("efcore_test.Data.History", b =>
                {
                    b.HasOne("efcore_test.Data.Device", "Device")
                        .WithMany("Histories")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("efcore_test.Data.Location", "Location")
                        .WithMany("Histories")
                        .HasForeignKey("LocationId");

                    b.Navigation("Device");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("efcore_test.Data.Device", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Histories");
                });

            modelBuilder.Entity("efcore_test.Data.Location", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
