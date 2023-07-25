using EcommerceAPI.Contracts.ServiceStripe.Interface;
using EcommerceAPI.Models.Stripe;
using Stripe;

namespace EcommerceAPI.Contracts.ServiceStripe
{
    public class StripeAppService : IStripeAppService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;
        public StripeAppService(CustomerService customerService,
                                TokenService tokenService,
                                ChargeService chargeService)
        {
            _customerService = customerService;
            _tokenService = tokenService;
            _chargeService = chargeService;

        }
        public async Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken ct)
        {
            TokenCreateOptions token = new TokenCreateOptions()
            {
                Card = new TokenCardOptions()
                {
                    Name = customer.Name,
                    Number = customer.CreditCard.CardNumber,
                    ExpYear = customer.CreditCard.ExpirationYear,
                    ExpMonth = customer.CreditCard.ExpirationMonth,
                    Cvc = customer.CreditCard.Cvc,
                }
            };

            var stripeToken = await _tokenService.CreateAsync(token);

            CustomerCreateOptions customerCreateOptions = new CustomerCreateOptions()
            {
                Name = customer.Name,
                Email = customer.Email,
                Source = stripeToken.Id
            };

            Customer createdCustomer = await _customerService.CreateAsync(customerCreateOptions);

            return new StripeCustomer(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        }

        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct)
        {
            ChargeCreateOptions paymentOptions = new ChargeCreateOptions()
            {
                Customer = payment.CustomerId,
                ReceiptEmail = payment.ReceiptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount,
            };

            var createdPayment = await _chargeService.CreateAsync(paymentOptions);

            return new StripePayment(
                createdPayment.CustomerId,
                createdPayment.ReceiptEmail,
                createdPayment.Description,
                createdPayment.Currency,
                createdPayment.Amount,
                createdPayment.Id
                );
        }
    }
}
