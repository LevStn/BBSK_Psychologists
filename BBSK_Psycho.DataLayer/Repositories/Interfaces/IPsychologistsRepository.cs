using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories;


public interface IPsychologistsRepository

    {
        Task <Psychologist?> GetPsychologist(int id);
        Task <List<Psychologist>> GetAllPsychologists();
        Task <Comment> AddCommentToPsyhologist(Comment comment, int psychologistId);
        Task <List<Comment>> GetCommentsByPsychologistId(int id);
        Task <List<Order>> GetOrdersByPsychologistsId(int id);
        Task <int> AddPsychologist(Psychologist psychologist);
        Task UpdatePsychologist(Psychologist psychologist, int id);
        Task DeletePsychologist(int id);

        Task<Psychologist?> GetPsychologistByEmail(string email);
    }
