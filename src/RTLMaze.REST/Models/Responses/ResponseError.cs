using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace RTLMaze.REST.Models.Responses;

public partial class ResponseError<T> : Response<T>
{
	public IDictionary<string, string[]>? Errors { get; set; }

	#region Error constructors
	public ResponseError( IDictionary<string, string[]> errors, HttpStatusCode code = HttpStatusCode.BadRequest ) : base()
	{
		this.Code = code;
		this.Errors = errors;
	}

	public ResponseError( string error, HttpStatusCode code = HttpStatusCode.BadRequest ) : this( new string[]{ error }, code ){ }
	public ResponseError( string param, string error, HttpStatusCode code = HttpStatusCode.BadRequest ) : this( new Dictionary<string, string[]>(){ { param, new string[]{ error } } }, code ){ }
	public ResponseError( string[] errors, HttpStatusCode code = HttpStatusCode.BadRequest ) : this( new Dictionary<string, string[]>(){ { "request", errors } }, code ) { }

	public ResponseError( ValidationProblemDetails validationDetails ) : this( validationDetails.Errors ) {} 

	#endregion
}