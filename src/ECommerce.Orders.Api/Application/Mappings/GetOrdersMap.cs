using AutoMapper;

namespace ECommerce.Orders.Api.Application.Mappings;

public struct GetOrdersMap
{
    public string? OrderId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OrderTotal { get; set; }
    public decimal TotalAmount { get; set; }
    public string? OrderStatus { get; set; }
}