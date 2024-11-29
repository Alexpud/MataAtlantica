using FluentValidation;
using MataAtlantica.API.Application.Models;
using MataAtlantica.API.Application.Services;
using MataAtlantica.API.Domain.Abstract.Repositories;
using MataAtlantica.API.Domain.Abstract.Services;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Models.Validators;
using MataAtlantica.API.Domain.Profiles;
using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Infrastructure.Data;
using MataAtlantica.API.Infrastructure.Repositories;
using MataAtlantica.API.Infrastructure.Services;
using MataAtlantica.API.Presentation.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
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

builder.Services.AddAutoMapper(typeof(DomainProfile).Assembly);
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<ImagensProdutoService>();
builder.Services.AddScoped<FileStorageClientFactory>();

builder.Services.AddScoped<IValidator<AdicionarFornecedorDto>, CriarFornecedorValidator>();
builder.Services.AddScoped<IValidator<AdicionarProdutoDto>, CriarProdutoValidator>();
builder.Services.AddScoped<IValidator<AlterarProdutoDto>, AlterarProdutoValidator>();
builder.Services.AddScoped<IValidator<AlterarFornecedorDto>, AlterarFornecedorValidator>();
builder.Services.AddScoped<IValidator<MataAtlantica.API.Application.Models.AdicionarImagemProdutoDto>, AdicionarImagemProdutoValidator>();

builder.Services.Configure<FilesOptions>(
    builder.Configuration.GetSection(FilesOptions.Posicao));

builder.Services.AddDbContext<MataAtlanticaDbContext>(p =>
    p.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Desativar query tracking para contextos de leitura em um CQRS seria interessante

var app = builder.Build();

using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetRequiredService<MataAtlanticaDbContext>();
    context.Database.Migrate();
}

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
