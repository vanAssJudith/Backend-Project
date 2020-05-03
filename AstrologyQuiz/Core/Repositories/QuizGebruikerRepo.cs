using Core.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    // Todo: niet vergeten  generic interface te koppelen aan interface!!!!!!
    public class QuizGebruikerRepo : GenericRepo<QuizGebruiker>, IQuizGebruikerRepo
    {
        private readonly AstrologyQuizDbContext context;

        public QuizGebruikerRepo(AstrologyQuizDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<QuizGebruiker>> GetAllAsync()
        {
            return await context.QuizGebruikers
               .Include(v => v.QuizGebruikerAntwoorden)
               .ThenInclude(v => v.Antwoord)
               .ThenInclude(a => a.Vraag)
               .ToListAsync();
        }

        public async override Task<QuizGebruiker> GetAsync(Guid id)
        {
            return await context.QuizGebruikers
                .Include(qg => qg.QuizGebruikerAntwoorden)
              
                .Include(qg => qg.Quiz)
                .ThenInclude(q => q.Vragen)
                .ThenInclude(qga => qga.Antwoorden)
                .SingleOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<QuizGebruiker>> GetTopScore(int getal, Guid quizId)
        {
            return await context.QuizGebruikers
                .Include(q => q.Quiz)
                .Include(a => a.QuizGebruikerAntwoorden)
                .Include(g => g.Gebruiker)
                .Where(q => q.QuizId == quizId)
                .OrderByDescending(t => t.TotaalScore)
                .Take(getal)
                .ToListAsync();
        }
    }
}
