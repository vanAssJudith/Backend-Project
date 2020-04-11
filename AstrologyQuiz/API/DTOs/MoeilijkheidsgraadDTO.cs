using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MoeilijkheidsgraadDTO
    {
        public int Id { get; set; }
        public string Titel { get; set; }

        public virtual ICollection<QuizDTO> Quiz { get; set; }
    }
}
