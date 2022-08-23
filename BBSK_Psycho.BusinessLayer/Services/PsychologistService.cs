using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.BusinessLayer
{
    public class PsychologistService : IPsychologistService
    {
        private readonly IPsychologistsValidator _psychologistsValidator;
        private readonly IPsychologistsRepository _psychologistsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly ISearchByFilter _searchByFilter;

        public PsychologistService(IPsychologistsValidator psychologistsValidator,
                                   IPsychologistsRepository psychologistsRepository, 
                                   IClientsRepository clientsRepository, 
                                   IOrdersRepository ordersRepository)
        {
            _psychologistsValidator = psychologistsValidator;
            _psychologistsRepository = psychologistsRepository;
            _ordersRepository = ordersRepository;
            _clientsRepository = clientsRepository;
            _searchByFilter = searchByFilter;
        }

        public async Task <int> AddCommentToPsyhologist(Comment comment, int psychologistId, ClaimModel claim)
        {
            var commonOrder = /*await*/ _ordersRepository.GetOrderByPsychIdAndClientId(psychologistId, comment.Client.Id);
            if (commonOrder == null)
            {
                throw new AccessException("$It is impossible to leave a comment to a psychologist with whom there have been no sessions!");
            }
            var client = await _clientsRepository.GetClientById(comment.Client.Id);
            if (client == null)
            {
                throw new EntityNotFoundException($"Client not found");
            }
            if (claim.Email != client.Email)
            {
                throw new AccessException($"Access denied");
            }

            var result = await _psychologistsRepository.AddCommentToPsyhologist(comment, psychologistId);
            return result.Id;
        }

        public async Task <int> AddPsychologist(Psychologist psychologist)
        {
            await _psychologistsValidator.CheckEmailForUniqueness(psychologist.Email);
            
            psychologist.Password = PasswordHash.HashPassword(psychologist.Password);
            var result = await _psychologistsRepository.AddPsychologist(psychologist);
            return result;
        }

        public async Task DeletePsychologist(int id, ClaimModel claim)
        {
            var psychologist = await _psychologistsRepository.GetPsychologist(id);
            if (psychologist == null)

            {
                throw new EntityNotFoundException($"Psychologist {id} not found");
            }

            if (claim.Role == Role.Psychologist
                && claim.Id != id)
            {
                throw new AccessException($"Access denied");
            }
            _psychologistsRepository.DeletePsychologist(id);
        }

        public async Task <List<Psychologist>> GetAllPsychologists(ClaimModel claim) => await _psychologistsRepository.GetAllPsychologists();


        public async Task <List<Comment>> GetCommentsByPsychologistId(int id, ClaimModel claim)
        {
            var psychologist = await _psychologistsRepository.GetPsychologist(id);
            if (psychologist == null)
            {
                throw new EntityNotFoundException($"Psychologist {id} not found");
            }
            await _psychologistsValidator.CheckAccessForPsychologistManagersAndClients(id, claim);

            var result= await _psychologistsRepository.GetCommentsByPsychologistId(id);
            return result;
        }

        public async Task <List<Order>> GetOrdersByPsychologistId(int id, ClaimModel claim)
        {

            var psycho = await _psychologistsRepository.GetPsychologist(id);
            if (psycho == null)
            {
                throw new EntityNotFoundException($"Orders by psychologist {id} not found");
            }
            await _psychologistsValidator.CheckAccessForPsychologistManagersAndClients(id, claim);

            return await _psychologistsRepository.GetOrdersByPsychologistsId(id);
        }

        public async Task <Psychologist?> GetPsychologist(int id, ClaimModel claim)
        {
            var result = await _psychologistsRepository.GetPsychologist(id);
            await _psychologistsValidator.CheckAccessOnlyForPsychologistAndManagers(id, claim);
            return result;
        }

        public async Task UpdatePsychologist(Psychologist psychologist, int id, ClaimModel claim)
        {
            var result = await _psychologistsRepository.GetPsychologist(id);
            await _psychologistsValidator.CheckAccessOnlyForPsychologistAndManagers(id, claim);
            await _psychologistsRepository.UpdatePsychologist(psychologist, id);
        }
    }
}