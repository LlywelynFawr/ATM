using ATMBlazorApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ATMBlazorApp.DAL;
using System.Net;
using Microsoft.EntityFrameworkCore;
using ATMBlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
HttpClient.DefaultProxy = new WebProxy("http://webproxy.civica.com:8080", true)
{
    Credentials = CredentialCache.DefaultCredentials
};

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ATMContext>(opt => opt.UseSqlServer(connectionstring));
builder.Services.AddDbContext<ATMContext>(opt => opt.UseSqlServer(connectionstring));

builder.Services.AddScoped<IAccountServices, AccountServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
