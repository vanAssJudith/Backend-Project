using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class QuizGebruiker
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int GebruikerId { get; set; }
        public DateTime StartDatum { get; set; }
        public int? TotaalScore { get; set; }
    }
}
