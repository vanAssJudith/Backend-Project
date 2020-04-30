using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class TopScoresViewComponent : ViewComponent
    {
        private readonly IQuizGebruikerRepo quizGebruikerRepo ;

        public TopScoresViewComponent(IQuizGebruikerRepo userQuizRepo)
        {
            quizGebruikerRepo = userQuizRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid quizid, int aantal)
        {
            var items = await quizGebruikerRepo.GetTopScore(aantal, quizid);
            return View(items);
        }
    }
}
