using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using BussinessLogic.Interfaces;
using CommonMethods;
using System.Collections.Generic;

namespace AgenteWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParamEnvioCorreoAdjuntoController : ControllerBase
    {
        private readonly IParamEnvioCorreoAdjuntoLN _paramAdjuntoLN;
        private readonly Exceptions gObjExcepciones = new Exceptions();

        public ParamEnvioCorreoAdjuntoController(IParamEnvioCorreoAdjuntoLN paramAdjuntoLN)
        {
            _paramAdjuntoLN = paramAdjuntoLN;
        }

        // =======================================================
        // Manejo centralizado de errores y respuestas
        // =======================================================
        private ActionResult ManejoError(System.Exception ex)
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

        // =======================================================
        // MÉTODOS DEL API
        // =======================================================

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<ParamEnvioCorreoAdjunto>> RecParamEnvioCorreoAdjuntos()
        {
            try
            {
                var lista = _paramAdjuntoLN.RecParamEnvioCorreoAdjuntos();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{nombre}")]
        [HttpGet]
        public IActionResult RecParamEnvioCorreoAdjuntoXNombre(string nombre)
        {
            try
            {
                var adjunto = _paramAdjuntoLN.RecParamEnvioCorreoAdjuntoXNombre(nombre);
                return HandleResponse(adjunto);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsParamEnvioCorreoAdjunto([FromBody] ParamEnvioCorreoAdjunto adjunto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _paramAdjuntoLN.InsParamEnvioCorreoAdjunto(adjunto);
                return CreatedAtAction(nameof(RecParamEnvioCorreoAdjuntoXNombre),
                    new { nombre = adjunto.NOMBRE }, adjunto);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ModParamEnvioCorreoAdjunto([FromBody] ParamEnvioCorreoAdjunto adjunto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                _paramAdjuntoLN.ModParamEnvioCorreoAdjunto(adjunto);
                return Ok(adjunto);
            }
            catch (System.Exception ex)
            {
                return ManejoError(ex);
            }
        }

        [Route("[action]/{nombre}")]
        [HttpDelete]
        public IActionResult DelParamEnvioCorreoAdjunto(string nombre)
        {
            try
            {
                var adjunto = _paramAdjuntoLN.RecParamEnvioCorreoAdjuntoXNombre(nombre);
                if (adjunto == null)
                    return NotFound("Adjunto no encontrado");

                _paramAdjuntoLN.DelParamEnvioCorreoAdjunto(nombre);
                return Ok(adjunto);
