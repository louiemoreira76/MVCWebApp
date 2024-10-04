using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using MVCWebApp.DataBase;

var builder = WebApplication.CreateBuilder(args);
// Configurar tudo aqui 

// Injetando db context
builder.Services.AddDbContext<DbContextH>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FornecedorPortal"),
        new MySqlServerVersion(new Version(8, 0, 33))));

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});

builder.Services.AddControllersWithViews();
// Configura��o do logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Testar a conex�o com o banco de dados
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DbContextH>();
    try
    {
        await dbContext.Database.CanConnectAsync();
        Console.WriteLine("Conex�o com o banco de dados bem-sucedida.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro na conex�o com o banco de dados: " + ex.Message);
    }
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
