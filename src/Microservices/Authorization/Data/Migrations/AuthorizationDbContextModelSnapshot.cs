﻿// <auto-generated />
using System;
using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InnowiseClinic.Microservices.Authorization.Data.Migrations
{
    [DbContext(typeof(AuthorizationDbContext))]
    partial class AuthorizationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InnowiseClinic.Microservices.Shared.Data.Entities.Entity", b =>
                {
                    b.Property<Guid>("Id")
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
                            Id = new Guid("a8275ec5-2367-49fe-b824-a753e2ad3302"),
                            CreatedAt = new DateTime(2023, 12, 22, 8, 36, 31, 129, DateTimeKind.Utc).AddTicks(5290),
                            Email = "admin@mydomain.com",
                            IsEmailVerified = true,
                            Password = "AQAAAAIAAYagAAAAEOuknF0esqDT2vsfd0OkImuBWb6Yidjhzgheno+c6P23+IYeyPBF2C5Ml9bj4Sj0Yg==",
                            Role = "receptionist"
                        });
                });

            modelBuilder.Entity("InnowiseClinic.Microservices.Authorization.Data.Entities.RefreshTokenEntity", b =>
                {
                    b.HasBaseType("InnowiseClinic.Microservices.Shared.Data.Entities.Entity");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
