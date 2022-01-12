﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Projekat.Migrations
{
    [DbContext(typeof(BibliotekaContext))]
    [Migration("20220112090208_V11")]
    partial class V11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Biblioteka", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("M")
                        .HasColumnType("int");

                    b.Property<int>("MaxKolicina")
                        .HasColumnType("int");

                    b.Property<int>("N")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Biblioteke");
                });

            modelBuilder.Entity("Models.Izdavac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Deskripcija")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Izdavaci");
                });

            modelBuilder.Entity("Models.Knjiga", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Autor")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("BibliotekaOveKnjigeID")
                        .HasColumnType("int");

                    b.Property<int?>("IzdavacID")
                        .HasColumnType("int");

                    b.Property<int>("M")
                        .HasColumnType("int");

                    b.Property<int>("N")
                        .HasColumnType("int");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Opis")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("TrenKolicina")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BibliotekaOveKnjigeID");

                    b.HasIndex("IzdavacID");

                    b.ToTable("Knjige");
                });

            modelBuilder.Entity("Models.Knjiga", b =>
                {
                    b.HasOne("Models.Biblioteka", "BibliotekaOveKnjige")
                        .WithMany("Knjige")
                        .HasForeignKey("BibliotekaOveKnjigeID");

                    b.HasOne("Models.Izdavac", "Izdavac")
                        .WithMany("Knjige")
                        .HasForeignKey("IzdavacID");

                    b.Navigation("BibliotekaOveKnjige");

                    b.Navigation("Izdavac");
                });

            modelBuilder.Entity("Models.Biblioteka", b =>
                {
                    b.Navigation("Knjige");
                });

            modelBuilder.Entity("Models.Izdavac", b =>
                {
                    b.Navigation("Knjige");
                });
#pragma warning restore 612, 618
        }
    }
}
