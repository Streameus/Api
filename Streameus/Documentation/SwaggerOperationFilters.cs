using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace Streameus.Documentation
{
    /// <summary>
    /// @TODO
    /// </summary>
    public class AddStandardResponseMessages : IOperationFilter
    {
        /// <summary>
        /// @TODO
        /// </summary>
        /// <param name="dataTypeRegistry"></param>
        /// <param name="apiDescription"></param>
        /// <param name="operation"></param>
        public void Apply(Operation operation, DataTypeRegistry dataTypeRegistry, ApiDescription apiDescription)
        {
            operation.ResponseMessages.Add(new ResponseMessage
            {
                Code = (int) HttpStatusCode.OK,
                Message = "It's all good!"
            });

            operation.ResponseMessages.Add(new ResponseMessage
            {
                Code = (int) HttpStatusCode.InternalServerError,
                Message = "Somethings up!"
            });
        }
    }

    /// <summary>
    /// @TODO
    /// </summary>
    public class AddAuthorizationResponseMessages : IOperationFilter
    {
        /// <summary>
        /// @TODO
        /// </summary>
        /// <param name="dataTypeRegistry"></param>
        /// <param name="apiDescription"></param>
        /// <param name="operation"></param>
        public void Apply(Operation operation, DataTypeRegistry dataTypeRegistry, ApiDescription apiDescription)
        {
            if (apiDescription.ActionDescriptor.GetFilters().OfType<AuthorizeAttribute>().Any())
            {
                operation.ResponseMessages.Add(new ResponseMessage
                {
                    Code = (int) HttpStatusCode.Unauthorized,
                    Message = "Authentication required"
                });
            }
        }
    }
}