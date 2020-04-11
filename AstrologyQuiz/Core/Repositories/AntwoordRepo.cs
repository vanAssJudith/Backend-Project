using Core.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class AntwoordRepo : GenericRepo<Antwoord>, IAntwoordRepo
    {
        private readonly AstrologyQuizDbContext context;

        public AntwoordRepo(AstrologyQuizDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<Antwoord>> GetAllAsync()
        {
            return await context.Antwoorden
               .Include(v => v.Vraag)
               .ToListAsync();
        }
    }
}
