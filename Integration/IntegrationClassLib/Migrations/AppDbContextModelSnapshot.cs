﻿// <auto-generated />
using System;
using Integration.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Integration.Model.Benefit", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Description")
                    .HasColumnType("text");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("timestamp without time zone");

                b.Property<bool>("Hidden")
                    .HasColumnType("boolean");

                b.Property<int>("PharmacyId")
                    .HasColumnType("integer");

                b.Property<bool>("Published")
                    .HasColumnType("boolean");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("Title")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("PharmacyId");

                b.ToTable("Benefits");
            });

            modelBuilder.Entity("Integration.Model.City", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<int>("CountryId")
                    .HasColumnType("integer");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<int>("PostalCode")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.HasIndex("CountryId");

                b.ToTable("Cities");
            });

            modelBuilder.Entity("Integration.Model.Complaint", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("Description")
                    .HasColumnType("text");

                b.Property<int>("ManagerId")
                    .HasColumnType("integer");

                b.Property<int>("PharmacyId")
                    .HasColumnType("integer");

                b.Property<string>("Title")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("ManagerId");

                b.HasIndex("PharmacyId");

                b.ToTable("Complaints");
            });

            modelBuilder.Entity("Integration.Model.ComplaintResponse", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<int>("ComplaintId")
                    .HasColumnType("integer");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("Text")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("ComplaintId")
                    .IsUnique();

                b.ToTable("ComplaintResponses");
            });

            modelBuilder.Entity("Integration.Model.Country", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Countries");
            });

            modelBuilder.Entity("Integration.Model.Manager", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<string>("Surname")
                    .HasColumnType("text");

                b.Property<string>("Username")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Managers");
            });

            modelBuilder.Entity("Integration.Model.Medicine", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Medicines");
            });

            modelBuilder.Entity("Integration.Model.MedicineSpecificationFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("MedicineSpecificationFiles");
                });

            modelBuilder.Entity("Integration.Model.Pharmacy", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<Guid>("ApiKey")
                    .HasColumnType("uuid");

                b.Property<string>("BaseUrl")
                    .HasColumnType("text");

                b.Property<int>("CityId")
                    .HasColumnType("integer");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<string>("StreetName")
                    .HasColumnType("text");

                b.Property<string>("StreetNumber")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("CityId");

                b.ToTable("Pharmacies");
            });

            modelBuilder.Entity("Integration.Model.Receipt", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<int>("AmountSpent")
                    .HasColumnType("integer");

                b.Property<int>("MedicineId")
                    .HasColumnType("integer");

                b.Property<DateTime>("ReceiptDate")
                    .HasColumnType("timestamp without time zone");

                b.HasKey("Id");

                b.HasIndex("MedicineId");

                b.ToTable("Receipts");
                modelBuilder.Entity("Integration.Model.Benefit", b =>
                {
                    b.HasOne("Integration.Model.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");
                });

                modelBuilder.Entity("Integration.Model.City", b =>
                {
                    b.HasOne("Integration.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

                modelBuilder.Entity("Integration.Model.Complaint", b =>
                {
                    b.HasOne("Integration.Model.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integration.Model.Pharmacy", "Pharmacy")
                        .WithMany("Complaints")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");

                    b.Navigation("Pharmacy");
                });

                modelBuilder.Entity("Integration.Model.ComplaintResponse", b =>
                {
                    b.HasOne("Integration.Model.Complaint", "Complaint")
                        .WithOne("ComplaintResponse")
                        .HasForeignKey("Integration.Model.ComplaintResponse", "ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");
                });

                modelBuilder.Entity("Integration.Model.MedicineSpecificationFile", b =>
                {
                    b.HasOne("Integration.Model.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");
                });

                modelBuilder.Entity("Integration.Model.Pharmacy", b =>
                {
                    b.HasOne("Integration.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

                modelBuilder.Entity("Integration.Model.Receipt", b =>
                {
                    b.HasOne("Integration.Model.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");
                });

                modelBuilder.Entity("Integration.Model.Complaint", b => { b.Navigation("ComplaintResponse"); });

                modelBuilder.Entity("Integration.Model.Pharmacy", b => { b.Navigation("Complaints"); });
#pragma warning restore 612, 618
            });
        }
    }
}
