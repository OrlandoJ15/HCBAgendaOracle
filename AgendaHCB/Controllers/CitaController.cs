using BusinessLogic.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaHCB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitaController : ControllerBase
    {
        private readonly ICitaBL _citaBL;

        public CitaController(ICitaBL citaBL)
        {
            _citaBL = citaBL;
        }

        // POST /api/cita
        [HttpPost]
        public async Task<ActionResult<OperationResult>> InsertarCita([FromBody] InsertarCitaRequest request)
        {
            if (request == null || request.Cita == null)
                return BadRequest(new OperationResult { Code = -1, Message = "Request inválido" });

            var result = await _citaBL.InsertarCitaAsync(request.Cita, request.Servicios ?? new List<CitaProcedimiento>(), request.BitacoraDatosDespues);

            if (result == null)
                return StatusCode(500, new OperationResult { Code = -1, Message = "Error inesperado" });

            if (result.Code != 0)
                return BadRequest(result);

            return Ok(result);
        }
    }

    // DTO para el body del POST
    public class InsertarCitaRequest
    {
        public Cita Cita { get; set; }
        public List<CitaProcedimiento> Servicios { get; set; }
        public string BitacoraDatosDespues { get; set; }
    }
}
