using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SaveVraagDTO:VraagDTO
    {
        // TODO: mapper niet vergeten toevoegen bij profile (api -> domain class)
        public Guid QuizId { get; set; }
    }
}
