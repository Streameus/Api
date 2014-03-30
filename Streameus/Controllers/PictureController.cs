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
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data/Picture/");
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

                        var oldfileName = file.LocalFileName;
                        // recup le userId
                        int userId = 1;
                        string fileExtension = null;

                        // Recup l'extension de fichier
                        switch (file.Headers.ContentType.MediaType)
                        {
                            case "image/png":
                                fileExtension = ".png";
                                break;
                            case "image/jpeg":
                                fileExtension = ".jpg";
                                break;
                            default:
                                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                        }
                        // Si le fichier existe
                        if (File.Exists(root + userId + ".png"))
                            File.Delete(root + userId + ".png");
                        if (File.Exists(root + userId + ".jpg"))
                            File.Delete(root + userId + ".jpg");
                        // renomme le fichier temporaire en path + id + .extension
                        try
                        {
                            File.Move(oldfileName, root + userId + fileExtension);
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
                        //Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                        //Trace.WriteLine("Server file path: " + file.LocalFileName);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                });

            return task;
        }
    }
}