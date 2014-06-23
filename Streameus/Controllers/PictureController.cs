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
using Microsoft.AspNet.Identity;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions.HttpErrors;

namespace Streameus.Controllers
{
    /// <summary>
    ///     Picture controller
    /// </summary>
    public class PictureController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IConferenceServices _conferenceServices;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public PictureController(IUserServices userServices, IConferenceServices conferenceServices)
        {
            if (userServices == null) throw new ArgumentNullException("userServices");
            if (conferenceServices == null) throw new ArgumentNullException("conferenceServices");
            this._userServices = userServices;
            this._conferenceServices = conferenceServices;
        }

        // DELETE api/picture/{id}
        /// <summary>
        ///     Delete User Picture
        /// </summary>
        /// <exception cref="NotFoundException">Picture not found</exception>
        [Authorize]
        [Route("api/Picture/User/")]
        public void DeleteUserPicture()
        {
            var root = HttpContext.Current.Server.MapPath("~/App_Data/Picture/");
            var file = root + this.GetCurrentUserId();
            this.DeletePicture(file);
        }

        // DELETE api/picture/{id}
        /// <summary>
        ///     Delete Conference Picture
        /// </summary>
        /// <exception cref="ForbiddenException">You don't have the rights to update this conference picture</exception>
        /// <exception cref="NotFoundException">Picture not found</exception>
        [Authorize]
        [Route("api/Picture/Conference/{id}")]
        public void DeleteConferencePicture(int id)
        {
            var root = HttpContext.Current.Server.MapPath("~/App_Data/Picture/");
            var conference = this._conferenceServices.GetById(id);
            if (conference.OwnerId != this.GetCurrentUserId())
                throw new ForbiddenException(Translation.ForbiddenConfUpdate);
            var file = root + "conference" + id;
            this.DeletePicture(file);
        }


        // GET api/picture/{id}
        /// <summary>
        ///     Get User Picture from id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApiController.NotFound">Picture not found</exception>
        [Route("api/Picture/User/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var root = HttpContext.Current.Server.MapPath("~/App_Data/Picture/");
            var file = root + id;
            return this.ReturnPicture(file);
        }

        // GET api/picture/{id}
        /// <summary>
        ///     Get Conference Picture from id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApiController.NotFound">Picture not found</exception>
        [Route("api/Picture/Conference/{id}")]
        public HttpResponseMessage GetConference(int id)
        {
            var root = HttpContext.Current.Server.MapPath("~/App_Data/Picture/");
            var file = root + "conference" + id;
            return this.ReturnPicture(file);
        }


        // POST api/picture
        /// <summary>
        ///     Post a new User picture
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpStatusCode.UnsupportedMediaType">This type of media is not supported</exception>
        /// <exception cref="HttpStatusCode.InternalServerError">Internal Server Error</exception>
        /// <exception cref="FileNotFoundException">File Not Found</exception>
        /// <exception cref="IOException">File Not Found</exception>
        [Authorize]
        [Route("api/Picture/User")]
        public async Task<HttpResponseMessage> PostUserPicture()
        {
            int userId = this.GetCurrentUserId();
            return await this.SavePictureTask("" + userId);
        }

        // POST api/picture
        /// <summary>
        ///     Post a new Conference picture
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ForbiddenException">Conference update forbidden</exception>
        /// <exception cref="HttpStatusCode.UnsupportedMediaType">This type of media is not supported</exception>
        /// <exception cref="HttpStatusCode.InternalServerError">Internal Server Error</exception>
        /// <exception cref="FileNotFoundException">File Not Found</exception>
        /// <exception cref="IOException">File Not Found</exception>
        [Authorize]
        [Route("api/Picture/Conference/{id}")]
        public async Task<HttpResponseMessage> PostConferencePicture(int id)
        {
            var conference = this._conferenceServices.GetById(id);
            if (conference.OwnerId != this.GetCurrentUserId())
                throw new ForbiddenException(Translation.ForbiddenConfUpdate);
            return await this.SavePictureTask("conference" + conference.Id);
        }

        private async Task<HttpResponseMessage> SavePictureTask(string filename)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }


            var root = HttpContext.Current.Server.MapPath("~/App_Data/Picture/");
            var provider = new MultipartFormDataStreamProvider(root);

            // Read the form data and return an async task.
            var task = await Request.Content.ReadAsMultipartAsync(provider).
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
                        // Recup l'extension de fichier
                        string fileExtension = null;
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
                        if (File.Exists(root + filename + ".png"))
                            File.Delete(root + filename + ".png");
                        if (File.Exists(root + filename + ".jpg"))
                            File.Delete(root + filename + ".jpg");
                        // renomme le fichier temporaire en path + id + .extension
                        try
                        {
                            File.Move(oldfileName, root + filename + fileExtension);
                            //File.Delete(oldfileName);
                        }
                        catch (FileNotFoundException)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                        catch (IOException)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                        catch (HttpResponseException)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                });
            return task;
        }

        private void DeletePicture(string filename)
        {
            var path = "";

            if (File.Exists(filename + ".jpg"))
                path = filename + ".jpg";
            else if (File.Exists(filename + ".png"))
                path = filename + ".png";
            else
                throw new NotFoundException();
            File.Delete(path);
        }

        private HttpResponseMessage ReturnPicture(string filename)
        {
            string path;
            string type;

            if (File.Exists(filename + ".jpg"))
            {
                path = filename + ".jpg";
                type = "jpeg";
            }
            else if (File.Exists(filename + ".png"))
            {
                path = filename + ".png";
                type = "png";
            }
            else
            {
                var defaultPicture = HttpContext.Current.Server.MapPath("~/Content/defaultUser.png");
                path = defaultPicture;
                type = "png";
            }

            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content =
                    new StreamContent(fileStream)
                    {
                        Headers = {ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + type)}
                    }
            };
        }
    }
}