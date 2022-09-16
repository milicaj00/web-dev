﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Template.Migrations
{
    [DbContext(typeof(IspitDbContext))]
    partial class IspitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Models.Artikal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrendId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artikli");
                });

            modelBuilder.Entity("Models.Brend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProdavnicaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdavnicaId");

                    b.ToTable("Brendovi");
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

                    b.Property<int?>("ArtikalId")
                        .HasColumnType("int");

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<int>("L")
                        .HasColumnType("int");

                    b.Property<int>("M")
                        .HasColumnType("int");

                    b.Property<int?>("ProdavnicaId")
                        .HasColumnType("int");

                    b.Property<int>("S")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtikalId");

                    b.HasIndex("ProdavnicaId");

                    b.ToTable("ProdavnicaArtikal");
                });

            modelBuilder.Entity("Models.Brend", b =>
                {
                    b.HasOne("Models.Prodavnica", null)
                        .WithMany("Brendovi")
                        .HasForeignKey("ProdavnicaId");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.HasOne("Models.Artikal", "Artikal")
                        .WithMany()
                        .HasForeignKey("ArtikalId");

                    b.HasOne("Models.Prodavnica", "Prodavnica")
                        .WithMany("Artikli")
                        .HasForeignKey("ProdavnicaId");

                    b.Navigation("Artikal");

                    b.Navigation("Prodavnica");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Navigation("Artikli");

                    b.Navigation("Brendovi");
                });
#pragma warning restore 612, 618
        }
    }
}
