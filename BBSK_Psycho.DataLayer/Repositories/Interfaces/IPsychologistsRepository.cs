using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public interface IPsychologistsRepository
    {
        Psychologist? GetPsychologist(int id);
        List<Psychologist> GetAllPsychologists();
        public int AddCommentToPsyhologist(Comment comment, int psychologistId);
        List<Comment> GetCommentsByPsychologistId(int id);
        int AddPsychologist(Psychologist psychologist);
        void UpdatePsychologist(Psychologist psychologist);
        void DeletePsychologist(int id);
    }
}