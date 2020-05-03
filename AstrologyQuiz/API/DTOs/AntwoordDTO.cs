using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AntwoordDTO
    {
        public Guid Id { get; set; }
        public string Omschrijving { get; set; }
        public string AfbeeldingURL { get; set; }
        public bool IsCorrect { get; set; }
        public string Uitleg { get; set; }
    }
}
