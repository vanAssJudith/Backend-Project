using Core.Data;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public class MoeilijkheidsgraadRepo : GenericRepo<Moeilijkheidsgraad>, IMoeilijkheidsgraadRepo
    {
        private readonly AstrologyQuizDbContext context;

        public MoeilijkheidsgraadRepo(AstrologyQuizDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
