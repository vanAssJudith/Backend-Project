using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SaveQuizDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Onderwerp { get; set; }
        [Required]
        public int MoeilijkheidsgraadId { get; set; }
        [Required]
        public string Beschrijving { get; set; }
        public virtual ICollection<VraagDTO> Vragen { get; set; }
    }
}
