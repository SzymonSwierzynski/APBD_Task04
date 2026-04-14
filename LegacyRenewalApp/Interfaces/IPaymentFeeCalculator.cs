namespace LegacyRenewalApp;

public interface IPaymentFeeCalculator
{
    decimal Calculate(string paymentMethod, decimal amount);
}