using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Services;
using ClinicaPro.Infrastructure.Data;
using ClinicaPro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexão com o banco de dados
builder.Services.AddDbContext<ClinicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Teste: exibir connection string no console
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"🔹 Conectando ao banco: {conn}");
Console.ResetColor();

// 🔹 Injeção de dependência dos repositórios genéricos
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// 🔹 Injeção de dependência dos serviços via interfaces
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

// 🔹 Adiciona suporte a controllers e views (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔹 Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 🔹 Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
