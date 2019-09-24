﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicPlayer.Data;

namespace MusicPlayer.Data.Migrations
{
    [DbContext(typeof(MusicPlayerContext))]
    [Migration("20190923211154_ChangedImagePathAlbumProperty")]
    partial class ChangedImagePathAlbumProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicPlayer.Data.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("ImagePaths")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicPlayer.Data.Entities.SongInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId");

                    b.Property<string>("Author")
                        .HasMaxLength(300);

                    b.Property<string>("BitRate")
                        .HasMaxLength(20);

                    b.Property<DateTime>("Created");

                    b.Property<TimeSpan>("Duration");

                    b.Property<string>("Extension")
                        .HasMaxLength(5);

                    b.Property<string>("Genre")
                        .HasMaxLength(100);

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name")
                        .HasMaxLength(300);

                    b.Property<string>("RelativePath")
                        .HasMaxLength(300);

                    b.Property<int>("TimesPlayed");

                    b.Property<int>("TrackNo");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("SongInfos");
                });

            modelBuilder.Entity("MusicPlayer.Data.Entities.SongInfo", b =>
                {
                    b.HasOne("MusicPlayer.Data.Entities.Album", "Album")
                        .WithMany("SongInfos")
                        .HasForeignKey("AlbumId");
                });
#pragma warning restore 612, 618
        }
    }
}
