using AutoMapper;
using ECommerce.Orders.Api.Application.Notifications;
using ECommerce.Orders.Api.Domain.Entities;
using ECommerce.Orders.Api.Domain.Interfaces;
using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class OrderHandler : IRequestHandler<AddOrderCommand, OrderDto>
{
    private readonly ILogger<OrderHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IEventHandlerService _eventHandlerService;

    public OrderHandler(
        ILogger<OrderHandler> logger,
        IMapper mapper, 
        IOrdersRepository ordersRepository,
        IEventHandlerService eventHandlerService)
    {
        _logger = logger;
        _mapper = mapper;
        _ordersRepository = ordersRepository;
        _eventHandlerService = eventHandlerService;
    }

    public async Task<OrderDto> Handle(
        AddOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var result = _mapper.Map<Order>(request.Order);

        _logger.LogInformation("Save the order");

        await _ordersRepository.AddOrderAsync(result);

        _logger.LogInformation("Produce event to process the order");
        var orderNotification = new OrderNotification()
        {
            OrderId = result.OrderId,
            OrderDate = result.OrderDate
        };

        await _eventHandlerService.ProduceMessage("send-email", orderNotification);

        _logger.LogInformation("Send response to client");

        return await Task.FromResult(new OrderDto()
        {
            ResponseMessage = $"Order generated at {result.OrderDate:dd/MM/yyyy hh:mm:ss}",
            orderId = result.OrderId
        });
    }
}