using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepo quizRepo;
        private readonly IMapper mapper;

        public QuizController(IQuizRepo quizRepo, IMapper mapper)
        {
            this.quizRepo = quizRepo;
            this.mapper = mapper;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var quizzen = await quizRepo.GetQuizzenAsync();

                var quizzenDTO = mapper.Map<IEnumerable<QuizDTO>>(quizzen);

                return Ok(quizzenDTO);
            }
            catch (Exception)
            {

                return BadRequest();
            }
          
        }

        // GET: api/Quiz/5
        [HttpGet("{id}", Name = "Get")]
        public string GetById(Guid id)
        {
            return "value";
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Quiz quiz)
        {
            try
            {
                
                //hebben we hierbij een mapper nodig?
                await quizRepo.AddQuizAsync(quiz);
                return Ok();
                    //return CreatedAtAction("Get", new { id = quiz.Id }, quiz); ;
            }
            catch (Exception ex)
            {
                return BadRequest("Toevoegen mislukt");
                throw ex;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var quiz = await quizRepo.GetQuizAsync(id);

                if (quiz == null)
                    return NotFound();
                await quizRepo.DeleteQuizAsync(quiz);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Verwijderen mislukt");
                throw ex;
            }
        }
    }
}
