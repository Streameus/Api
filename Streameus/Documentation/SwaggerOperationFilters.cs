using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Models;

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
        /// <param name="apiDescription"></param>
        /// <param name="operationSpec"></param>
        /// <param name="modelSpecRegistrar"></param>
        /// <param name="modelSpecGenerator"></param>
        public void Apply(ApiDescription apiDescription, OperationSpec operationSpec,
            ModelSpecRegistrar modelSpecRegistrar,
            ModelSpecGenerator modelSpecGenerator)
        {
            operationSpec.ResponseMessages.Add(new ResponseMessageSpec
            {
                Code = (int) HttpStatusCode.OK,
                Message = "It's all good!"
            });

            operationSpec.ResponseMessages.Add(new ResponseMessageSpec
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
        /// <param name="apiDescription"></param>
        /// <param name="operationSpec"></param>
        /// <param name="modelSpecRegistrar"></param>
        /// <param name="modelSpecGenerator"></param>
        public void Apply(ApiDescription apiDescription, OperationSpec operationSpec,
            ModelSpecRegistrar modelSpecRegistrar,
            ModelSpecGenerator modelSpecGenerator)
        {
            if (apiDescription.ActionDescriptor.GetFilters().OfType<AuthorizeAttribute>().Any())
            {
                operationSpec.ResponseMessages.Add(new ResponseMessageSpec
                {
                    Code = (int) HttpStatusCode.Unauthorized,
                    Message = "Authentication required"
                });
            }
        }
    }
}