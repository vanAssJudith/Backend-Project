using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.Models
{
    public class Moeilijkheidsgraad
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public virtual ICollection<Quiz> Quizzen { get; set; }
        public Moeilijkheidsgraad()
        {
            Quizzen = new Collection<Quiz>();
        }
    }
}
