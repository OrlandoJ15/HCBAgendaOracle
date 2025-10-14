using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DataAccess.Implementation
{
    public class CitaAD : ICitaAD
    {
        private readonly string _connection;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaAD(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<Cita> RecCitas()
        {
            var lista = new List<Cita>();

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecCitasPA", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cita = new Cita
                            {
                                NumCita = Convert.ToInt32(reader["NUM_CITA"]),
                                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                                FecHoraInicial = Convert.ToDateTime(reader["FEC_HORAINICIAL"]),
                                FecHoraFinal = Convert.ToDateTime(reader["FEC_HORAFINAL"]),
                                NumTipoCita = Convert.ToInt32(reader["NUM_TIPOCITA"]),
                                IndPacienteRecargo = Convert.ToInt32(reader["IND_PACIENTERECARGO"]),
                                NumAgendaCompartida = reader["NUM_AGENDACOMPARTIDA"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_AGENDACOMPARTIDA"]),
                                NumExpediente = reader["NUM_EXPEDIENTE"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_EXPEDIENTE"]),
                                NumOrdServ = reader["NUM_ORDSERV"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_ORDSERV"]),
                                NomPaciente = reader["NOM_PACIENTE"]?.ToString() ?? string.Empty,
                                FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                                UsrRegistra = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                                IndAsistencia = Convert.ToInt32(reader["IND_ASISTENCIA"]),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_ACTUALIZA"]),
                                UsrActualiza = reader["USR_ACTUALIZA"]?.ToString() ?? string.Empty,
                                DesRecargo = reader["DES_RECARGO"]?.ToString(),
                                DesObservacion = reader["DES_OBSERVACION"]?.ToString(),
                                IndReferenciaMedica = Convert.ToInt32(reader["IND_REFERENCIAMEDICA"]),
                                CodProfRefiere = reader["COD_PROFREFIERE"]?.ToString(),
                                DesReferencia = reader["DES_REFERENCIA"]?.ToString(),
                                IndReplicar = reader["IND_REPLICAR"]?.ToString() ?? "S",
                                NumCitaMadre = reader["NUM_CITAMADRE"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_CITAMADRE"]),
                                IndConfirmacion = Convert.ToInt32(reader["IND_CONFIRMACION"]),
                                FecConfirmacion = reader["FEC_CONFIRMACION"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FEC_CONFIRMACION"]),
                                DesConfirmacion = reader["DES_CONFIRMACION"]?.ToString(),
                                UsrConfirmacion = reader["USR_CONFIRMACION"]?.ToString(),
                                HoraPresentarse = reader["HORA_PRESENTARSE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["HORA_PRESENTARSE"]),
                                NumTipoProc = reader["NUM_TIPOPROC"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_TIPOPROC"]),
                                IndAplicaSeguro = Convert.ToInt32(reader["IND_APLICASEGURO"]),
                                IndCopago = Convert.ToInt32(reader["IND_COPAGO"]),
                                IndDeducible = Convert.ToInt32(reader["IND_DEDUCIBLE"]),
                                IndCoberturaTotal = Convert.ToInt32(reader["IND_COBERTURATOTAL"]),
                                ObservacionesSeguro = reader["OBSERVACIONESSEGURO"]?.ToString(),
                                CodInstituc = reader["COD_INSTITUC"]?.ToString(),
                                IndCitaSecundaria = Convert.ToInt32(reader["IND_CITASECUNDARIA"]),
                                IndAceptaObs = reader["IND_ACEPTAOBS"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["IND_ACEPTAOBS"])
                            };

                            lista.Add(cita);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public Cita? RecCitaXId(int numCita)
        {
            Cita? cita = null;

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecCitaXId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_NUM_CITA", OracleDbType.Int32).Value = numCita;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cita = new Cita
                            {
                                NumCita = Convert.ToInt32(reader["NUM_CITA"]),
                                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                                FecHoraInicial = Convert.ToDateTime(reader["FEC_HORAINICIAL"]),
                                FecHoraFinal = Convert.ToDateTime(reader["FEC_HORAFINAL"]),
                                NumTipoCita = Convert.ToInt32(reader["NUM_TIPOCITA"]),
                                IndPacienteRecargo = Convert.ToInt32(reader["IND_PACIENTERECARGO"]),
                                NumAgendaCompartida = reader["NUM_AGENDACOMPARTIDA"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_AGENDACOMPARTIDA"]),
                                NumExpediente = reader["NUM_EXPEDIENTE"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_EXPEDIENTE"]),
                                NumOrdServ = reader["NUM_ORDSERV"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_ORDSERV"]),
                                NomPaciente = reader["NOM_PACIENTE"]?.ToString() ?? string.Empty,
                                FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                                UsrRegistra = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                                IndAsistencia = Convert.ToInt32(reader["IND_ASISTENCIA"]),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_ACTUALIZA"]),
                                UsrActualiza = reader["USR_ACTUALIZA"]?.ToString() ?? string.Empty,
                                DesRecargo = reader["DES_RECARGO"]?.ToString(),
                                DesObservacion = reader["DES_OBSERVACION"]?.ToString(),
                                IndReferenciaMedica = Convert.ToInt32(reader["IND_REFERENCIAMEDICA"]),
                                CodProfRefiere = reader["COD_PROFREFIERE"]?.ToString(),
                                DesReferencia = reader["DES_REFERENCIA"]?.ToString(),
                                IndReplicar = reader["IND_REPLICAR"]?.ToString() ?? "S",
                                NumCitaMadre = reader["NUM_CITAMADRE"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_CITAMADRE"]),
                                IndConfirmacion = Convert.ToInt32(reader["IND_CONFIRMACION"]),
                                FecConfirmacion = reader["FEC_CONFIRMACION"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FEC_CONFIRMACION"]),
                                DesConfirmacion = reader["DES_CONFIRMACION"]?.ToString(),
                                UsrConfirmacion = reader["USR_CONFIRMACION"]?.ToString(),
                                HoraPresentarse = reader["HORA_PRESENTARSE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["HORA_PRESENTARSE"]),
                                NumTipoProc = reader["NUM_TIPOPROC"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["NUM_TIPOPROC"]),
                                IndAplicaSeguro = Convert.ToInt32(reader["IND_APLICASEGURO"]),
                                IndCopago = Convert.ToInt32(reader["IND_COPAGO"]),
                                IndDeducible = Convert.ToInt32(reader["IND_DEDUCIBLE"]),
                                IndCoberturaTotal = Convert.ToInt32(reader["IND_COBERTURATOTAL"]),
                                ObservacionesSeguro = reader["OBSERVACIONESSEGURO"]?.ToString(),
                                CodInstituc = reader["COD_INSTITUC"]?.ToString(),
                                IndCitaSecundaria = Convert.ToInt32(reader["IND_CITASECUNDARIA"]),
                                IndAceptaObs = reader["IND_ACEPTAOBS"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["IND_ACEPTAOBS"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return cita;
        }

        // Métodos para insertar, actualizar y eliminar citas (ejecutando SP)
        public bool InsCita(Cita cita) => EjecutarProcedimiento("InsCitaPA", cita);
        public bool ModCita(Cita cita) => EjecutarProcedimiento("ModCitaPA", cita);
        public bool DelCita(int numCita) => EjecutarProcedimiento("DelCitaPA", new Cita { NumCita = numCita });

        private bool EjecutarProcedimiento(string procedimiento, Cita cita)
        {
            bool resultado = false;

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand(procedimiento, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (procedimiento == "DelCitaPA")
                    {
                        cmd.Parameters.Add("P_NUM_CITA", OracleDbType.Int32).Value = cita.NumCita;
                    }
                    else
                    {
                        cmd.Parameters.Add("P_NUM_AGENDA", OracleDbType.Int32).Value = cita.NumAgenda;
                        cmd.Parameters.Add("P_FEC_HORAINICIAL", OracleDbType.Date).Value = cita.FecHoraInicial;
                        cmd.Parameters.Add("P_FEC_HORAFINAL", OracleDbType.Date).Value = cita.FecHoraFinal;
                        cmd.Parameters.Add("P_NUM_TIPOCITA", OracleDbType.Int32).Value = cita.NumTipoCita;
                        cmd.Parameters.Add("P_IND_PACIENTERECARGO", OracleDbType.Int32).Value = cita.IndPacienteRecargo;
                        cmd.Parameters.Add("P_NUM_AGENDACOMPARTIDA", OracleDbType.Int32).Value = cita.NumAgendaCompartida ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = cita.NumExpediente ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_NUM_ORDSERV", OracleDbType.Int32).Value = cita.NumOrdServ ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_NOM_PACIENTE", OracleDbType.Varchar2).Value = cita.NomPaciente;
                        cmd.Parameters.Add("P_FEC_REGISTRA", OracleDbType.Date).Value = cita.FecRegistra;
                        cmd.Parameters.Add("P_USR_REGISTRA", OracleDbType.Varchar2).Value = cita.UsrRegistra;
                        cmd.Parameters.Add("P_IND_ASISTENCIA", OracleDbType.Int32).Value = cita.IndAsistencia;
                        cmd.Parameters.Add("P_FEC_ACTUALIZA", OracleDbType.Date).Value = cita.FecActualiza;
                        cmd.Parameters.Add("P_USR_ACTUALIZA", OracleDbType.Varchar2).Value = cita.UsrActualiza;
                        cmd.Parameters.Add("P_DES_RECARGO", OracleDbType.Varchar2).Value = cita.DesRecargo ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_DES_OBSERVACION", OracleDbType.Varchar2).Value = cita.DesObservacion ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_IND_REFERENCIAMEDICA", OracleDbType.Int32).Value = cita.IndReferenciaMedica;
                        cmd.Parameters.Add("P_COD_PROFREFIERE", OracleDbType.Varchar2).Value = cita.CodProfRefiere ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_DES_REFERENCIA", OracleDbType.Varchar2).Value = cita.DesReferencia ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_IND_REPLICAR", OracleDbType.Varchar2).Value = cita.IndReplicar;
                        cmd.Parameters.Add("P_NUM_CITAMADRE", OracleDbType.Int32).Value = cita.NumCitaMadre ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_IND_CONFIRMACION", OracleDbType.Int32).Value = cita.IndConfirmacion;
                        cmd.Parameters.Add("P_FEC_CONFIRMACION", OracleDbType.Date).Value = cita.FecConfirmacion ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_DES_CONFIRMACION", OracleDbType.Varchar2).Value = cita.DesConfirmacion ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_USR_CONFIRMACION", OracleDbType.Varchar2).Value = cita.UsrConfirmacion ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_HORA_PRESENTARSE", OracleDbType.Date).Value = cita.HoraPresentarse;
                        cmd.Parameters.Add("P_NUM_TIPOPROC", OracleDbType.Int32).Value = cita.NumTipoProc ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_IND_APLICASEGURO", OracleDbType.Int32).Value = cita.IndAplicaSeguro;
                        cmd.Parameters.Add("P_IND_COPAGO", OracleDbType.Int32).Value = cita.IndCopago;
                        cmd.Parameters.Add("P_IND_DEDUCIBLE", OracleDbType.Int32).Value = cita.IndDeducible;
                        cmd.Parameters.Add("P_IND_COBERTURATOTAL", OracleDbType.Int32).Value = cita.IndCoberturaTotal;
                        cmd.Parameters.Add("P_OBSERVACIONESSEGURO", OracleDbType.Varchar2).Value = cita.ObservacionesSeguro ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_COD_INSTITUC", OracleDbType.Varchar2).Value = cita.CodInstituc ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_IND_CITASECUNDARIA", OracleDbType.Int32).Value = cita.IndCitaSecundaria;
                        cmd.Parameters.Add("P_IND_ACEPTAOBS", OracleDbType.Int32).Value = cita.IndAceptaObs ?? (object)DBNull.Value;

                        if (procedimiento == "ModCitaPA")
                            cmd.Parameters.Add("P_NUM_CITA", OracleDbType.Int32).Value = cita.NumCita;
                    }

                    connection.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return resultado;
        }
    }
}
