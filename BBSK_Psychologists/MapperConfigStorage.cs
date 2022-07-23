using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;

namespace BBSK_Psycho;

public class MapperConfigStorage: Profile
{
	public MapperConfigStorage()
	{
		CreateMap<ClientRegisterRequest, Client>();
		CreateMap<Client, ClientResponse>();
		CreateMap<Comment, CommentResponse>();

		CreateMap<ApplicationForPsychologistSearchRequest, ApplicationForPsychologistSearch>();
		CreateMap<ApplicationForPsychologistSearch, ApplicationForPsychologistSearchResponse>();

		CreateMap<ApplicationForPsychologistSearchUpdateRequest, ApplicationForPsychologistSearch>();
		CreateMap<ApplicationForPsychologistSearch, ApplicationForPsychologistSearchUpdateRequest>();




	}

}
