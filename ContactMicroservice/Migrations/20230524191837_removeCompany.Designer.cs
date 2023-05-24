﻿// <auto-generated />
using System;
using ContactMicroservice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ContactMicroservice.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230524191837_removeCompany")]
    partial class removeCompany
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ContactMicroservice.Models.Company", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ContactMicroservice.Models.ContactInfo", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PersonUUID")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("UUID");

                    b.HasIndex("PersonUUID");

                    b.ToTable("ContactInfos");
                });

            modelBuilder.Entity("ContactMicroservice.Models.Person", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.ToTable("People");
                });

            modelBuilder.Entity("ContactMicroservice.Models.ContactInfo", b =>
                {
                    b.HasOne("ContactMicroservice.Models.Person", null)
                        .WithMany("ContactInfos")
                        .HasForeignKey("PersonUUID");
                });

            modelBuilder.Entity("ContactMicroservice.Models.Person", b =>
                {
                    b.Navigation("ContactInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
