using Gerenciamento_eventos.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciamento_eventos.Models
{
    public class Inscricao
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public string ParticipanteUsuarioId { get; set; }

        public Participante Participante { get; set; }

        public DateTime DataInscricao { get; set; }
    }
}
