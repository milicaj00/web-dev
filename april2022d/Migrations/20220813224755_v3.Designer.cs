﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Template.Migrations
{
    [DbContext(typeof(IspitDbContext))]
    [Migration("20220813224755_v3")]
    partial class v3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Models.Brend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brend");
                });

            modelBuilder.Entity("Models.Komponenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Brend")
                        .HasColumnType("int");

                    b.Property<int>("Tip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Komponente");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prodavnice");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("Cena")
                        .HasColumnType("float");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int>("Komponenta")
                        .HasColumnType("int");

                    b.Property<int>("Prodavnica")
                        .HasColumnType("int");

                    b.Property<int?>("ProdavnicaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdavnicaId");

                    b.ToTable("Spojevi");
                });

            modelBuilder.Entity("Models.Tip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tip");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.HasOne("Models.Prodavnica", null)
                        .WithMany("Komponente")
                        .HasForeignKey("ProdavnicaId");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Navigation("Komponente");
                });
#pragma warning restore 612, 618
        }
    }
}
