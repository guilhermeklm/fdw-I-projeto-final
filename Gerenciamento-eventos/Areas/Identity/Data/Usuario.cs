using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Gerenciamento_eventos.Areas.Identity.Data;

public class Usuario : IdentityUser
{
    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Nome { get; set; }
}

