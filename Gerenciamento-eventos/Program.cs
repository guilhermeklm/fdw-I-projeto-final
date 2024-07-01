using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Gerenciamento_eventos.Models;
using Gerenciamento_eventos.Data;
using Gerenciamento_eventos.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Gerenciamento_eventosContextConnection") ?? throw new InvalidOperationException("Connection string 'Gerenciamento_eventosContextConnection' not found.");

builder.Services.AddDbContext<Gerenciamento_eventosContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Gerenciamento_eventosContext>();

// Adiciona os serviços de controle com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Adiciona autenticação
app.UseAuthorization();  // Adiciona autorização

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
