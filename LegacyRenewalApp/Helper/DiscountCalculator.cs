namespace LegacyRenewalApp.Helper;

public class DiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscount(Customer customer, SubscriptionPlan plan, int seatCount, decimal baseAmount,
        bool useLoyaltyPoints)
    {
        decimal discount = 0m;

        switch (customer.Segment)
        {
            case "Silver": discount += baseAmount * 0.05m; break;
            case "Gold": discount += baseAmount * 0.10m; break;
            case "Platinium": discount += baseAmount * 0.15m; break;
            case "Education" when plan.IsEducationEligible: discount += baseAmount * 0.20m; break;
        }

        if (customer.YearsWithCompany >= 5)
            discount += baseAmount * 0.07m;
        else if (customer.YearsWithCompany >= 2)
            discount += baseAmount * 0.03m;

        if (seatCount >= 50)
            discount += baseAmount * 0.12m;
        else if (seatCount >= 20)
            discount += baseAmount * 0.08m;
        else if (seatCount >= 10)
            discount += baseAmount * 0.04m;

        if (useLoyaltyPoints && customer.LoyaltyPoints > 0)
        {
            int points = customer.LoyaltyPoints > 200 ? 200 : customer.LoyaltyPoints;
            discount += points;
        }

        return discount;
    }
}