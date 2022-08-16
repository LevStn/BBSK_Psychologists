using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface ISearchByFilter
    {
        Task<List<Psychologist>> GetPsychologistsByParametrs(Price price, List<int> problems, Gender? gender, List<Psychologist> psychologists);
    }
}