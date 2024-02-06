using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Data.Documents;

public static class DataToApplicationMap
{
    private static readonly IReadOnlyDictionary<Type, ProfileType> _profileTypeMap = new Dictionary<Type, ProfileType>
    {
        [typeof(PatientProfile)] = ProfileType.Patient,
        [typeof(DoctorProfile)] = ProfileType.Doctor,
        [typeof(ReceptionistProfile)] = ProfileType.Receptionist,
    };
        
    public static TProfile ToProfile<TProfile>(ProfileDocument document) where TProfile : Profile
    {
        if (!_profileTypeMap.TryGetValue(typeof(TProfile), out var profileType))
        {
            ThrowUnmappedProfileTypeException(typeof(TProfile));
        }

        if (document.Type != profileType)
        {
            throw new ArgumentException(
                paramName: nameof(document),
                message: $"May not map profile document of type {Enum.GetName(document.Type)} "
                    + $"to {typeof(TProfile)} requiring document type {Enum.GetName(profileType)}");
        }

        Profile profile = null!;

        switch (profileType)
        {
            case ProfileType.Patient:
                profile = ToPatientProfile(document);
                break;

            case ProfileType.Doctor:
                profile = ToDoctorProfile(document);
                break;

            case ProfileType.Receptionist:
                profile = ToReceptionistProfile(document);
                break;

            default:
                throw new InvalidOperationException($"Unhandled value {profileType} of enum {profileType.GetType()}");
        }

        Entity.SetId(profile, document.Id);

        return (TProfile)profile;
    }

    public static ProfileDocument ToDocument<TProfile>(TProfile profile) where TProfile : Profile
    {
        if (!_profileTypeMap.TryGetValue(typeof(TProfile), out var profileType))
        {
            ThrowUnmappedProfileTypeException(typeof(TProfile));
        }

        var typelessProfile = (Profile)profile;
        ProfileDocument document = null!;

        switch (profileType)
        {
            case ProfileType.Patient:
                document = ToPatientProfileDocument((PatientProfile)typelessProfile);
                break;
            
            case ProfileType.Doctor:
                document = ToDoctorProfileDocument((DoctorProfile)typelessProfile);
                break;

            case ProfileType.Receptionist:
                document = ToReceptionistProfileDocument((ReceptionistProfile)typelessProfile);
                break;

            default:
                throw new InvalidOperationException($"Unhandled value {profileType} of enum {profileType.GetType()}");
        }

        return document;
    }

    private static void ThrowUnmappedProfileTypeException(Type profileType)
    {
        throw new ArgumentException(message: $"Unmapped profile type {profileType}");
    }

    private static PatientProfile ToPatientProfile(ProfileDocument document)
    {
        return new PatientProfile(
            accountId: document.AccountId,
            name: ExtractName(document),
            phoneNumber: document.PhoneNumber!,
            dateOfBirth: (DateOnly)document.DateOfBirth!);
    }

    private static DoctorProfile ToDoctorProfile(ProfileDocument document)
    {
        return new DoctorProfile(
            accountId: document.AccountId,
            officeId: (Guid)document.OfficeId!,
            specializationId: (Guid)document.SpecializationId!,
            name: ExtractName(document),
            dateOfBirth: (DateOnly)document.DateOfBirth!,
            careerStartYear: (int)document.CareerStartYear!,
            status: (DoctorStatus)document.DoctorStatus!);
    }

    private static ReceptionistProfile ToReceptionistProfile(ProfileDocument document)
    {
        return new ReceptionistProfile(
            accountId: document.AccountId,
            officeId: (Guid)document.OfficeId!,
            name: ExtractName(document));
    }

    private static Name ExtractName(ProfileDocument document)
    {
        return new(document.FirstName, document.LastName, document.MiddleName);
    }

    private static ProfileDocument ToPatientProfileDocument(PatientProfile profile)
    {
        return new ProfileDocument(
            Id: (Guid)profile.Id!,
            AccountId: profile.AccountId,
            OfficeId: null,
            SpecializationId: null,
            Type: ProfileType.Patient,
            FirstName: profile.Name.First,
            LastName: profile.Name.Last,
            MiddleName: profile.Name.Middle,
            PhoneNumber: profile.PhoneNumber,
            DateOfBirth: profile.DateOfBirth,
            CareerStartYear: null,
            DoctorStatus: null);
    }

    private static ProfileDocument ToDoctorProfileDocument(DoctorProfile profile)
    {
        return new ProfileDocument(
            Id: (Guid)profile.Id!,
            AccountId: profile.AccountId,
            OfficeId: profile.OfficeId,
            SpecializationId: profile.SpecializationId,
            Type: ProfileType.Doctor,
            FirstName: profile.Name.First,
            LastName: profile.Name.Last,
            MiddleName: profile.Name.Middle,
            PhoneNumber: null,
            DateOfBirth: profile.DateOfBirth,
            CareerStartYear: profile.CareerStartYear,
            DoctorStatus: (int)profile.Status);
    }

    private static ProfileDocument ToReceptionistProfileDocument(ReceptionistProfile profile)
    {
        return new ProfileDocument(
            Id: (Guid)profile.Id!,
            AccountId: profile.AccountId,
            OfficeId: profile.OfficeId,
            SpecializationId: null,
            Type: ProfileType.Receptionist,
            FirstName: profile.Name.First,
            LastName: profile.Name.Last,
            MiddleName: profile.Name.Middle,
            PhoneNumber: null,
            DateOfBirth: null,
            CareerStartYear: null,
            DoctorStatus: null);
    }
}
