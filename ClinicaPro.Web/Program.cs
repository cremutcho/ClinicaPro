using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Services;
using ClinicaPro.Core;
using ClinicaPro.Infrastructure.Data;
using ClinicaPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ClinicaPro.Core.Entities;
using FluentValidation;

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

// =========================
// Repositórios Genéricos
// =========================
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// =========================
// Repositórios Específicos
// =========================
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IContaPagarRepository, ContaPagarRepository>();
builder.Services.AddScoped<IContaReceberRepository, ContaReceberRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();

// =========================
// 🔥 SERVICES (ESSENCIAL)
// =========================
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
// (adicione outros services conforme for refatorando)

// =========================
// MediatR (CQRS)
// =========================
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Medico).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// =========================
// Validações
// =========================
builder.Services.AddValidatorsFromAssembly(typeof(Medico).Assembly);

// =========================
// MVC
// =========================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔹 Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// 🔹 Seed de roles e usuários
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Admin", "Medico", "Recepcionista", "RH" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

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
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao criar roles ou usuários: {ex.Message}");
    }
}

app.Run();
