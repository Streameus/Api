using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Streameus.DataAbstractionLayer.Contracts;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// Implementation for IRoomService
    /// </summary>
    public class RoomServices : IRoomServices
    {
        private string LicodeServerUrl { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RoomServices()
        {
            this.LicodeServerUrl = ConfigurationManager.AppSettings.Get("licodeServerURL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<string> CreateRoom(string roomName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(LicodeServerUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("/createRoom/" + roomName).Result;
                response.EnsureSuccessStatusCode();
                return (await response.Content.ReadAsAsync<RoomObject>())._id;
            }
        }

        /// <summary>
        /// Create a token to connect to a conference
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string CreateToken(string roomId, string userName, string role)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(LicodeServerUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var param = new {room = roomId, username = userName, role = role};
                HttpResponseMessage response = client.PostAsJsonAsync("/createToken/", param).Result;
                response.EnsureSuccessStatusCode();
                var token = response.Content.ReadAsStringAsync().Result;
                return token;
            }
        }

        private class RoomObject
        {
            public string _id { get; set; }
            public string name { get; set; }
        }
    }
}