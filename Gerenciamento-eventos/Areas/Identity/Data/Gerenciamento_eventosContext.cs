using Gerenciamento_eventos.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_eventos.Models;
using System.Reflection.Emit;

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

        builder.Entity<Inscricao>()
            .HasOne(i => i.Evento)
            .WithMany(e => e.Inscricoes)
            .HasForeignKey(i => i.EventoId);

        builder.Entity<Inscricao>()
            .HasOne(i => i.Participante)
            .WithMany(p => p.Inscricoes)
            .HasForeignKey(i => i.ParticipanteUsuarioId)
            .HasPrincipalKey(p => p.UsuarioId);
    }

    public DbSet<Gerenciamento_eventos.Models.Evento> Evento { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Local> Local { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Participante> Participante { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Inscricao> Inscricao { get; set; } = default!;

    public DbSet<Gerenciamento_eventos.Models.Patrocinador> Patrocinador { get; set; } = default!;
}
