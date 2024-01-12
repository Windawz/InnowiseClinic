using FluentMigrator;
using InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Migrations;

[Migration(20240112143500)]
public class Initial : Migration
{
    private readonly IEntityMetadataProvider _entityMetadataProvider;

    public Initial(IEntityMetadataProvider entityMetadataProvider)
    {
        _entityMetadataProvider = entityMetadataProvider;
    }

    public override void Up()
    {
        Create.Table(_entityMetadataProvider.GetTableName<PatientEntity>())
            .WithColumn(nameof(PatientEntity.Id))
                .AsGuid()
                .NotNullable()
                .PrimaryKey()
                .Identity()
            .WithColumn(nameof(PatientEntity.AccountId))
                .AsGuid()
                .NotNullable()
            .WithColumn(nameof(PatientEntity.FirstName))
                .AsString()
                .NotNullable()
            .WithColumn(nameof(PatientEntity.LastName))
                .AsString()
                .NotNullable()
            .WithColumn(nameof(PatientEntity.MiddleName))
                .AsString()
                .Nullable()
            .WithColumn(nameof(PatientEntity.PhoneNumber))
                .AsString()
                .NotNullable()
            .WithColumn(nameof(PatientEntity.DateOfBirth))
                .AsDate()
                .NotNullable();

        Create.Table(_entityMetadataProvider.GetTableName<DoctorEntity>())
            .WithColumn(nameof(DoctorEntity.Id))
                .AsGuid()
                .NotNullable()
                .PrimaryKey()
                .Identity()
            .WithColumn(nameof(DoctorEntity.AccountId))
                .AsGuid()
                .NotNullable()
            .WithColumn(nameof(DoctorEntity.OfficeId))
                .AsGuid()
                .NotNullable()
            .WithColumn(nameof(DoctorEntity.SpecializationId))
                .AsGuid()
                .NotNullable()
            .WithColumn(nameof(DoctorEntity.FirstName))
                .AsString()
                .NotNullable()
            .WithColumn(nameof(DoctorEntity.LastName))
                .AsString()
                .NotNullable()
            .WithColumn(nameof(DoctorEntity.MiddleName))
                .AsString()
                .Nullable()
            .WithColumn(nameof(DoctorEntity.DateOfBirth))
                .AsDate()
                .NotNullable()
            .WithColumn(nameof(DoctorEntity.CareerStartYear))
                .AsInt32()
                .NotNullable()
            .WithColumn(nameof(DoctorEntity.Status))
                .AsInt32()
                .NotNullable();

        Create.Table(_entityMetadataProvider.GetTableName<ReceptionistEntity>())
            .WithColumn(nameof(ReceptionistEntity.Id))
                .AsGuid()
                .NotNullable()
                .PrimaryKey()
                .Identity()
            .WithColumn(nameof(ReceptionistEntity.AccountId))
                .AsGuid()
                .NotNullable()
            .WithColumn(nameof(ReceptionistEntity.OfficeId))
                .AsGuid()
                .NotNullable()
            .WithColumn(nameof(ReceptionistEntity.FirstName))
                .AsString()
                .NotNullable()
            .WithColumn(nameof(ReceptionistEntity.LastName))
                .AsString()
                .NotNullable()
            .WithColumn(nameof(ReceptionistEntity.MiddleName))
                .AsString()
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table(_entityMetadataProvider.GetTableName<ReceptionistEntity>());
        Delete.Table(_entityMetadataProvider.GetTableName<DoctorEntity>());
        Delete.Table(_entityMetadataProvider.GetTableName<PatientEntity>());
    }
}