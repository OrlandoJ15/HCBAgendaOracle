/*using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;
using System.Linq;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaLN _agendaLN;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public AgendaController(IAgendaLN agendaLN)
        {
            _agendaLN = agendaLN;
        }

        // Manejo centralizado de errores
        private ActionResult ManejoError(System.Exception ex)
        {
            gObjExcepciones.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        private IActionResult HandleResponse<T>(T response)
        {
            if (response == null)
                return new JsonResult(null); // 404 Not Found

            return Ok(response); // 200 OK
        }

        // =======================================================
        // MÉTODOS DEL API
        // =======================================================

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<Agenda>> RecAgenda()
        {
            try
            {
                var lista = _agendaLN.RecAgenda();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpGet("{numAgenda}")]
        public IActionResult RecAgendaXId(int numAgenda)
        {
            try
            {
                var agenda = _agendaLN.RecAgendaXId(numAgenda);
                return HandleResponse(agenda);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsAgenda([FromBody] Agenda agenda)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _agendaLN.InsAgenda(agenda);
                return CreatedAtAction(nameof(RecAgendaXId), new { numAgenda = agenda.NumAgenda }, agenda);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModAgenda([FromBody] Agenda agenda)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _agendaLN.ModAgenda(agenda);
                return Ok(agenda);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numAgenda}")]
        [HttpDelete]
        public IActionResult DelAgenda(int numAgenda)
        {
            try
            {
                var agenda = _agendaLN.RecAgendaXId(numAgenda);
                if (agenda == null)
                    return NotFound("Agenda no encontrada");

                _agendaLN.DelAgenda(numAgenda);
                return Ok(agenda);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
*/