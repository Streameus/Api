using System.Threading.Tasks;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Licode room service
    /// </summary>
    public interface IRoomServices
    {
        /// <summary>
        /// Create a new Licode room
        /// </summary>
        /// <param name="roomName">the name of the room</param>
        /// <returns>a task to get the roomId</returns>
        Task<string> CreateRoom(string roomName);

        /// <summary>
        /// Create a token to allow a user to connect to a room
        /// </summary>
        /// <param name="roomId">The id of the room you want a token for</param>
        /// <param name="userName">the username of the user who wants to connect</param>
        /// <param name="role">The role to give to this user in the room</param>
        /// <returns>a task to get the token</returns>
        string CreateToken(string roomId, string userName, string role);
    }
}