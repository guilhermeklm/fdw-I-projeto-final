﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Gerenciamento_eventos.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Usuario class
public class Usuario : IdentityUser
{
    [Required]
    public string Nome { get; set; }
}
