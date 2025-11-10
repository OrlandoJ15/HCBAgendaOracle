using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICitaDA
    {
        Task<OperationResult> InsertarCitaAsync(Cita cita, List<CitaProcedimiento> servicios, string bitacoraDatosDespues);
        Task<OperationResult> InsertarBitacoraAsync(int numCita, DateTime fecHoraInicial, DateTime fecHoraFinal, string indOperacion,
                                                     string usuarioRegistra, string datosAntes, string datosDespues, int numAgenda);
    }
}
