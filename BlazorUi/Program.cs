using BlazorUi.Data;
//using DemoLibrary;
//using DemoLibrary.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//Aggiungo il servizio DataAccess alla DI
//builder.Services.AddTransient<IDataAccess, DemoDataAccess>(); //non e' consigliato usare un Singleton

//Aggiungo MediatR
//builder.Services.AddMediatR(typeof(DemoLibraryMediatREntryPoint).Assembly);

//builder.Services.AddMediatR(typeof(DemoDataAccess).Assembly); //cosa fa? non carica solamente DemoDataAccess ma tutto l'Assembly alal ricerca di tutto cio che ha a che fare con MediatR
// builder.Services.AddMediatR(typeof(Program).Assembly); solitamente viene messa la classe Programs (o Startup) ma questa riguarda BlazorUI che non a che fare con MediatR

builder.Services.AddHttpClient();

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
