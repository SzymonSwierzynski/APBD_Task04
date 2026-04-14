namespace LegacyRenewalApp.Helper;

public class TaxCalculator : ITaxCalculator
{
    public decimal CalculateTax(string country, decimal amount)
    {
        return country switch
        {
            "Poland" => amount * 0.23m,
            "Germany" => amount * 0.19m,
            "UK" => amount * 0.20m,
            _ => 0m
        };
    }
}