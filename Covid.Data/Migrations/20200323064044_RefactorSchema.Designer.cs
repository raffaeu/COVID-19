﻿// <auto-generated />
using System;
using Covid.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Covid.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200323064044_RefactorSchema")]
    partial class RefactorSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Covid.Data.Models.RawData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ConfirmedCount")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeadCount")
                        .HasColumnType("int");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecoveredCount")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Country")
                        .HasName("IX_RAW_DATA_COUNTRY")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("Date")
                        .HasName("IX_RAW_DATA_DATE")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("State")
                        .HasName("IX_RAW_DATA_STATE")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("TBL_RAW_DATA");
                });
#pragma warning restore 612, 618
        }
    }
}
