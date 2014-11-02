using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Payement services
    /// </summary>
    public interface IPaymentServices
    {
        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        void CreatePayementIdForUser(int userId, string token);

        /// <summary>
        /// Charge a user to refill its balance
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <returns>the new balance</returns>
        double RefillUserBalance(int userId, int amount);

        /// <summary>
        /// charge an amount on the user's balance
        /// </summary>
        /// <param name="user"></param>
        /// <param name="amount"></param>
        void ChargeUser(User user, double amount);
    }
}