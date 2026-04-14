namespace LegacyRenewalApp;

public interface IDiscountCalculator
{
    decimal CalculateDiscount(
        Customer customer,
        SubscriptionPlan plan,
        int seatCount,
        decimal baseAmount,
        bool useLoyaltyPoints
        );
}