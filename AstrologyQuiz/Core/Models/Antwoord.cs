using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.Models
{
    public class Antwoord
    {
        public Guid Id { get; set; }
        public Guid VraagId { get; set; }
        public string Omschrijving { get; set; }
        public string AfbeeldingURL { get; set; }
        public bool IsCorrect { get; set; }
        public string Uitleg { get; set; }
        public Vraag Vraag { get; set; }
        public virtual ICollection<QuizGebruikerAntwoord> QuizGebruikerAntwoorden { get; set; }
        public Antwoord()
        {
            QuizGebruikerAntwoorden = new Collection<QuizGebruikerAntwoord>();
        }
    }
}
