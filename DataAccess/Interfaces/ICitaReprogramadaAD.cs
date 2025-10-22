using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface ICitaReprogramadaAD
    {
        // =======================================================
        // Recupera todas las citas reprogramadas
        // =======================================================
        List<CitaReprogramada> RecCitasReprogramadas();

        // =======================================================
        // Recupera una cita reprogramada por su ID
        // =======================================================
        CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada);

        // =======================================================
        // Inserta una nueva cita reprogramada
        // =======================================================
        bool InsCitaReprogramada(CitaReprogramada citaReprogramada);

        // =======================================================
        // Modifica una cita reprogramada existente
        // =======================================================
        bool ModCitaReprogramada(CitaReprogramada citaReprogramada);

        // =======================================================
        // Elimina una cita reprogramada por su ID
        // =======================================================
        bool DelCitaReprogramada(int numCitaReprogramada);
    }
}
