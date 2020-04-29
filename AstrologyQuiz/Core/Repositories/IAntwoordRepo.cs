using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IAntwoordRepo : IGenericRepo<Antwoord>
    {
        Task<IEnumerable<Antwoord>> GetJuisteAntwoord(Guid id);
    }
}