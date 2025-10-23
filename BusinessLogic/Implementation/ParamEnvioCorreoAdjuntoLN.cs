using Entities.Models;
using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using CommonMethods;

namespace BussinessLogic.Implementation
{
    public class ParamEnvioCorreoAdjuntoLN : IParamEnvioCorreoAdjuntoLN
    {
        private readonly IParamEnvioCorreoAdjuntoAD _paramEnvioCorreoAdjuntoAD;
        private readonly Exceptions _exceptions;

        public ParamEnvioCorreoAdjuntoLN(IParamEnvioCorreoAdjuntoAD paramEnvioCorreoAdjuntoAD, Exceptions exceptions)
        {
            _paramEnvioCorreoAdjuntoAD = paramEnvioCorreoAdjuntoAD;
            _exceptions = exceptions;
        }

        public List<ParamEnvioCorreoAdjunto> RecParamEnvioCorreoAdjuntos()
        {
            try
            {
                return _paramEnvioCorreoAdjuntoAD.RecParamEnvioCorreoAdjuntos();
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
        }

        public ParamEnvioCorreoAdjunto? RecParamEnvioCorreoAdjuntoXNombre(string nombre)
        {
            try
            {
                return _paramEnvioCorreoAdjuntoAD.RecParamEnvioCorreoAdjuntoXNombre(nombre);
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
        }

        public bool InsParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto)
        {
            try
            {
                return _paramEnvioCorreoAdjuntoAD.InsParamEnvioCorreoAdjunto(adjunto);
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
        }

        public bool ModParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto)
        {
            try
            {
                return _paramEnvioCorreoAdjuntoAD.ModParamEnvioCorreoAdjunto(adjunto);
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
        }

        public bool DelParamEnvioCorreoAdjunto(string nombre)
        {
            try
            {
                return _paramEnvioCorreoAdjuntoAD.DelParamEnvioCorreoAdjunto(nombre);
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
        }
    }
}
