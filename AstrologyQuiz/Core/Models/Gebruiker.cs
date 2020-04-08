using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Gebruiker : IdentityUser
    {
        public string Naam { get; set; }
        public string Paswoord { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();
        public object QuizGebruiker { get; internal set; }
    }
}

