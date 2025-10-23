using Entities.Models;

namespace BussinessLogic.Interfaces
{
    public interface ICitaProcedimientoLN
    {
        List<CitaProcedimiento> RecCitaProcedimientos();
        CitaProcedimiento? RecCitaProcedimientoXId(int numCita, string codArticulo);
        bool InsCitaProcedimiento(CitaProcedimiento citaProc);
        bool ModCitaProcedimiento(CitaProcedimiento citaProc);
        bool DelCitaProcedimiento(int numCita, string codArticulo);
    }
}
