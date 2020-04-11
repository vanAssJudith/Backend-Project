using Core.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class VraagRepo : GenericRepo<Vraag>, IVraagRepo
    {
        private readonly AstrologyQuizDbContext context;

        public VraagRepo(AstrologyQuizDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<Vraag>> GetAllAsync()
        {
            return await context.Vragen
               .Include(v => v.Antwoorden)
               .ToListAsync();
        }
    }
}
