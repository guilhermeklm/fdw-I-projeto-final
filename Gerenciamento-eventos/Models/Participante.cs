using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_eventos.Models
{
    public class Participante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
        public List<Evento> Eventos { get; set; } = new List<Evento>();  
    }
}
