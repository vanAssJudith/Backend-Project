using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }
        [Required]
        public string Onderwerp { get; set; }
        [Required]
        public int MoeilijkheidsgraadId { get; set; }
        public string Beschrijving { get; set; }
        public virtual ICollection<Vraag> Vragen { get; set; }
        public virtual ICollection<QuizGebruiker> QuizGebruikers { get; set; }
        public Moeilijkheidsgraad Moeilijkheidsgraad { get; set; }
        public Quiz()
        {
            this.Vragen = new Collection<Vraag>();
            this.QuizGebruikers = new Collection<QuizGebruiker>();
        }

    }
}
