namespace RTLMaze.Core;

public class SourceUnavailibleException : Exception
{
	public SourceUnavailibleException() { }

    public SourceUnavailibleException( string message ) : base( message ) { }

    public SourceUnavailibleException( string message, Exception inner ) : base(message, inner) { }
}