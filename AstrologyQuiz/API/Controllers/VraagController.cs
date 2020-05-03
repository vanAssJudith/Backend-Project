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
    public class VraagController : ControllerBase
    {
        private readonly IVraagRepo vraagRepo;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public VraagController(IVraagRepo vraagRepo, IMapper mapper, ILogger<VraagController> logger)
        {
            this.vraagRepo = vraagRepo;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Vraag
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var vragen = await vraagRepo.GetAllAsync();
                var vragenDTO = mapper.Map<IEnumerable<VraagDTO>>(vragen);
                return Ok(vragenDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/Vraag/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var vraag = await vraagRepo.GetAsync(id);
                var vraagDTO = mapper.Map<VraagDTO>(vraag);
                return Ok(vraagDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/Vraag
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveVraagDTO saveVraagDTO)
        {
            try
            {
                var vraag = mapper.Map<Vraag>(saveVraagDTO);
                vraagRepo.Add(vraag);
                await vraagRepo.SaveChangesAsync();
                var vraagDTO = mapper.Map<VraagDTO>(vraag);
                return CreatedAtAction(nameof(GetById), new { id = vraag.Id }, vraagDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Toevoegen mislukt");
            }
        }

        // PUT: api/Vraag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] SaveVraagDTO saveVraagDTO)
        {
            try
            {
                if (id != saveVraagDTO.Id)
                    return BadRequest("Id's kloppen niet");
                var vraag = await vraagRepo.GetAsync(saveVraagDTO.Id);
                if (vraag == null)
                    return NotFound();
                mapper.Map(saveVraagDTO, vraag);
                await vraagRepo.SaveChangesAsync();
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
                var vraag = await vraagRepo.GetAsync(id);
                if (vraag == null)
                    return NotFound();
                vraagRepo.Delete(vraag);
                await vraagRepo.SaveChangesAsync();
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