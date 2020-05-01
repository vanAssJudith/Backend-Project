using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly IQuizRepo quizRepo;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly IQuizGebruikerRepo quizGebruikerRepo;
        private readonly IQuizService quizService;
        private readonly IMoeilijkheidsgraadRepo moeilijkheidsGraadRepo;

        public QuizController(IQuizRepo quizRepo, UserManager<Gebruiker> userManager, IQuizGebruikerRepo quizGebruikerRepo, IQuizService quizService, IMoeilijkheidsgraadRepo moeilijkheidsGraadRepo)
        {
            _userManager = userManager;
            this.quizGebruikerRepo = quizGebruikerRepo;
            this.quizService = quizService;
            this.moeilijkheidsGraadRepo = moeilijkheidsGraadRepo;
            this.quizRepo = quizRepo;
        }
        // TODO Task niet vergeten

        public async Task<IActionResult> Index()
        {
            var quizzen = await this.quizRepo.GetAllAsync();

            return View(quizzen);
        }

        public async Task<IActionResult> Speel(Guid id)
        {
            try
            {
                var quiz = await this.quizRepo.GetAsync(id);

                return View(quiz);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Speel(List<Guid> antwoorden, Guid id)
        {
            try
            {
                var gebruiker = await _userManager.GetUserAsync(User);

                // 1 lijst met aanwoorden overlopen en en nieuw object QuizGebruikerAntwoord
                var quizGebruiker = await quizService.SaveQuizGebruikerAsync(antwoorden, id, gebruiker.Id);

                return RedirectToAction(nameof(Score), new { id = quizGebruiker.Id });
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IActionResult> Score(Guid id)
        {
            var quizGebruiker = await quizGebruikerRepo.GetAsync(id);

            var resultaatVM = new ResultaatVM()
            {
                QuizGebruikerAntwoorden = quizGebruiker.QuizGebruikerAntwoorden,
                Vragen = quizGebruiker.Quiz.Vragen,
                Score = quizGebruiker.TotaalScore,
                QuizId = quizGebruiker.QuizId

            };

            return View(resultaatVM);
        }


        public async Task<IActionResult> NieuweQuiz()
        {
            var test = await moeilijkheidsGraadRepo.GetAllAsync();
            ViewBag.MoeilijkheidsgraadId = new SelectList(test,"Id","Titel");


            return View();
        }

        public async Task<IActionResult> TopScores()
        {
            var quizzen = await quizRepo.GetAllAsync();
            return View(quizzen);
        }
    }
}