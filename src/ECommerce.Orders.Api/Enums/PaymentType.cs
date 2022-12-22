using System.ComponentModel;

namespace ECommerce.Orders.Api.Enums;

public enum PaymentType
{
    [Description("CreditCard")]
    CreditCard,
    [Description("DebitCard")]
    DebitCard,
    [Description("Paypal")]
    Paypal,
    [Description("GooglePay")]
    GooglePay,
    [Description("ApplePay")]
    ApplePay
    
}