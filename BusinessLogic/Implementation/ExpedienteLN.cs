using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BussinessLogic.Interfaces;

namespace LogicaNegocio.Implementation
{
    public class CitaReprogramadaLN : ICitaReprogramadaLN
    {
        private readonly ICitaReprogramadaAD gObjCitaReprogramadaAD;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaReprogramadaLN(IConfiguration configuration)
        {
            gObjCitaReprogramadaAD = new CitaReprogramadaAD(configuration);
        }

        private T EjecutarProcConEntidad<T>(Func<T> funcion)
        {
            try { return funcion(); }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        private bool EjecutarProcSinEntidad(Func<bool> funcion)
        {
            try { return funcion(); }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        public List<CitaReprogramada> RecCitasReprogramadas()
            => EjecutarProcConEntidad(() => gObjCitaReprogramadaAD.RecCitasReprogramadas());

        public CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada)
            => EjecutarProcConEntidad(() => gObjCitaReprogramadaAD.RecCitaReprogramadaXId(numCitaReprogramada));

        public bool InsCitaReprogramada(CitaReprogramada cita)
            => EjecutarProcSinEntidad(() => gObjCitaReprogramadaAD.InsCitaReprogramada(cita));

        public bool ModCitaReprogramada(CitaReprogramada cita)
            => EjecutarProcSinEntidad(() => gObjCitaReprogramadaAD.ModCitaReprogramada(cita));

        public bool DelCitaReprogramada(int numCitaReprogramada)
            => EjecutarProcSinEntidad(() => gObjCitaReprogramadaAD.DelCitaReprogramada(numCitaReprogramada));
    }
}
