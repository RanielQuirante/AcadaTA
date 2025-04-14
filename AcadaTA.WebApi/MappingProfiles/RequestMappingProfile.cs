using AcadaTA.Models.Dtos.Request;
using AcadaTA.Models.Entities;
using AutoMapper;

namespace AcadaTA.WebApi.MappingProfiles
{
    public class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            CreateMap<ProductEntity, ProductRequestDto>().ReverseMap();
        }
    }
}
