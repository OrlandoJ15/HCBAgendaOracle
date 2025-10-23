using Entities.Models;

namespace BussinessLogic.Interfaces
{
    public interface IParamEnvioCorreoAdjuntoLN
    {
        List<ParamEnvioCorreoAdjunto> RecParamEnvioCorreoAdjuntos();
        ParamEnvioCorreoAdjunto? RecParamEnvioCorreoAdjuntoXNombre(string nombre);
        bool InsParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto);
        bool ModParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto);
        bool DelParamEnvioCorreoAdjunto(string nombre);
    }
}
