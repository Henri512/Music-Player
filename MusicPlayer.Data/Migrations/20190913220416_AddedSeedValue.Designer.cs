﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MusicPlayer.Data.Migrations
{
    [DbContext(typeof(MusicPlayerContext))]
    [Migration("20190913220416_AddedSeedValue")]
    partial class AddedSeedValue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicPlayer.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.ToTable("Albums");

                    b.HasData(
                        new { Id = 1, Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), LastModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "This Side Of The Big River", Year = new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("MusicPlayer.Models.SongInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId");

                    b.Property<string>("Author")
                        .HasMaxLength(100);

                    b.Property<int>("BitRate");

                    b.Property<DateTime>("Created");

                    b.Property<TimeSpan>("Duration");

                    b.Property<string>("Extension")
                        .HasMaxLength(5);

                    b.Property<string>("Genre")
                        .HasMaxLength(30);

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("RelativePath")
                        .HasMaxLength(255);

                    b.Property<int>("TimesPlayed");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("SongInfos");

                    b.HasData(
                        new { Id = 1, AlbumId = 1, Author = "Chip Taylor", BitRate = 798, Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 0, 3, 17, 0), Extension = "FLAC", Genre = "Country", LastModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "01 - Same Ol' Story ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0 },
                        new { Id = 2, AlbumId = 1, Author = "Chip Taylor", BitRate = 694, Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 0, 2, 47, 0), Extension = "FLAC", Genre = "Country", LastModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "02 - Holding Me Together ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0 },
                        new { Id = 3, AlbumId = 1, Author = "Chip Taylor", BitRate = 845, Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 0, 3, 24, 0), Extension = "FLAC", Genre = "Country", LastModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "03 - Gettin' Older, Lookin' Back ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0 },
                        new { Id = 4, AlbumId = 1, Author = "Chip Taylor", BitRate = 751, Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 0, 5, 30, 0), Extension = "FLAC", Genre = "Country", LastModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "04 - John Tucker's On The Wagon Again ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0 },
                        new { Id = 5, AlbumId = 1, Author = "Chip Taylor", BitRate = 901, Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 0, 3, 17, 0), Extension = "FLAC", Genre = "Country", LastModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "05 - Big River ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0 },
                        new { Id = 6, AlbumId = 1, Author = "Chip Taylor", BitRate = 725, Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Duration = new TimeSpan(0, 0, 4, 8, 0), Extension = "FLAC", Genre = "Country", LastModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "06 - May God Be With Me ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0 }
                    );
                });

            modelBuilder.Entity("MusicPlayer.Models.SongInfo", b =>
                {
                    b.HasOne("MusicPlayer.Models.Album", "Album")
                        .WithMany("SongInfos")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
