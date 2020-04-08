using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public int MoeilijkheidsgraadId { get; set; }
        public string Beschrijving { get; set; }

    }
}
