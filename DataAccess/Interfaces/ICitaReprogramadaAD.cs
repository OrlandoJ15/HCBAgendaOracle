using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface ICitaReprogramadaAD
    {
        List<CitaReprogramada> RecCitasReprogramadas();
        CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada);
        bool InsCitaReprogramada(CitaReprogramada cita);
        bool ModCitaReprogramada(CitaReprogramada cita);
        bool DelCitaReprogramada(int numCitaReprogramada);
    }
}
