using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using API.DataLogic;

namespace API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170706163738_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("API.DataLogic.Models.Beacon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BeaconId")
                        .IsRequired();

                    b.Property<string>("FriendlyName");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("MajorVersion");

                    b.Property<string>("MinorVersion");

                    b.Property<string>("UUID")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Beacons");
                });

            modelBuilder.Entity("API.DataLogic.Models.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("API.DataLogic.Models.Metadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Metadata");
                });

            modelBuilder.Entity("API.DataLogic.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContentId");

                    b.Property<int>("RatingCount");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("API.DataLogic.Models.ScheduledItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BeaconId");

                    b.Property<int>("ContentId");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<DateTime>("StartDateTime");

                    b.HasKey("Id");

                    b.ToTable("ScheduledItems");
                });
        }
    }
}
