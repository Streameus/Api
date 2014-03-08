using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;

namespace Streameus.Hooks
{
    /// <summary>
    /// Cette classe permet de retourner une herreur http en cas d'exception
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception as ApiException;
            if (exception != null)
            {
                context.Response = context.Request.CreateErrorResponse(exception.StatusCode, exception.Message);
            }
        }
    }
}