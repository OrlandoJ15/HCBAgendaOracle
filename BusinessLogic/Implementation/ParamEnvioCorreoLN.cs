﻿using Entities.Models;
using BussinessLogic.Interfaces;
using DataAccess.Interfaces;
using System.Collections.Generic;

namespace BussinessLogic.Implementation
{
    public class ParamEnvioCorreoLN : IParamEnvioCorreoLN
    {
        private readonly IParamEnvioCorreoAD _paramEnvioCorreoAD;

        public ParamEnvioCorreoLN(IParamEnvioCorreoAD paramEnvioCorreoAD)
        {
            _paramEnvioCorreoAD = paramEnvioCorreoAD;
        }

        public List<ParamEnvioCorreo> RecParamsEnvioCorreo()
        {
            return _paramEnvioCorreoAD.RecParamsEnvioCorreo();
        }

        public ParamEnvioCorreo RecParamEnvioCorreoXId(string compania)
        {
            return _paramEnvioCorreoAD.RecParamEnvioCorreoXId(compania);
        }

        public void InsParamEnvioCorreo(ParamEnvioCorreo param)
        {
            _paramEnvioCorreoAD.InsParamEnvioCorreo(param);
        }

        public void ModParamEnvioCorreo(ParamEnvioCorreo param)
        {
            _paramEnvioCorreoAD.ModParamEnvioCorreo(param);
        }

        public void DelParamEnvioCorreo(string compania)
        {
            _paramEnvioCorreoAD.DelParamEnvioCorreo(compania);
        }
    }
}
