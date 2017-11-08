using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaViewer.Exceptions
{
    public class ApiException : Exception
    {
		public int StatusCode { get; set; }
		
		public ApiException(string message, int statusCode = 500)
			: base(message)
		{
			StatusCode = statusCode;
		}
    }

	// Class that is used as standard error response in json
	public class ApiError
	{
		public string message { get; set; }
		public bool isError { get; set; }
		public string detail { get; set; }

		public ApiError(string message)
		{
			this.message = message;
			isError = true;
		}
	}
}
