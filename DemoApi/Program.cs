using DemoLibrary;
using DemoLibrary.Middlewares;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using DemoApi;
using Infrastructure.Repositories;
using Infrastructure.Idempotency;
using Infrastructure;

using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;

using Domain.AggregatesModel.StudentAggregate;

using DemoApi.Application.Handler.StudentHandler;
using DemoApi.Application.Behaviors;
using DemoApi.Application.Handler;
using DemoApi.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    //devo dichiarare i servizi qui dentro
    /*builder.RegisterType<Mediator>()
           .As<IMediator>()
           .InstancePerLifetimeScope();*/
    builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

    builder.RegisterAssemblyTypes(
                              typeof(AddStudentCommandHandler).GetTypeInfo().Assembly).
                                   AsClosedTypesOf(typeof(IRequestHandler<,>));

    builder.Register<ServiceFactory>(context =>
        {
            var c = context.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });
    //builder.RegisterAssemblyTypes(typeof().GetTypeInfo().Assembly).AsImplementedInterfaces();

    builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency(); //Di Default anche senza esplicitare
    builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency(); 

    builder.RegisterGeneric(typeof(IdentifiedCommandHandler<,>)).As(typeof(IRequestHandler<,>)).InstancePerDependency();

    builder.RegisterType(typeof(StudentQueries)).As(typeof(IStudentQueries)).InstancePerDependency();
    builder.RegisterType(typeof(RequestManager)).As(typeof(IRequestManager)).InstancePerLifetimeScope();

    builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope(); //potrebbe essere anche transient, ma cosi' e piu efficiente

});

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
//builder.Services.AddDbContext<SchoolContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

//Aggiungo il services per il Db ---- QUesto e' presenete in Infrastructure
builder.Services.AddDbContext<Infrastructure.SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

//builder.Services.AddScoped(Infrastructure.SchoolContext);

//Aggiungo il servizio DataAccess alla DI
//builder.Services.AddSingleton<IDataAccess, DemoDataAccess>(); //non e' consigliato usare un Singleton

//builder.Services.AddTransient<ISchoolContext>(provider => provider.GetService<SchoolContext>());

//builder.Services.AddTransient<ISchoolContext, SchoolContext>();
//builder.Services.AddScoped<IRequestManager, RequestManager>();

//var assembly = AppDomain.CurrentDomain.Load("DemoLibrary");
//builder.Services.AddMediatR(assembly);
//builder.Services.AddScoped(typeof(IRequestHandler<,>), typeof(IdentifiedCommandHandler<,>));
//Aggiungo MediatR
//builder.Services.AddMediatR(typeof(DemoLibraryMediatREntryPoint).Assembly); //carica l'intero Assembly

//builder.Services.AddScoped(typeof(IRequestHandler<,>), typeof(IdentifiedCommandHandler<,>));

//builder.Services.AddTransient(typeof(IRequestManager), typeof(RequestManager));

//behavior per logging
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); 

//behavior per validazione FluentValidation
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

//Entry point per caricare la libreria DemoLibrary
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>)); //aggiunge un Behavior per l'exception Handling
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestGenericExceptionHandler<,>)); //aggiunge un Behavior per l'exception Handling

//behavior per Exception Handling
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

    //var context = services.GetRequiredService<SchoolContext>();
    var context = services.GetRequiredService<Infrastructure.SchoolContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
    SeedDB.Initialize(context);
}



app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
