using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;

namespace BBSK_Psycho.BusinessLayer
{
    public class PsychologistService : IPsychologistServices
    {
        private readonly IPsychologistsRepository _psychologistsRepository;

        public PsychologistService(IPsychologistsRepository psychologistsRepository)
        {
            _psychologistsRepository= psychologistsRepository;
        }
        public Comment AddCommentToPsyhologist(Comment comment, int psychologistId)
        {
            throw new NotImplementedException();
        }

        public int AddPsychologist(Psychologist psychologist)
        {
            throw new NotImplementedException();
        }

        public void DeletePsychologist(int id)
        {
            throw new NotImplementedException();
     
        public List<Psychologist> GetAllPsychologists()
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByPsychologistId(int id)
        {
            throw new NotImplementedException();
        }

        public Psychologist? GetPsychologist(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePsychologist(Psychologist psychologist, int id)
        {
            throw new NotImplementedException();
        }
    }
}