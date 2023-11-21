using BTLW.Models;
using BTLW.Repository;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectingString = builder.Configuration.GetConnectionString("Lttqnhom6Context");
builder.Services.AddDbContext<Lttqnhom6Context>(x=>x.UseSqlServer(connectingString));
builder.Services.AddScoped<ILoaiNoiThatRepository, LoaiNoiThatRepository>();
builder.Services.AddScoped<INuocSanXuatRepository, NuocSanXuatRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddSession();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


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
//
//app.UseAuthentication();
app.MapRazorPages();
//app.MapDefaultControllerRoute();
//
app.UseRouting();
app.UseAuthentication();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
