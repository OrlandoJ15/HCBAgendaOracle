using Entities.Models;

namespace BussinessLogic.Interfaces
{
    public interface ICitaLN
    {
        List<Cita> RecCitas();
        Cita? RecCitaXId(int numCita);
        bool InsCita(Cita cita);
        bool ModCita(Cita cita);
        bool DelCita(int numCita);
    }
}
