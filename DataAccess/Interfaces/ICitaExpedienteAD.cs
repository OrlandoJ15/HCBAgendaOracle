using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface ITExpedienteAD
    {
        List<TExpediente> RecExpedientes();
        TExpediente? RecExpedienteXId(int numExpediente);
        bool InsExpediente(TExpediente expediente);
        bool ModExpediente(TExpediente expediente);
        bool DelExpediente(int numExpediente);
    }
}
