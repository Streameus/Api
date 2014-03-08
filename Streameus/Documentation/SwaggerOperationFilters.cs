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

    public class AddStandardResponseMessages : IOperationFilter
    {
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


    public class AddAuthorizationResponseMessages : IOperationFilter
    {
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