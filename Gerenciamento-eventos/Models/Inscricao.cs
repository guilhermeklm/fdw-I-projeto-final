using Gerenciamento_eventos.Areas.Identity.Data;

namespace Gerenciamento_eventos.Models
{
    public class Inscricao
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public string ParticipanteUsuarioId { get; set; }
        public DateTime DataInscricao { get; set; }
        public Evento Evento { get; set; }
        public Participante Participante { get; set; }
    }
}
