using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
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
    public class AntwoordController : ControllerBase
    {
        private readonly IAntwoordRepo antwoordRepo;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AntwoordController(IAntwoordRepo antwoordRepo, IMapper mapper, ILogger<AntwoordController> logger)
        {
            this.antwoordRepo = antwoordRepo;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Antwoord
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var antwoorden = await antwoordRepo.GetAllAsync();
                var antwoordenDTO = mapper.Map<IEnumerable<AntwoordDTO>>(antwoorden);
                return Ok(antwoordenDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/Antwoord/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var antwoord = await antwoordRepo.GetAsync(id);
                var antwoordDTO = mapper.Map<AntwoordDTO>(antwoord);
                return Ok(antwoordDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/Antwoord
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveAntwoordDTO saveAntwoordDTO)
        {
            try
            {
                var antwoord = mapper.Map<Antwoord>(saveAntwoordDTO);
                antwoordRepo.Add(antwoord);
                await antwoordRepo.SaveChangesAsync();
                var antwoordDTO = mapper.Map<AntwoordDTO>(antwoord);
                return CreatedAtAction(nameof(GetById), new { id = antwoord.Id }, antwoordDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Toevoegen mislukt");
            }
        }

        // PUT: api/Antwoord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id,[FromBody] SaveAntwoordDTO saveAntwoordDTO)
        {
            try
            {
                if (id != saveAntwoordDTO.Id)
                    return BadRequest("Id's kloppen niet");
                var antwoord = await antwoordRepo.GetAsync(saveAntwoordDTO.Id);
                if (antwoord == null)
                    return NotFound();
                mapper.Map(saveAntwoordDTO, antwoord);
                await antwoordRepo.SaveChangesAsync();
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
                var antwoord = await antwoordRepo.GetAsync(id);
                if (antwoord == null)
                    return NotFound();
                antwoordRepo.Delete(antwoord);
                await antwoordRepo.SaveChangesAsync();
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