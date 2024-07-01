namespace Gerenciamento_eventos.Models
{
    public class Inscricao
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int ParticipanteId { get; set; }
        public string StatusPagamento { get; set; }
        public DateTime DataInscricao { get; set; }

        // Navegação
        public Evento Evento { get; set; }
        public Participante Participante { get; set; }
    }
}
