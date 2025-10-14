using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BussinessLogic.Interfaces;

namespace LogicaNegocio.Implementation
{
    public class CitaLN : ICitaLN
    {
        private readonly ICitaAD gObjCitaAD;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaLN(IConfiguration configuration)
        {
            gObjCitaAD = new CitaAD(configuration);
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

        public List<Cita> RecCitas()
        {
            return EjecutarProcConEntidad(() => gObjCitaAD.RecCitas());
        }

        public Cita? RecCitaXId(int numCita)
        {
            return EjecutarProcConEntidad(() => gObjCitaAD.RecCitaXId(numCita));
        }

        public bool InsCita(Cita cita)
        {
            return EjecutarProcSinEntidad(() => gObjCitaAD.InsCita(cita));
        }

        public bool ModCita(Cita cita)
        {
            return EjecutarProcSinEntidad(() => gObjCitaAD.ModCita(cita));
        }

        public bool DelCita(int numCita)
        {
            return EjecutarProcSinEntidad(() => gObjCitaAD.DelCita(numCita));
        }
    }
}
