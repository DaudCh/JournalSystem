﻿// <auto-generated />
using System;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JournalSystem.Repositories.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250326094630_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JournalSystem.Core.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.CostCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostCenterID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CostCenter");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrencyCode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.Dimension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DimensionID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dimensions");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.JournalEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<int?>("CurrencyId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("ExchangeRate")
                        .HasColumnType("real");

                    b.Property<int>("JournalNumber")
                        .HasColumnType("int");

                    b.Property<string>("JournalType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("CurrencyId1");

                    b.ToTable("JournalEntries");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.JournalLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("AccountId1")
                        .HasColumnType("int");

                    b.Property<int>("CostCenterId")
                        .HasColumnType("int");

                    b.Property<int?>("CostCenterId1")
                        .HasColumnType("int");

                    b.Property<float>("Credit")
                        .HasColumnType("real");

                    b.Property<float>("Debit")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DimensionId")
                        .HasColumnType("int");

                    b.Property<int?>("DimensionId1")
                        .HasColumnType("int");

                    b.Property<int>("JournalEntryId")
                        .HasColumnType("int");

                    b.Property<int>("LineNumber")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountId1");

                    b.HasIndex("CostCenterId");

                    b.HasIndex("CostCenterId1");

                    b.HasIndex("DimensionId");

                    b.HasIndex("DimensionId1");

                    b.HasIndex("JournalEntryId");

                    b.ToTable("JournalLines");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.JournalEntry", b =>
                {
                    b.HasOne("JournalSystem.Core.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JournalSystem.Core.Entities.Currency", null)
                        .WithMany("JournalEntries")
                        .HasForeignKey("CurrencyId1");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.JournalLine", b =>
                {
                    b.HasOne("JournalSystem.Core.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JournalSystem.Core.Entities.Account", null)
                        .WithMany("JournalLines")
                        .HasForeignKey("AccountId1");

                    b.HasOne("JournalSystem.Core.Entities.CostCenter", "CostCenter")
                        .WithMany()
                        .HasForeignKey("CostCenterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JournalSystem.Core.Entities.CostCenter", null)
                        .WithMany("JournalLines")
                        .HasForeignKey("CostCenterId1");

                    b.HasOne("JournalSystem.Core.Entities.Dimension", "Dimension")
                        .WithMany()
                        .HasForeignKey("DimensionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JournalSystem.Core.Entities.Dimension", null)
                        .WithMany("JournalLines")
                        .HasForeignKey("DimensionId1");

                    b.HasOne("JournalSystem.Core.Entities.JournalEntry", "JournalEntry")
                        .WithMany("JournalLines")
                        .HasForeignKey("JournalEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("CostCenter");

                    b.Navigation("Dimension");

                    b.Navigation("JournalEntry");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.Account", b =>
                {
                    b.Navigation("JournalLines");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.CostCenter", b =>
                {
                    b.Navigation("JournalLines");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.Currency", b =>
                {
                    b.Navigation("JournalEntries");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.Dimension", b =>
                {
                    b.Navigation("JournalLines");
                });

            modelBuilder.Entity("JournalSystem.Core.Entities.JournalEntry", b =>
                {
                    b.Navigation("JournalLines");
                });
#pragma warning restore 612, 618
        }
    }
}
