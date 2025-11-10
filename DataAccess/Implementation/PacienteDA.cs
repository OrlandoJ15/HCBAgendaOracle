using CommonMethods;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class PacienteDA:IPacienteDA
    {
        private readonly string _connection;
        public readonly Exceptions _exceptions;

        public PacienteDA(IConfiguration configuration, Exceptions exceptions)
        {
            _connection = configuration.GetConnectionString("OracleDb");
            _exceptions = exceptions;
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<Expediente> GetRecordByName(string primerNom, string segundoNom, string primerAp, string segundoAp)
        {
            var lista = new List<Expediente>();

            // Construir el parámetro de búsqueda igual que en VB
            var partes = new List<string>();
            if (!string.IsNullOrEmpty(primerAp)) partes.Add(primerAp);
            if (!string.IsNullOrEmpty(segundoAp)) partes.Add(segundoAp);
            if (!string.IsNullOrEmpty(primerNom)) partes.Add(primerNom);
            if (!string.IsNullOrEmpty(segundoNom)) partes.Add(segundoNom);
            string nomPaciente = "%" + string.Join("% ", partes).ToUpper() + "%";

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("AGENDA.EXPEDIENTE.exp_consultar_x_nombre", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.Add("pnompaciente", OracleDbType.Varchar2, nomPaciente, ParameterDirection.Input);

                    // Cursor de salida
                    cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = new Expediente
                            {
                                NumExpediente = reader.GetInt32(reader.GetOrdinal("NUM_EXPEDIENTE")),
                                NumCarnet = reader["NUM_CARNET"]?.ToString(),
                                PrimerAp = reader["PRIMER_AP"]?.ToString(),
                                SegundoAp = reader["SEGUNDO_AP"]?.ToString(),
                                PrimerNom = reader["PRIMER_NOM"]?.ToString(),
                                SegundoNom = reader["SEGUNDO_NOM"]?.ToString(),
                                Sexo = reader["SEXO"]?.ToString(),
                                CodTipDoc = reader["COD_TIPDOC"]?.ToString(),
                                NumId = reader["NUM_ID"]?.ToString(),
                                CodClase = reader["COD_CLASE"]?.ToString(),
                                FecNacimiento = reader["FEC_NACIMIENTO"] == DBNull.Value ? null : (DateTime?)reader["FEC_NACIMIENTO"],
                                TelHab = reader["TEL_HAB"] == DBNull.Value ? null : (int?)reader["TEL_HAB"],
                                TelOfic = reader["TEL_OFIC"] == DBNull.Value ? null : (int?)reader["TEL_OFIC"],
                                TelOpc = reader["TEL_OPC"] == DBNull.Value ? null : (int?)reader["TEL_OPC"],
                                Apartado = reader["APARTADO"]?.ToString(),
                                DireccionHab = reader["DIRECCION_HAB"]?.ToString(),
                                CodProvincia = reader["COD_PROVINCIA"]?.ToString(),
                                CodCanton = reader["COD_CANTON"]?.ToString(),
                                CodDistrito = reader["COD_DISTRITO"]?.ToString(),
                                CodBarrio = reader["COD_BARRIO"]?.ToString(),
                                FecExpediente = reader["FEC_EXPEDIENTE"] == DBNull.Value ? null : (DateTime?)reader["FEC_EXPEDIENTE"],
                                EstadoExp = reader["ESTADO_EXP"]?.ToString(),
                                Medico = reader["MEDICO"]?.ToString(),
                                CodMedico = reader["COD_MEDICO"]?.ToString(),
                                Observ = reader["OBSERV"]?.ToString(),
                                NomPaciente = reader["NOM_PACIENTE"]?.ToString(),
                                Usuario = reader["USUARIO"]?.ToString(),
                                NumVisitas = reader["NUM_VISITAS"] == DBNull.Value ? null : (int?)reader["NUM_VISITAS"],
                                CorreoElectronico = reader["CORREO_ELECTRONICO"]?.ToString(),
                                IndDonador = reader["IND_DONADOR"]?.ToString(),
                                NumDonador = reader["NUM_DONADOR"] == DBNull.Value ? null : (int?)reader["NUM_DONADOR"],
                                UserActualiza = reader["USER_ACTUALIZA"]?.ToString(),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : (DateTime?)reader["FEC_ACTUALIZA"],
                                DscCampoActualizado = reader["DSC_CAMPO_ACTUALIZADO"]?.ToString(),
                                CodGrupo = reader["COD_GRUPO"]?.ToString(),
                                CodSigno = reader["COD_SIGNO"]?.ToString(),
                                IndFallecido = reader["IND_FALLECIDO"]?.ToString(),
                                CodTipoTricare = reader["COD_TIPO_TRICARE"]?.ToString(),
                                SaldoDeducTricare = reader["SALDO_DEDUC_TRICARE"] == DBNull.Value ? null : (decimal?)reader["SALDO_DEDUC_TRICARE"],
                                MonConsumidoTricare = reader["MON_CONSUMIDO_TRICARE"] == DBNull.Value ? null : (decimal?)reader["MON_CONSUMIDO_TRICARE"],
                                CodIdioma = reader["COD_IDIOMA"]?.ToString(),
                                CodEtnia = reader["COD_ETNIA"]?.ToString(),
                                CodReligion = reader["COD_RELIGION"]?.ToString(),
                                NumIdAnterior = reader["NUM_ID_ANTERIOR"]?.ToString(),
                                CodProfesion = reader["COD_PROFESION"]?.ToString(),
                                IndConsEnvioInfo = reader["IND_CONS_ENVIOINFO"]?.ToString(),
                                CodPaisNac = reader["COD_PAIS_NAC"]?.ToString(),
                                CodCategoriaACSocial = reader["COD_CATEGORIA_ACSOCIAL"]?.ToString(),
                                TelCelular = reader["TEL_CELULAR"] == DBNull.Value ? null : (int?)reader["TEL_CELULAR"],
                                IndOrigen = reader["IND_ORIGEN"]?.ToString(),
                                IndVivePais = reader["IND_VIVEPAIS"] != DBNull.Value && Convert.ToInt32(reader["IND_VIVEPAIS"]) == 1,
                                CodPaisVive = reader["COD_PAISVIVE"]?.ToString(),
                                CodEstadoPaisVive = reader["COD_ESTADO_PAISVIVE"]?.ToString(),
                                IndPacienteConvenio = reader["IND_PACIENTE_CONVENIO"] != DBNull.Value && Convert.ToInt32(reader["IND_PACIENTE_CONVENIO"]) == 1,
                                CodConvenio = reader["COD_CONVENIO"]?.ToString(),
                                CorreoElectronicoFE = reader["CORREO_ELECTRONICO_FE"]?.ToString()
                            };

                            lista.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public List<Expediente> GetRecordByIdentification(string pidentificacion, string pcod_tipdoc)
        {
            var lista = new List<Expediente>();

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("AGENDA.EXPEDIENTE.exp_consultar_x_identificacion", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.Add("pidentificacion", OracleDbType.Varchar2, pidentificacion, ParameterDirection.Input);
                    cmd.Parameters.Add("pcod_tipdoc", OracleDbType.Varchar2, pcod_tipdoc, ParameterDirection.Input);

                    // Cursor de salida
                    cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = new Expediente
                            {
                                NumExpediente = reader.GetInt32(reader.GetOrdinal("NUM_EXPEDIENTE")),
                                NumCarnet = reader["NUM_CARNET"]?.ToString(),
                                PrimerAp = reader["PRIMER_AP"]?.ToString(),
                                SegundoAp = reader["SEGUNDO_AP"]?.ToString(),
                                PrimerNom = reader["PRIMER_NOM"]?.ToString(),
                                SegundoNom = reader["SEGUNDO_NOM"]?.ToString(),
                                Sexo = reader["SEXO"]?.ToString(),
                                CodTipDoc = reader["COD_TIPDOC"]?.ToString(),
                                NumId = reader["NUM_ID"]?.ToString(),
                                CodClase = reader["COD_CLASE"]?.ToString(),
                                FecNacimiento = reader["FEC_NACIMIENTO"] == DBNull.Value ? null : (DateTime?)reader["FEC_NACIMIENTO"],
                                TelHab = reader["TEL_HAB"] == DBNull.Value ? null : (int?)reader["TEL_HAB"],
                                TelOfic = reader["TEL_OFIC"] == DBNull.Value ? null : (int?)reader["TEL_OFIC"],
                                TelOpc = reader["TEL_OPC"] == DBNull.Value ? null : (int?)reader["TEL_OPC"],
                                Apartado = reader["APARTADO"]?.ToString(),
                                DireccionHab = reader["DIRECCION_HAB"]?.ToString(),
                                CodProvincia = reader["COD_PROVINCIA"]?.ToString(),
                                CodCanton = reader["COD_CANTON"]?.ToString(),
                                CodDistrito = reader["COD_DISTRITO"]?.ToString(),
                                CodBarrio = reader["COD_BARRIO"]?.ToString(),
                                FecExpediente = reader["FEC_EXPEDIENTE"] == DBNull.Value ? null : (DateTime?)reader["FEC_EXPEDIENTE"],
                                EstadoExp = reader["ESTADO_EXP"]?.ToString(),
                                Medico = reader["MEDICO"]?.ToString(),
                                CodMedico = reader["COD_MEDICO"]?.ToString(),
                                Observ = reader["OBSERV"]?.ToString(),
                                NomPaciente = reader["NOM_PACIENTE"]?.ToString(),
                                Usuario = reader["USUARIO"]?.ToString(),
                                NumVisitas = reader["NUM_VISITAS"] == DBNull.Value ? null : (int?)reader["NUM_VISITAS"],
                                CorreoElectronico = reader["CORREO_ELECTRONICO"]?.ToString(),
                                IndDonador = reader["IND_DONADOR"]?.ToString(),
                                NumDonador = reader["NUM_DONADOR"] == DBNull.Value ? null : (int?)reader["NUM_DONADOR"],
                                UserActualiza = reader["USER_ACTUALIZA"]?.ToString(),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : (DateTime?)reader["FEC_ACTUALIZA"],
                                DscCampoActualizado = reader["DSC_CAMPO_ACTUALIZADO"]?.ToString(),
                                CodGrupo = reader["COD_GRUPO"]?.ToString(),
                                CodSigno = reader["COD_SIGNO"]?.ToString(),
                                IndFallecido = reader["IND_FALLECIDO"]?.ToString(),
                                CodTipoTricare = reader["COD_TIPO_TRICARE"]?.ToString(),
                                SaldoDeducTricare = reader["SALDO_DEDUC_TRICARE"] == DBNull.Value ? null : (decimal?)reader["SALDO_DEDUC_TRICARE"],
                                MonConsumidoTricare = reader["MON_CONSUMIDO_TRICARE"] == DBNull.Value ? null : (decimal?)reader["MON_CONSUMIDO_TRICARE"],
                                CodIdioma = reader["COD_IDIOMA"]?.ToString(),
                                CodEtnia = reader["COD_ETNIA"]?.ToString(),
                                CodReligion = reader["COD_RELIGION"]?.ToString(),
                                NumIdAnterior = reader["NUM_ID_ANTERIOR"]?.ToString(),
                                CodProfesion = reader["COD_PROFESION"]?.ToString(),
                                IndConsEnvioInfo = reader["IND_CONS_ENVIOINFO"]?.ToString(),
                                CodPaisNac = reader["COD_PAIS_NAC"]?.ToString(),
                                CodCategoriaACSocial = reader["COD_CATEGORIA_ACSOCIAL"]?.ToString(),
                                TelCelular = reader["TEL_CELULAR"] == DBNull.Value ? null : (int?)reader["TEL_CELULAR"],
                                IndOrigen = reader["IND_ORIGEN"]?.ToString(),
                                IndVivePais = reader["IND_VIVEPAIS"] != DBNull.Value && Convert.ToInt32(reader["IND_VIVEPAIS"]) == 1,
                                CodPaisVive = reader["COD_PAISVIVE"]?.ToString(),
                                CodEstadoPaisVive = reader["COD_ESTADO_PAISVIVE"]?.ToString(),
                                IndPacienteConvenio = reader["IND_PACIENTE_CONVENIO"] != DBNull.Value && Convert.ToInt32(reader["IND_PACIENTE_CONVENIO"]) == 1,
                                CodConvenio = reader["COD_CONVENIO"]?.ToString(),
                                CorreoElectronicoFE = reader["CORREO_ELECTRONICO_FE"]?.ToString()
                            };

                            lista.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return lista;
        }
    }
}
