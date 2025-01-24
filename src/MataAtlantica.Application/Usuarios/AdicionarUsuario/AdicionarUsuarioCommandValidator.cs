using FluentValidation;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Validators;

namespace MataAtlantica.Application.Usuarios.AdicionarUsuario;

public class AdicionarUsuarioCommandValidator : AbstractValidator<AdicionarUsuarioCommand>
{
    public AdicionarUsuarioCommandValidator()
    {
        RuleFor(p => p.Login)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithErrorCode(nameof(BusinessErrors.UsuarioComLoginDesformatado))
            .WithMessage(BusinessErrors.UsuarioComLoginDesformatado.Message);

        RuleFor(p => p.Senha)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.SenhaObrigatoriaParaUsuario))
            .WithMessage(BusinessErrors.SenhaObrigatoriaParaUsuario.Message);

        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.NomeObrigatorioParaUsuario))
            .WithMessage(BusinessErrors.NomeObrigatorioParaUsuario.Message);

        RuleFor(p => p.Sobrenome)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.SobrenomeObrigatorioParaUsuario))
            .WithMessage(BusinessErrors.SobrenomeObrigatorioParaUsuario.Message);

        RuleFor(p => p.Endereco).SetValidator(new EnderecoFornecedorValidator());
    }
}
