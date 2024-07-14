using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gerenciamento_eventos.Models.ViewModel
{
    public class EventoFilterViewModel
    {
        public List<Evento> Eventos { get; set; }
        public SelectList Locais { get; set; }
        public SelectList Patrocinadores { get; set; }
    }
}
