using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IAgendaHorarioAD
    {
        // =======================================================
        // Recupera todos los registros de Agendas Horario
        // =======================================================
        List<AgendaHorario> RecAgendasHorario();

        // =======================================================
        // Recupera un registro de Agenda Horario por su ID
        // =======================================================
        AgendaHorario? RecAgendasHorarioXId(int idAgendaHorario);

        // =======================================================
        // Inserta una nueva Agenda Horario
        // =======================================================
        bool InsAgendasHorario(AgendaHorario agendaHorario);

        // =======================================================
        // Modifica una Agenda Horario existente
        // =======================================================
        bool ModAgendasHorario(AgendaHorario agendaHorario);

        // =======================================================
        // Elimina una Agenda Horario por su ID
        // =======================================================
        bool DelAgendasHorario(int idAgendaHorario);
    }
}
