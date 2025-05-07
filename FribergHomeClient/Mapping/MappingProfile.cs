using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;

namespace FribergHomeClient.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PropertyDTO, PropertyFormViewModel>().ReverseMap();
            CreateMap<ApplicationDTO, ApplicationViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ReverseMap();

        }
        
    }
}
