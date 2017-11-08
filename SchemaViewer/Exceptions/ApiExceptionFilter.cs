using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SchemaViewer.Exceptions
{
	public class ApiExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			ApiError apiError = null;

			if (context.Exception is ApiException ex)
			{
				context.Exception = null;
				apiError = new ApiError(ex.Message);

				context.HttpContext.Response.StatusCode = ex.StatusCode;
			}
			else if (context.Exception is WebException webEx && webEx.Response is HttpWebResponse errorResponse)
			{
#if !DEBUG
				string msg = "";
				switch (errorResponse.StatusCode)
				{
					case HttpStatusCode.Unauthorized:
					case HttpStatusCode.Forbidden:
						msg = "Access to the calendar at specified url was not allowed by server.";
						break;
					case HttpStatusCode.InternalServerError:
						msg = "Internal server error at remote server on specified url.";
						break;
					case HttpStatusCode.NotFound:
						msg = "Calendar was not found at the specified url.";
						break;
					case HttpStatusCode.RequestTimeout:
						msg = "Request timed out, server at specified url did not respond in time. Please try again.";
						break;
					default:
						msg = "Unable to get calendar from specified url. Please check url and try again";
						break;
				}


				apiError = new ApiError(msg);
#else
				apiError = new ApiError(webEx.Message)
				{
					detail = webEx.StackTrace
				};

#endif
				context.HttpContext.Response.StatusCode = (int)errorResponse.StatusCode;
			}
			else
			{
				// unhandled errors
#if !DEBUG
				var msg = "An unhandled error occured.";
				string stack = null;
#else
				var msg = context.Exception.GetBaseException().Message;
				string stack = context.Exception.StackTrace;
#endif

				apiError = new ApiError(msg)
				{
					detail = stack
				};

				context.HttpContext.Response.StatusCode = 500;
			}

			// create json result
			context.Result = new JsonResult(apiError);

			base.OnException(context);
		}
	}
}
