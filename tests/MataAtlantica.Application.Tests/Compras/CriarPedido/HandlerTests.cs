using FluentResults;
using MataAtlantica.Application.Compras.CriarPedido;
using MataAtlantica.Application.Tests.Builders;
using MataAtlantica.Domain.Abstract.Services;
using MataAtlantica.Domain.Entidades.Compras;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Compras;
using NSubstitute;

namespace MataAtlantica.Application.Tests.Compras.PedidoCompras;

public class CommandHandlerTests
{
    private readonly IError _erroAleatorio = new ValidationError("aletorio", "mensagem");
    private readonly IPagamentoService _pagamentoServiceMock;
    private readonly IPedidoCompraService _pedidoCompraServiceMock;
    private readonly CommandHandler _commandHandler;

    public CommandHandlerTests()
    {
        _pagamentoServiceMock = Substitute.For<IPagamentoService>();
        _pedidoCompraServiceMock = Substitute.For<IPedidoCompraService>();
        _commandHandler = new CommandHandler(_pagamentoServiceMock, _pedidoCompraServiceMock);
    }

    [Trait("Feature", "Criar Pedido Compra")]
    [Fact(DisplayName = "Deve retornar erros quando Adicionar falhar")]
    public async Task Handle_DeveRetornarErros_QuandoAdicionarFalhar()
    {
        // Arrange
        var command = new CriarPedidoCompraCommand();

        var adicionarResult = Result.Fail<PedidoCompra>(_erroAleatorio);
        _pedidoCompraServiceMock.Adicionar(Arg.Any<PedidoCompraDto>()).Returns(adicionarResult);

        // Act
        var response = await _commandHandler.Handle(command, default);

        // Assert
        Assert.Multiple(
            () => Assert.True(response.EhInvalida),
            () => _pedidoCompraServiceMock.Received(1).Adicionar(Arg.Any<PedidoCompraDto>()));
    }

    [Trait("Feature", "Criar Pedido Compra")]
    [Fact(DisplayName = "Deve retornar erros quando ValidarInformacoesPagamento falhar")]
    public async Task Handle_DeveRetornarErros_QuandoValidarInformacoesPagamentoFalhar()
    {
        // Arrange
        var command = new CriarPedidoCompraCommand
        {
            InformacaoPagamento = new InformacaoPagamentoDto()
        };

        var adicionarResult = Result.Ok(new PedidoCompraBuilder().BuildDefault().Create());
        _pedidoCompraServiceMock.Adicionar(Arg.Any<PedidoCompraDto>())
            .Returns(adicionarResult);

        var validarPagamentoResult = Result.Fail<bool>(_erroAleatorio);
        _pagamentoServiceMock.ValidarInformacoesPagamento(Arg.Any<InformacaoPagamentoDto>())
            .Returns(validarPagamentoResult);

        // Act
        var response = await _commandHandler.Handle(command, default);

        // Assert
        Assert.Multiple(
            () => Assert.True(response.EhInvalida),
            () => _pedidoCompraServiceMock.Received(1).Adicionar(Arg.Any<PedidoCompraDto>()),
            () => _pagamentoServiceMock.Received(1).ValidarInformacoesPagamento(Arg.Any<InformacaoPagamentoDto>()));
    }
}