using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IParamEnvioCorreoAdjuntoAD
    {
        // Recupera todos los adjuntos de parámetros de envío de correo
        List<ParamEnvioCorreoAdjunto> RecParamEnvioCorreoAdjuntos();

        // Recupera un adjunto por su nombre
        ParamEnvioCorreoAdjunto? RecParamEnvioCorreoAdjuntoXNombre(string nombre);

        // Inserta un nuevo adjunto
        bool InsParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto);

        // Modifica un adjunto existente
        bool ModParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto);

        // Elimina un adjunto por nombre
        bool DelParamEnvioCorreoAdjunto(string nombre);
    }
}
