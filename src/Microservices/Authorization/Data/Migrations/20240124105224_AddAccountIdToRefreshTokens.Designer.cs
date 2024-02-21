﻿// <auto-generated />
using System;
using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InnowiseClinic.Microservices.Authorization.Data.Migrations
{
    [DbContext(typeof(AuthorizationDbContext))]
    [Migration("20240124105224_AddAccountIdToRefreshTokens")]
    partial class AddAccountIdToRefreshTokens
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InnowiseClinic.Microservices.Shared.Data.Entities.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("InnowiseClinic.Microservices.Authorization.Data.Entities.AccountEntity", b =>
                {
                    b.HasBaseType("InnowiseClinic.Microservices.Shared.Data.Entities.Entity");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmail")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("71992c68-c246-49d5-a22f-84fae24cba89"),
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@mydomain.com",
                            IsEmailVerified = true,
                            Password = "AQAAAAIAAYagAAAAEAsfZPi+5Ij52HHpQMwi5cOWHuShAyGhZ/QG34onfHAjDUuYYZWM94ARK2EM1LRgwQ==",
                            Role = "receptionist"
                        });
                });

            modelBuilder.Entity("InnowiseClinic.Microservices.Authorization.Data.Entities.RefreshTokenEntity", b =>
                {
                    b.HasBaseType("InnowiseClinic.Microservices.Shared.Data.Entities.Entity");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("InnowiseClinic.Microservices.Authorization.Data.Entities.RefreshTokenEntity", b =>
                {
                    b.HasOne("InnowiseClinic.Microservices.Authorization.Data.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
