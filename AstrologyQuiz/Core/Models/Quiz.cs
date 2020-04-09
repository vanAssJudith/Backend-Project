using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Onderwerp { get; set; }
        public int MoeilijkheidsgraadId { get; set; }
        public string Beschrijving { get; set; }
        public virtual ICollection<Vraag> Vragen { get; set; }
        public Moeilijkheidsgraad Moeilijkheidsgraad { get; set; }
        public Quiz()
        {
            this.Vragen = new Collection<Vraag>();
        }

    }
}
