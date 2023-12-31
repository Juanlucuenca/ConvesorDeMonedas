﻿// <auto-generated />
using System;
using ConvesorDeMonedas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConvesorDeMonedas.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231031215738_InitialMigration2")]
    partial class InitialMigration2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("ConvesorDeMonedas.Models.Conversion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("FromCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FromCurrencyId");

                    b.HasIndex("ToCurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Conversions");
                });

            modelBuilder.Entity("ConvesorDeMonedas.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Ic")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ic = 0.002,
                            Name = "ARS",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 2,
                            Ic = 1.0900000000000001,
                            Name = "EUR",
                            Symbol = "€"
                        },
                        new
                        {
                            Id = 3,
                            Ic = 0.19700000000000001,
                            Name = "BRL",
                            Symbol = "R$"
                        });
                });

            modelBuilder.Entity("ConvesorDeMonedas.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmoutOfConvertions")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AmoutOfConvertions = 3,
                            Name = "Free",
                            Price = 0
                        },
                        new
                        {
                            Id = 2,
                            AmoutOfConvertions = 8,
                            Name = "Trial",
                            Price = 5
                        },
                        new
                        {
                            Id = 3,
                            AmoutOfConvertions = 15,
                            Name = "Pro",
                            Price = 10
                        });
                });

            modelBuilder.Entity("ConvesorDeMonedas.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConvertionsCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConvertionsCount = 0,
                            Mail = "juan@gmail.com",
                            Password = "pepe123",
                            Role = 1,
                            SubscriptionId = 1,
                            UserName = "Juan"
                        },
                        new
                        {
                            Id = 7,
                            ConvertionsCount = 0,
                            Mail = "pedro@gmail.com",
                            Password = "pelito123567",
                            Role = 1,
                            SubscriptionId = 2,
                            UserName = "Pedro"
                        },
                        new
                        {
                            Id = 9,
                            ConvertionsCount = 0,
                            Mail = "marta@gmail.com",
                            Password = "martalamejor",
                            Role = 1,
                            SubscriptionId = 3,
                            UserName = "Marta"
                        });
                });

            modelBuilder.Entity("ConvesorDeMonedas.Models.Conversion", b =>
                {
                    b.HasOne("ConvesorDeMonedas.Models.Currency", "FromCurrency")
                        .WithMany()
                        .HasForeignKey("FromCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConvesorDeMonedas.Models.Currency", "ToCurrency")
                        .WithMany()
                        .HasForeignKey("ToCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConvesorDeMonedas.Models.User", "User")
                        .WithMany("Conversions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromCurrency");

                    b.Navigation("ToCurrency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConvesorDeMonedas.Models.User", b =>
                {
                    b.HasOne("ConvesorDeMonedas.Models.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("ConvesorDeMonedas.Models.Subscription", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ConvesorDeMonedas.Models.User", b =>
                {
                    b.Navigation("Conversions");
                });
#pragma warning restore 612, 618
        }
    }
}
