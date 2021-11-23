﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pharmacy.EfStructures;

namespace Pharmacy.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211120174117_AddedHospitalInReport")]
    partial class AddedHospitalInReport
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Pharmacy.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            modelBuilder.Entity("Pharmacy.Model.Complaint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("HospitalId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("Pharmacy.Model.ComplaintResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ComplaintId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ComplaintId");

                    b.ToTable("ComplaintResponses");
                });

            modelBuilder.Entity("Pharmacy.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Pharmacy.Model.Hospital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("Pharmacy.Model.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("Pharmacy.Model.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<List<string>>("MainPrecautions")
                        .HasColumnType("text[]");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("MedicinesThatCanBeCombined")
                        .HasColumnType("text[]");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<List<string>>("PotentialDangers")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("Reactions")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("SideEffects")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("Substances")
                        .HasColumnType("text[]");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("Usage")
                        .HasColumnType("text");

                    b.Property<double>("WeightInMilligrams")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("Pharmacy.Model.MedicineReportFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<int>("HospitalId")
                        .HasColumnType("integer");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.ToTable("MedicineReportFiles");
                });

            modelBuilder.Entity("Pharmacy.Model.City", b =>
                {
                    b.HasOne("Pharmacy.Model.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Pharmacy.Model.Complaint", b =>
                {
                    b.HasOne("Pharmacy.Model.Hospital", "Hospital")
                        .WithMany("Complaints")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("Pharmacy.Model.ComplaintResponse", b =>
                {
                    b.HasOne("Pharmacy.Model.Complaint", "Complaint")
                        .WithMany()
                        .HasForeignKey("ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");
                });

            modelBuilder.Entity("Pharmacy.Model.Hospital", b =>
                {
                    b.HasOne("Pharmacy.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Pharmacy.Model.Medicine", b =>
                {
                    b.HasOne("Pharmacy.Model.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("Pharmacy.Model.MedicineReportFile", b =>
                {
                    b.HasOne("Pharmacy.Model.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("Pharmacy.Model.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Pharmacy.Model.Hospital", b =>
                {
                    b.Navigation("Complaints");
                });
#pragma warning restore 612, 618
        }
    }
}
