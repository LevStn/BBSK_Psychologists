using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.Models;

namespace BBSK_Psycho;

public class MapperConfigStorage
{

    private static Mapper _instance;

    public static Mapper GetInstanse()
    {
        if(_instance is null)
            InitMapperConfigStorage();
        return _instance;
    }

    private static void InitMapperConfigStorage()
    {
        _instance = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ClientRegisterRequest, Client>();
        }));
    }
}
