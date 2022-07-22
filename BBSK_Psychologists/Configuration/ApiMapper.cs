
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
            //.ForMember(dest => dest.Client.Id, opt => opt.MapFrom(src => src.ClientId))
            //.ForMember(dest => dest.Psychologist.Id, opt => opt.MapFrom(src => src.PsychologistId));

            CreateMap<OrderCreateRequest, Client>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId));
            CreateMap<OrderCreateRequest, Psychologist>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PsychologistId));
        }
    }
}
