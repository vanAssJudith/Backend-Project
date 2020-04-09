using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public QuizController(IQuizRepo quizRepo)
        {
            this.quizRepo = quizRepo;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var quizzen = await quizRepo.GetQuizzenAsync();
                            

                return Ok(quizzen);
            }
            catch (Exception)
            {

                return BadRequest();
            }
          
        }

        // GET: api/Quiz/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
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

        // PUT: api/Quiz/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
