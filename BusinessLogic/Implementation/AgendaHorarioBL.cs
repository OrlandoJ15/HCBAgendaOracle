using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using CommonMethods;
using BussinessLogic.Interfaces;

namespace BussinessLogic.Implementation
{
    public class AgendaHorarioBL : IAgendaHorarioBL
    {
        private readonly IAgendaHorarioDA gObjAgAgendaHorarioAD;
        public Exceptions gObjExceptions = new Exceptions();

        public AgendaHorarioBL(IConfiguration configuration)
        {
            gObjAgAgendaHorarioAD = new AgendaHorarioDA(configuration);
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

        public List<AgendaHorario> RecAgendasHorario()
        {
            return EjecutarProcConEntidad(() => gObjAgAgendaHorarioAD.RecAgendasHorario());
        }

        public AgendaHorario? RecAgendasHorarioXId(int idAgendaHorario)
        {
            return EjecutarProcConEntidad(() => gObjAgAgendaHorarioAD.RecAgendasHorarioXId(idAgendaHorario));
        }

        public bool InsAgendasHorario(AgendaHorario agendaHorario)
        {
            return EjecutarProcSinEntidad(() => gObjAgAgendaHorarioAD.InsAgendasHorario(agendaHorario));
        }

        public bool ModAgendasHorario(AgendaHorario agendaHorario)
        {
            return EjecutarProcSinEntidad(() => gObjAgAgendaHorarioAD.ModAgendasHorario(agendaHorario));
        }

        public bool DelAgendasHorario(int idAgendaHorario)
        {
            return EjecutarProcSinEntidad(() => gObjAgAgendaHorarioAD.DelAgendasHorario(idAgendaHorario));
        }
    }
}
