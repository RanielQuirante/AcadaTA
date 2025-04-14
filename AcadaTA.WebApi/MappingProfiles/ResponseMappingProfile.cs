using AcadaTA.Models.Dtos.Response;
using AcadaTA.Models.Entities;
using AutoMapper;

namespace AcadaTA.WebApi.MappingProfiles
{
    public class ResponseMappingProfile : Profile
    {
        public ResponseMappingProfile()
        {
            CreateMap<ProductEntity, ProductResponseDto>();
        }
    }
}
