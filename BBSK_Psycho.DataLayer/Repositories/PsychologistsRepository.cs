using BBSK_Psycho.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public class PsychologistsRepository : IPsychologistsRepository
    {
        private readonly BBSK_PsychoContext _context;

        public PsychologistsRepository(BBSK_PsychoContext context)
        {
            _context = context;
        }

        public async Task <Psychologist?> GetPsychologist(int id) => 
            await _context.Psychologists
            .Include(e => e.Educations)
            .Include(tm => tm.TherapyMethods)
            .Include(p => p.Problems)
            .Include(s => s.Schedules)
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task <List <Psychologist>> GetAllPsychologists() => await _context.Psychologists.Where(p => p.IsDeleted == false ).ToListAsync();

        public async Task<List<Psychologist>> GetAllPsychologistsWithFullInformations() =>
            await _context.Psychologists
            .Where(p => p.IsDeleted == false)
            .Include(tm => tm.TherapyMethods)
            .Include(p => p.Problems)
            .ToListAsync();

        public async Task<Psychologist?> GetPsychologistByEmail(string email) => await _context.Psychologists.FirstOrDefaultAsync(p => p.Email == email);

        public async Task <List<Order>> GetOrdersByPsychologistsId(int id) => await _context.Orders.Where(p => p.Psychologist.Id == id && !p.IsDeleted).ToListAsync();
        public async Task <List<Comment>> GetCommentsByPsychologistId(int id) => await _context.Comments.Where(с => с.IsDeleted == false).ToListAsync();

        public async Task <int> AddPsychologist (Psychologist psychologist)
        {
            _context.Psychologists.Add(psychologist);
            await _context.SaveChangesAsync();

            return psychologist.Id;
        }

        public async Task <Comment> AddCommentToPsyhologist(Comment comment, int psychologistId)
        {
            var psycho = await GetPsychologist(psychologistId);
            comment.Psychologist = psycho;

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == comment.Client.Id);
            comment.Client = client;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }


        public async Task UpdatePsychologist(Psychologist newProperty, int id)
        {
            var psychologist = await GetPsychologist(id);
            psychologist.Gender = newProperty.Gender;
            psychologist.Phone = newProperty.Phone;
            psychologist.TherapyMethods.Clear();
            psychologist.Educations.Clear();
            psychologist.Problems.Clear();
            _context.SaveChanges();
            psychologist.TherapyMethods = newProperty.TherapyMethods;
            psychologist.Problems = newProperty.Problems;
            psychologist.Educations = newProperty.Educations;
            psychologist.Price = newProperty.Price;
            _context.Psychologists.Update(psychologist);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePsychologist (int id)
        {
            var psychologist = await _context.Psychologists.FirstOrDefaultAsync(o => o.Id == id);
            psychologist.IsDeleted = true;
            _context.SaveChangesAsync();
        }
    }
}
