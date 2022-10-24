using Demo_ASP_MVC_04_Models.BLL.Interfaces;
using Demo_ASP_MVC_04_Models.BLL.Services;
using Demo_ASP_MVC_04_Models.DAL.Interfaces;
using Demo_ASP_MVC_04_Models.DAL.Repositories;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IDbConnection>(service =>
{
    return new SqlConnection(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddTransient<IEngineCarRepository, EngineCarRepository>();

builder.Services.AddTransient<IBrandRepository, BrandRepository>();

builder.Services.AddTransient<IEngineCarService, EngineCarService>();

builder.Services.AddTransient<IBrandService, BrandService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
