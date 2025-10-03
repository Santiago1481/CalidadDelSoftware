using AutoMapper;
using Entity.Dtos.Business.Attendants;
using Entity.Model.Business;

namespace Utilities.MappersApp.Business
{
    public class AttendantsMap : Profile
    {
        public AttendantsMap()
        {
            // Mapeo de Rol a RolDto y viceversa
            CreateMap<Attendants, AttendantsDto>().ReverseMap();

            CreateMap<Attendants, AttendantsQueryDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(t => $"{t.Person.FisrtName} {t.Person.LastName}"))
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(t => t.Person.DocumentTypeId))
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(t => t.Person.Identification))
                .ForMember(dest => dest.AcronymDocument, opt => opt.MapFrom(t => t.Person.DocumentType.Acronym))
                .ReverseMap();
        }
    }
}