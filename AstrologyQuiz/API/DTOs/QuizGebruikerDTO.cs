using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class QuizGebruikerDTO
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string GebruikerId { get; set; }
        public DateTime StartDatum { get; set; }
        public int? TotaalScore { get; set; }
        public Quiz Quiz { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public virtual ICollection<QuizGebruikerAntwoord> QuizGebruikerAntwoorden { get; set; }
    }
}
