using System.Net;

namespace RTLMaze.Core;

public class SourceUnavailibleException : Exception
{
	public HttpStatusCode LastStatuscode { get; set; }

	public SourceUnavailibleException() { }

    public SourceUnavailibleException( string message, HttpStatusCode lastStatusCode ) : base( message ) { LastStatuscode = lastStatusCode; }

    public SourceUnavailibleException( string message, HttpStatusCode lastStatusCode, Exception inner ) : base( message, inner ) { LastStatuscode = lastStatusCode; }
}