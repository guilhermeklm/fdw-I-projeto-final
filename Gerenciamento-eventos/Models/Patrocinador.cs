using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_eventos.Models
{
    public class Patrocinador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        public List<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
