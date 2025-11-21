using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Services;
using ClinicaPro.Core; // Contém ValidationBehavior
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

// Repositório Genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Repositórios Específicos
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>(); 
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

// MediatR (CQRS)
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(Medico).Assembly); 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); 
});

// Validação
builder.Services.AddValidatorsFromAssembly(typeof(Medico).Assembly); 

// MVC
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

        // 1️⃣ Roles do sistema
        string[] roles = { "Admin", "Medico", "Recepcionista", "RH" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                Console.WriteLine($"✅ Role criada: {role}");
            }
        }

        // 2️⃣ Usuário Admin
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

        // 3️⃣ Usuário RH
        string rhEmail = "rh@clinicapro.com";
        string rhPass = "RH@123";
        if (await userManager.FindByEmailAsync(rhEmail) == null)
        {
            var rhUser = new IdentityUser
            {
                UserName = rhEmail,
                Email = rhEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(rhUser, rhPass);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(rhUser, "RH");
                Console.WriteLine($"✅ Usuário RH criado: {rhEmail} / {rhPass}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao criar roles ou usuários: {ex.Message}");
    }
}

app.Run();
