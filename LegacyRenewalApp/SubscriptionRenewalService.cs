using System;
using LegacyRenewalApp.Helper;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubsciptionRepository _planRepository;
        private readonly IRenewalServiceValidator _validator;
        private readonly IBillingGateway _billingGateway;
        private readonly IDiscountCalculator _discountCalculator;
        private readonly IPaymentFeeCalculator _paymentFeeCalculator;
        private readonly ITaxCalculator _taxCalculator;

        public SubscriptionRenewalService() { }

        public SubscriptionRenewalService(ICustomerRepository customerRepository, ISubsciptionRepository planRepository,
            IRenewalServiceValidator validator, IBillingGateway billingGateway, IDiscountCalculator discountCalculator,
            IPaymentFeeCalculator paymentFeeCalculator, ITaxCalculator taxCalculator)
        {
            _customerRepository = customerRepository;
            _planRepository = planRepository;
            _validator = validator;
            _billingGateway = billingGateway;
            _discountCalculator = discountCalculator;
            _paymentFeeCalculator = paymentFeeCalculator;
            _taxCalculator = taxCalculator;
        }


        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            _validator.Validate(customerId, planCode, seatCount, paymentMethod);

            var customer = _customerRepository.GetById(customerId);
            var plan = _planRepository.GetByCode(planCode);

            var baseAmount = plan.MonthlyPricePerSeat * seatCount;

            var discount = _discountCalculator.CalculateDiscount(
                customer, plan, seatCount, baseAmount, useLoyaltyPoints);

            decimal supportFee = 0m;
            if (includePremiumSupport)
            {
                if (plan.Code == "BASIC") supportFee = 50m;
                else if (plan.Code == "PRO") supportFee = 100m;
                else if (plan.Code == "ENTERPRISE") supportFee = 200m;
            }

            var subtotal = baseAmount - discount + supportFee;

            var paymentFee = _paymentFeeCalculator.Calculate(paymentMethod, subtotal);

            var tax = _taxCalculator.CalculateTax(customer.Country, subtotal);

            var total = subtotal + paymentFee + tax;

            var invoice = new RenewalInvoice
            {
                CustomerId = customer.Id,
                PlanCode = plan.Code,
                BaseAmount = baseAmount,
                Discount = discount,
                SupportFee = supportFee,
                PaymentFee = paymentFee,
                Tax = tax,
                TotalAmount = total,
                CreatedAt = DateTime.UtcNow
            };

            _billingGateway.SaveInvoice(invoice);
            _billingGateway.SendEmail(customer.Email, "Invoice", "Renewal completed");

            return invoice;
        }
    }
}