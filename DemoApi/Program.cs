using DemoLibrary;
using DemoLibrary.Context;
using DemoLibrary.DataAccess;
using DemoLibrary.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Aggiungo il services per il Db
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

//Aggiungo il servizio DataAccess alla DI
builder.Services.AddSingleton<IDataAccess, DemoDataAccess>(); //non e' consigliato usare un Singleton
//Aggiungo MediatR
builder.Services.AddMediatR(typeof(DemoLibraryMediatREntryPoint).Assembly); //carica l'intero Assembly

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    //context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
