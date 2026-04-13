namespace LegacyRenewalApp;

public interface ISubsciptionRepository
{
    public SubscriptionPlan GetByCode(string code);
}