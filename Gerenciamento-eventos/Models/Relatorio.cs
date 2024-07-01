namespace Gerenciamento_eventos.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // Tipo do relatório, por exemplo, financeiro, participação, etc.
        public string Descricao { get; set; }
        public DateTime DataGeracao { get; set; }
    }
}
