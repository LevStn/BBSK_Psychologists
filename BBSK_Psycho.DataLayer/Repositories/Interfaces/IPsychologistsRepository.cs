using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public interface IPsychologistsRepository
    {
        Psychologist? GetPsychologist(int id);
    }
}