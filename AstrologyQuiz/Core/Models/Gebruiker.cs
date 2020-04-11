using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.Models
{
    public class Gebruiker : IdentityUser
    {
        public string Naam { get; set; }
        //public string Paswoord { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
        public virtual ICollection<QuizGebruiker> QuizGebruikers { get; set; }

        public Gebruiker()
        {
            Roles = new Collection<IdentityUserRole<string>>();
            QuizGebruikers = new Collection<QuizGebruiker>();
        }
    }
}

