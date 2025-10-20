using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Services; // Mantido para IConsultaService
using ClinicaPro.Infrastructure.Data;
using ClinicaPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR; 
using ClinicaPro.Core.Entities; 

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
// 🔹 INJEÇÃO DE DEPENDÊNCIA (Limpando Services e Otimizando MediatR)
// =================================================================

// 🔹 Repositório Genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// 🔹 Repositórios Específicos
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>(); 
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

// ❌ REMOVIDOS: Injeções de serviços antigos (IPacienteService e IMedicoService)
// O PacientesController não os usa mais, e removê-los simplifica o DI.
// builder.Services.AddScoped<IPacienteService, PacienteService>();
// builder.Services.AddScoped<IMedicoService, MedicoService>();

// ✅ MANTIDO: Serviço que ainda não refatoramos
builder.Services.AddScoped<IConsultaService, ConsultaService>();


// 🔹 MediatR (CQRS)
builder.Services.AddMediatR(cfg => 
{
    // 🚨 CORRIGIDO: Garante que o MediatR encontre os Handlers no projeto ClinicaPro.Core 
    // e no projeto ClinicaPro.Web (onde está o Program.cs)
    cfg.RegisterServicesFromAssembly(typeof(Medico).Assembly); // Ex: Queries/Commands de Paciente/Medico
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); // Ex: O próprio projeto Web
});

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