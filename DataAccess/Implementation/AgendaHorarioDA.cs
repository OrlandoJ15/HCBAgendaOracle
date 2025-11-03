using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class AgendaHorarioDA : IAgendaHorarioDA
    {
        private readonly string _connection;
        public Exceptions gObjExceptions = new Exceptions();

        public AgendaHorarioDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<AgendaHorario> RecAgendasHorario()
        {
            var lista = new List<AgendaHorario>();

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecAgendasHorarioPA", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var agendaHorario = new AgendaHorario
                            {
                                IdAgendaHorario = Convert.ToInt32(reader["ID_AGENDAHORARIO"]),
                                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                                NumTipoCita = reader["NUM_TIPOCITA"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_TIPOCITA"]),
                                IndPrincipal = Convert.ToInt32(reader["IND_PRINCIPAL"]) == 1,
                                FechaInicial = Convert.ToDateTime(reader["FECHA_INICIAL"]),
                                FechaFinal = reader["FECHA_FINAL"] == DBNull.Value ? null : Convert.ToDateTime(reader["FECHA_FINAL"]),
                                Descripcion = reader["DESCRIPCION"]?.ToString() ?? string.Empty,
                                UsrRegistra = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                                FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                                IndPrincipalAnt = reader["IND_PRINCIPAL_ANT"] == DBNull.Value ? false : Convert.ToInt32(reader["IND_PRINCIPAL_ANT"]) == 1,
                                IndEstado = reader["IND_ESTADO"] == DBNull.Value ? false : Convert.ToInt32(reader["IND_ESTADO"]) == 1,
                                UsrModifica = reader["USR_MODIFICA"]?.ToString(),
                                FecModifica = reader["FEC_MODIFICA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_MODIFICA"])
                            };

                            lista.Add(agendaHorario);
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

        public AgendaHorario? RecAgendasHorarioXId(int idAgendaHorario)
        {
            AgendaHorario? agendaHorario = null;

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecAgendasHorarioXIdPA", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_ID_AGENDAHORARIO", OracleDbType.Int32).Value = idAgendaHorario;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            agendaHorario = new AgendaHorario
                            {
                                IdAgendaHorario = Convert.ToInt32(reader["ID_AGENDAHORARIO"]),
                                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                                NumTipoCita = reader["NUM_TIPOCITA"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_TIPOCITA"]),
                                IndPrincipal = Convert.ToInt32(reader["IND_PRINCIPAL"]) == 1,
                                FechaInicial = Convert.ToDateTime(reader["FECHA_INICIAL"]),
                                FechaFinal = reader["FECHA_FINAL"] == DBNull.Value ? null : Convert.ToDateTime(reader["FECHA_FINAL"]),
                                Descripcion = reader["DESCRIPCION"]?.ToString() ?? string.Empty,
                                UsrRegistra = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                                FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                                IndPrincipalAnt = reader["IND_PRINCIPAL_ANT"] == DBNull.Value ? false : Convert.ToInt32(reader["IND_PRINCIPAL_ANT"]) == 1,
                                IndEstado = reader["IND_ESTADO"] == DBNull.Value ? false : Convert.ToInt32(reader["IND_ESTADO"]) == 1,
                                UsrModifica = reader["USR_MODIFICA"]?.ToString(),
                                FecModifica = reader["FEC_MODIFICA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_MODIFICA"])
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

            return agendaHorario;
        }

        public bool InsAgendasHorario(AgendaHorario agenda)
        {
            return EjecutarProcedimiento("InsAgendasHorarioPA", agenda);
        }

        public bool ModAgendasHorario(AgendaHorario agenda)
        {
            return EjecutarProcedimiento("ModAgendasHorarioPA", agenda);
        }

        public bool DelAgendasHorario(int idAgendaHorario)
        {
            return EjecutarProcedimiento("DelAgendasHorarioPA", new AgendaHorario { IdAgendaHorario = idAgendaHorario });
        }

        // =======================================================
        // MÉTODO CENTRALIZADO PARA EJECUTAR SP
        // =======================================================
        private bool EjecutarProcedimiento(string procedimiento, AgendaHorario agenda)
        {
            bool resultado = false;

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand(procedimiento, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (procedimiento == "DelAgendasHorarioPA")
                    {
                        cmd.Parameters.Add("P_ID_AGENDAHORARIO", OracleDbType.Int32).Value = agenda.IdAgendaHorario;
                    }
                    else
                    {
                        cmd.Parameters.Add("P_NUM_AGENDA", OracleDbType.Int32).Value = agenda.NumAgenda;
                        cmd.Parameters.Add("P_NUM_TIPOCITA", OracleDbType.Int32).Value = (object?)agenda.NumTipoCita ?? DBNull.Value;
                        cmd.Parameters.Add("P_IND_PRINCIPAL", OracleDbType.Int32).Value = agenda.IndPrincipal ? 1 : 0;
                        cmd.Parameters.Add("P_FECHA_INICIAL", OracleDbType.Date).Value = agenda.FechaInicial;
                        cmd.Parameters.Add("P_FECHA_FINAL", OracleDbType.Date).Value = (object?)agenda.FechaFinal ?? DBNull.Value;
                        cmd.Parameters.Add("P_DESCRIPCION", OracleDbType.Varchar2).Value = agenda.Descripcion;
                        cmd.Parameters.Add("P_USR_REGISTRA", OracleDbType.Varchar2).Value = agenda.UsrRegistra;
                        cmd.Parameters.Add("P_IND_PRINCIPAL_ANT", OracleDbType.Int32).Value = agenda.IndPrincipalAnt ? 1 : 0;
                        cmd.Parameters.Add("P_IND_ESTADO", OracleDbType.Int32).Value = agenda.IndEstado ? 1 : 0;
                        cmd.Parameters.Add("P_USR_MODIFICA", OracleDbType.Varchar2).Value = agenda.UsrModifica ?? (object)DBNull.Value;
                        cmd.Parameters.Add("P_FEC_MODIFICA", OracleDbType.Date).Value = (object?)agenda.FecModifica ?? DBNull.Value;

                        if (procedimiento == "ModAgendasHorarioPA")
                            cmd.Parameters.Add("P_ID_AGENDAHORARIO", OracleDbType.Int32).Value = agenda.IdAgendaHorario;
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
