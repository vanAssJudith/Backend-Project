using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizRepo quizRepo;

        public QuizController(IQuizRepo quizRepo)
        {
            this.quizRepo = quizRepo;
        }
        // TODO Task niet vergeten
        public async Task<IActionResult> Index()
        {
            var quizzes =await this.quizRepo.GetAllAsync();

            return View(quizzes);
        }

        public async Task<IActionResult> Play(Guid id)
        {
            var quiz = await this.quizRepo.GetAsync(id);

            return View(quiz);
        }
    }
}