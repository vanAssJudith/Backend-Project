﻿using Core.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        
        public async Task<IEnumerable<Antwoord>> GetJuisteAntwoord(Guid id)
        {
            return await context.Antwoorden
                .Include(q => q.Vraag)
                .ThenInclude(a => a.Quiz)
                .Where(a => a.IsCorrect == true)
                .Where(a => a.Vraag.QuizId == id)
                .ToListAsync()
                ;

            // include 
            // parent tabel -- child tabel
            // thenincludde
            // childe --> deeper child

            // where = lijst die voldoet aan eisen binnen tabel en navigation property
            // any = lijst (vaak in where method = kijkt of er een of meerdere voloden aan de eisen)
            // singleOrdefault && fristordefault && last or default = GEEFT 1 OBJECT terug
        }
    }
}
