using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// Classe de base pour les exceptions de l'API
    /// </summary>
    public class ApiException : Exception
    {
        private readonly HttpStatusCode _statusCode;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode">Code d'erreur</param>
        /// <param name="reason">Message d'erreur</param>
        /// <param name="ex">Exception</param>
        public ApiException(HttpStatusCode statusCode, string reason, Exception ex)
            : base(reason, ex)
        {
            this._statusCode = statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode">Code d'erreur</param>
        /// <param name="reason">Message d'erreur</param>
        public ApiException(HttpStatusCode statusCode, string reason)
            : base(reason)
        {
            this._statusCode = statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode">Code d'erreur</param>
        public ApiException(HttpStatusCode statusCode)
        {
            this._statusCode = statusCode;
        }

        /// <summary>
        /// Get errors' status code
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return this._statusCode; }
        }
    }
}