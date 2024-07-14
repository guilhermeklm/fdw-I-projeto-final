using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gerenciamento_eventos.Models.ViewModel
{
    public class EventoViewModel
    {
        public Evento Evento { get; set; }
        public int InscricoesCount { get; set; }
    }
}
