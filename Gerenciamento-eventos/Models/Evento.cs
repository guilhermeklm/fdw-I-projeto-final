﻿using Gerenciamento_eventos.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_eventos.Models
{
    public class Evento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [StringLength(3000, ErrorMessage = "A descrição deve ter no máximo 3000 caracteres.")]
        [Required(ErrorMessage = "A Descricao é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data inicio é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida.")]
        [Display(Name = "Data do inicio do Evento")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data fim é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida.")]
        [Display(Name = "Data do fim do Evento")]
        public DateTime DataFim { get; set; }

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
