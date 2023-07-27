using Microsoft.EntityFrameworkCore;
using ModelManagerServer;
using ModelManagerServer.Repositories;
using ModelManagerServer.St4;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

var connectionString = builder.Configuration.GetConnectionString("DocuSQL");
builder.Services.AddDbContext<ModelManagerContext>(options => {
    options.UseSqlServer(connectionString);
});
var st4connectionString = builder.Configuration.GetConnectionString("TPEConfigurator");
builder.Services.AddDbContext<TpeConfiguratorContext>(options => {
    options.UseSqlServer(st4connectionString);
});

builder.Services.AddScoped<ModelRepository>();
builder.Services.AddScoped<St4PartsRepository>();

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
