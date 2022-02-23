using DemoLibrary;
using DemoLibrary.DataAccess;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
