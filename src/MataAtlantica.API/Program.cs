using MataAtlantica.API.DI;
using MataAtlantica.API.Middleware;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Profiles;
using MataAtlantica.Infrastructure;
using MataAtlantica.Infrastructure.Data;
using MataAtlantica.Infrastructure.Identity;
using MataAtlantica.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Mata Atlantica API",
        Description = "API em dotnet core da loja Mata Atlantica",
    });

    //use fully qualified object names
    options.CustomSchemaIds(x => x.FullName);

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddDependencies();

builder.Services.AddCacheDependencies(builder.Configuration);

builder.Services.AddOtherDepdencies();

builder.Services.AddAuthorization();

builder.Services.AddAuthenticationDepdencies(builder.Configuration);

builder.Services.Configure<FilesOptions>(
    builder.Configuration.GetSection(FilesOptions.Posicao));

builder.Services
    .AddDbContext<MataAtlanticaDbContext>(p =>
        p.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
// Desativar query tracking para contextos de leitura em um CQRS seria interessante


builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();


using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetRequiredService<MataAtlanticaDbContext>();
    var identityContext = Scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();

    context.Database.Migrate();
    identityContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestContextMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseOutputCache();

app.MapControllers();

app.MapIdentityApi<User>();


app.Run();
