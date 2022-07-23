using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.Models;

namespace BBSK_Psycho;

public class MapperConfigStorage: Profile
{
	public MapperConfigStorage()
	{
		CreateMap<ClientRegisterRequest, Client>();
		CreateMap<Client, ClientResponse>();
		CreateMap<Comment, CommentResponse>();

        CreateMap<OrderCreateRequest, Order>();

        CreateMap<OrderCreateRequest, Client>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId));
        CreateMap<OrderCreateRequest, Psychologist>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PsychologistId));
    }
}
