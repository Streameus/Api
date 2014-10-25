namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Payement services
    /// </summary>
    public interface IPayementServices
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
        float ChargeUser(int userId, float amount);
    }
}