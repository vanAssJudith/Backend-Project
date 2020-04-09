using Core.Data;
using Core.Models;
using System;
using System.Collections.Generic;
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
                throw exc;
            }
        }
        public async Task AddQuizToGebruiker(QuizGebruiker quizGebruiker)
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
