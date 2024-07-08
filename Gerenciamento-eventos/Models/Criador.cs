using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciamento_eventos.Models
{
    public class Criador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UsuarioId { get; set; }

        [DisplayName("Criador do Evento")]
        public string Nome { get; set; }

        public Criador(string UsuarioId, string Nome)
        {
            this.UsuarioId = UsuarioId;
            this.Nome = Nome;
        }
    }
}
