using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{


    public interface IEspecialidades
    {
        // =======================================================
        // Recupera todos los registros de R_Especialidades ligados a un usuario
        // =======================================================
        List<R_Especialidad> RecEspecialidadesxUsuario();

    }
}
