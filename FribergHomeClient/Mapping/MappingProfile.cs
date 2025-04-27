using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using Microsoft.CodeAnalysis;

namespace FribergHomeClient.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PropertyFormViewModel, PropertyDTO>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.ImageUrls != null
                    ? src.ImageUrls.Split(new char[] { ',' }) // ✅ Removed optional parameter
                    .Select(url => new PropertyImageDTO { ImgURL = url.Trim() }).ToList()
                    : new List<PropertyImageDTO>()))
                .ReverseMap()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    string.Join(",", src.ImageUrls.Select(img => img.ImgURL))));



        }

    }
}
