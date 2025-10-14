using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface ICitaAD
    {
        // Recupera todas las citas
        List<Cita> RecCitas();

        // Recupera una cita por su ID
        Cita? RecCitaXId(int numCita);

        // Inserta una nueva cita
        bool InsCita(Cita cita);

        // Modifica una cita existente
        bool ModCita(Cita cita);

        // Elimina una cita por su ID
        bool DelCita(int numCita);
    }
}
