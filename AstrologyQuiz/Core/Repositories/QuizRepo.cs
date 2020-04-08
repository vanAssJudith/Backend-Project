using Core.Data;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class QuizRepo
    {
        private readonly AstrologyQuizDbContext context;

        public QuizRepo(AstrologyQuizDbContext context)
        {
            this.context = context;
        }

        public async Task<Quiz> AddQuiz(Quiz quiz)
        {
            try
            {
                var result = context.Quizzen.AddAsync(quiz);
                await context.SaveChangesAsync();
                return quiz;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.Message);
                return null;
            }
        }
        public async Task AddQuizToGebruiker(int quizId, int gebruikerId)
        {
            await context.QuizGebruikers.AddAsync(new QuizGebruiker
            {
                QuizId = quizId,
                GebruikerId = gebruikerId
            });

        }

    }
}
