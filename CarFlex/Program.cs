using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarFlexDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CarFlexDbContext") ?? throw new InvalidOperationException("Connection string 'CarFlexDbContext' not found.")));
builder.Services.AddDbContext<CarFlexContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarFlexContext") ?? throw new InvalidOperationException("Connection string 'CarFlexContext' not found.")));

// Add services to the container.
//testline
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
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