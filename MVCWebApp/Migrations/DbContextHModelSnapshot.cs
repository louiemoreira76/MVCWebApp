﻿// <auto-generated />
using System;
using MVCWebApp.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCWebApp.Migrations
{
    [DbContext(typeof(DbContextH))]
    partial class DbContextHModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MVCWebApp.Models.Entities.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Cep")
                        .HasColumnType("int");

                    b.Property<int>("Cnpj")
                        .HasColumnType("int");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte>("Segmento")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });
#pragma warning restore 612, 618
        }
    }
}
