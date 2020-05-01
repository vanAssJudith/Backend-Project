using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepo quizRepo;
        private readonly IAntwoordRepo antwoordRepo;

        public QuizService(IQuizRepo quizRepo, IAntwoordRepo antwoordRepo)
        {
            this.quizRepo = quizRepo;
            this.antwoordRepo = antwoordRepo;
        }

        public async Task<QuizGebruiker> SaveQuizGebruikerAsync(List<Guid> antwoorden, Guid id, string gebruikerId)
        {
            var quizGebruikerAntwoorden = new List<QuizGebruikerAntwoord>();

            foreach (var antwoord in antwoorden)
            {
                quizGebruikerAntwoorden.Add(new QuizGebruikerAntwoord()
                {
                    AntwoordId = antwoord
                });
            }

            var juisteAntwoordenVanQuiz = await antwoordRepo.GetJuisteAntwoord(id) as List<Antwoord>;

            var juisteAntwoorden = juisteAntwoordenVanQuiz.Where(a => antwoorden.Any(an => an == a.Id));

            var totaalScore = juisteAntwoorden.Sum(a => a.Vraag.Score);

            var quizGebruiker = new QuizGebruiker()
            {
                QuizId = id,
                GebruikerId = gebruikerId,
                QuizGebruikerAntwoorden = quizGebruikerAntwoorden,
                StartDatum = DateTime.Now,
                TotaalScore = totaalScore
            };

            quizRepo.AddQuizToGebruiker(quizGebruiker);
            await quizRepo.SaveChangesAsync();
            return quizGebruiker;

        }

    }
}
