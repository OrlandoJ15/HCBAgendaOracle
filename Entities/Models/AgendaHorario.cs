namespace Entities.Models
{
    public class AgendaHorario
    {
        public int IdAgendaHorario { get; set; }            // ID_AGENDAHORARIO
        public int NumAgenda { get; set; }                  // NUM_AGENDA
        public int? NumTipoCita { get; set; }               // NUM_TIPOCITA (nullable)
        public bool IndPrincipal { get; set; }              // IND_PRINCIPAL (0 o 1)
        public DateTime FechaInicial { get; set; }          // FECHA_INICIAL
        public DateTime? FechaFinal { get; set; }           // FECHA_FINAL (nullable)
        public string Descripcion { get; set; }             // DESCRIPCION
        public string UsrRegistra { get; set; }             // USR_REGISTRA
        public DateTime FecRegistra { get; set; }           // FEC_REGISTRA
        public bool IndPrincipalAnt { get; set; } = false; // IND_PRINCIPAL_ANT
        public bool IndEstado { get; set; } = false;       // IND_ESTADO
        public string UsrModifica { get; set; }            // USR_MODIFICA
        public DateTime? FecModifica { get; set; }          // FEC_MODIFICA (nullable)
    }
}
