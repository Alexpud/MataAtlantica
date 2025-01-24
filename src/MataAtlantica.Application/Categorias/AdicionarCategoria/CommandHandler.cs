using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Categorias;
using MediatR;

namespace MataAtlantica.Application.Categorias.AdicionarCategoria;

public record AdicionarCategoriaCommand(string Nome) : IRequest<Result<CategoriaDto>> { }

public class AdicionarCategoriaCommandValidator : AbstractValidator<AdicionarCategoriaCommand>
{
    public AdicionarCategoriaCommandValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaCategoria))
            .WithMessage(nameof(EntityValidationErrors.NomeObrigatorioParaCategoria.Message));
    }
}

public class CommandHandler(ICategoriaRepository categoriaRepository,
    IMapper mapper) : IRequestHandler<AdicionarCategoriaCommand, Result<CategoriaDto>>
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CategoriaDto>> Handle(AdicionarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = new Categoria(request.Nome);
        _categoriaRepository.Adicionar(categoria);
        await _categoriaRepository.Commit();

        return Result.Ok(_mapper.Map<CategoriaDto>(categoria));
    }
}
