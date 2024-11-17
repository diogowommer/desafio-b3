using DesafioB3.API.Features.CalculateAmount;
using DesafioB3.API.Infrastructure.Handlers;
using DesafioB3.Core.Application.Behaviors;
using DesafioB3.Core.Application.Extensions;
using DesafioB3.Core.Application.Middlewares;
using DesafioB3.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

var accountsAssembly = typeof(Program).Assembly;
var accountsAssemblyName = accountsAssembly.GetName();

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(configuration =>
     {
         configuration.RegisterServicesFromAssemblyContaining<Program>();
         configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
     });

builder.Services.AddValidators(accountsAssembly);
builder.Services.AddSingleton<IExceptionHandler, ExceptionHandler>();
builder.Services.AddCors(options => options.AddPolicy(name: "FrontEnd",
    policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }
));

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapPost("/api/calculate-amount", CalculateAmountEndpoint.PostAsync);

app.UseCors("FrontEnd");

app.MapFallbackToFile("/index.html");

await app.RunAsync();
