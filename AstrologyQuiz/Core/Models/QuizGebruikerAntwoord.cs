using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class QuizGebruikerAntwoord
    {
        public Guid QuizGebruikerId { get; set; }
        public Guid AntwoordId { get; set; }
        public QuizGebruiker QuizGebruiker { get; set; }
        public Antwoord Antwoord { get; set; }
    }
}
