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

            modelBuilder.Entity("Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrojOcena")
                        .HasColumnType("int");

                    b.Property<int>("KategorijaId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProdukcijskaKucaId")
                        .HasColumnType("int");

                    b.Property<double>("ProsecnaOcena")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ProdukcijskaKucaId");

                    b.ToTable("Filmovi");
                });

            modelBuilder.Entity("Models.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProdukcijskaKucaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdukcijskaKucaId");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("Models.ProdukcijskaKuca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProdukcijskaKuca");
                });

            modelBuilder.Entity("Models.Film", b =>
                {
                    b.HasOne("Models.ProdukcijskaKuca", null)
                        .WithMany("Filmovi")
                        .HasForeignKey("ProdukcijskaKucaId");
                });

            modelBuilder.Entity("Models.Kategorija", b =>
                {
                    b.HasOne("Models.ProdukcijskaKuca", null)
                        .WithMany("ListaKategorija")
                        .HasForeignKey("ProdukcijskaKucaId");
                });

            modelBuilder.Entity("Models.ProdukcijskaKuca", b =>
                {
                    b.Navigation("Filmovi");

                    b.Navigation("ListaKategorija");
                });
#pragma warning restore 612, 618
        }
    }
}
