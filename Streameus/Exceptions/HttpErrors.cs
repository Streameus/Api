using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;

namespace Streameus.Exceptions
{
    public class HttpErrors
    {
        /// <summary>
        /// creates an <see cref="HttpResponseException"/> with a response code of 400
        /// and places the reason in the reason header and the body.
        /// </summary>
        /// <param name="reason">Explanation text for the client.</param>
        /// <returns>A new HttpResponseException</returns>
        public static HttpResponseException BadRequest(string reason)
        {
            return CreateHttpResponseException(reason, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// creates an <see cref="HttpResponseException"/> with a response code of 404
        /// and places the reason in the reason header and the body.
        /// </summary>
        /// <param name="reason">Explanation text for the client.</param>
        /// <returns>A new HttpResponseException</returns>
        public static HttpResponseException NotFound(string reason)
        {
            return CreateHttpResponseException(reason, HttpStatusCode.NotFound);
        }

        /// <summary>
        /// creates an <see cref="HttpResponseException"/> with a response code of 204
        /// and places the reason in the reason header and the body.
        /// </summary>
        /// <param name="reason">Explanation text for the client.</param>
        /// <returns>A new HttpResponseException</returns>
        public static HttpResponseException NoResult(string reason)
        {
            return CreateHttpResponseException(reason, HttpStatusCode.NoContent);
        }

        /// <summary>
        /// creates an <see cref="HttpResponseException"/> with a response code of 422
        /// and places the reason in the reason header and the body.
        /// </summary>
        /// <param name="validationModel">ModelState dictionnary used for validation.</param>
        /// <returns>A new HttpResponseException</returns>
        public static HttpResponseException ValidationError(ModelStateDictionary validationModel)
        {
            return
                CreateHttpResponseException(
                    JsonConvert.SerializeObject(
                        validationModel.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList()),
                    (HttpStatusCode) 422);
        }


        /// <summary>
        /// Creates an <see cref="HttpResponseException"/> to be thrown by the api.
        /// </summary>
        /// <param name="reason">Explanation text, also added to the body.</param>
        /// <param name="code">The HTTP status code.</param>
        /// <returns>A new <see cref="HttpResponseException"/></returns>
        private static HttpResponseException CreateHttpResponseException(string reason, HttpStatusCode code)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = code,
                ReasonPhrase = reason,
                Content = new StringContent(reason)
            };
            throw new HttpResponseException(response);
        }

        /// <summary>
        /// creates an <see cref="HttpResponseException"/> with a response code of 409
        /// and places the reason in the reason header and the body.
        /// </summary>
        /// <param name="reason">Explanation text for the client.</param>
        /// <returns>A new HttpResponseException</returns>
        public static HttpResponseException Conflict(string reason)
        {
            return CreateHttpResponseException(reason, HttpStatusCode.Conflict);
        }
    }
}