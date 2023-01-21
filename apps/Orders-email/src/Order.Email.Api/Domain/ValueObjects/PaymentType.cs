using System.ComponentModel;

namespace Order.Email.Api.Domain.ValueObjects;

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