using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace RTLMaze.REST.Models.Responses;

public partial class Response<T> : IConvertToActionResult
{
	public HttpStatusCode Code { get; set; }
	public T? Value { get; set; }

	public Response()
	{
		this.Code = HttpStatusCode.OK;
	}

	public Response( T value ) : this()
	{
		this.Value = value;
	}

	public IActionResult Convert()
	{
		return new ObjectResult( this ) { 
			StatusCode = this.Code == HttpStatusCode.NotFound ? 200 : (int)this.Code 
		};
	}
}