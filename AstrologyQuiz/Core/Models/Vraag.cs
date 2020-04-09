using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.Models
{
    public class Vraag
    {
        public Guid Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public string AfbeeldingURL { get; set; }
        public int Score { get; set; }
        public int QuizId { get; set; }

        public virtual ICollection<Antwoord> Antwoorden { get; set; }

        public Quiz Quiz { get; set; }

        public Vraag()
        {
            this.Antwoorden = new Collection<Antwoord>();
        }
    }

}
