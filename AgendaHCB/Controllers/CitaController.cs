using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using BussinessLogic.Interfaces;
using CommonMethods;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICitaLN _citaLN;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public CitaController(ICitaLN citaLN)
        {
            _citaLN = citaLN;
        }

        // =======================================================
        // MÉTODOS PRIVADOS DE MANEJO DE ERRORES Y RESPUESTAS
        // =======================================================

        private ActionResult ManejoError(Exception ex)
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
        public ActionResult<List<Cita>> RecCitas()
        {
            try
            {
                var lista = _citaLN.RecCitas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCita}")]
        [HttpGet]
        public IActionResult RecCitaXId(int numCita)
        {
            try
            {
                var cita = _citaLN.RecCitaXId(numCita);
                return HandleResponse(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsCita([FromBody] Cita cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaLN.InsCita(cita);
                return CreatedAtAction(nameof(RecCitaXId), new { numCita = cita.NumCita }, cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModCita([FromBody] Cita cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _citaLN.ModCita(cita);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{numCita}")]
        [HttpDelete]
        public IActionResult DelCita(int numCita)
        {
            try
            {
                var cita = _citaLN.RecCitaXId(numCita);
                if (cita == null)
                    return NotFound("Cita no encontrada");

                _citaLN.DelCita(numCita);
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return ManejoError(ex);
            }
        }
    }
}
