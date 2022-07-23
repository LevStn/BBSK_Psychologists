using BBSK_Psycho.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IPsychologistServices
    {
        Psychologist? GetPsychologist(int id);
        List<Psychologist> GetAllPsychologists();
        public Comment AddCommentToPsyhologist(Comment comment, int psychologistId);
        List<Comment> GetCommentsByPsychologistId(int id);
        List <Order> GetOrdersByPsychologistId(int id);
        int AddPsychologist(Psychologist psychologist);
        void UpdatePsychologist(Psychologist psychologist, int id);
        void DeletePsychologist(int id);
    }
}
