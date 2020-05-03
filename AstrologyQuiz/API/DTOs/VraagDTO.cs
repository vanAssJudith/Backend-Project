using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class VraagDTO
    {
        public Guid Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public string AfbeeldingURL { get; set; }
        public int Score { get; set; }
        public virtual ICollection<AntwoordDTO> Antwoorden { get; set; }
    }
}
