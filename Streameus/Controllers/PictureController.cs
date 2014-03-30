using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
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

        // POST api/picture
        /// <summary>
        /// PostFormData
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnsupportedMediaType">This type of media is not supported</exception>
        /// <exception cref="InternalServerError">Internal Server Error</exception>
        public Task<HttpResponseMessage> PostFormData()
        {
            string oldfileName;
            string filetype;
            string fileExtension;

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data/Picture/");
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
                        oldfileName = file.LocalFileName;
                        
                        //Debug.WriteLine("Type {0}", file.Headers.ContentType);
                        filetype = file.Headers.ContentType.MediaType;
                        try
                        {
                            if (filetype == "image/png")
                                fileExtension = ".png";
                            else if (filetype == "image/jpeg")
                                fileExtension = ".jpg";
                            else
                                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                            File.Move(oldfileName, root + "1" + fileExtension);
                            //File.Delete(oldfileName);
                        }
                        catch (FileNotFoundException e)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                        catch (IOException e)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                        catch (HttpResponseException e)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                        Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                        Trace.WriteLine("Server file path: " + file.LocalFileName);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                });

            return task;
        }
    }
}