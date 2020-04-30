using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ResultaatVM
    {
        public ICollection<Vraag> Vragen { get; set; }
        public ICollection<QuizGebruikerAntwoord> QuizGebruikerAntwoorden { get; set; }
        public Guid QuizId { get; set; }
        public int? Score { get; set; }
    }
}
