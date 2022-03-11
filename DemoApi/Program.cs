using DemoLibrary.Middlewares;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
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
using DemoApi.Application.Queries.InstructorQuery;
using DemoApi.Application.Handler.InstructorHandler;
using Domain.AggregatesModel.InstructorAggregate;

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
    builder.RegisterAssemblyTypes(
                              typeof(AddInstructorCommandHandler).GetTypeInfo().Assembly).
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

    builder.RegisterType(typeof(InstructorQueries)).As(typeof(IInstructorQueries)).InstancePerDependency();
    builder.RegisterType(typeof(StudentQueries)).As(typeof(IStudentQueries)).InstancePerDependency();

    builder.RegisterType(typeof(RequestManager)).As(typeof(IRequestManager)).InstancePerLifetimeScope();

    builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope(); //potrebbe essere anche transient, ma cosi' e piu efficiente
    builder.RegisterType<InstructorRepository>().As<IInstructorRepository>().InstancePerLifetimeScope(); //potrebbe essere anche transient, ma cosi' e piu efficiente
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

//Aggiungo il services per il Db ---- QUesto e' presenete in Infrastructure
builder.Services.AddDbContext<Infrastructure.SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

//Entry point per caricare la libreria DemoLibrary
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

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
