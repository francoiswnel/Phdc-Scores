﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhdcScores.Shared.Data;

#nullable disable

namespace PhdcScores.Shared.Data.Migrations
{
    [DbContext(typeof(DataContext.DataContext))]
    [Migration("20230402165718_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("PhdcScores.Shared.Common.Entities.LeagueStanding", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LeagueStandings");
                });

            modelBuilder.Entity("PhdcScores.Shared.Common.Entities.MatchScore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AwayGoals")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AwayTeamName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HomeGoals")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HomeTeamName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MatchScores");
                });
#pragma warning restore 612, 618
        }
    }
}
