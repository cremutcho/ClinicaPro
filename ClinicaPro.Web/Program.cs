using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using ClinicaPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation;

// Usings que já estavam (ou deveriam estar) corretos
using ClinicaPro.Core.Services; // Para o ValidationBehavior
using ClinicaPro.Core.Entities; // Para as entidades Medico, Consulta, etc.

// Usings necessários para referenciar as Queries e Handlers (Core Assembly)
using ClinicaPro.Core.Features.Consultas.Queries; 
// O using problemático foi removido: using ClinicaPro.Core.Features.Medicos.Entities; 
// Se precisar referenciar a entidade Medico, ClinicaPro.Core.Entities é o mais provável.

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexão com o banco de dados
builder.Services.AddDbContext<ClinicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ClinicaDbContext>();

// =================================================================
// 🔹 INJEÇÃO DE DEPENDÊNCIA
// =================================================================

// 🔹 Repositório Genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// 🔹 Repositórios Específicos
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>(); 
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
// ----------------------------------------------------------------------
// 🔹 MediatR (CQRS) - Registro de Handlers
builder.Services.AddMediatR(cfg => 
{
    // CORREÇÃO: Usa a ObterConsultaPorIdQuery para escanear a Assembly Core.
    cfg.RegisterServicesFromAssembly(typeof(ObterConsultaPorIdQuery).Assembly); 
    
    // Registra Handlers e outros serviços na Assembly Web (mantido)
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); 

    // MELHORIA: Unifica o registro do ValidationBehavior na configuração do MediatR
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// =================================================================
// 🚀 CONFIGURAÇÃO DA VALIDAÇÃO (FluentValidation)
// =================================================================

// 1. Encontra e registra todos os validadores (FluentValidation) no Core Assembly
// Usa a entidade Medico (assumindo que está em ClinicaPro.Core.Entities)
builder.Services.AddValidatorsFromAssembly(typeof(Medico).Assembly); 

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

// 🔹 Ativar autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Necessário para Identity

// 🔹 Seed de roles e usuário Admin
using (var scope = app.Services.CreateScope())
{
    // Código de Seed (Mantido)
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Admin", "Medico", "Recepcionista" };

        // Criar roles se não existirem
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                Console.WriteLine($"✅ Role criada: {role}");
            }
        }

        // Criar usuário Admin padrão
        string adminEmail = "admin@clinicapro.com";
        string adminPass = "Admin@123";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPass);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine($"✅ Usuário Admin criado: {adminEmail} / {adminPass}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao criar roles ou admin: {ex.Message}");
    }
}

app.Run();