using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class ParamEnvioCorreoAdjuntoAD : IParamEnvioCorreoAdjuntoAD
    {
        private readonly string _connection;
        private readonly Exceptions _exceptions;

        public ParamEnvioCorreoAdjuntoAD(IConfiguration configuration, Exceptions exceptions)
        {
            _connection = configuration.GetConnectionString("OracleDb");
            _exceptions = exceptions;
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<ParamEnvioCorreoAdjunto> RecParamEnvioCorreoAdjuntos()
        {
            var lista = new List<ParamEnvioCorreoAdjunto>();

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecParamEnvioCorreoAdjuntosPA", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var adjunto = new ParamEnvioCorreoAdjunto
                            {
                                NOMBRE = reader["NOMBRE"]?.ToString() ?? string.Empty,
                                CONTENIDO = reader["CONTENIDO"]?.ToString()?.Split(',') ?? new string[0]
                            };
                            lista.Add(adjunto);
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

        public ParamEnvioCorreoAdjunto? RecParamEnvioCorreoAdjuntoXNombre(string nombre)
        {
            ParamEnvioCorreoAdjunto? adjunto = null;

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecParamEnvioCorreoAdjuntoXNombrePA", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_NOMBRE", OracleDbType.Varchar2).Value = nombre;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            adjunto = new ParamEnvioCorreoAdjunto
                            {
                                NOMBRE = reader["NOMBRE"]?.ToString() ?? string.Empty,
                                CONTENIDO = reader["CONTENIDO"]?.ToString()?.Split(',') ?? new string[0]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return adjunto;
        }

        public bool InsParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto)
            => EjecutarProcedimiento("InsParamEnvioCorreoAdjuntoPA", adjunto);

        public bool ModParamEnvioCorreoAdjunto(ParamEnvioCorreoAdjunto adjunto)
            => EjecutarProcedimiento("ModParamEnvioCorreoAdjuntoPA", adjunto);

        public bool DelParamEnvioCorreoAdjunto(string nombre)
        {
            var adjunto = new ParamEnvioCorreoAdjunto { NOMBRE = nombre };
            return EjecutarProcedimiento("DelParamEnvioCorreoAdjuntoPA", adjunto);
        }

        // =======================================================
        // MÉTODO CENTRALIZADO PARA EJECUTAR SP
        // =======================================================
        private bool EjecutarProcedimiento(string procedimiento, ParamEnvioCorreoAdjunto adjunto)
        {
            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand(procedimiento, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("P_NOMBRE", OracleDbType.Varchar2).Value = adjunto.NOMBRE;

                    if (procedimiento != "DelParamEnvioCorreoAdjuntoPA")
                    {
                        string contenido = adjunto.CONTENIDO != null ? string.Join(",", adjunto.CONTENIDO) : string.Empty;
                        cmd.Parameters.Add("P_CONTENIDO", OracleDbType.Clob).Value = contenido;
                    }

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
        }
    }
}
