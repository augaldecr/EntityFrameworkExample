﻿// <auto-generated />
using System;
using EntityFrameworkExample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace EntityFrameworkExample.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220220185656_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence<int>("BillNumber", "bill");

            modelBuilder.Entity("CinemaMovie", b =>
                {
                    b.Property<int>("CinemasId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("CinemasId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CinemaMovie");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PhotoURL")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.ActorMovie", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<string>("Character")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("ActorsMovies");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BillNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR bill.BillNumber");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("From")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("From");

                    b.Property<DateTime>("To")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("To");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Bills", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb.UseHistoryTable("BillsHistory");
                            ttb
                                .HasPeriodStart("From")
                                .HasColumnName("From");
                            ttb
                                .HasPeriodEnd("To")
                                .HasColumnName("To");
                        }
                    ));
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.BillDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Product")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComputedColumnSql("Price * Quantity");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("BillsDetails");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CinemaType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("TwoD");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("TheMovieTheater")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheMovieTheater");

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Genre", b =>
                {
                    b.Property<int>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Identifier"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Example")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("PeriodEnd")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodEnd");

                    b.Property<DateTime>("PeriodStart")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodStart");

                    b.Property<string>("UsersCreation")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("UsersModification")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Identifier");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("Deleted = 'false'");

                    b.ToTable("Genres", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("PeriodStart")
                                .HasColumnName("PeriodStart");
                            ttb
                                .HasPeriodEnd("PeriodEnd")
                                .HasColumnName("PeriodEnd");
                        }
                    ));
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Keyless.LocationlessMovieTheater", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.ToView(null);

                    b.ToSqlQuery("Select Id, Name FROM MovieTheaters");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Keyless.MovieWithStats", b =>
                {
                    b.Property<int>("ActorCount")
                        .HasColumnType("int");

                    b.Property<int>("GenresCount")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("MovieTheatersCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Example")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceptorId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceptorId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("OnCinemas")
                        .HasColumnType("bit");

                    b.Property<string>("PosterURL")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.MovieTheater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("MovieTheaters");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.MovieTheaterDetails", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("EthicsCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("History")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Values")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovieTheaters", (string)null);
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.MovieTheaterOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Begins")
                        .HasColumnType("date");

                    b.Property<decimal>("Discount")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTime>("Ends")
                        .HasColumnType("date");

                    b.Property<int?>("MovieTheaterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieTheaterId")
                        .IsUnique()
                        .HasFilter("[MovieTheaterId] IS NOT NULL");

                    b.ToTable("MovieTheaterOffers");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payments");

                    b.HasDiscriminator<int>("PaymentType");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.Property<int>("GenresIdentifier")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("GenresIdentifier", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("GenreMovie");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.CardPayment", b =>
                {
                    b.HasBaseType("EntityFrameworkExample.Entitites.Payment");

                    b.Property<string>("Last4Digits")
                        .IsRequired()
                        .HasColumnType("char(4)");

                    b.HasDiscriminator().HasValue(2);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 500m,
                            Date = new DateTime(2022, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentType = 2,
                            Last4Digits = "0123"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 120m,
                            Date = new DateTime(2022, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentType = 2,
                            Last4Digits = "1234"
                        });
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Merchandising", b =>
                {
                    b.HasBaseType("EntityFrameworkExample.Entitites.Product");

                    b.Property<bool>("Clothes")
                        .HasColumnType("bit");

                    b.Property<bool>("Colectionable")
                        .HasColumnType("bit");

                    b.Property<bool>("OnInventory")
                        .HasColumnType("bit");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.ToTable("Merchandising", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "T-Shirt One Piece",
                            Price = 11m,
                            Clothes = true,
                            Colectionable = false,
                            OnInventory = true,
                            Volume = 1.0,
                            Weight = 1.0
                        });
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.PaypalPayment", b =>
                {
                    b.HasBaseType("EntityFrameworkExample.Entitites.Payment");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasDiscriminator().HasValue(1);

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Amount = 157m,
                            Date = new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentType = 1,
                            Email = "felipe@hotmail.com"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 9.99m,
                            Date = new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentType = 1,
                            Email = "claudia@hotmail.com"
                        });
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.RentableMovie", b =>
                {
                    b.HasBaseType("EntityFrameworkExample.Entitites.Product");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.ToTable("RentableMovie", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Spider-Man",
                            Price = 5.99m,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("CinemaMovie", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.Cinema", null)
                        .WithMany()
                        .HasForeignKey("CinemasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFrameworkExample.Entitites.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Actor", b =>
                {
                    b.OwnsOne("EntityFrameworkExample.Entitites.Address", "BillingAddress", b1 =>
                        {
                            b1.Property<int>("ActorId")
                                .HasColumnType("int");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Province")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ActorId");

                            b1.ToTable("Actors");

                            b1.WithOwner()
                                .HasForeignKey("ActorId");
                        });

                    b.OwnsOne("EntityFrameworkExample.Entitites.Address", "HomeAddress", b1 =>
                        {
                            b1.Property<int>("ActorId")
                                .HasColumnType("int");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.Property<string>("Province")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Province");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("ActorId");

                            b1.ToTable("Actors");

                            b1.WithOwner()
                                .HasForeignKey("ActorId");
                        });

                    b.Navigation("BillingAddress");

                    b.Navigation("HomeAddress");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.ActorMovie", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.Actor", "Actor")
                        .WithMany("ActorsMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFrameworkExample.Entitites.Movie", "Movie")
                        .WithMany("ActorsMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.BillDetails", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.Bill", null)
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Cinema", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.MovieTheater", "MovieTheater")
                        .WithMany("Cinemas")
                        .HasForeignKey("TheMovieTheater")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieTheater");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Message", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.Person", "Receptor")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceptorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFrameworkExample.Entitites.Person", "Sender")
                        .WithMany("SendedMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receptor");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.MovieTheater", b =>
                {
                    b.OwnsOne("EntityFrameworkExample.Entitites.Address", "Address", b1 =>
                        {
                            b1.Property<int>("MovieTheaterId")
                                .HasColumnType("int");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.Property<string>("Province")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Province");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("MovieTheaterId");

                            b1.ToTable("MovieTheaters");

                            b1.WithOwner()
                                .HasForeignKey("MovieTheaterId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.MovieTheaterDetails", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.MovieTheater", "MovieTheater")
                        .WithOne("MovieTheaterDetails")
                        .HasForeignKey("EntityFrameworkExample.Entitites.MovieTheaterDetails", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieTheater");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.MovieTheaterOffer", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.MovieTheater", null)
                        .WithOne("MovieTheaterOffer")
                        .HasForeignKey("EntityFrameworkExample.Entitites.MovieTheaterOffer", "MovieTheaterId");
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFrameworkExample.Entitites.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Merchandising", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.Product", null)
                        .WithOne()
                        .HasForeignKey("EntityFrameworkExample.Entitites.Merchandising", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.RentableMovie", b =>
                {
                    b.HasOne("EntityFrameworkExample.Entitites.Product", null)
                        .WithOne()
                        .HasForeignKey("EntityFrameworkExample.Entitites.RentableMovie", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Actor", b =>
                {
                    b.Navigation("ActorsMovies");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Movie", b =>
                {
                    b.Navigation("ActorsMovies");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.MovieTheater", b =>
                {
                    b.Navigation("Cinemas");

                    b.Navigation("MovieTheaterDetails");

                    b.Navigation("MovieTheaterOffer");
                });

            modelBuilder.Entity("EntityFrameworkExample.Entitites.Person", b =>
                {
                    b.Navigation("ReceivedMessages");

                    b.Navigation("SendedMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
