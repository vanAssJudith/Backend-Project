using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Data;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepo quizRepo;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public QuizController(IQuizRepo quizRepo, IMapper mapper, ILogger<QuizController> logger)
        {
            this.quizRepo = quizRepo;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Quiz
        [Authorize(Roles = "Deelnemer, Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var quizzen = await quizRepo.GetAllAsync();
                var quizzenDTO = mapper.Map<IEnumerable<QuizDTO>>(quizzen);
                return Ok(quizzenDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        // GET: api/Quiz/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var quiz = await quizRepo.GetAsync(id);
                var quizDTO = mapper.Map<QuizDTO>(quiz);
                return Ok(quizDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveQuizDTO saveQuizDTO)
        {
            try
            {
                var quiz = mapper.Map<Quiz>(saveQuizDTO);
                quizRepo.Add(quiz);
                await quizRepo.SaveChangesAsync();
                var quizDTO = mapper.Map<QuizDTO>(quiz);
                return CreatedAtAction(nameof(GetById), new { id = quiz.Id }, quizDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Toevoegen mislukt");
            }
        }

        // PUT: api/Quiz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id,[FromBody] SaveQuizDTO quizDTO)
        {
            try
            {
                if (id != quizDTO.Id)
                    return BadRequest("Id's kloppen niet");
                var quiz = await quizRepo.GetAsync(quizDTO.Id);
                if (quiz == null)
                    return NotFound();
                mapper.Map(quizDTO, quiz);
                await quizRepo.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Verandering mislukt");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var quiz = await quizRepo.GetAsync(id);
                if (quiz == null)
                    return NotFound();
                quizRepo.Delete(quiz);
                await quizRepo.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Verwijderen mislukt");
            }
        }
    }
}
