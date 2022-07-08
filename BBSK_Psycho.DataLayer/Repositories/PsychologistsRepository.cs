using BBSK_Psycho.DataLayer.Entities;
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

        public Psychologist? GetPsychologist(int id) => _context.Psychologists.FirstOrDefault(p => p.Id == id);

        public int AddPsychologist (Psychologist psychologist)
        {
            _context.Psychologists.Add(psychologist);
            _context.SaveChanges();

            return psychologist.Id;
        }

        public void UpdatePsychologist(Psychologist psychologist)
        {
            _context.Psychologists.Update(psychologist);
            _context.SaveChanges();
        }
        public void DeletePsychologist (int id)
        {
            var psychologist = GetPsychologist(id);
            _context.Psychologists.Remove(psychologist);
            _context.SaveChanges ();
        }
    }
}
