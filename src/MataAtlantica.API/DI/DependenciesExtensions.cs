using FluentValidation;
using MataAtlantica.API.Cache;
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
using MataAtlantica.Infrastructure.Abstract;
using MataAtlantica.Infrastructure.Identity;
using MataAtlantica.Infrastructure.Repositories;
using MataAtlantica.Infrastructure.Services;
using MataAtlantica.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MataAtlantica.API.DI;

public static class DependenciesExtensions
{
    public static IServiceCollection AddAuthenticationDepdencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
    .       AddBearerToken(IdentityConstants.BearerScheme);

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddApiEndpoints();

        services.Configure<IdentityOptions>(options =>
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

        services.AddDbContext<AuthenticationDbContext>(p =>
                p.UseSqlServer(configuration.GetConnectionString("Default")));

        return services;
    }

    public static IServiceCollection AddOtherDepdencies(this IServiceCollection services)
    {
        services.AddScoped<RequestContextId>();
        services.AddScoped<ILogService, LogService>();

        services.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.RegisterServicesFromAssemblies(typeof(ValidationBehavior<,>).Assembly);

        });

        return services;
    }
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainProfile).Assembly);
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        
        services.AddScoped<CategoriaService>();
        services.AddScoped<FornecedorService>();
        services.AddScoped<ProdutoService>();
        services.AddScoped<UsuarioService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<FileStorageClientFactory>();
        services.AddScoped<UserIdentityService>();
        services.AddScoped<IUserManagerWrapper, UserManagerWrapper>();
        
        services.AddScoped<IValidator<AdicionarFornecedorDto>, CriarFornecedorValidator>();
        services.AddScoped<IValidator<AdicionarProdutoDto>, CriarProdutoValidator>();
        services.AddScoped<IValidator<AlterarProdutoDto>, AlterarProdutoValidator>();
        services.AddScoped<IValidator<AlterarFornecedorDto>, AlterarFornecedorValidator>();
        services.AddScoped<IValidator<AdicionarImagemProdutoDto>, AdicionarImagemProdutoValidator>();
        services.AddScoped<IValidator<AdicionarThumbnailCommand>, AdicionarThumbnailCommandValidator>();
        services.AddScoped<IValidator<AdicionarProdutoCommand>, AdicionarProdutoCommandValidtor>();
        services.AddScoped<IValidator<AlterarProdutoCommand>, AlterarProdutoCommandValidator>();
        services.AddScoped<IValidator<AdicionarCategoriaCommand>, AdicionarCategoriaCommandValidator>();
        services.AddScoped<IValidator<AdicionarUsuarioCommand>, AdicionarUsuarioCommandValidator>();
        services.AddScoped<IValidator<AdicionarMetodoPagamentoCommand>, AdicionarMetodoPagamentoCommandValidator>();
        services.AddScoped<IValidator<AdicionarMetodoPagamentoDto>, AdicionarMetodoPagamentoDtoValidator>();
        services.AddScoped<IValidator<AlterarMetodoPagamentoDto>, AlterarMetodoPagamentoDtoValidator>();
        services.AddScoped<IValidadorCartao, ValidadorCartao>();
        return services;
    }

    public static IServiceCollection AddCacheDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisOutputCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "MataAtlantica";
        });

        services.AddOutputCache(configure =>
        {
            //configure.AddBasePolicy(p => p.AddPolicy<CustomPolicy>().SetCacheKeyPrefix("custom-"), true);
            configure.AddPolicy("CustomPolicy", new CustomPolicy());
        });
        return services;
    }
}
