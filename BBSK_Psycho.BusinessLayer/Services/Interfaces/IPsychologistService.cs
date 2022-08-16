using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IPsychologistService
    {
        Task <Psychologist?> GetPsychologist(int id, ClaimModel claim);
        Task <List<Psychologist>> GetAllPsychologists(ClaimModel claim);
        Task <int> AddCommentToPsyhologist(Comment comment, int psychologistId, ClaimModel claim);
        Task <List<Comment>> GetCommentsByPsychologistId(int id, ClaimModel claim);
        Task <List <Order>> GetOrdersByPsychologistId(int id, ClaimModel claim);
        Task <int> AddPsychologist(Psychologist psychologist);
        Task UpdatePsychologist(Psychologist psychologist, int id, ClaimModel claim);
        Task DeletePsychologist(int id, ClaimModel claim);

        Task<List<Psychologist>> GetPsychologistsByParametrs(Price price, List<int> problems, Gender? gender);
    }
}
