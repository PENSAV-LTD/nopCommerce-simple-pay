using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;
using Nop.Plugin.Payments.SimplePay.Controllers;
using Nop.Plugin.Payments.SimplePay.ViewModels;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.SimplePay.Unit.Test.Controllers;
public class SimplePayPaymentControllerTests
{
    private SimplePayPaymentController _sut;
    private Mock<IOrderService> _orderServiceMock;
    private Mock<IStoreContext> _storeContextMock;

    public SimplePayPaymentControllerTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _storeContextMock = new Mock<IStoreContext>();
        _sut = new SimplePayPaymentController(
            _orderServiceMock.Object,
            _storeContextMock.Object
            );
    }

    [Fact]
    public async Task CalledOrderServiceGetByIdAsync()
    {
        var orderId = 1;
        Expression<Func<IOrderService, Task<Order>>> expectedMethod = _ => _.GetOrderByIdAsync(orderId);
        _orderServiceMock.Setup(expectedMethod)
                        .ReturnsAsync(new Order());

        await _sut.Payment("", orderId);

        _orderServiceMock.Verify(expectedMethod, Times.Once());
    }

    [Fact]
    public async Task ThrowExceptionIfNoOrderFound()
    {
        _orderServiceMock.Setup(o => o.GetOrderByIdAsync(It.IsAny<int>()));

        Func<Task> act = () => _sut.Payment("", 1);

        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task CalledStoreContextCurrentStore()
    {
        SetupOrderServiceMock();
        Expression<Func<IStoreContext, Task<Store>>> expectedMethod = _ => _.GetCurrentStoreAsync();
        _storeContextMock.Setup(expectedMethod)
            .ReturnsAsync(new Store());

        await _sut.Payment("", 1);

        _storeContextMock.Verify(expectedMethod, Times.Once());
    }

    [Fact]
    public async Task PaymentUrlIsInTheResult()
    {
        var paymentUrl = "https://sandbox.simplepay.hu/pay/pay/pspHU/8f4oKRec5R1B696xlxbOcj1jRhhABA2pwSLQDPW60zoGSDWzDU";
        SetupOrderServiceMock();
        SetupStoreContextMock();

        var result = await _sut.Payment(paymentUrl, 1);

        result.Model.Should().BeOfType<PaymentViewModel>();
        var model = result.Model as PaymentViewModel;
        model.Should().NotBeNull();
        model.PaymentUrl.Should().Be(paymentUrl);
    }

    private void SetupStoreContextMock()
    {
        _storeContextMock
            .Setup(s => s.GetCurrentStoreAsync())
            .ReturnsAsync(new Store());
    }

    private void SetupOrderServiceMock()
    {
        _orderServiceMock.Setup(o => o.GetOrderByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync(new Order());
    }
}
