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
    [Migration("20220813220414_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Models.Komponenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Brend")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tip")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int?>("KomponentaId")
                        .HasColumnType("int");

                    b.Property<int?>("ProdavnicaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KomponentaId");

                    b.HasIndex("ProdavnicaId");

                    b.ToTable("Spojevi");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.HasOne("Models.Komponenta", "Komponenta")
                        .WithMany()
                        .HasForeignKey("KomponentaId");

                    b.HasOne("Models.Prodavnica", "Prodavnica")
                        .WithMany("Komponente")
                        .HasForeignKey("ProdavnicaId");

                    b.Navigation("Komponenta");

                    b.Navigation("Prodavnica");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Navigation("Komponente");
                });
#pragma warning restore 612, 618
        }
    }
}
