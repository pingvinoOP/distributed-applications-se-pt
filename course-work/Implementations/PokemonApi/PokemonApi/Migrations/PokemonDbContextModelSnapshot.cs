﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonApi.Data;

#nullable disable

namespace PokemonApi.Migrations
{
    [DbContext(typeof(PokemonDbContext))]
    partial class PokemonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PokemonApi.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Accuracy")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("MoveName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PP")
                        .HasColumnType("int");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("PokemonApi.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Attack")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Defense")
                        .HasColumnType("bigint");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PokemonTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokemonTypeId");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("PokemonApi.Models.PokemonMove", b =>
                {
                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("MoveId")
                        .HasColumnType("int");

                    b.HasKey("PokemonId", "MoveId");

                    b.HasIndex("MoveId");

                    b.ToTable("PokemonMoves");
                });

            modelBuilder.Entity("PokemonApi.Models.PokemonType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Resistance")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Weakness")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("PokemonTypes");
                });

            modelBuilder.Entity("PokemonApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2025, 3, 14, 23, 48, 10, 68, DateTimeKind.Utc).AddTicks(5126),
                            Email = "admin@pokemonapi.com",
                            IsActive = true,
                            PasswordHash = "$2a$11$fC9rYjAs8y2sePZtlHXMBOuhvgccKAVGECJIj0NWb6dT/vwPdw25O",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("PokemonApi.Models.Pokemon", b =>
                {
                    b.HasOne("PokemonApi.Models.PokemonType", "PokemonType")
                        .WithMany("Pokemon")
                        .HasForeignKey("PokemonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PokemonType");
                });

            modelBuilder.Entity("PokemonApi.Models.PokemonMove", b =>
                {
                    b.HasOne("PokemonApi.Models.Move", "Move")
                        .WithMany("PokemonMoves")
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonApi.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonMoves")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Move");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("PokemonApi.Models.Move", b =>
                {
                    b.Navigation("PokemonMoves");
                });

            modelBuilder.Entity("PokemonApi.Models.Pokemon", b =>
                {
                    b.Navigation("PokemonMoves");
                });

            modelBuilder.Entity("PokemonApi.Models.PokemonType", b =>
                {
                    b.Navigation("Pokemon");
                });
#pragma warning restore 612, 618
        }
    }
}
