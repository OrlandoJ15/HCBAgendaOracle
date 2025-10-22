using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Implementation
{
    public class CitaDA : ICitaDA
    {
        private readonly string _connection;

        public CitaDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        public async Task<OperationResult> InsertarCitaAsync(Cita cita, List<CitaProcedimiento> servicios, string bitacoraDatosDespues)
        {
            var outputs = new OperationResult { Code = -1, Message = "Error desconocido" };
            string numCitaStr = null;

            // Usamos TransactionScope con soporte async
            var tsOptions = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await using var conn = new OracleConnection(_connection);
                await conn.OpenAsync();

                // 1) consultar_key -> verificar conflicto
                await using (var cmdCheck = new OracleCommand("AGENDA.CITAINSERTAR.consultar_key", conn))
                {
                    cmdCheck.CommandType = CommandType.StoredProcedure;
                    cmdCheck.Parameters.Add("pnumagenda", OracleDbType.Int32).Value = cita.NumAgenda;
                    cmdCheck.Parameters.Add("pfecha_inicial", OracleDbType.Date).Value = cita.FecHoraInicial;
                    cmdCheck.Parameters.Add("pfecha_final", OracleDbType.Date).Value = cita.FecHoraFinal;
                    cmdCheck.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    await using var reader = await cmdCheck.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        // Existe conflicto
                        outputs.Code = 1;
                        outputs.Message = "No es posible crear una cita para este horario";
                        return outputs;
                    }
                }

                // 2) insertar en ag_citainsertar (AGENDA.CITAINSERTAR.Insertar)
                await using (var cmdInsertarTmp = new OracleCommand("AGENDA.CITAINSERTAR.Insertar", conn))
                {
                    cmdInsertarTmp.CommandType = CommandType.StoredProcedure;
                    cmdInsertarTmp.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = cita.NumAgenda;
                    cmdInsertarTmp.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = cita.FecHoraInicial;
                    cmdInsertarTmp.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = cita.FecHoraFinal;
                    var pCodErrorTmp = new OracleParameter("pcoderror", OracleDbType.Int32) { Direction = ParameterDirection.Output };
                    var pDesErrorTmp = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000) { Direction = ParameterDirection.Output };
                    cmdInsertarTmp.Parameters.Add(pCodErrorTmp);
                    cmdInsertarTmp.Parameters.Add(pDesErrorTmp);

                    await cmdInsertarTmp.ExecuteNonQueryAsync();

                    var codTmp = Convert.ToInt32(pCodErrorTmp.Value == DBNull.Value ? 0 : pCodErrorTmp.Value);
                    var desTmp = pDesErrorTmp.Value?.ToString() ?? string.Empty;

                    if (codTmp != 0)
                    {
                        outputs.Code = codTmp;
                        outputs.Message = desTmp;
                        return outputs;
                    }
                }

                // 3) insertar en ag_cita (AGENDA.CITA.insertar)
                await using (var cmdInsertarCita = new OracleCommand("AGENDA.CITA.insertar", conn))
                {
                    cmdInsertarCita.CommandType = CommandType.StoredProcedure;

                    cmdInsertarCita.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = cita.NumAgenda;
                    cmdInsertarCita.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = cita.FecHoraInicial;
                    cmdInsertarCita.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = cita.FecHoraFinal;
                    cmdInsertarCita.Parameters.Add("pnum_tipocita", OracleDbType.Int32).Value = cita.NumTipoCita;
                    cmdInsertarCita.Parameters.Add("pind_pacienterecargo", OracleDbType.Int32).Value = cita.IndPacienteRecargo;
                    cmdInsertarCita.Parameters.Add("pnum_expediente", OracleDbType.Int32).Value = cita.NumExpediente;
                    cmdInsertarCita.Parameters.Add("pnum_ordserv", OracleDbType.Int32).Value = cita.NumOrdServ;
                    cmdInsertarCita.Parameters.Add("pnom_paciente", OracleDbType.Varchar2).Value = cita.NomPaciente ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pusr_registra", OracleDbType.Varchar2).Value = cita.UsrRegistra ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pdes_recargo", OracleDbType.Varchar2).Value = cita.DesRecargo ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pdes_observacion", OracleDbType.Varchar2).Value = cita.DesObservacion ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pind_referenciamedica", OracleDbType.Int32).Value = cita.IndReferenciaMedica;
                    cmdInsertarCita.Parameters.Add("pcod_profrefiere", OracleDbType.Varchar2).Value = cita.CodProfRefiere ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pdes_referencia", OracleDbType.Varchar2).Value = cita.DesReferencia ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pnum_citamadre", OracleDbType.Int32).Value = cita.NumCitaMadre == 0 ? (object)DBNull.Value : cita.NumCitaMadre;
                    cmdInsertarCita.Parameters.Add("pnum_agendacompartida", OracleDbType.Int32).Value = cita.NumAgendaCompartida;
                    cmdInsertarCita.Parameters.Add("phora_presentarse", OracleDbType.Date).Value = cita.HoraPresentarse ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pnum_tipoproc", OracleDbType.Int32).Value = cita.NumTipoProc == 0 ? (object)DBNull.Value : cita.NumTipoProc;
                    cmdInsertarCita.Parameters.Add("pind_aplicaseguro", OracleDbType.Int16).Value = cita.IndAplicaSeguro;
                    cmdInsertarCita.Parameters.Add("pind_copago", OracleDbType.Int16).Value = cita.IndCopago;
                    cmdInsertarCita.Parameters.Add("pind_deducible", OracleDbType.Int16).Value = cita.IndDeducible;
                    cmdInsertarCita.Parameters.Add("pind_coberturatotal", OracleDbType.Int16).Value = cita.IndCoberturaTotal;
                    cmdInsertarCita.Parameters.Add("pobservaciones_seguro", OracleDbType.Varchar2).Value = cita.ObservacionesSeguro ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pcod_instituc", OracleDbType.Varchar2).Value = cita.CodInstituc ?? (object)DBNull.Value;
                    cmdInsertarCita.Parameters.Add("pind_citasecundaria", OracleDbType.Int16).Value = cita.IndCitaSecundaria;

                    var pCodError = new OracleParameter("pcoderror", OracleDbType.Int32) { Direction = ParameterDirection.Output };
                    var pDesError = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000) { Direction = ParameterDirection.Output };
                    var pNumCitaOut = new OracleParameter("pnum_cita", OracleDbType.Int32) { Direction = ParameterDirection.Output };

                    cmdInsertarCita.Parameters.Add(pCodError);
                    cmdInsertarCita.Parameters.Add(pDesError);
                    cmdInsertarCita.Parameters.Add(pNumCitaOut);

                    await cmdInsertarCita.ExecuteNonQueryAsync();

                    var cod = Convert.ToInt32(pCodError.Value == DBNull.Value ? 0 : pCodError.Value);
                    var des = pDesError.Value?.ToString() ?? string.Empty;

                    if (cod != 0)
                    {
                        outputs.Code = cod;
                        outputs.Message = des;
                        return outputs;
                    }

                    numCitaStr = pNumCitaOut.Value?.ToString();
                }

                // 4) Insertar procedimientos si hay
                if (servicios != null && servicios.Count > 0)
                {
                    foreach (var servicio in servicios)
                    {
                        await using var cmdServicio = new OracleCommand("AGENDA.CITAPROCEDIMIENTO.insertar", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmdServicio.Parameters.Add("pnum_cita", OracleDbType.Int32).Value = Convert.ToInt32(numCitaStr);
                        cmdServicio.Parameters.Add("pcod_articulo", OracleDbType.Varchar2).Value = servicio.COD_ARTICULO ?? (object)DBNull.Value;
                        cmdServicio.Parameters.Add("pdes_detalle", OracleDbType.Varchar2).Value = servicio.DES_DETALLE ?? (object)DBNull.Value;

                        var pCodErrorSvc = new OracleParameter("pcoderror", OracleDbType.Int32) { Direction = ParameterDirection.Output };
                        var pDesErrorSvc = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000) { Direction = ParameterDirection.Output };
                        cmdServicio.Parameters.Add(pCodErrorSvc);
                        cmdServicio.Parameters.Add(pDesErrorSvc);

                        await cmdServicio.ExecuteNonQueryAsync();

                        var codSvc = Convert.ToInt32(pCodErrorSvc.Value == DBNull.Value ? 0 : pCodErrorSvc.Value);
                        var desSvc = pDesErrorSvc.Value?.ToString() ?? string.Empty;

                        if (codSvc != 0)
                        {
                            outputs.Code = codSvc;
                            outputs.Message = desSvc;
                            return outputs;
                        }
                    }
                }

                // 5) Insertar bitacora (llamamos a método helper)
                var bitRes = await InsertarBitacoraAsync(Convert.ToInt32(numCitaStr), cita.FecHoraInicial, cita.FecHoraFinal,
                                            "I", cita.UsrRegistra ?? string.Empty, string.Empty, bitacoraDatosDespues ?? string.Empty, cita.NumAgenda);
                if (bitRes.Code != 0)
                {
                    return bitRes;
                }

                // 6) Eliminar registro temporal de citainsertar (AGENDA.CITAINSERTAR.eliminar)
                await using (var cmdEliminarTmp = new OracleCommand("AGENDA.CITAINSERTAR.eliminar", conn))
                {
                    cmdEliminarTmp.CommandType = CommandType.StoredProcedure;
                    cmdEliminarTmp.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = cita.NumAgenda;
                    cmdEliminarTmp.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = cita.FecHoraInicial;
                    cmdEliminarTmp.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = cita.FecHoraFinal;
                    var pCodErrorDel = new OracleParameter("pcoderror", OracleDbType.Int32) { Direction = ParameterDirection.Output };
                    var pDesErrorDel = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000) { Direction = ParameterDirection.Output };
                    cmdEliminarTmp.Parameters.Add(pCodErrorDel);
                    cmdEliminarTmp.Parameters.Add(pDesErrorDel);

                    await cmdEliminarTmp.ExecuteNonQueryAsync();

                    var codDel = Convert.ToInt32(pCodErrorDel.Value == DBNull.Value ? 0 : pCodErrorDel.Value);
                    var desDel = pDesErrorDel.Value?.ToString() ?? string.Empty;

                    if (codDel != 0)
                    {
                        outputs.Code = codDel;
                        outputs.Message = desDel;
                        return outputs;
                    }
                }

                // Complete transaction
                scope.Complete();

                outputs.Code = 0;
                outputs.Message = "OK";
                outputs.Data = numCitaStr;
                return outputs;
            } // end using TransactionScope
        }

        public async Task<OperationResult> InsertarBitacoraAsync(int numCita, DateTime fecHoraInicial, DateTime fecHoraFinal, string indOperacion,
                                                                 string usuarioRegistra, string datosAntes, string datosDespues, int numAgenda)
        {
            var outputs = new OperationResult { Code = -1, Message = "Error desconocido bitacora" };

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.CITABITACORA.insertar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("pnum_cita", OracleDbType.Int32).Value = numCita;
            cmd.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = fecHoraInicial;
            cmd.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = fecHoraFinal;
            cmd.Parameters.Add("pind_operacion", OracleDbType.Varchar2).Value = indOperacion ?? (object)DBNull.Value;
            cmd.Parameters.Add("pusr_registra", OracleDbType.Varchar2).Value = usuarioRegistra ?? (object)DBNull.Value;
            cmd.Parameters.Add("pdes_datoantes", OracleDbType.Varchar2).Value = datosAntes ?? (object)DBNull.Value;
            cmd.Parameters.Add("pdes_datodespues", OracleDbType.Varchar2).Value = datosDespues ?? (object)DBNull.Value;
            cmd.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = numAgenda;

            var pCodError = new OracleParameter("pcoderror", OracleDbType.Int32) { Direction = ParameterDirection.Output };
            var pDesError = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pCodError);
            cmd.Parameters.Add(pDesError);

            await cmd.ExecuteNonQueryAsync();

            var cod = Convert.ToInt32(pCodError.Value == DBNull.Value ? 0 : pCodError.Value);
            var des = pDesError.Value?.ToString() ?? string.Empty;

            outputs.Code = cod;
            outputs.Message = des == string.Empty ? "OK" : des;

            return outputs;
        }
    }
}
