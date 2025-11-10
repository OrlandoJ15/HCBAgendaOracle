using BusinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Interfaces;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Implementation
{
    public class CitaBL : ICitaBL
    {
        private readonly ICitaDA _citaDA;
        private readonly AsyncExceptions _exceptions;

        public CitaBL(ICitaDA citaDA, AsyncExceptions exceptions)
        {
            _citaDA = citaDA;
            _exceptions = exceptions;
        }

        public async Task<OperationResult> InsertarCitaAsync(Cita cita, List<CitaProcedimiento> servicios, string bitacoraDatosDespues)
        {
            // Ejecutamos y dejamos que Exceptions registre errores si ocurre alguno
            return await _exceptions.EjecutarProcConEntidadAsync(async () =>
            {
                var result = await _citaDA.InsertarCitaAsync(cita, servicios, bitacoraDatosDespues);
                return result;
            });
        }
    }
}
