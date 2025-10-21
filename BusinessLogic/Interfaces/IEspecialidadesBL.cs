using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IEspecialidadesBL
    {
        Task<List<R_Especialidad>> RecEspecialidadesxUsuarioAsync(int tipoAgenda, int usuarioId, int sucursal);
    }
}
