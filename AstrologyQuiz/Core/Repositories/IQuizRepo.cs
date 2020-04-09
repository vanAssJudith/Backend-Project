using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IQuizRepo
    {
        Task<Quiz> AddQuizAsync(Quiz quiz);
        Task AddQuizToGebruikerAsync(QuizGebruiker quizGebruiker);
        Task<Quiz> GetQuizAsync(Guid id);
        Task<IEnumerable<Quiz>> GetQuizzenAsync();
        Task UpdateQuizGebruikerAsync(QuizGebruiker quizGebruiker);
        Task DeleteQuizAsync(Quiz quiz);
    }
}