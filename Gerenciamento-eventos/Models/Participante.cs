namespace Gerenciamento_eventos.Models
{
    public class Participante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        // Navegação
        public ICollection<Inscricao> Inscricoes { get; set; }
    }
}
