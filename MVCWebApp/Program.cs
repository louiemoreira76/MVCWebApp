using Microsoft.EntityFrameworkCore;
using MVCWebApp.DataBase;

var builder = WebApplication.CreateBuilder(args);
// Configurar tudo aqui 
builder.Services.AddControllersWithViews();

// Injetando db context
builder.Services.AddDbContext<DbContextH>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FornecedorPortal"),
        new MySqlServerVersion(new Version(8, 0, 33))  // vers�o do seu MySQL SELECT VERSION();
    ));

// Configura��o do logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Testar a conex�o com o banco de dados
try
{
    await app.Services.GetService<DbContextH>().Database.CanConnectAsync();
    Console.WriteLine("Conex�o com o banco de dados bem-sucedida.");
}
catch (Exception ex)
{
    Console.WriteLine("Erro na conex�o com o banco de dados: " + ex.Message);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padr�o do HSTS � de 30 dias. Voc� pode querer mudar isso para cen�rios de produ��o, veja https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "fornecedor",
    pattern: "{controller=Fornecedor}/{action=Add}/{id?}");

app.Run();
