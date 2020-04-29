using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IQuizService
    {
        Task<QuizGebruiker> SaveQuizGebruikerAsync(List<Guid> antwoorden, Guid id, string gebruikerId);
    }
}