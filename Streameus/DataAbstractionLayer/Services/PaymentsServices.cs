using System;
using Microsoft.Ajax.Utilities;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Stripe;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    ///     Service in charge of all the payement related actions
    /// </summary>
    public class PaymentsServices : IPayementServices
    {
        private readonly StripeChargeService _chargeService;
        private readonly StripeCustomerService _customerService;
        private readonly IUserServices _userServices;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="customerService"></param>
        /// <param name="chargeService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PaymentsServices(IUserServices userServices, StripeCustomerService customerService,
            StripeChargeService chargeService)
        {
            if (userServices == null) throw new ArgumentNullException("userServices");
            if (customerService == null) throw new ArgumentNullException("customerService");
            if (chargeService == null) throw new ArgumentNullException("chargeService");
            this._userServices = userServices;
            this._customerService = customerService;
            this._chargeService = chargeService;
        }

        /// <summary>
        ///     Add a credit card token to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        public void CreatePayementIdForUser(int userId, string token)
        {
            var user = this._userServices.GetById(userId);

            try
            {
                //Create a stripe customer
                var newCustomer = new StripeCustomerCreateOptions {TokenId = token};
                StripeCustomer stripeCustomer = this._customerService.Create(newCustomer);
                //Save the stripe customer id
                user.StripeCustomerId = stripeCustomer.Id;
                this._userServices.UpdateUser(user);
            }
            catch (StripeException e)
            {
                throw new ServerErrorException(e.StripeError.Message);
            }
        }

        /// <summary>
        ///     Charge a user to refill its balance
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <returns>the new balance</returns>
        public float RefillUserBalance(int userId, float amount)
        {
            var user = this._userServices.GetById(userId);

            if (user.StripeCustomerId.IsNullOrWhiteSpace())
                throw new StreameusStripeError(Translation.AddCardFirst);
            var newCharge = new StripeChargeCreateOptions();
            newCharge.Amount = (int) amount;
            newCharge.Currency = "eur";
            newCharge.CustomerId = user.StripeCustomerId;
            StripeCharge charge = this._chargeService.Create(newCharge);
            if (charge.Amount.HasValue)
                user.Balance += charge.Amount.Value;
            this._userServices.UpdateUser(user);
            return user.Balance;
        }

        /// <summary>
        /// charge an amount on the user's balance
        /// </summary>
        /// <param name="user"></param>
        /// <param name="amount"></param>
        public void ChargeUser(User user, float amount)
        {
            if (amount > user.Balance)
                throw new PaymentRequiredException(String.Format(Translation.BalanceTooLow, amount));
            user.Balance -= amount;
            this._userServices.UpdateUser(user);
        }
    }
}