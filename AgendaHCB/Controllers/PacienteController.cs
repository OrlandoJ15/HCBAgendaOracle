using BusinessLogic.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteBL _pacienteBL;

        public PacienteController(IPacienteBL pacienteBL)
        {
            _pacienteBL = pacienteBL;
        }

        // GET: api/Paciente?primerNom=...&segundoNom=...&primerAp=...&segundoAp=...
        [HttpGet("ByName")]
        public ActionResult<List<Expediente>> GetRecordByName(
            [FromQuery] string primerNom = null,
            [FromQuery] string segundoNom = null,
            [FromQuery] string primerAp = null,
            [FromQuery] string segundoAp = null)
        {
            try
            {
                var resultados = _pacienteBL.GetRecordByName(primerNom, segundoNom, primerAp, segundoAp);

                if (resultados == null || resultados.Count == 0)
                    return NotFound("No se encontraron expedientes con los parámetros proporcionados.");

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error con tu clase Exceptions si quieres
                return StatusCode(500, $"Ocurrió un error al obtener los expedientes: {ex.Message}");
            }
        }
        
        [HttpGet("ByIdentification")]
        public ActionResult<List<Expediente>> GetRecordByIdentification(
           [FromQuery] string pidentificacion = null,
           [FromQuery] string pcod_tipdoc = null)
        {
            try
            {
                var resultados = _pacienteBL.GetRecordByIdentification(pidentificacion, pcod_tipdoc);

                if (resultados == null || resultados.Count == 0)
                    return NotFound("No se encontraron expedientes con los parámetros proporcionados.");

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error con tu clase Exceptions si quieres
                return StatusCode(500, $"Ocurrió un error al obtener los expedientes: {ex.Message}");
            }
        }
    }
}
