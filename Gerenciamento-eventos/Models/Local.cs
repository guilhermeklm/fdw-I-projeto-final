using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_eventos.Models
{
    public class Local
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [StringLength(200, ErrorMessage = "O endereço não pode exceder 200 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "A capacidade é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser um valor positivo")]
        public int Capacidade { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
        public string Descricao { get; set; }

        public List<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
