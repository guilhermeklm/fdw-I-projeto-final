using Gerenciamento_eventos.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_eventos.Models;

namespace Gerenciamento_eventos.Data;

public class Gerenciamento_eventosContext : IdentityDbContext<Usuario>
{
    public Gerenciamento_eventosContext(DbContextOptions<Gerenciamento_eventosContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Gerenciamento_eventos.Models.Evento> Evento { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Local> Local { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Participante> Participante { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Inscricao> Inscricao { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Patrocinador> Patrocinador { get; set; } = default!;
}
