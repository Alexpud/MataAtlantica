using FluentValidation;
using MataAtlantica.API.Middleware;
using MataAtlantica.Application.Categorias.AdicionarCategoria;
using MataAtlantica.Application.Common;
using MataAtlantica.Application.Produtos.AdicionarProduto;
using MataAtlantica.Application.Produtos.AdicionarThumbnail;
using MataAtlantica.Application.Produtos.AlterarProduto;
using MataAtlantica.Application.Usuarios.AdicionarMetodoPagamento;
using MataAtlantica.Application.Usuarios.AdicionarUsuario;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Abstract.Services;
using MataAtlantica.Domain.Models.Fornecedores;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Models.Usuarios;
using MataAtlantica.Domain.Models.Validators;
using MataAtlantica.Domain.Profiles;
using MataAtlantica.Domain.Services;
using MataAtlantica.Infrastructure;
using MataAtlantica.Infrastructure.Abstract;
using MataAtlantica.Infrastructure.Data;
using MataAtlantica.Infrastructure.Identity;
using MataAtlantica.Infrastructure.Repositories;
using MataAtlantica.Infrastructure.Services;
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

builder.Services.AddAutoMapper(typeof(DomainProfile).Assembly);
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();   

builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<FileStorageClientFactory>();
builder.Services.AddScoped<UserIdentityService>();
builder.Services.AddScoped<IUserManagerWrapper, UserManagerWrapper>();

builder.Services.AddScoped<IValidator<AdicionarFornecedorDto>, CriarFornecedorValidator>();
builder.Services.AddScoped<IValidator<AdicionarProdutoDto>, CriarProdutoValidator>();
builder.Services.AddScoped<IValidator<AlterarProdutoDto>, AlterarProdutoValidator>();
builder.Services.AddScoped<IValidator<AlterarFornecedorDto>, AlterarFornecedorValidator>();
builder.Services.AddScoped<IValidator<AdicionarImagemProdutoDto>, AdicionarImagemProdutoValidator>();
builder.Services.AddScoped<IValidator<AdicionarThumbnailCommand>, AdicionarThumbnailCommandValidator>();
builder.Services.AddScoped<IValidator<AdicionarProdutoCommand>, AdicionarProdutoCommandValidtor>();
builder.Services.AddScoped<IValidator<AlterarProdutoCommand>, AlterarProdutoCommandValidator>();
builder.Services.AddScoped<IValidator<AdicionarCategoriaCommand>, AdicionarCategoriaCommandValidator>();
builder.Services.AddScoped<IValidator<AdicionarUsuarioCommand>, AdicionarUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<AdicionarMetodoPagamentoCommand>, AdicionarMetodoPagamentoCommandValidator>();
builder.Services.AddScoped<IValidator<AdicionarMetodoPagamentoDto>, AdicionarMetodoPagamentoDtoValidator>();
builder.Services.AddScoped<IValidator<AlterarMetodoPagamentoDto>, AlterarMetodoPagamentoDtoValidator>();

builder.Services.AddScoped<IValidadorCartao, ValidadorCartao>(); 

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);


builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AuthenticationDbContext>()
    .AddApiEndpoints();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    //options.Password.RequireDigit = true;
    //options.Password.RequireLowercase = true;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequireUppercase = true;
    //options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddDbContext<AuthenticationDbContext>(p =>
        p.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<RequestContextId>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.RegisterServicesFromAssemblies(typeof(ValidationBehavior<,>).Assembly);

});

//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.Configure<FilesOptions>(
    builder.Configuration.GetSection(FilesOptions.Posicao));

builder.Services
    .AddDbContext<MataAtlanticaDbContext>(p =>
        p.UseNpgsql(builder.Configuration.GetConnectionString("Default")));


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

app.MapControllers();

app.MapIdentityApi<User>();


app.Run();
