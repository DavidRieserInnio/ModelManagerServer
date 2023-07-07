using Microsoft.EntityFrameworkCore;
using ModelManagerServer;
using ModelManagerServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

var connectionString = builder.Configuration.GetConnectionString("DocuSQL");
builder.Services.AddDbContext<ModelManagerContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ModelRepository>();

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
