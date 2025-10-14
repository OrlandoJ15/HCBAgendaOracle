using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BussinessLogic.Interfaces;

namespace LogicaNegocio.Implementation
{
    public class AgendaLN : IAgendaLN
    {
        private readonly IAgendaAD gObjAgendaAD;
        public Exceptions gObjExceptions = new Exceptions();

        public AgendaLN(IConfiguration configuration)
        {
            gObjAgendaAD = new AgendaAD(configuration);
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

        public List<Agenda> RecAgenda()
        {
            return EjecutarProcConEntidad(() => gObjAgendaAD.RecAgenda());
        }

        public Agenda? RecAgendaXId(int numAgenda)
        {
            return EjecutarProcConEntidad(() => gObjAgendaAD.RecAgendaXId(numAgenda));
        }

        public bool InsAgenda(Agenda agenda)
        {
            return EjecutarProcSinEntidad(() => gObjAgendaAD.InsAgenda(agenda));
        }

        public bool ModAgenda(Agenda agenda)
        {
            return EjecutarProcSinEntidad(() => gObjAgendaAD.ModAgenda(agenda));
        }

        public bool DelAgenda(int numAgenda)
        {
            return EjecutarProcSinEntidad(() => gObjAgendaAD.DelAgenda(numAgenda));
        }
    }
}
