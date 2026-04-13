namespace LegacyRenewalApp;

public interface IRenewalServiceValidator
{
    void Validate(
        int customerId,
        string planCode,
        int seatCount,
        string paymentMethod
    );
}