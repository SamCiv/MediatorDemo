using DemoLibrary;
using DemoLibrary.Behaviors;
using DemoLibrary.Context;
using DemoLibrary.DataAccess;
using DemoLibrary.Middlewares;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using DemoApi;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(policy =>
    {
        policy.AddPolicy("CorsPolicy", opt => opt
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
    });

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

builder.Services.AddTransient<ISchoolContext>(provider => provider.GetService<SchoolContext>());

//builder.Services.AddTransient<ISchoolContext, SchoolContext>();

//Aggiungo MediatR
builder.Services.AddMediatR(typeof(DemoLibraryMediatREntryPoint).Assembly); //carica l'intero Assembly

//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); //behavior per logging
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>)); //behavior per logging

//builder.Services.AddTransient(typeof(ExceptionHandlingMiddleware))

builder.Services.AddValidatorsFromAssembly(typeof(DemoLibraryMediatREntryPoint).Assembly);
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>)); //aggiunge un Behavior per l'exception Handling
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestGenericExceptionHandler<,>)); //aggiunge un Behavior per l'exception Handling

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


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
    context.Database.EnsureCreated();
   // DbInitializer.Initialize(context);
}



app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
