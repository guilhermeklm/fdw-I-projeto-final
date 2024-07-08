namespace Gerenciamento_eventos.Models
{
    public class Patrocinador
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public List<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
