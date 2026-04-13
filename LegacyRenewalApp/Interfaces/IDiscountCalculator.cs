namespace LegacyRenewalApp;

public interface IDiscountCalculator
{
    decimal CalculateDiscount(
        int customerId,
        string planCode,
        int seatCount,
        string paymentMethod,
        bool includePremiumSupport,
        bool useLoyaltyPoints
        );
}