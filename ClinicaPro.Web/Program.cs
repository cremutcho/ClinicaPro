using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Services;
using ClinicaPro.Infrastructure.Data;
using ClinicaPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation;
using ClinicaPro.Core.Features.ConvenioMedico.Commands;
using ClinicaPro.Core.Features.ConvenioMedico.Queries;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexão com SQLite
builder.Services.AddDbContext<ClinicaDbContext>(options =>
    options.UseSqlite("Data Source=clinicapro.db"));

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

// Repositórios genéricos
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Repositórios específicos
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
builder.Services.AddScoped<IConvenioMedicoRepository, ConvenioMedicoRepository>();

// Services
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IConvenioMedicoService, ConvenioMedicoService>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CriarConvenioMedicoCommandHandler).Assembly);
});

// Validações
builder.Services.AddValidatorsFromAssembly(typeof(CriarConvenioMedicoCommandValidator).Assembly);

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔥 CRIA BANCO AUTOMATICAMENTE (SEM QUEBRAR MIGRATION)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClinicaDbContext>();

    // 👇 ISSO EVITA O ERRO QUE VOCÊ TEVE
    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
}

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
                await roleManager.CreateAsync(new IdentityRole(role));
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