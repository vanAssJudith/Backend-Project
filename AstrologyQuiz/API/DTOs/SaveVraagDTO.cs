using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SaveVraagDTO:VraagDTO
    {
        public Guid QuizId { get; set; }
    }
}
