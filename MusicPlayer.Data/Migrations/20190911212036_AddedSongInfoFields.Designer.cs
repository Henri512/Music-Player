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
    [Migration("20190911212036_AddedSongInfoFields")]
    partial class AddedSongInfoFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicPlayer.Models.SongInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Album")
                        .HasMaxLength(100);

                    b.Property<string>("Author")
                        .HasMaxLength(100);

                    b.Property<int>("BitRate");

                    b.Property<TimeSpan>("Duration");

                    b.Property<string>("Extension")
                        .HasMaxLength(5);

                    b.Property<string>("Genre")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("RelativePath")
                        .HasMaxLength(255);

                    b.Property<int>("TimesPlayed");

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.ToTable("SongInfos");

                    b.HasData(
                        new { Id = 1, Album = "This Side Of The Big River", Author = "Chip Taylor", BitRate = 798, Duration = new TimeSpan(0, 0, 3, 17, 0), Extension = "FLAC", Genre = "Country", Name = "01 - Same Ol' Story ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0, Year = new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 2, Album = "This Side Of The Big River", Author = "Chip Taylor", BitRate = 694, Duration = new TimeSpan(0, 0, 2, 47, 0), Extension = "FLAC", Genre = "Country", Name = "02 - Holding Me Together ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0, Year = new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 3, Album = "This Side Of The Big River", Author = "Chip Taylor", BitRate = 845, Duration = new TimeSpan(0, 0, 3, 24, 0), Extension = "FLAC", Genre = "Country", Name = "03 - Gettin' Older, Lookin' Back ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0, Year = new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 4, Album = "This Side Of The Big River", Author = "Chip Taylor", BitRate = 751, Duration = new TimeSpan(0, 0, 5, 30, 0), Extension = "FLAC", Genre = "Country", Name = "04 - John Tucker's On The Wagon Again ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0, Year = new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 5, Album = "This Side Of The Big River", Author = "Chip Taylor", BitRate = 901, Duration = new TimeSpan(0, 0, 3, 17, 0), Extension = "FLAC", Genre = "Country", Name = "05 - Big River ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0, Year = new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 6, Album = "This Side Of The Big River", Author = "Chip Taylor", BitRate = 725, Duration = new TimeSpan(0, 0, 4, 8, 0), Extension = "FLAC", Genre = "Country", Name = "06 - May God Be With Me ", PhysicalPath = "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", TimesPlayed = 0, Year = new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
