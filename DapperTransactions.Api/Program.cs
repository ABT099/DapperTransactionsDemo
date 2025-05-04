using DapperTransactions.Api.Middleware;
using DapperTransactions.Data;
using DapperTransactions.Data.Database;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation(x => 
        x.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
        
builder.Services.AddDataAccess(
    connectionString: builder.Configuration.GetConnectionString("Default") 
                      ?? throw new NullReferenceException("Connection string not found")
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapControllers();

// Migrate database
var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
databaseInitializer.InitializeDb();

app.Run();
