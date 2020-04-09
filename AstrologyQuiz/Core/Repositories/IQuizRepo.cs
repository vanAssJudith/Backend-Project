using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IQuizRepo
    {
        Task<Quiz> AddQuiz(Quiz quiz);
        Task AddQuizToGebruiker(QuizGebruiker quizGebruiker);
        Task UpdateQuizGebruikerAsync(QuizGebruiker quizGebruiker);
    }
}