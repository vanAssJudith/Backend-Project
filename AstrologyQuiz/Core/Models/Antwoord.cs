using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Antwoord
    {
        public int Id { get; set; }
        public int VraagId { get; set; }
        public string Omschrijving { get; set; }
        public string AfbeeldingURL { get; set; }
        public bool IsCorrect { get; set; }
        public string Uitleg { get; set; }
    }
}
