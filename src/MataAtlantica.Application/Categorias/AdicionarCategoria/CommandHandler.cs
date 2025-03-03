using AutoMapper;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Categorias;
using MediatR;

namespace MataAtlantica.Application.Categorias.AdicionarCategoria;

public record AdicionarCategoriaCommand(string Nome) : BaseCommand, IRequest<CommandResponse<CategoriaDto>> 
{
    public override ValidationResult Validate()
    {
        var validator = new AdicionarCategoriaCommandValidator();
        return validator.Validate(this);
    }
}

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

public record class CommandResponse<TValue> : BaseResponse
{
    public TValue Value { get; set; }
    public CommandResponse(TValue value) : base()
    {
        Value = value;
    }
    public CommandResponse() {  }
}

public class CommandHandler(ICategoriaRepository categoriaRepository,
    IMapper mapper) : IRequestHandler<AdicionarCategoriaCommand, CommandResponse<CategoriaDto>>
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CommandResponse<CategoriaDto>> Handle(AdicionarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = new Categoria(request.Nome);
        _categoriaRepository.Adicionar(categoria);
        await _categoriaRepository.Commit();

        return new CommandResponse<CategoriaDto>(_mapper.Map<CategoriaDto>(categoria));
    }
}
