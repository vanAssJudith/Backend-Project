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
using Microsoft.Extensions.Logging;
using Web.Models;
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
        private readonly ILogger<QuizController> logger;

        public QuizController(IQuizRepo quizRepo, UserManager<Gebruiker> userManager, IQuizGebruikerRepo quizGebruikerRepo, IQuizService quizService, IMoeilijkheidsgraadRepo moeilijkheidsGraadRepo, ILogger<QuizController> logger)
        {
            _userManager = userManager;
            this.quizGebruikerRepo = quizGebruikerRepo;
            this.quizService = quizService;
            this.moeilijkheidsGraadRepo = moeilijkheidsGraadRepo;
            this.logger = logger;
            this.quizRepo = quizRepo;
        }
        // TODO Task niet vergeten

        public async Task<IActionResult> Index()
        {
            try
            {
                var quizzen = await this.quizRepo.GetAllAsync();

                return View(quizzen);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.TraceIdentifier });
            }
        }

        public async Task<IActionResult> Speel(Guid id)
        {
            try
            {
                var quiz = await this.quizRepo.GetAsync(id);

                return View(quiz);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.TraceIdentifier });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Speel(List<Guid> antwoorden, Guid id)
        {
            try
            {
                var gebruiker = await _userManager.GetUserAsync(User);

                // 1 lijst met aanwoorden overlopen en en nieuw object QuizGebruikerAntwoord
                var quizGebruiker = await quizService.SaveQuizGebruikerAsync(antwoorden, id, gebruiker.Id);

                return RedirectToAction(nameof(Score), new { id = quizGebruiker.Id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.TraceIdentifier });
            }

        }


        public async Task<IActionResult> Score(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.TraceIdentifier });
            }
        }


        public async Task<IActionResult> NieuweQuiz()
        {
            try
            {
                var test = await moeilijkheidsGraadRepo.GetAllAsync();
                ViewBag.MoeilijkheidsgraadId = new SelectList(test,"Id","Titel");


                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NieuweQuiz([Bind("Onderwerp, MoeilijkheidsgraadId, Beschrijving, Vragen")]Quiz quiz)
        {
            try
            {
                quizRepo.Add(quiz);
                await quizRepo.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.TraceIdentifier });
            }

        }

        public async Task<IActionResult> TopScores()
        {
            try
            {
                var quizzen = await quizRepo.GetAllAsync();
                return View(quizzen);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.TraceIdentifier });
            }
        }
    }
}