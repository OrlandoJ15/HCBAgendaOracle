using DataAccess.Implementation;
using Entities.Models;

namespace BussinessLogic.Interfaces
{
    public interface IAgendaHorarioBL
    {
        // Recupera todos los registros de agenda horario
        List<AgendaHorario> RecAgendasHorario();

        // Recupera un registro de agenda horario por su ID
        AgendaHorario? RecAgendasHorarioXId(int idAgendaHorario);

        // Inserta un nuevo registro de agenda horario
        bool InsAgendasHorario(AgendaHorario agendaHorario);

        // Modifica un registro de agenda horario existente
        bool ModAgendasHorario(AgendaHorario agendaHorario);

        // Elimina un registro de agenda horario por su ID
        bool DelAgendasHorario(int idAgendaHorario);
    }
}
