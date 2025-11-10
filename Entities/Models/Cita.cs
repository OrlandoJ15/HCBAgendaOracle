namespace Entities.Models
{
    public class Cita
    {
        public int NumCita { get; set; }                     // PK
        public int NumAgenda { get; set; }
        public DateTime FecHoraInicial { get; set; }
        public DateTime FecHoraFinal { get; set; }
        public int NumTipoCita { get; set; }
        public int IndPacienteRecargo { get; set; }
        public int? NumAgendaCompartida { get; set; }
        public int? NumExpediente { get; set; }
        public int? NumOrdServ { get; set; }
        public string NomPaciente { get; set; } = string.Empty;
        public DateTime FecRegistra { get; set; }
        public string UsrRegistra { get; set; } = string.Empty;
        public int IndAsistencia { get; set; }
        public DateTime FecActualiza { get; set; }
        public string UsrActualiza { get; set; } = string.Empty;
        public string? DesRecargo { get; set; }
        public string? DesObservacion { get; set; }
        public short IndReferenciaMedica { get; set; }
        public string? CodProfRefiere { get; set; }
        public string? DesReferencia { get; set; }
        public string IndReplicar { get; set; } = "S";
        public int? NumCitaMadre { get; set; }
        public short IndConfirmacion { get; set; }
        public DateTime? FecConfirmacion { get; set; }
        public string? DesConfirmacion { get; set; }
        public string? UsrConfirmacion { get; set; }
        public DateTime? HoraPresentarse { get; set; }
        public int? NumTipoProc { get; set; }
        public short IndAplicaSeguro { get; set; }
        public short IndCopago { get; set; }
        public short IndDeducible { get; set; }
        public short IndCoberturaTotal { get; set; }
        public string? ObservacionesSeguro { get; set; }
        public string? CodInstituc { get; set; }
        public short IndCitaSecundaria { get; set; }
        public short? IndAceptaObs { get; set; }
    }
}
