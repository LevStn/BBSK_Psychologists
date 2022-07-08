using BBSK_Psycho.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public class PsychologistsRepository
    {
        private readonly BBSK_PsychoContext _context;

        public PsychologistsRepository (BBSK_PsychoContext context)
        {
            _context = context;
        }

        public Psychologist? GetPsychologist(int id) => _context.Psychologists.FirstOrDefault (p => p.Id == id);
    }
}
