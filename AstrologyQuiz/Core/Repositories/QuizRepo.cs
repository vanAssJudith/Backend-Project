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
    public class QuizRepo : IQuizRepo
    {
        private readonly AstrologyQuizDbContext context;

        public QuizRepo(AstrologyQuizDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzenAsync()
        {
            return await context.Quizzen.ToListAsync();
        }
        public async Task<Quiz> GetQuizAsync(Guid id)
        {
            return await context.Quizzen.SingleOrDefaultAsync(q => q.Id == id);
        }
        public async Task<Quiz> AddQuizAsync(Quiz quiz)
        {
                var result = context.Quizzen.AddAsync(quiz);
                await context.SaveChangesAsync();
                return quiz;
            
        }
        public async Task AddQuizToGebruikerAsync(QuizGebruiker quizGebruiker)
        {
            await context.QuizGebruikers.AddAsync(quizGebruiker);
            await context.SaveChangesAsync();

        }

        public async Task UpdateQuizGebruikerAsync(QuizGebruiker quizGebruiker)
        {
            context.QuizGebruikers.Update(quizGebruiker);
            await context.SaveChangesAsync();
        }
    }
}
