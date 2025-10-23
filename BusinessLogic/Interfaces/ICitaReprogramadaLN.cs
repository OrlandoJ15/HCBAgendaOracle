using Entities.Models;

namespace BussinessLogic.Interfaces
{
    public interface ICitaReprogramadaLN
    {
        List<CitaReprogramada> RecCitasReprogramadas();
        CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada);
        bool InsCitaReprogramada(CitaReprogramada cita);
        bool ModCitaReprogramada(CitaReprogramada cita);
        bool DelCitaReprogramada(int numCitaReprogramada);
    }
}
