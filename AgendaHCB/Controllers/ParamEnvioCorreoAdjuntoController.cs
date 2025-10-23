using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using BussinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitaReprogramadaController : ControllerBase
    {
        private readonly ICitaReprogramadaLN _citaReprogramadaLN;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public CitaReprogramadaController(ICitaReprogramadaLN citaReprogramadaLN)
        {
            _citaReprogramadaLN = citaReprogramadaLN;
        }

        private ActionResult ManejoError(Exception ex)
        {
            gObjExcepciones.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        private IActionResult HandleResponse<T>(T response)
        {
            if (response == null)
                return NotFound("Registro no encontrado");
            return Ok(response);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<CitaReprogramada>> RecCitasReprogramadas()
        {
            try
            {
                var lista = _citaReprogramadaLN.RecCitasReprogramadas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCitaReprogramada}")]
        [HttpGet]
        public IActionResult RecCitaReprogramadaXId(int numCitaReprogramada)
        {
            try
            {
                var cita = _citaReprogramadaLN.RecCitaReprogramadaXId(numCitaReprogramada);
                return HandleResponse(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsCitaReprogramada([FromBody] CitaReprogramada cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaReprogramadaLN.InsCitaReprogramada(cita);
                return CreatedAtAction(nameof(RecCitaReprogramadaXId),
                    new { numCitaReprogramada = cita.NumCitaReprogramada }, cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModCitaReprogramada([FromBody] CitaReprogramada cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaReprogramadaLN.ModCitaReprogramada(cita);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCitaReprogramada}")]
        [HttpDelete]
        public IActionResult DelCitaReprogramada(int numCitaReprogramada)
        {
            try
            {
                var cita = _citaReprogramadaLN.RecCitaReprogramadaXId(numCitaReprogramada);
                if (cita == null)
                    return NotFound("Cita reprogramada no encontrada");

                _citaReprogramadaLN.DelCitaReprogramada(numCitaReprogramada);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
