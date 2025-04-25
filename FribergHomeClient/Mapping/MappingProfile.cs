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

        }
        
    }
}
