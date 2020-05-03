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
    public class QuizRepo : GenericRepo<Quiz>, IQuizRepo
    {
        private readonly AstrologyQuizDbContext context;

        public QuizRepo(AstrologyQuizDbContext context):base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await context.Quizzen
               .Include(q => q.Moeilijkheidsgraad)               
               .ToListAsync();
        }

        public override async Task<Quiz> GetAsync(Guid id)
        {
            return await context.Quizzen
               .Include(q => q.Moeilijkheidsgraad)
               .Include(q => q.Vragen)
               .ThenInclude(v => v.Antwoorden)
               .SingleOrDefaultAsync(q => q.Id == id);
        }

        public void AddQuizToGebruiker(QuizGebruiker quizGebruiker)
        {
            context.QuizGebruikers.Add(quizGebruiker);
        }
    }
}
