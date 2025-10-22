using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICitaBL
    {
        Task<OperationResult> InsertarCitaAsync(Cita cita, List<CitaProcedimiento> servicios, string bitacoraDatosDespues);
    }
}
