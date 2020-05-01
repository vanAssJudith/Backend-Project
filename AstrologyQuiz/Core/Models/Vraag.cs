using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models
{
    public class Vraag
    {
        public Guid Id { get; set; }
        [Required]
        public string Titel { get; set; }
        [StringLength(100)]
        public string Beschrijving { get; set; }
        public string AfbeeldingURL { get; set; }
        [Required]
        [Range(0,10)]
        public int Score { get; set; }
        [Required]
        public Guid QuizId { get; set; }

        public virtual ICollection<Antwoord> Antwoorden { get; set; }

        public Quiz Quiz { get; set; }

        public Vraag()
        {
            this.Antwoorden = new Collection<Antwoord>();
        }
    }

}
