
using BBSK_Psycho.Models;
using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.Configuration
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<OrderCreateRequest, Order>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
                //.ForMember("IsDeleted", o => )
        }
    }
}
