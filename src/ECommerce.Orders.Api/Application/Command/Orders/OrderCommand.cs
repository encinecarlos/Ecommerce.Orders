using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class OrderCommand : IRequestHandler<OrderRequest, OrderResponse>
{
    private ILogger<OrderCommand> Logger { get; }

    public OrderCommand(ILogger<OrderCommand> logger)
    {
        Logger = logger;
    }

    public async Task<OrderResponse> Handle(OrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            
            Logger.LogInformation("Start process of order");
            return await Task.FromResult(new OrderResponse()
            {
                ResponseMessage = $"Order generated at {DateTime.Now:dd-MM-yyyy hh:mm:ss}"
            });
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            throw;
        }
    }
}