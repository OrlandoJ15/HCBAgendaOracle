using Entities.Models;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface ICitaReprogramadaLN
    {
        // Recupera todas las citas reprogramadas
        List<CitaReprogramada> RecCitasReprogramadas();

        // Recupera una cita reprogramada por su ID
        CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada);

        // Inserta una nueva cita reprogramada
        bool InsCitaReprogramada(CitaReprogramada cita);

        // Modifica una cita reprogramada existente
        bool ModCitaReprogramada(CitaReprogramada cita);

        // Elimina una cita reprogramada por su ID
        bool DelCitaReprogramada(int numCitaReprogramada);
    }
}
