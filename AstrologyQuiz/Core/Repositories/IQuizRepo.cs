using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IQuizRepo:IGenericRepo<Quiz>
    {
      
        void AddQuizToGebruiker(QuizGebruiker quizGebruiker);
      
    }
}