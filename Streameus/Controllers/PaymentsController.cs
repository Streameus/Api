﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;

namespace Streameus.Controllers
{
    /// <summary>
    /// Controller in charge of all the payements
    /// </summary>
    [RoutePrefix("api")]
    public class PaymentsController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IPaymentServices _paymentServices;

        /// <summary>
        /// Default controller
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="paymentServices"></param>
        public PaymentsController(IUserServices userServices, IPaymentServices paymentServices)
        {
            if (userServices == null) throw new ArgumentNullException("userServices");
            if (paymentServices == null) throw new ArgumentNullException("paymentServices");
            this._userServices = userServices;
            this._paymentServices = paymentServices;
        }

        /// <summary>
        /// Returns the balance for current user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("User/me/Balance")]
        public double GetMyBalance()
        {
            return this._userServices.GetById(this.GetCurrentUserId()).Balance;
        }

        /// <summary>
        /// Add a stripe token to a user.
        /// Can only be called after a stripe form has been displayed
        /// </summary>
        /// <param name="token"></param>
        /// <returns>200 if OK, 500 otherwise</returns>
        [Authorize]
        [Route("User/me/Token")]
        public void PostAddCcToken(string token)
        {
            this._paymentServices.CreatePayementIdForUser(this.GetCurrentUserId(), token);
        }

        /// <summary>
        /// Charge the user to increase balance
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>The new balance</returns>
        [Authorize]
        [Route("User/me/Charge")]
        public double PutCharge(int amount)
        {
            try
            {
                return this._paymentServices.RefillUserBalance(this.GetCurrentUserId(), amount);
            }
            catch (StreameusStripeError e)
            {
                throw new ServerErrorException(e.Message);
            }
        }
    }
}