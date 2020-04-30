using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    
    public interface IQuizGebruikerRepo:IGenericRepo<QuizGebruiker>
    {
        Task<IEnumerable<QuizGebruiker>> GetTopScore(int getal, Guid quizId);

    }
}