﻿using BBSK_Psycho.BusinessLayer.Exceptions;
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
            _psychologistsRepository = psychologistsRepository;
        }

        public Comment AddCommentToPsyhologist(Comment comment, int psychologistId)
        {
            var result = _psychologistsRepository.AddCommentToPsyhologist(comment, psychologistId);
            return result;
        }

        public int AddPsychologist(Psychologist psychologist)
        {
            var isChecked = CheckEmailForUniqueness(psychologist.Email);
            if(!isChecked)
            {
                throw new UniquenessException($"That email is registred");
            }
            var result = _psychologistsRepository.AddPsychologist(psychologist);
            return result;
        }

        public void DeletePsychologist(int id)
        {
            _psychologistsRepository.DeletePsychologist(id);
        }

        public List<Psychologist> GetAllPsychologists()
        {
            var result=_psychologistsRepository.GetAllPsychologists();
            return result;
        }

        public List<Comment> GetCommentsByPsychologistId(int id)
        {
            var result= _psychologistsRepository.GetCommentsByPsychologistId(id);
            return result;
        }

        public Psychologist? GetPsychologist(int id)
        {
            var result = _psychologistsRepository.GetPsychologist(id);
            return result;
        }

        public void UpdatePsychologist(Psychologist psychologist, int id)
        {
            var result = _psychologistsRepository.GetPsychologist(id);
        }

        private bool CheckEmailForUniqueness(string email) => _psychologistsRepository.GetPsychologistByEmail(email) == null;
    }
}