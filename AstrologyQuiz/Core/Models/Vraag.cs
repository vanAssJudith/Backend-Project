using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Vraag
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public string AfbeeldingURL { get; set; }
        public int Score { get; set; }
        public int QuizId { get; set; }
    }

}
