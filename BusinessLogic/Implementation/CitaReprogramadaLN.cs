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

        // =======================================================
        // MÉTODOS PRIVADOS DE EJECUCIÓN CENTRALIZADA
        // =======================================================
        private T EjecutarProcConEntidad<T>(Func<T> funcion)
        {
            try
            {
                return funcion();
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        private bool EjecutarProcSinEntidad(Action accion)
        {
            try
            {
                accion();
                return true;
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }
        }

        // =======================================================
        // MÉTODOS PÚBLICOS DE NEGOCIO
        // =======================================================

        public List<CitaReprogramada> RecCitasReprogramadas()
        {
            return EjecutarProcConEntidad(() => gObjCitaReprogramadaAD.RecCitasReprogramadas());
        }

        public CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada)
        {
            return EjecutarProcConEntidad(() => gObjCitaReprogramadaAD.RecCitaReprogramadaXId(numCitaReprogramada));
        }

        public bool InsCitaReprogramada(CitaReprogramada cita)
        {
            return EjecutarProcSinEntidad(() => gObjCitaReprogramadaAD.InsCitaReprogramada(cita));
        }

        public bool ModCitaReprogramada(CitaReprogramada cita)
        {
            return EjecutarProcSinEntidad(() => gObjCitaReprogramadaAD.ModCitaReprogramada(cita));
        }

        public bool DelCitaReprogramada(int numCitaReprogramada)
        {
            return EjecutarProcSinEntidad(() => gObjCitaReprogramadaAD.DelCitaReprogramada(numCitaReprogramada));
        }
    }
}
