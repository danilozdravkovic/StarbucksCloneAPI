using AutoMapper;
using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;

namespace StarbuckClone.API.Profiles
{
    public class ProductCategotyAutoMapperProfile : Profile
    {
        public ProductCategotyAutoMapperProfile()
        {
            CreateMap<CreateProductCategoryDto, ProductCategory>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
