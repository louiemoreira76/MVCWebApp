    using Microsoft.EntityFrameworkCore;
    using MVCWebApp.DataBase;

    var builder = WebApplication.CreateBuilder(args);
    //configurar tudo aqui 
    builder.Services.AddControllersWithViews();

//Injetando db contex
builder.Services.AddDbContext<DbContextH>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FornecedorPortal"),
        new MySqlServerVersion(new Version(8, 0, 33))  //versão do seu MySQL SELECT VERSION();
    ));

var app = builder.Build();


    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
