namespace Entities.Models
{
    public class Cita
    {
        public int NumCita { get; set; }
        public int NumAgenda { get; set; }
        public DateTime FecHoraInicial { get; set; }
        public DateTime FecHoraFinal { get; set; }
        public int NumTipoCita { get; set; }
        public int IndPacienteRecargo { get; set; } = 0;
        public int? NumAgendaCompartida { get; set; }
        public int? NumExpediente { get; set; }
        public int? NumOrdServ { get; set; }
        public string NomPaciente { get; set; } = string.Empty;
        public DateTime FecRegistra { get; set; }
        public string UsrRegistra { get; set; } = string.Empty;
        public int IndAsistencia { get; set; } = 0;
        public DateTime FecActualiza { get; set; }
        public string UsrActualiza { get; set; } = string.Empty;
        public string? DesRecargo { get; set; }
        public string? DesObservacion { get; set; }
        public int IndReferenciaMedica { get; set; } = 0;
        public string? CodProfRefiere { get; set; }
        public string? DesReferencia { get; set; }
        public string IndReplicar { get; set; } = "S";
        public int? NumCitaMadre { get; set; }
        public int IndConfirmacion { get; set; } = 0;
        public DateTime? FecConfirmacion { get; set; }
        public string? DesConfirmacion { get; set; }
        public string? UsrConfirmacion { get; set; }
        public DateTime HoraPresentarse { get; set; }
        public int? NumTipoProc { get; set; }
        public int IndAplicaSeguro { get; set; } = 0;
        public int IndCopago { get; set; } = 0;
        public int IndDeducible { get; set; } = 0;
        public int IndCoberturaTotal { get; set; } = 0;
        public string? ObservacionesSeguro { get; set; }
        public string? CodInstituc { get; set; }
        public int IndCitaSecundaria { get; set; } = 0;
        public int? IndAceptaObs { get; set; }
    }
}
