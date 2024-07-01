namespace Gerenciamento_eventos.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public int Capacidade { get; set; }

        // Navegação
        public ICollection<Inscricao> Inscricoes { get; set; }
    }
}
