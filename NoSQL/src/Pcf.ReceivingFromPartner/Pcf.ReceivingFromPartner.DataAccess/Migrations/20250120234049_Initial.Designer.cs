﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pcf.ReceivingFromPartner.DataAccess;

#nullable disable

namespace Pcf.ReceivingFromPartner.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250120234049_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Pcf.ReceivingFromPartner.Core.Domain.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PartnerId");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<int>("NumberIssuedPromoCodes")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("Pcf.ReceivingFromPartner.Core.Domain.PartnerPromoCodeLimit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PartnerPromocodeLimitId");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Limit")
                        .HasColumnType("integer");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.ToTable("PartnerPromoCodeLimits");
                });

            modelBuilder.Entity("Pcf.ReceivingFromPartner.Core.Domain.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PromoCodeId");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Code")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PartnerManagerId")
                        .HasColumnType("uuid");

                    b.Property<int>("PreferenceId")
                        .HasColumnType("integer");

                    b.Property<string>("PreferenceName")
                        .HasColumnType("text");

                    b.Property<string>("ServiceInfo")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.ToTable("PromoCode");
                });

            modelBuilder.Entity("Pcf.ReceivingFromPartner.Core.Domain.PartnerPromoCodeLimit", b =>
                {
                    b.HasOne("Pcf.ReceivingFromPartner.Core.Domain.Partner", "Partner")
                        .WithMany("PartnerLimits")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Pcf.ReceivingFromPartner.Core.Domain.PromoCode", b =>
                {
                    b.HasOne("Pcf.ReceivingFromPartner.Core.Domain.Partner", "Partner")
                        .WithMany("PromoCodes")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Pcf.ReceivingFromPartner.Core.Domain.Partner", b =>
                {
                    b.Navigation("PartnerLimits");

                    b.Navigation("PromoCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
