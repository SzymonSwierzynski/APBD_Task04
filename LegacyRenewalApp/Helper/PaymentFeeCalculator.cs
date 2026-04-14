using System;

namespace LegacyRenewalApp.Helper;

public class PaymentFeeCalculator : IPaymentFeeCalculator
{
    public decimal Calculate(string paymentMethod, decimal amount)
    {
        return paymentMethod.ToUpper() switch
        {
            "CARD" => amount * 0.025m,
            "PAYPAL" => amount * 0.03m,
            "BANK_TRANSFER" => 5m,
            "INVOICE" => 0m,
            _ => throw new ArgumentException()
        };
    }
}