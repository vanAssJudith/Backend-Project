using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.Models
{
    public class QuizGebruiker
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string GebruikerId { get; set; }
        public DateTime StartDatum { get; set; }
        public int? TotaalScore { get; set; }
        public Quiz Quiz { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public virtual ICollection<QuizGebruikerAntwoord> QuizGebruikerAntwoorden { get; set; }
        public QuizGebruiker()
        {
            this.QuizGebruikerAntwoorden = new Collection<QuizGebruikerAntwoord>();
        }
    }
}
