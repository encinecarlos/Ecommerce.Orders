using AutoMapper;
using ECommerce.Orders.Api.Application.Command.Orders;
using ECommerce.Orders.Api.Application.Dtos;
using ECommerce.Orders.Api.Application.Notifications;
using ECommerce.Orders.Api.Domain.Entities;
using ECommerce.Orders.Api.Domain.Interfaces;
using ECommerce.Orders.Api.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;

namespace ECommerce.Orders.Test;

public class OrderHandlerTest
{
    private Mock<IOrdersRepository> _mockRepository;
    private Mock<IEventHandlerService> _mockEventHandlerService;
    private Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<OrderHandler>> _mockLogger;

    public OrderHandlerTest()
    {
        _mockLogger = new Mock<ILogger<OrderHandler>>();
        _mockRepository = new Mock<IOrdersRepository>();
        _mockEventHandlerService = new Mock<IEventHandlerService>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public async void ShouldCreateOrderAndPostEvent()
    {
        var orderCommand = new AddOrderCommand()
        {
            Order = new OrderRequest()
            {
                Customer = new Customer
                (
                    "customer",
                    "address",
                    "email",
                    "phone"
                ),
                Products = new List<Product>()
                {
                    new Product(
                        "Product",
                        1,
                        10
                    )
                },
                OrderTotal = 100,
                PaymentType = PaymentType.CreditCard,
                ShippingType = ShippingType.Standard
            }
        };
        _mockMapper.Setup(x => x.Map<Order>(It.IsAny<OrderRequest>()))
            .Returns(SetupOrder());

        _mockRepository.Setup(x => x.AddOrderAsync(It.IsAny<Order>()));

        _mockEventHandlerService.Setup(x => x.ProduceMessage(It.IsAny<string>(), It.IsAny<OrderNotification>()))
            .Returns(Task.FromResult(true));

        var order = new OrderHandler(
            _mockLogger.Object,
            _mockMapper.Object,
            _mockRepository.Object,
            _mockEventHandlerService.Object
        );

        var response = await order.Handle(orderCommand, new CancellationToken());
        Assert.NotNull(response.orderId);
    }

    private Order SetupOrder()
    {
        return new Order()
        {
            Id = Guid.NewGuid().ToString(),
            OrderId = DateTime.UtcNow.Ticks.ToString(),
            OrderDate = DateTime.Now,
            OrderStatus = OrderStatus.Pending,
            Products =
            {
                new Product($"test", 10, 1)
            }
        };
    }
}