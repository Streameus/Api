using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;

namespace Streameus.Controllers
{
    /// <summary>
    /// Picture controller
    /// </summary>
    public class PictureController : BaseController
    {
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PictureController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        // GET api/picture
        /// <summary>
        /// Affichage du form
        /// </summary>
        public void Get()
        {
        }

        /// <summary>
        /// PostFormData
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnsupportedMediaType">This type of media is not supported</exception>
        /// <exception cref="InternalServerError">Internal Server Error</exception>
        public Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            // Read the form data and return an async task.
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }

                    // This illustrates how to get the file names.
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                        Trace.WriteLine("Server file path: " + file.LocalFileName);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                });

            return task;
        }
    }
}