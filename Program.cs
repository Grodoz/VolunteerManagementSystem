using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VolunteerManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VolunteerManagementConnection")));
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();