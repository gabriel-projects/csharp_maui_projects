﻿// <auto-generated />
using System;
using Api.GRRInnovations.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.GRRInnovations.Infrastructure.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20240407171336_adicionando campo nome")]
    partial class adicionandocamponome
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Api.GRRInnovations.Domain.Models.Schedule", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAllDay")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Subject")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Api.GRRInnovations.Domain.Models.ScheduledAppointment", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ParentUid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.HasIndex("ParentUid");

                    b.ToTable("ScheduledAppointments");
                });

            modelBuilder.Entity("Api.GRRInnovations.Domain.Models.ScheduledAppointment", b =>
                {
                    b.HasOne("Api.GRRInnovations.Domain.Models.Schedule", "DbParent")
                        .WithMany("DbAppointments")
                        .HasForeignKey("ParentUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbParent");
                });

            modelBuilder.Entity("Api.GRRInnovations.Domain.Models.Schedule", b =>
                {
                    b.Navigation("DbAppointments");
                });
#pragma warning restore 612, 618
        }
    }
}
