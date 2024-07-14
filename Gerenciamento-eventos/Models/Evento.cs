using Gerenciamento_eventos.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_eventos.Models
{
    public class Evento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [StringLength(3000, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
        [Display(Name = "Data do Evento")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O local é obrigatório.")]
        [Display(Name = "Local")]
        public int LocalId { get; set; }
        [Required(ErrorMessage = "O local é obrigatório.")]
        public Local Local { get; set; }
        public string CriadorUsuarioId { get; set; }
        public Criador Criador { get; set; }
        [Display(Name = "Patrocinador")]
        public int? PatrocinadorId { get; set; }
        public Patrocinador Patrocinador { get; set; }  
        public List<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
    }
}
